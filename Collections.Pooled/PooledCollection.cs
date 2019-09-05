// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Collections.Pooled
{
    /// <summary>
    /// Provides the base class for a generic collection.
    /// Uses ArrayPool for the backing store.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    public class PooledCollection<T> : IList<T>, IList, IReadOnlyList<T>, IDisposable
    {
        private readonly IList<T> _items; // Do not rename (binary serialization)

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledCollection{T}"/> class that is empty.
        /// </summary>
        public PooledCollection()
        {
            _items = new PooledList<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledCollection{T}"/> class as a wrapper for the specified list.
        /// </summary>
        /// <param name="list">The list that is wrapped by the new collection.</param>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
        public PooledCollection(IList<T> list)
        {
            if (list is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
            }
            _items = list;
        }

        /// <summary>
        /// Gets the number of elements actually contained in the <see cref="PooledCollection{T}"/>.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Gets an <see cref="IList{T}"/> wrapper around the <see cref="PooledCollection{T}"/>.
        /// </summary>
        protected IList<T> Items => _items;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        public T this[int index]
        {
            get => _items[index];
            set
            {
                if (_items.IsReadOnly)
                {
                    ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
                }

                if ((uint)index >= (uint)_items.Count)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }

                SetItem(index, value);
            }
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Gets or sets the element at the given index.
        /// </summary>
        public T this[Index index]
        {
            get
            {
                int offset = index.GetOffset(_items.Count);
                if ((uint)offset >= (uint)_items.Count)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                return _items[offset];
            }
            set
            {
                int offset = index.GetOffset(_items.Count);
                if ((uint)offset >= (uint)_items.Count)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                SetItem(offset, value);
            }
        }
#endif

        /// <summary>
        /// Adds an object to the end of the <see cref="PooledCollection{T}"/>.
        /// </summary>
        public void Add(T item)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }

            int index = _items.Count;
            InsertItem(index, item);
        }

        /// <summary>
        /// Adds a set of objects to the end of the <see cref="PooledCollection{T}"/>.
        /// </summary>
        public virtual void AddRange(IEnumerable<T> enumerable)
        {
            if (enumerable is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);

            switch (_items)
            {
                case PooledList<T> pl:
                    pl.AddRange(enumerable);
                    break;
                case List<T> list:
                    list.AddRange(enumerable);
                    break;
                default:
                    foreach (var item in enumerable)
                    {
                        _items.Add(item);
                    }
                    break;
            }
        }

        /// <summary>
        /// Removes all elements from the <see cref="PooledCollection{T}"/>.
        /// </summary>
        public void Clear()
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }

            ClearItems();
        }

        /// <summary>
        /// Copies the entire <see cref="PooledCollection{T}"/> to a compatible one-dimensional Array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(T[] array, int index) => _items.CopyTo(array, index);

        /// <summary>
        /// Copies the entire <see cref="PooledCollection{T}"/> to a compatible <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="span"></param>
        public void CopyTo(Span<T> span)
        {
            if (_items.Count > span.Length)
            {
                ThrowHelper.ThrowArgumentException_DestinationTooShort();
            }

            for (int i = 0; i < _items.Count; i++)
            {
                span[i] = _items[i];
            }
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="PooledCollection{T}"/>.
        /// </summary>
        public bool Contains(T item) => _items.Contains(item);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="PooledCollection{T}"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="PooledCollection{T}"/>.
        /// </summary>
        public int IndexOf(T item) => _items.IndexOf(item);

        /// <summary>
        /// Inserts an element into the <see cref="PooledCollection{T}"/> at the specified index.
        /// </summary>
        public void Insert(int index, T item)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }

            if ((uint)index > (uint)_items.Count)
            {
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();
            }

            InsertItem(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="PooledCollection{T}"/>.
        /// </summary>
        public bool Remove(T item)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }

            int index = _items.IndexOf(item);
            if (index < 0)
            {
                return false;
            }

            RemoveItem(index);
            return true;
        }

        /// <summary>
        /// Removes the element at the specified index of the <see cref="PooledCollection{T}"/>.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }

            if ((uint)index >= (uint)_items.Count)
            {
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();
            }

            RemoveItem(index);
        }

        /// <summary>
        /// Removes all elements from the <see cref="PooledCollection{T}"/>.
        /// </summary>
        protected virtual void ClearItems() => _items.Clear();

        /// <summary>
        /// Inserts an element into the <see cref="PooledCollection{T}"/> at the specified index.
        /// </summary>
        protected virtual void InsertItem(int index, T item) => _items.Insert(index, item);

        /// <summary>
        /// Removes the element at the specified index of the <see cref="PooledCollection{T}"/>.
        /// </summary>
        /// <param name="index"></param>
        protected virtual void RemoveItem(int index) => _items.RemoveAt(index);

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        protected virtual void SetItem(int index, T item) => _items[index] = item;

        bool ICollection<T>.IsReadOnly => _items.IsReadOnly;

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => (_items is ICollection coll) ? coll.SyncRoot : this;

        void ICollection.CopyTo(Array array, int index)
        {
            if (array is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (array.Rank != 1)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }

            if (array.GetLowerBound(0) != 0)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            }

            if (index < 0)
            {
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            }

            if (array.Length - index < Count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            if (array is T[] tArray)
            {
                _items.CopyTo(tArray, index);
            }
            else
            {
                //
                // Catch the obvious case assignment will fail.
                // We can't find all possible problems by doing the check though.
                // For example, if the element type of the Array is derived from T,
                // we can't figure out if we can successfully copy the element beforehand.
                //
                var targetType = array.GetType().GetElementType()!;
                var sourceType = typeof(T);
                if (!(targetType.IsAssignableFrom(sourceType) || sourceType.IsAssignableFrom(targetType)))
                {
                    ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }

                //
                // We can't cast array of value type to object[], so we don't support 
                // widening of primitive types here.
                //
                object?[]? objects = array as object[];
                if (objects is null)
                {
                    ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }

                int count = _items.Count;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        objects[index++] = _items[i];
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }
            }
        }

        object? IList.this[int index]
        {
            get => _items[index];
            set
            {
                ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);

                try
                {
                    this[index] = (T)value!;
                }
                catch (InvalidCastException)
                {
                    ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
                }
            }
        }

        bool IList.IsReadOnly => _items.IsReadOnly;

        bool IList.IsFixedSize
        {
            get
            {
                // There is no IList<T>.IsFixedSize, so we must assume that only
                // readonly collections are fixed size, if our internal item 
                // collection does not implement IList.  Note that Array implements
                // IList, and therefore T[] and U[] will be fixed-size.
                if (_items is IList list)
                {
                    return list.IsFixedSize;
                }
                return _items.IsReadOnly;
            }
        }

        int IList.Add(object? value)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);

            try
            {
                Add((T)value!);
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
            }

            return Count - 1;
        }

        bool IList.Contains(object? value)
        {
            if (IsCompatibleObject(value))
            {
                return Contains((T)value!);
            }
            return false;
        }

        int IList.IndexOf(object? value)
        {
            if (IsCompatibleObject(value))
            {
                return IndexOf((T)value!);
            }
            return -1;
        }

        void IList.Insert(int index, object? value)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);

            try
            {
                Insert(index, (T)value!);
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
            }
        }

        void IList.Remove(object? value)
        {
            if (_items.IsReadOnly)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            }

            if (IsCompatibleObject(value))
            {
                Remove((T)value!);
            }
        }

        private static bool IsCompatibleObject(object? value)
        {
            // Non-null values are fine.  Only accept nulls if T is a class or Nullable<U>.
            // Note that default(T) is not equal to null for value types except when T is Nullable<U>. 
            return (value is T) || (value is null && default(T)! is null);
        }

        /// <summary>
        /// Returns the underlying storage to the pool and sets the Count to zero.
        /// </summary>
        public virtual void Dispose()
        {
            if (_items is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
