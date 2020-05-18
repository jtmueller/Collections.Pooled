// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace Collections.Pooled
{
    using static ClearModeUtil;

    /// <summary>
    /// Implements a variable-size list that uses a pooled array to store the
    /// elements. A PooledList has a capacity, which is the allocated length
    /// of the internal array. As elements are added to a PooledList, the capacity
    /// of the PooledList is automatically increased as required by reallocating the
    /// internal array.
    /// </summary>
    /// <remarks>
    /// This class is based on the code for <see cref="List{T}"/> but it supports <see cref="Span{T}"/>
    /// and uses <see cref="ArrayPool{T}"/> when allocating internal arrays.
    /// </remarks>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
    [Serializable]
#if NETCOREAPP3_0
    [System.Text.Json.Serialization.JsonConverter(typeof(PooledEnumerableJsonConverter))]
#endif
    public class PooledList<T> : IList<T>, IReadOnlyPooledList<T>, IList, IDisposable, IDeserializationCallback
    {
        // internal constant copied from Array.MaxArrayLength
        private const int s_maxArrayLength = 0x7FEFFFFF;
        private const int s_defaultCapacity = 16;

        [NonSerialized]
        private ArrayPool<T> _pool;
#pragma warning disable IDE0044
        [NonSerialized]
        private object? _syncRoot;
#pragma warning restore IDE0044

        private T[] _items; // Do not rename (binary serialization)
        private int _size; // Do not rename (binary serialization)
        private int _version; // Do not rename (binary serialization)
        private readonly bool _clearOnFree;

        #region Constructors

        /// <summary>
        /// Constructs a PooledList. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to DefaultCapacity, and then increased in multiples of two
        /// as required.
        /// </summary>
        public PooledList() : this(ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to DefaultCapacity, and then increased in multiples of two
        /// as required.
        /// </summary>
        public PooledList(ClearMode clearMode) : this(clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to DefaultCapacity, and then increased in multiples of two
        /// as required.
        /// </summary>
        public PooledList(ArrayPool<T> customPool) : this(ClearMode.Auto, customPool) { }

        /// <summary>
        /// Constructs a PooledList. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to DefaultCapacity, and then increased in multiples of two
        /// as required.
        /// </summary>
        public PooledList(ClearMode clearMode, ArrayPool<T> customPool)
        {
            _items = Array.Empty<T>();
            _pool = customPool ?? ArrayPool<T>.Shared;
            _clearOnFree = ShouldClear<T>(clearMode);
        }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity) : this(capacity, ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity, bool sizeToCapacity) : this(capacity, ClearMode.Auto, ArrayPool<T>.Shared, sizeToCapacity) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity, ClearMode clearMode) : this(capacity, clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity, ClearMode clearMode, bool sizeToCapacity) : this(capacity, clearMode, ArrayPool<T>.Shared, sizeToCapacity) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity, ArrayPool<T> customPool) : this(capacity, ClearMode.Auto, customPool) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity, ArrayPool<T> customPool, bool sizeToCapacity) : this(capacity, ClearMode.Auto, customPool, sizeToCapacity) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        public PooledList(int capacity, ClearMode clearMode, ArrayPool<T> customPool) : this(capacity, clearMode, customPool, false) { }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        /// <param name="capacity">The intial capacity of the list.</param>
        /// <param name="clearMode">Determines whether values are cleared before returning them to the ArrayPool. See <see cref="Pooled.ClearMode"/> for more details.</param>
        /// <param name="customPool">A custom ArrayPool to use when allocating internal arrays.</param>
        /// <param name="sizeToCapacity">If true, Count of list equals capacity. Depending on ClearMode, rented items may or may not hold dirty values.</param>
        public PooledList(int capacity, ClearMode clearMode, ArrayPool<T> customPool, bool sizeToCapacity)
        {
            if (capacity < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);

            _pool = customPool ?? ArrayPool<T>.Shared;
            _clearOnFree = ShouldClear<T>(clearMode);

            if (capacity == 0)
            {
                _items = Array.Empty<T>();
            }
            else
            {
                _items = _pool.Rent(capacity);
            }

            if (sizeToCapacity)
            {
                _size = capacity;
                if (clearMode != ClearMode.Never)
                {
                    Array.Clear(_items, 0, _size);
                }
            }
        }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(T[] array) : this(array.AsSpan(), ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(T[] array, ClearMode clearMode) : this(array.AsSpan(), clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(T[] array, ArrayPool<T> customPool) : this(array.AsSpan(), ClearMode.Auto, customPool) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(T[] array, ClearMode clearMode, ArrayPool<T> customPool) : this(array.AsSpan(), clearMode, customPool) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(ReadOnlySpan<T> span) : this(span, ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(ReadOnlySpan<T> span, ClearMode clearMode) : this(span, clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(ReadOnlySpan<T> span, ArrayPool<T> customPool) : this(span, ClearMode.Auto, customPool) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(ReadOnlySpan<T> span, ClearMode clearMode, ArrayPool<T> customPool)
        {
            _pool = customPool ?? ArrayPool<T>.Shared;
            _clearOnFree = ShouldClear<T>(clearMode);

            int count = span.Length;
            if (count == 0)
            {
                _items = Array.Empty<T>();
            }
            else
            {
                _items = _pool.Rent(count);
                span.CopyTo(_items);
                _size = count;
            }
        }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(IEnumerable<T> collection) : this(collection, ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(IEnumerable<T> collection, ClearMode clearMode) : this(collection, clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(IEnumerable<T> collection, ArrayPool<T> customPool) : this(collection, ClearMode.Auto, customPool) { }

        /// <summary>
        /// Constructs a PooledList, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        public PooledList(IEnumerable<T> collection, ClearMode clearMode, ArrayPool<T> customPool)
        {
            _pool = customPool ?? ArrayPool<T>.Shared;
            _clearOnFree = ShouldClear<T>(clearMode);

            if (collection is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);

            if (collection is ICollection<T> c)
            {
                int count = c.Count;
                if (count == 0)
                {
                    _items = Array.Empty<T>();
                }
                else
                {
                    _items = _pool.Rent(count);
                    c.CopyTo(_items, 0);
                    _size = count;
                }
            }
            else
            {
                _size = 0;
                _items = Array.Empty<T>();
                using var en = collection.GetEnumerator();
                while (en.MoveNext())
                    Add(en.Current);
            }
        }

        #endregion

        /// <summary>
        /// Gets a <see cref="Span{T}"/> for the items currently in the collection.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> Span
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Span<T>(_items, 0, _size);
            }
        }

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> for the items currently in the collection.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="ReadOnlySpan{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        ReadOnlySpan<T> IReadOnlyPooledList<T>.Span => Span;

        /// <summary>
        /// Gets and sets the capacity of this list.  The capacity is the size of
        /// the internal array used to hold items.  When set, the internal 
        /// Memory of the list is reallocated to the given capacity.
        /// Note that the return value for this property may be larger than the property was set to.
        /// </summary>
        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value < _size)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
                }

                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        var newItems = _pool.Rent(value);
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        ReturnArray();
                        _items = newItems;
                    }
                    else
                    {
                        ReturnArray();
                        _size = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Read-only property describing how many elements are in the List.
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// Returns the ClearMode behavior for the collection, denoting whether values are
        /// cleared from internal arrays before returning them to the pool.
        /// </summary>
        public ClearMode ClearMode => _clearOnFree ? ClearMode.Always : ClearMode.Never;

        bool IList.IsFixedSize => false;

        bool ICollection<T>.IsReadOnly => false;

        bool IList.IsReadOnly => false;

        int ICollection.Count => _size;

        bool ICollection.IsSynchronized => false;

        // Synchronization root for this object.
        object ICollection.SyncRoot
        {
#nullable disable
            get
            {
                if (_syncRoot is null)
                {
                    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
#nullable restore
        }

        /// <summary>
        /// Gets or sets the element at the given index.
        /// </summary>
        public T this[int index]
        {
            get
            {
                // Following trick can reduce the range check by one
                if ((uint)index >= (uint)_size)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                return _items[index];
            }

            set
            {
                if ((uint)index >= (uint)_size)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                _items[index] = value;
                _version++;
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
                int offset = index.GetOffset(_size);
                if ((uint)offset >= (uint)_size)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                return _items[offset];
            }
            set
            {
                int offset = index.GetOffset(_size);
                if ((uint)offset >= (uint)_size)
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                _items[offset] = value;
                _version++;
            }
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> for the given range.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> this[Range range] => Span[range];
#endif

        private static bool IsCompatibleObject(object? value)
        {
            // Non-null values are fine.  Only accept nulls if T is a class or Nullable<U>.
            // Note that default(T) is not equal to null for value types except when T is Nullable<U>. 
            return (value is T) || (value is null && default(T)! is null);
        }

        object? IList.this[int index]
        {
            get => this[index];
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

        /// <summary>
        /// Adds the given object to the end of this list. The size of the list is
        /// increased by one. If required, the capacity of the list is doubled
        /// before adding the new element.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            _version++;
            var array = _items;
            int size = _size;
            if ((uint)size < (uint)array.Length)
            {
                _size = size + 1;
                array[size] = item;
            }
            else
            {
                AddWithResize(item);
            }
        }

        // Non-inline from List.Add to improve its code quality as uncommon path
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddWithResize(T item)
        {
            int size = _size;
            EnsureCapacity(size + 1);
            _size = size + 1;
            _items[size] = item;
        }

        int IList.Add(object? item)
        {
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);

            try
            {
                Add((T)item!);
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
            }

            return Count - 1;
        }

        /// <summary>
        /// Adds the elements of the given collection to the end of this list. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.
        /// </summary>
        public void AddRange(IEnumerable<T> collection)
            => InsertRange(_size, collection);

        /// <summary>
        /// Adds the elements of the given array to the end of this list. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.
        /// </summary>
        public void AddRange(T[] array)
            => AddRange(array.AsSpan());

        /// <summary>
        /// Adds the elements of the given <see cref="ReadOnlySpan{T}"/> to the end of this list. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.
        /// </summary>
        public void AddRange(ReadOnlySpan<T> span)
        {
            var newSpan = InsertSpan(_size, span.Length, false);
            span.CopyTo(newSpan);
        }

        /// <summary>
        /// Advances the <see cref="Count"/> by the number of items specified,
        /// increasing the capacity if required, then returns a <see cref="Span{T}"/> representing
        /// the set of items to be added, allowing direct writes to that section
        /// of the collection.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        /// <param name="count">The number of items to add.</param>
        public Span<T> AddSpan(int count)
            => InsertSpan(_size, count);

        /// <summary>
        /// Returns a read-only version of the current list.
        /// </summary>
        public ReadOnlyCollection<T> AsReadOnly()
            => new ReadOnlyCollection<T>(this);

        /// <summary>
        /// Searches a section of the list for a given element using a binary search
        /// algorithm. 
        /// </summary>
        /// 
        /// <remarks><para>Elements of the list are compared to the search value using
        /// the given IComparer interface. If comparer is null, elements of
        /// the list are compared to the search value using the IComparable
        /// interface, which in that case must be implemented by all elements of the
        /// list and the given search value. This method assumes that the given
        /// section of the list is already sorted; if this is not the case, the
        /// result will be incorrect.</para>
        ///
        /// <para>The method returns the index of the given value in the list. If the
        /// list does not contain the given value, the method returns a negative
        /// integer. The bitwise complement operator (~) can be applied to a
        /// negative result to produce the index of the first element (if any) that
        /// is larger than the given search value. This is also the index at which
        /// the search value should be inserted into the list in order for the list
        /// to remain sorted.
        /// </para></remarks>
        public int BinarySearch(int index, int count, T item, IComparer<T>? comparer)
        {
            if (index < 0)
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            if (count < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            if (_size - index < count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);

            return Array.BinarySearch(_items, index, count, item, comparer);
        }

        /// <summary>
        /// Searches the list for a given element using a binary search
        /// algorithm. If the item implements <see cref="IComparable{T}"/>
        /// then that is used for comparison, otherwise <see cref="Comparer{T}.Default"/> is used.
        /// </summary>
        public int BinarySearch(T item)
            => BinarySearch(0, Count, item, null);

        /// <summary>
        /// Searches the list for a given element using a binary search
        /// algorithm. If the item implements <see cref="IComparable{T}"/>
        /// then that is used for comparison, otherwise <see cref="Comparer{T}.Default"/> is used.
        /// </summary>
        public int BinarySearch(T item, IComparer<T> comparer)
            => BinarySearch(0, Count, item, comparer);

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Searches the list for a given element using a binary search
        /// algorithm. If the item implements <see cref="IComparable{T}"/>
        /// then that is used for comparison, otherwise <see cref="Comparer{T}.Default"/> is used.
        /// </summary>
        public int BinarySearch(Index index, int count, T item, IComparer<T>? comparer = null)
            => BinarySearch(index.GetOffset(_size), count, item, comparer);

        /// <summary>
        /// Searches the list for a given element using a binary search
        /// algorithm. If the item implements <see cref="IComparable{T}"/>
        /// then that is used for comparison, otherwise <see cref="Comparer{T}.Default"/> is used.
        /// </summary>
        public int BinarySearch(Range range, T item, IComparer<T>? comparer = null)
        {
            var (offset, length) = range.GetOffsetAndLength(_size);
            return Array.BinarySearch(_items, offset, length, item, comparer);
        }
#endif

        /// <summary>
        /// Clears the contents of the PooledList.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _version++;
            int size = _size;
            _size = 0;

            if (size > 0 && _clearOnFree)
            {
                // Clear the elements so that the gc can reclaim the references.
                Array.Clear(_items, 0, _size);
            }
        }

        /// <summary>
        /// Contains returns true if the specified element is in the List.
        /// It does a linear, O(n) search.  Equality is determined by calling
        /// EqualityComparer{T}.Default.Equals.
        /// </summary>
        public bool Contains(T item)
        {
            // PERF: IndexOf calls Array.IndexOf, which internally
            // calls EqualityComparer<T>.Default.IndexOf, which
            // is specialized for different types. This
            // boosts performance since instead of making a
            // virtual method call each iteration of the loop,
            // via EqualityComparer<T>.Default.Equals, we
            // only make one virtual call to EqualityComparer.IndexOf.

            return _size != 0 && IndexOf(item) != -1;
        }

        bool IList.Contains(object? item)
        {
            if (IsCompatibleObject(item))
            {
                return Contains((T)item!);
            }
            return false;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PooledList{TOutput}"/> containing
        /// the output of running the <paramref name="converter"/> function on each item
        /// in the current list.
        /// </summary>
        /// <typeparam name="TOutput">The type to convert the contents of this list to.</typeparam>
        /// <param name="converter">The function that converts each item in the current list.</param>
        /// <returns>A new instance of <see cref="PooledList{TOutput}"/>.</returns>
        public PooledList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
        {
            if (converter is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
            }

            var list = new PooledList<TOutput>(_size);
            for (int i = 0; i < _size; i++)
            {
                list._items[i] = converter(_items[i]);
            }
            list._size = _size;
            return list;
        }

        /// <summary>
        /// Copies this list to the given span.
        /// </summary>
        public void CopyTo(Span<T> span)
        {
            if (span.Length < Count)
                throw new ArgumentException("Destination is shorter than the list to be copied.");

            Span.CopyTo(span);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => Array.Copy(_items, 0, array, arrayIndex, _size);

        // Copies this List into array, which must be of a 
        // compatible array type.  
        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if ((array != null) && (array.Rank != 1))
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }

            try
            {
                // Array.Copy will check for NULL.
                Array.Copy(_items, 0, array!, arrayIndex, _size);
            }
            catch (ArrayTypeMismatchException)
            {
                ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
            }
        }

        /// <summary>
        /// Ensures that the capacity of this list is at least the given minimum
        /// value. If the current capacity of the list is less than min, the
        /// capacity is increased to twice the current capacity or to min,
        /// whichever is larger.
        /// </summary>
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? s_defaultCapacity : _items.Length * 2;
                // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
                // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
                if ((uint)newCapacity > s_maxArrayLength) newCapacity = s_maxArrayLength;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        /// <summary>
        /// Returns true if the given <paramref name="match"/> function returns true for any item in the list.
        /// </summary>
        /// <param name="match"></param>
        public bool Exists(Func<T, bool> match)
            => FindIndex(match) != -1;

        /// <summary>
        /// The first item for which the given <paramref name="match"/> function returns true
        /// will be returned, or the default value of <typeparamref name="T"/> if no match is found.
        /// </summary>
        /// <param name="match"></param>
        [return: MaybeNull]
        public T Find(Func<T, bool> match)
        {
            if (match is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
            }

            for (int i = 0; i < _size; i++)
            {
                if (match(_items[i]))
                {
                    return _items[i];
                }
            }
            return default!;
        }

        /// <summary>
        /// The first item for which the given <paramref name="match"/> function returns true
        /// will be returned in the <paramref name="result"/> output parameter. The method will
        /// return true if a match was found, otherwise false.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="result"></param>
        public bool TryFind(Func<T, bool> match, [MaybeNullWhen(false)] out T result)
        {
            if (match is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            for (int i = 0; i < _size; i++)
            {
                if (match(_items[i]))
                {
                    result = _items[i];
                    return true;
                }
            }

            result = default!;
            return false;
        }

        /// <summary>
        /// Returns a new instance of <see cref="PooledList{T}"/> containing
        /// all of the items for which the <paramref name="match"/> function
        /// returns true.
        /// </summary>
        /// <param name="match">A predicate function that returns true for matches.</param>
        public PooledList<T> FindAll(Func<T, bool> match)
        {
            if (match is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            var list = new PooledList<T>();
            for (int i = 0; i < _size; i++)
            {
                if (match(_items[i]))
                {
                    list.Add(_items[i]);
                }
            }
            return list;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the first occurrence within the <see cref="PooledList{T}"/>
        /// or a portion of it. This method returns -1 if an item that matches the conditions is not found.
        /// </summary>
        public int FindIndex(Func<T, bool> match)
            => FindIndex(0, _size, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the first occurrence within the <see cref="PooledList{T}"/>
        /// or a portion of it. This method returns -1 if an item that matches the conditions is not found.
        /// </summary>
        public int FindIndex(int startIndex, Func<T, bool> match)
            => FindIndex(startIndex, _size - startIndex, match);

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the first occurrence within the <see cref="PooledList{T}"/>
        /// or a portion of it. This method returns -1 if an item that matches the conditions is not found.
        /// </summary>
        public int FindIndex(Index startIndex, Func<T, bool> match)
        {
            int offset = startIndex.GetOffset(_size);
            return FindIndex(offset, _size - offset, match);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the first occurrence within the <see cref="PooledList{T}"/>
        /// or a portion of it. This method returns -1 if an item that matches the conditions is not found.
        /// </summary>
        public int FindIndex(Range range, Func<T, bool> match)
        {
            var (offset, length) = range.GetOffsetAndLength(_size);
            return FindIndex(offset, length, match);
        }
#endif

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the first occurrence within the <see cref="PooledList{T}"/>
        /// or a portion of it. This method returns -1 if an item that matches the conditions is not found.
        /// </summary>
        public int FindIndex(int startIndex, int count, Func<T, bool> match)
        {
            if ((uint)startIndex > (uint)_size)
                ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();

            if (count < 0 || startIndex > _size - count)
                ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();

            if (match is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            int endIndex = startIndex + count;
            for (int i = startIndex; i < endIndex; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// The last item for which the given <paramref name="match"/> function returns true
        /// will be returned. If no match is found, the default value for <typeparamref name="T"/> is returned.
        /// </summary>
        /// <param name="match"></param>
        [return: MaybeNull]
        public T FindLast(Predicate<T> match)
        {
            if (match is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
            }

            for (int i = _size - 1; i >= 0; i--)
            {
                if (match(_items[i]))
                {
                    return _items[i];
                }
            }
            return default!;
        }

        /// <summary>
        /// The last item for which the given <paramref name="match"/> function returns true
        /// will be returned in the <paramref name="result"/> output parameter. The method will
        /// return true if a match was found, otherwise false.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="result"></param>
        public bool TryFindLast(Func<T, bool> match, [MaybeNullWhen(false)] out T result)
        {
            if (match is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
            }

            for (int i = _size - 1; i >= 0; i--)
            {
                if (match(_items[i]))
                {
                    result = _items[i];
                    return true;
                }
            }

            result = default!;
            return false;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the last occurrence within the <see cref="PooledList{T}"/> or a portion of it.
        /// </summary>
        public int FindLastIndex(Func<T, bool> match)
            => FindLastIndex(_size - 1, _size, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the last occurrence within the <see cref="PooledList{T}"/> or a portion of it.
        /// </summary>
        public int FindLastIndex(int startIndex, Func<T, bool> match)
            => FindLastIndex(startIndex, startIndex + 1, match);

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the last occurrence within the <see cref="PooledList{T}"/> or a portion of it.
        /// </summary>
        public int FindLastIndex(Index startIndex, Func<T, bool> match)
        {
            int offset = startIndex.GetOffset(_size);
            return FindLastIndex(offset, offset + 1, match);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the last occurrence within the <see cref="PooledList{T}"/> or a portion of it.
        /// </summary>
        public int FindLastIndex(Range range, Func<T, bool> match)
        {
            int start = range.Start.GetOffset(_size);
            int end = range.End.GetOffset(_size);

            if ((uint)start > (uint)_size)
                ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();

            if (end < start || end > _size)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.range, ExceptionResource.ArgumentOutOfRange_EndIndexStartIndex);

            if (match is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            for (int i = end; i >= start; i--)
            {
                if (match(_items[i]))
                    return i;
            }

            return -1;
        }
#endif

        /// <summary>
        /// Searches for an element that matches the conditions defined by a specified predicate, 
        /// and returns the zero-based index of the last occurrence within the <see cref="PooledList{T}"/> or a portion of it.
        /// </summary>
        public int FindLastIndex(int startIndex, int count, Func<T, bool> match)
        {
            if (match is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
            }

            if (_size == 0)
            {
                // Special case for 0 length List
                if (startIndex != -1)
                {
                    ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
                }
            }
            else
            {
                // Make sure we're not out of range
                if ((uint)startIndex >= (uint)_size)
                {
                    ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
                }
            }

            // 2nd half of this also catches when startIndex == MAXINT, so MAXINT - 0 + 1 == -1, which is < 0.
            if (count < 0 || startIndex - count + 1 < 0)
            {
                ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
            }

            int endIndex = startIndex - count;
            for (int i = startIndex; i > endIndex; i--)
            {
                if (match(_items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Passes each item in the list to the given <paramref name="action"/> function.
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T> action)
        {
            if (action is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
            }

            int version = _version;
            for (int i = 0; i < _size; i++)
            {
                if (version != _version)
                {
                    break;
                }
                action(_items[i]);
            }

            if (version != _version)
                ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
        }

        /// <summary>
        /// Passes each item in the list and the item's index to the given <paramref name="action"/> function.
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T, int> action)
        {
            if (action is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
            }

            int version = _version;
            for (int i = 0; i < _size; i++)
            {
                if (version != _version)
                {
                    break;
                }
                action(_items[i], i);
            }

            if (version != _version)
                ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
        }

        /// <summary>
        /// Returns an enumerator for this list with the given
        /// permission for removal of elements. If modifications made to the list 
        /// while an enumeration is in progress, the MoveNext and 
        /// GetObject methods of the enumerator will throw an exception.
        /// </summary>
        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(this);

        /// <summary>
        /// Returns a <see cref="Span{T}"/> allowing read/write access to a subset of the entire list.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> GetRange(int index, int count)
        {
            if (index < 0)
            {
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            }

            if (count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (_size - index < count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
            }

            return Span.Slice(index, count);
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> starting from the given index and going to the end of the list.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> GetRange(int index)
        {
            if ((uint)index > (uint)_size)
                ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();

            return Span.Slice(index);
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Returns a <see cref="Span{T}"/> allowing read/write access to a subset of the entire list.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> GetRange(Range range) => Span[range];

        /// <summary>
        /// Returns a <see cref="Span{T}"/> allowing read/write access to a subset of the entire list.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> GetRange(Index startIndex) => Span[startIndex..];
#endif

        /// <summary>
        /// Returns the index of the first occurrence of a given value in
        /// this list. The list is searched forwards from beginning to end.
        /// </summary>
        public int IndexOf(T item)
            => Array.IndexOf(_items, item, 0, _size);

        int IList.IndexOf(object? item)
        {
            if (IsCompatibleObject(item))
            {
                return IndexOf((T)item!);
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the first occurrence of a given value in a range of
        /// this list. The list is searched forwards, starting at index
        /// index and ending at count number of elements. 
        /// </summary>
        public int IndexOf(T item, int index)
        {
            if (index > _size)
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();
            return Array.IndexOf(_items, item, index, _size - index);
        }

        /// <summary>
        /// Returns the index of the first occurrence of a given value in a range of
        /// this list. The list is searched forwards, starting at index
        /// index and upto count number of elements. 
        /// </summary>
        public int IndexOf(T item, int index, int count)
        {
            if (index > _size)
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();

            if (count < 0 || index > _size - count)
                ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();

            return Array.IndexOf(_items, item, index, count);
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Returns the index of the first occurrence of a given value in a range of
        /// this list. The list is searched forwards, starting at index
        /// index and upto count number of elements. 
        /// </summary>
        public int IndexOf(T item, Index index)
            => IndexOf(item, index.GetOffset(_size));

        /// <summary>
        /// Returns the index of the first occurrence of a given value in a range of
        /// this list. The list is searched forwards, starting at index
        /// index and upto count number of elements. 
        /// </summary>
        public int IndexOf(T item, Range range)
        {
            var (start, count) = range.GetOffsetAndLength(_size);
            return Array.IndexOf(_items, item, start, count);
        }

        /// <summary>
        /// Inserts an element into this list at a given index. The size of the list
        /// is increased by one. If required, the capacity of the list is doubled
        /// before inserting the new element.
        /// </summary>
        public void Insert(Index index, T item)
            => Insert(index.GetOffset(_size), item);
#endif

        /// <summary>
        /// Inserts an element into this list at a given index. The size of the list
        /// is increased by one. If required, the capacity of the list is doubled
        /// before inserting the new element.
        /// </summary>
        public void Insert(int index, T item)
        {
            // Note that insertions at the end are legal.
            if ((uint)index > (uint)_size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
            }

            if (_size == _items.Length) EnsureCapacity(_size + 1);
            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = item;
            _size++;
            _version++;
        }

        void IList.Insert(int index, object? item)
        {
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);

            try
            {
                Insert(index, (T)item!);
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
            }
        }

        /// <summary>
        /// Inserts the elements of the given collection at a given index. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.  Ranges may be added
        /// to the end of the list by setting index to the List's size.
        /// </summary>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
            }

            if ((uint)index > (uint)_size)
            {
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();
            }

            if (collection is ICollection<T> c)
            {
                int count = c.Count;
                if (count > 0)
                {
                    EnsureCapacity(_size + count);
                    if (index < _size)
                    {
                        Array.Copy(_items, index, _items, index + count, _size - index);
                    }

                    // If we're inserting a List into itself, we want to be able to deal with that.
                    if (this == c)
                    {
                        // Copy first part of _items to insert location
                        Array.Copy(_items, 0, _items, index, index);
                        // Copy last part of _items back to inserted location
                        Array.Copy(_items, index + count, _items, index * 2, _size - index);
                    }
                    else
                    {
                        c.CopyTo(_items, index);
                    }
                    _size += count;
                }
            }
            else
            {
                using var en = collection.GetEnumerator();
                while (en.MoveNext())
                {
                    Insert(index++, en.Current);
                }
            }

            _version++;
        }

        /// <summary>
        /// Inserts the elements of the given collection at a given index. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.  Ranges may be added
        /// to the end of the list by setting index to the List's size.
        /// </summary>
        public void InsertRange(int index, ReadOnlySpan<T> span)
        {
            var newSpan = InsertSpan(index, span.Length, false);
            span.CopyTo(newSpan);
        }

        /// <summary>
        /// Inserts the elements of the given collection at a given index. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.  Ranges may be added
        /// to the end of the list by setting index to the List's size.
        /// </summary>
        public void InsertRange(int index, T[] array)
        {
            if (array is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            InsertRange(index, array.AsSpan());
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Inserts the elements of the given collection at a given index. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.  Ranges may be added
        /// to the end of the list by setting index to the List's size.
        /// </summary>
        public void InsertRange(Index index, IEnumerable<T> collection)
            => InsertRange(index.GetOffset(_size), collection);

        /// <summary>
        /// Inserts the elements of the given collection at a given index. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.  Ranges may be added
        /// to the end of the list by setting index to the List's size.
        /// </summary>
        public void InsertRange(Index index, ReadOnlySpan<T> span)
            => InsertRange(index.GetOffset(_size), span);

        /// <summary>
        /// Inserts the elements of the given collection at a given index. If
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger.  Ranges may be added
        /// to the end of the list by setting index to the List's size.
        /// </summary>
        public void InsertRange(Index index, T[] array)
            => InsertRange(index.GetOffset(_size), array.AsSpan());

        /// <summary>
        /// Inserts the given number of items at the given index, increasing the
        /// capacity if required, then returns a <see cref="Span{T}"/> representing the set of items
        /// to be inserted, allowing direct writes to that section of the collection.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> InsertSpan(Index index, int count)
            => InsertSpan(index.GetOffset(_size), count, true);
#endif

        /// <summary>
        /// Inserts the given number of items at the given index, increasing the
        /// capacity if required, then returns a <see cref="Span{T}"/> representing the set of items
        /// to be inserted, allowing direct writes to that section of the collection.
        /// WARNING: Be careful not to modify the list until you're finished with the returned 
        /// <see cref="Span{T}"/>. Actions that change the size of the list will not affect
        /// the length of the Span, and may invalidate the Span entirely by making changes to the backing store.
        /// Continuing to use the Span after making changes to the PooledList is NOT guaranteed to work.
        /// </summary>
        public Span<T> InsertSpan(int index, int count)
            => InsertSpan(index, count, true);

        private Span<T> InsertSpan(int index, int count, bool clearOutput)
        {
            // Note that insertions at the end are legal.
            if ((uint)index > (uint)_size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
            }

            EnsureCapacity(_size + count);

            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + count, _size - index);
            }

            _size += count;
            _version++;

            var output = _items.AsSpan(index, count);

            if (clearOutput && _clearOnFree)
            {
                output.Clear();
            }

            return output;
        }

        /// <summary>
        /// Returns the index of the last occurrence of a given value in a range of
        /// this list. The list is searched backwards, starting at the end 
        /// and ending at the first element in the list.
        /// </summary>
        public int LastIndexOf(T item)
        {
            if (_size == 0)
            {  // Special case for empty list
                return -1;
            }
            else
            {
                return LastIndexOf(item, _size - 1, _size);
            }
        }

        /// <summary>
        /// Returns the index of the last occurrence of a given value in a range of
        /// this list. The list is searched backwards, starting at index
        /// and ending at the first element in the list.
        /// </summary>
        public int LastIndexOf(T item, int index)
        {
            if (index >= _size)
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();
            return LastIndexOf(item, index, index + 1);
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Returns the index of the last occurrence of a given value in a range of
        /// this list. The list is searched backwards, starting at index
        /// and ending at the first element in the list.
        /// </summary>
        public int LastIndexOf(T item, Index index)
        {
            int offset = index.GetOffset(_size);
            return LastIndexOf(item, offset, offset + 1);
        }

        /// <summary>
        /// Returns the index of the last occurrence of a given value in a range of
        /// this list. The list is searched backwards, starting at index
        /// index and up to count elements
        /// </summary>
        public int LastIndexOf(T item, Index index, int count)
            => LastIndexOf(item, index.GetOffset(_size), count);

        /// <summary>
        /// Returns the index of the last occurrence of a given value in a range of
        /// this list. The list is searched backwards, starting at index
        /// index and up to count elements
        /// </summary>
        public int LastIndexOf(T item, Range range)
        {
            int start = range.Start.GetOffset(_size);
            int end = range.End.GetOffset(_size);

            if ((uint)start > (uint)_size)
                ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();

            if (end < start || end > _size)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.range, ExceptionResource.ArgumentOutOfRange_EndIndexStartIndex);

            return Array.LastIndexOf(_items, item, end == _size ? end - 1 : end, end - start);
        }
#endif

        /// <summary>
        /// Returns the index of the last occurrence of a given value in a range of
        /// this list. The list is searched backwards, starting at index
        /// index and up to count elements
        /// </summary>
        public int LastIndexOf(T item, int index, int count)
        {
            if (Count != 0 && index < 0)
            {
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            }

            if (Count != 0 && count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (_size == 0)
            {
                // Special case for empty list
                return -1;
            }

            if (index >= _size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
            }

            if (count > index + 1)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
            }

            return Array.LastIndexOf(_items, item, index, count);
        }

        /// <summary>
        /// Removes the first occurrence of the given item. The size of the list is
        /// decreased by one.
        /// </summary>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        void IList.Remove(object? item)
        {
            if (IsCompatibleObject(item))
            {
                Remove((T)item!);
            }
        }

        /// <summary>
        /// This method removes all items which match the predicate.
        /// The complexity is O(n).
        /// </summary>
        public int RemoveAll(Func<T, bool> match)
        {
            if (match is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            int freeIndex = 0;   // the first free slot in items array

            // Find the first item which needs to be removed.
            while (freeIndex < _size && !match(_items[freeIndex])) freeIndex++;
            if (freeIndex >= _size) return 0;

            int current = freeIndex + 1;
            while (current < _size)
            {
                // Find the first item which needs to be kept.
                while (current < _size && match(_items[current])) current++;

                if (current < _size)
                {
                    // copy item to the free slot.
                    _items[freeIndex++] = _items[current++];
                }
            }

            if (_clearOnFree)
            {
                // Clear the removed elements so that the gc can reclaim the references.
                Array.Clear(_items, freeIndex, _size - freeIndex);
            }

            int result = _size - freeIndex;
            _size = freeIndex;
            _version++;
            return result;
        }

        /// <summary>
        /// Removes the element at the given index. The size of the list is
        /// decreased by one.
        /// </summary>
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();

            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _version++;

            if (_clearOnFree)
            {
                // Clear the removed element so that the gc can reclaim the reference.
                _items[_size] = default!;
            }
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Removes the element at the given index. The size of the list is
        /// decreased by one.
        /// </summary>
        public void RemoveAt(Index index)
            => RemoveAt(index.GetOffset(_size));

        /// <summary>
        /// Removes a range of elements from this list.
        /// </summary>
        public void RemoveRange(Index index, int count)
            => RemoveRange(index.GetOffset(_size), count);

        /// <summary>
        /// Removes a range of elements from this list.
        /// </summary>
        public void RemoveRange(Range range)
        {
            var (offset, length) = range.GetOffsetAndLength(_size);
            RemoveRange(offset, length);
        }
#endif

        /// <summary>
        /// Removes a range of elements from this list.
        /// </summary>
        public void RemoveRange(int index, int count)
        {
            if (index < 0)
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();

            if (count < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);

            if (_size - index < count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);

            if (count > 0)
            {
                _size -= count;
                if (index < _size)
                {
                    Array.Copy(_items, index + count, _items, index, _size - index);
                }

                _version++;

                if (_clearOnFree)
                {
                    // Clear the removed elements so that the gc can reclaim the references.
                    Array.Clear(_items, _size, count);
                }
            }
        }

        /// <summary>
        /// Reverses the elements in this list.
        /// </summary>
        public void Reverse()
            => Reverse(0, _size);

        /// <summary>
        /// Reverses the elements in a range of this list. Following a call to this
        /// method, an element in the range given by index and count
        /// which was previously located at index i will now be located at
        /// index index + (index + count - i - 1).
        /// </summary>
        public void Reverse(int index, int count)
        {
            if (index < 0)
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();

            if (count < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);

            if (_size - index < count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);

            if (count > 1)
            {
                Array.Reverse(_items, index, count);
            }
            _version++;
        }

#if NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// Reverses the elements in a range of this list. Following a call to this
        /// method, an element in the range given by index and count
        /// which was previously located at index i will now be located at
        /// index index + (index + count - i - 1).
        /// </summary>
        public void Reverse(Index index, int count)
            => Reverse(index.GetOffset(_size), count);

        /// <summary>
        /// Reverses the elements in a range of this list. Following a call to this
        /// method, an element in the range given by index and count
        /// which was previously located at index i will now be located at
        /// index index + (index + count - i - 1).
        /// </summary>
        public void Reverse(Range range)
        {
            var (start, count) = range.GetOffsetAndLength(_size);
            Reverse(start, count);
        }

        /// <summary>
        /// Sorts the elements in this list.  Uses Array.Sort with the
        /// provided comparer.
        /// </summary>
        public void Sort(Index index, int count, IComparer<T>? comparer = null)
            => Sort(index.GetOffset(_size), count, comparer);

        /// <summary>
        /// Sorts the elements in this list.  Uses Array.Sort with the
        /// provided comparer.
        /// </summary>
        public void Sort(Range range, IComparer<T>? comparer = null)
        {
            var (offset, length) = range.GetOffsetAndLength(_size);
            Sort(offset, length, comparer);
        }
#endif

        /// <summary>
        /// Sorts the elements in this list.  Uses the default comparer and 
        /// Array.Sort.
        /// </summary>
        public void Sort()
            => Sort(0, Count, null);

        /// <summary>
        /// Sorts the elements in this list.  Uses Array.Sort with the
        /// provided comparer.
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<T> comparer)
            => Sort(0, Count, comparer);

        /// <summary>
        /// Sorts the elements in a section of this list. The sort compares the
        /// elements to each other using the given IComparer interface. If
        /// comparer is null, the elements are compared to each other using
        /// the IComparable interface, which in that case must be implemented by all
        /// elements of the list.
        /// 
        /// This method uses the Array.Sort method to sort the elements.
        /// </summary>
        public void Sort(int index, int count, IComparer<T>? comparer = null)
        {
            if (index < 0)
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();

            if (count < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);

            if (_size - index < count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);

            if (count > 1)
            {
                Array.Sort(_items, index, count, comparer);
            }
            _version++;
        }

        /// <summary>
        /// Sorts the elements in a section of this list. The sort compares the
        /// elements to each other using the given IComparer interface. If
        /// comparer is null, the elements are compared to each other using
        /// the IComparable interface, which in that case must be implemented by all
        /// elements of the list.
        /// 
        /// This method uses the Array.Sort method to sort the elements.
        /// </summary>
        public void Sort(Func<T, T, int> comparison)
        {
            if (comparison is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
            }

            if (_size > 1)
            {
                // List<T> uses ArraySortHelper here but since it's an internal class,
                // we're creating an IComparer<T> using the comparison function to avoid
                // duplicating all that code.
                Array.Sort(_items, 0, _size, new Comparer(comparison));
            }
            _version++;
        }

        /// <summary>
        /// ToArray returns an array containing the contents of the List.
        /// This requires copying the List, which is an O(n) operation.
        /// </summary>
        public T[] ToArray()
        {
            if (_size == 0)
            {
                return Array.Empty<T>();
            }

            return Span.ToArray();
        }

        /// <summary>
        /// Sets the capacity of this list to the size of the list. This method can
        /// be used to minimize a list's memory overhead once it is known that no
        /// new elements will be added to the list. To completely clear a list and
        /// release all memory referenced by the list, execute the following
        /// statements:
        /// <code>
        /// list.Clear();
        /// list.TrimExcess();
        /// </code>
        /// </summary>
        public void TrimExcess()
        {
            int threshold = (int)(_items.Length * 0.9);
            if (_size < threshold)
            {
                Capacity = _size;
            }
        }

        /// <summary>
        /// Returns true if all the items in the list return true
        /// for the given predicate function.
        /// </summary>
        /// <param name="match"></param>
        public bool TrueForAll(Func<T, bool> match)
        {
            if (match is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
            }

            for (int i = 0; i < _size; i++)
            {
                if (!match(_items[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private void ReturnArray()
        {
            if (_items.Length == 0)
                return;

            try
            {
                // Clear the elements so that the gc can reclaim the references.
                _pool.Return(_items, clearArray: _clearOnFree);
            }
            catch (ArgumentException)
            {
                // oh well, the array pool didn't like our array
            }

            _items = Array.Empty<T>();
        }

        /// <summary>
        /// Returns the internal buffers to the pool.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns the internal buffers to the pool.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReturnArray();
                _size = 0;
                _version++;
            }
        }

        void IDeserializationCallback.OnDeserialization(object? sender)
        {
            // We can't serialize array pools, so deserialized PooledLists will
            // have to use the shared pool, even if they were using a custom pool
            // before serialization.
            _pool = ArrayPool<T>.Shared;
        }

        /// <summary>
        /// An enumerator, for enumerating the <see cref="PooledList{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly PooledList<T> _list;
            private int _index;
            private readonly int _version;
            private T _current;

            internal Enumerator(PooledList<T> list)
            {
                _list = list;
                _index = 0;
                _version = list._version;
                _current = default!;
            }

            void IDisposable.Dispose()
            {
            }

            /// <summary>
            /// Advances to the next item in the list. Returns false if there are no more items.
            /// </summary>
            public bool MoveNext()
            {
                var localList = _list;

                if (_version == localList._version && ((uint)_index < (uint)localList._size))
                {
                    _current = localList._items[_index];
                    _index++;
                    return true;
                }
                return MoveNextRare();
            }

            private bool MoveNextRare()
            {
                if (_version != _list._version)
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                _index = _list._size + 1;
                _current = default!;
                return false;
            }

            /// <summary>
            /// Returns the current item.
            /// </summary>
            public T Current => _current;

            object? IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || _index == _list._size + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }
                    return Current;
                }
            }

            void IEnumerator.Reset()
            {
                if (_version != _list._version)
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                _index = 0;
                _current = default!;
            }
        }

        private readonly struct Comparer : IComparer<T>
        {
            private readonly Func<T, T, int> _comparison;

            public Comparer(Func<T, T, int> comparison) => _comparison = comparison;

            public int Compare([AllowNull] T x, [AllowNull] T y) => _comparison(x!, y!);
        }
    }
}
