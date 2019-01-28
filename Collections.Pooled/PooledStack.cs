// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*=============================================================================
**
**
** Purpose: An array implementation of a generic stack.
**
**
=============================================================================*/

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Collections.Pooled
{
    /// <summary>
    /// A simple stack of objects.  Internally it is implemented as an array,
    /// so Push can be O(n).  Pop is O(1).
    /// </summary>
    [DebuggerTypeProxy(typeof(StackDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class PooledStack<T> : IEnumerable<T>, ICollection, IReadOnlyCollection<T>, IDisposable
    {
        private static readonly ArrayPool<T> s_pool = ArrayPool<T>.Shared;

        private T[] _array; // Storage for stack elements. Do not rename (binary serialization)
        private int _size; // Number of items in the stack. Do not rename (binary serialization)
        private int _version; // Used to keep enumerator in sync w/ collection. Do not rename (binary serialization)
        private object _syncRoot;

        private const int DefaultCapacity = 4;

        public PooledStack()
        {
            _array = Array.Empty<T>();
        }

        /// <summary>
        /// Create a stack with a specific initial capacity.  The initial capacity
        /// must be a non-negative number.
        /// </summary>
        public PooledStack(int capacity)
        {
            if (capacity < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }
            _array = s_pool.Rent(capacity);
        }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(IEnumerable<T> enumerable)
        {
            switch (enumerable)
            {
                case null:
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);
                    break;

                case ICollection<T> collection:
                    if (collection.Count == 0)
                    {
                        _array = Array.Empty<T>();
                    }
                    else
                    {
                        _array = s_pool.Rent(collection.Count);
                        collection.CopyTo(_array, 0);
                        _size = collection.Count;
                    }
                    break;

                default:
                    _array = Array.Empty<T>();
                    using (var en = enumerable.GetEnumerator())
                    {
                        while (en.MoveNext())
                            Push(en.Current);
                    }
                    break;
            }
        }

        public PooledStack(T[] array) : this(array.AsSpan()) { }

        public PooledStack(ReadOnlySpan<T> span)
        {
            _array = s_pool.Rent(span.Length);
            span.CopyTo(_array);
            _size = span.Length;
        }

        public int Count => _size;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }

        /// <summary>
        /// Removes all Objects from the Stack.
        /// </summary>
        public void Clear()
        {
#if NETCOREAPP2_1 
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                Array.Clear(_array, 0, _size); // Don't need to doc this but we clear the elements so that the gc can reclaim the references.
            }
#else
            Array.Clear(_array, 0, _size);
#endif
            _size = 0;
            _version++;
        }

        /// <summary>
        /// Compares items using the default equality comparer
        /// </summary>
        public bool Contains(T item)
        {
            // PERF: Internally Array.LastIndexOf calls
            // EqualityComparer<T>.Default.LastIndexOf, which
            // is specialized for different types. This
            // boosts performance since instead of making a
            // virtual method call each iteration of the loop,
            // via EqualityComparer<T>.Default.Equals, we
            // only make one virtual call to EqualityComparer.LastIndexOf.

            return _size != 0 && Array.LastIndexOf(_array, item, _size - 1) != -1;
        }

        /// <summary>
        /// This method removes all items which match the predicate.
        /// The complexity is O(n).
        /// </summary>
        public int RemoveWhere(Func<T, bool> match)
        {
            if (match == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            int freeIndex = 0;   // the first free slot in items array

            // Find the first item which needs to be removed.
            while (freeIndex < _size && !match(_array[freeIndex])) freeIndex++;
            if (freeIndex >= _size) return 0;

            int current = freeIndex + 1;
            while (current < _size)
            {
                // Find the first item which needs to be kept.
                while (current < _size && match(_array[current])) current++;

                if (current < _size)
                {
                    // copy item to the free slot.
                    _array[freeIndex++] = _array[current++];
                }
            }

#if NETCOREAPP2_1
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                // Clear the removed elements so that the gc can reclaim the references.
                Array.Clear(_array, freeIndex, _size - freeIndex);
            }
#else
            Array.Clear(_array, freeIndex, _size - freeIndex);
#endif

            int result = _size - freeIndex;
            _size = freeIndex;
            _version++;
            return result;
        }

        // Copies the stack into an array.
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex);
            }

            if (array.Length - arrayIndex < _size)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            Debug.Assert(array != _array);
            int srcIndex = 0;
            int dstIndex = arrayIndex + _size;
            while (srcIndex < _size)
            {
                array[--dstIndex] = _array[srcIndex++];
            }
        }

        public void CopyTo(Span<T> span)
        {
            if (span.Length < _size)
            {
                ThrowHelper.ThrowArgumentException_DestinationTooShort();
            }

            int srcIndex = 0;
            int dstIndex = _size;
            while (srcIndex < _size)
            {
                span[--dstIndex] = _array[srcIndex++];
            }
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (array.Rank != 1)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }

            if (array.GetLowerBound(0) != 0)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound, ExceptionArgument.array);
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex);
            }

            if (array.Length - arrayIndex < _size)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
            }

            try
            {
                Array.Copy(_array, 0, array, arrayIndex, _size);
                Array.Reverse(array, arrayIndex, _size);
            }
            catch (ArrayTypeMismatchException)
            {
                ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
            }
        }

        /// <summary>
        /// Returns an IEnumerator for this PooledStack.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
            => new Enumerator(this);

        /// <internalonly/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(this);

        public void TrimExcess()
        {
            if (_size == 0)
            {
                ReturnArray(replaceWith: Array.Empty<T>());
                _version++;
                return;
            }

            int threshold = (int)(_array.Length * 0.9);
            if (_size < threshold)
            {
                var newArray = s_pool.Rent(_size);
                if (newArray.Length < _array.Length)
                {
                    Array.Copy(_array, newArray, _size);
                    ReturnArray(replaceWith: newArray);
                    _version++;
                }
                else
                {
                    // The array from the pool wasn't any smaller than the one we already had,
                    // (we can only control minimum size) so return it and do nothing.
                    // If we create an exact-sized array not from the pool, we'll
                    // get an exception when returning it to the pool.
                    s_pool.Return(newArray);
                }
            }
        }

        /// <summary>
        /// Returns the top object on the stack without removing it.  If the stack
        /// is empty, Peek throws an InvalidOperationException.
        /// </summary>
        public T Peek()
        {
            int size = _size - 1;
            T[] array = _array;

            if ((uint)size >= (uint)array.Length)
            {
                ThrowForEmptyStack();
            }

            return array[size];
        }

        public bool TryPeek(out T result)
        {
            int size = _size - 1;
            T[] array = _array;

            if ((uint)size >= (uint)array.Length)
            {
                result = default;
                return false;
            }
            result = array[size];
            return true;
        }

        /// <summary>
        /// Pops an item from the top of the stack.  If the stack is empty, Pop
        /// throws an InvalidOperationException.
        /// </summary>
        public T Pop()
        {
            int size = _size - 1;
            T[] array = _array;

            // if (_size == 0) is equivalent to if (size == -1), and this case
            // is covered with (uint)size, thus allowing bounds check elimination 
            // https://github.com/dotnet/coreclr/pull/9773
            if ((uint)size >= (uint)array.Length)
            {
                ThrowForEmptyStack();
            }

            _version++;
            _size = size;
            T item = array[size];
#if NETCOREAPP2_1
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                array[size] = default;     // Free memory quicker.
            }
#else
            array[size] = default;
#endif
            return item;
        }

        public bool TryPop(out T result)
        {
            int size = _size - 1;
            T[] array = _array;

            if ((uint)size >= (uint)array.Length)
            {
                result = default;
                return false;
            }

            _version++;
            _size = size;
            result = array[size];
#if NETCOREAPP2_1
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                array[size] = default;     // Free memory quicker.
            }
#else
            array[size] = default;
#endif
            return true;
        }

        /// <summary>
        /// Pushes an item to the top of the stack.
        /// </summary>
        public void Push(T item)
        {
            int size = _size;
            T[] array = _array;

            if ((uint)size < (uint)array.Length)
            {
                array[size] = item;
                _version++;
                _size = size + 1;
            }
            else
            {
                PushWithResize(item);
            }
        }

        // Non-inline from Stack.Push to improve its code quality as uncommon path
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void PushWithResize(T item)
        {
            var newArray = s_pool.Rent((_array.Length == 0) ? DefaultCapacity : 2 * _array.Length);
            Array.Copy(_array, newArray, _size);
            ReturnArray(replaceWith: newArray);
            _array[_size] = item;
            _version++;
            _size++;
        }

        /// <summary>
        /// Copies the Stack to an array, in the same order Pop would return the items.
        /// </summary>
        public T[] ToArray()
        {
            if (_size == 0)
                return Array.Empty<T>();

            T[] objArray = new T[_size];
            int i = 0;
            while (i < _size)
            {
                objArray[i] = _array[_size - i - 1];
                i++;
            }
            return objArray;
        }

        private void ThrowForEmptyStack()
        {
            Debug.Assert(_size == 0);
            throw new InvalidOperationException("Stack was empty.");
        }

        private void ReturnArray(T[] replaceWith = null)
        {
            if (_array?.Length > 0)
            {
#if NETCOREAPP2_1
                s_pool.Return(_array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
#else
                s_pool.Return(_array, true);
#endif
            }

            if (!(replaceWith is null))
            {
                _array = replaceWith;
            }
        }

        public void Dispose()
        {
            ReturnArray(replaceWith: Array.Empty<T>());
            _size = 0;
            _version++;
        }

        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "not an expected scenario")]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly PooledStack<T> _stack;
            private readonly int _version;
            private int _index;
            private T _currentElement;

            internal Enumerator(PooledStack<T> stack)
            {
                _stack = stack;
                _version = stack._version;
                _index = -2;
                _currentElement = default;
            }

            public void Dispose()
            {
                _index = -1;
            }

            public bool MoveNext()
            {
                bool retval;
                if (_version != _stack._version) throw new InvalidOperationException("Collection was modified during enumeration.");
                if (_index == -2)
                {  // First call to enumerator.
                    _index = _stack._size - 1;
                    retval = (_index >= 0);
                    if (retval)
                        _currentElement = _stack._array[_index];
                    return retval;
                }
                if (_index == -1)
                {  // End of enumeration.
                    return false;
                }

                retval = (--_index >= 0);
                if (retval)
                    _currentElement = _stack._array[_index];
                else
                    _currentElement = default;
                return retval;
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                        ThrowEnumerationNotStartedOrEnded();
                    return _currentElement;
                }
            }

            private void ThrowEnumerationNotStartedOrEnded()
            {
                Debug.Assert(_index == -1 || _index == -2);
                throw new InvalidOperationException(_index == -2 ? "Enumeration was not started." : "Enumeration has ended.");
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                if (_version != _stack._version) throw new InvalidOperationException("Collection was modified during enumeration.");
                _index = -2;
                _currentElement = default;
            }
        }
    }
}
