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
using System.Runtime.Serialization;
using System.Threading;

#if NETCOREAPP3_0
using System.Text.Json.Serialization;
#endif

namespace Collections.Pooled
{
    using static ClearModeUtil;

    /// <summary>
    /// A simple stack of objects.  Internally it is implemented as an array,
    /// so Push can be O(n).  Pop is O(1).
    /// </summary>
    [DebuggerTypeProxy(typeof(StackDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
#if NETCOREAPP3_0
    [JsonConverter(typeof(PooledEnumerableJsonConverter))]
#endif
    public class PooledStack<T> : IEnumerable<T>, ICollection, IReadOnlyCollection<T>, IDisposable, IDeserializationCallback
    {
        [NonSerialized]
        private ArrayPool<T> _pool;
#pragma warning disable IDE0044
        [NonSerialized]
        private object? _syncRoot;
#pragma warning restore IDE0044

        private T[] _array; // Storage for stack elements. Do not rename (binary serialization)
        private int _size; // Number of items in the stack. Do not rename (binary serialization)
        private int _version; // Used to keep enumerator in sync w/ collection. Do not rename (binary serialization)
        private readonly bool _clearOnFree;

        private const int s_defaultCapacity = 4;

        #region Constructors

        /// <summary>
        /// Create a stack with the default initial capacity. 
        /// </summary>
        public PooledStack() : this(ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Create a stack with the default initial capacity. 
        /// </summary>
        public PooledStack(ClearMode clearMode) : this(clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Create a stack with the default initial capacity. 
        /// </summary>
        public PooledStack(ArrayPool<T> customPool) : this(ClearMode.Auto, customPool) { }

        /// <summary>
        /// Create a stack with the default initial capacity and a custom ArrayPool.
        /// </summary>
        public PooledStack(ClearMode clearMode, ArrayPool<T> customPool)
        {
            _pool = customPool ?? ArrayPool<T>.Shared;
            _array = Array.Empty<T>();
            _clearOnFree = ShouldClear<T>(clearMode);
        }

        /// <summary>
        /// Create a stack with a specific initial capacity.  The initial capacity
        /// must be a non-negative number.
        /// </summary>
        public PooledStack(int capacity) : this(capacity, ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Create a stack with a specific initial capacity.  The initial capacity
        /// must be a non-negative number.
        /// </summary>
        public PooledStack(int capacity, ClearMode clearMode) : this(capacity, clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Create a stack with a specific initial capacity.  The initial capacity
        /// must be a non-negative number.
        /// </summary>
        public PooledStack(int capacity, ArrayPool<T> customPool) : this(capacity, ClearMode.Auto, customPool) { }

        /// <summary>
        /// Create a stack with a specific initial capacity.  The initial capacity
        /// must be a non-negative number.
        /// </summary>
        public PooledStack(int capacity, ClearMode clearMode, ArrayPool<T> customPool)
        {
            if (capacity < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity,
                    ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }
            _pool = customPool ?? ArrayPool<T>.Shared;
            _array = _pool.Rent(capacity);
            _clearOnFree = ShouldClear<T>(clearMode);
        }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(IEnumerable<T> enumerable) : this(enumerable, ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(IEnumerable<T> enumerable, ClearMode clearMode) : this(enumerable, clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(IEnumerable<T> enumerable, ArrayPool<T> customPool) : this(enumerable, ClearMode.Auto, customPool) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(IEnumerable<T> enumerable, ClearMode clearMode, ArrayPool<T> customPool)
        {
            _pool = customPool ?? ArrayPool<T>.Shared;
            _clearOnFree = ShouldClear<T>(clearMode);

            switch (enumerable)
            {
                case null:
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);
                    _array = Array.Empty<T>();
                    break;

                case ICollection<T> collection:
                    if (collection.Count == 0)
                    {
                        _array = Array.Empty<T>();
                    }
                    else
                    {
                        _array = _pool.Rent(collection.Count);
                        collection.CopyTo(_array, 0);
                        _size = collection.Count;
                    }
                    break;

                default:
                    using (var list = new PooledList<T>(enumerable))
                    {
                        _array = _pool.Rent(list.Count);
                        list.Span.CopyTo(_array);
                        _size = list.Count;
                    }
                    break;
            }
        }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(T[] array) : this(array.AsSpan(), ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(T[] array, ClearMode clearMode) : this(array.AsSpan(), clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(T[] array, ArrayPool<T> customPool) : this(array.AsSpan(), ClearMode.Auto, customPool) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(T[] array, ClearMode clearMode, ArrayPool<T> customPool) : this(array.AsSpan(), clearMode, customPool) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(ReadOnlySpan<T> span) : this(span, ClearMode.Auto, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(ReadOnlySpan<T> span, ClearMode clearMode) : this(span, clearMode, ArrayPool<T>.Shared) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(ReadOnlySpan<T> span, ArrayPool<T> customPool) : this(span, ClearMode.Auto, customPool) { }

        /// <summary>
        /// Fills a Stack with the contents of a particular collection.  The items are
        /// pushed onto the stack in the same order they are read by the enumerator.
        /// </summary>
        public PooledStack(ReadOnlySpan<T> span, ClearMode clearMode, ArrayPool<T> customPool)
        {
            _pool = customPool ?? ArrayPool<T>.Shared;
            _clearOnFree = ShouldClear<T>(clearMode);
            _array = _pool.Rent(span.Length);
            span.CopyTo(_array);
            _size = span.Length;
        }

        #endregion

        /// <summary>
        /// The number of items in the stack.
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// Returns the ClearMode behavior for the collection, denoting whether values are
        /// cleared from internal arrays before returning them to the pool.
        /// </summary>
        public ClearMode ClearMode => _clearOnFree ? ClearMode.Always : ClearMode.Never;

        bool ICollection.IsSynchronized => false;

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
        /// Removes all Objects from the Stack.
        /// </summary>
        public void Clear()
        {
            if (_clearOnFree)
            {
                Array.Clear(_array, 0, _size); // clear the elements so that the gc can reclaim the references.
            }
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
            if (match is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

            int freeIndex = 0;   // the first free slot in items array

            // Find the first item which needs to be removed.
            while (freeIndex < _size && !match(_array[freeIndex]))
                freeIndex++;
            if (freeIndex >= _size) return 0;

            int current = freeIndex + 1;
            while (current < _size)
            {
                // Find the first item which needs to be kept.
                while (current < _size && match(_array[current]))
                    current++;

                if (current < _size)
                {
                    // copy item to the free slot.
                    _array[freeIndex++] = _array[current++];
                }
            }

            if (_clearOnFree)
            {
                // Clear the removed elements so that the gc can reclaim the references.
                Array.Clear(_array, freeIndex, _size - freeIndex);
            }

            int result = _size - freeIndex;
            _size = freeIndex;
            _version++;
            return result;
        }

        ///<summary>Copies the stack into an array.</summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
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

        ///<summary>Copies the stack into a span.</summary>
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

        /// <summary>
        /// Sets the capacity to the actual number of elements in the <see cref="PooledStack{T}"/>, 
        /// if that number is less than 90 percent of current capacity.
        /// </summary>
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
                var newArray = _pool.Rent(_size);
                if (newArray.Length < _array.Length)
                {
                    Array.Copy(_array, 0, newArray, 0, _size);
                    ReturnArray(replaceWith: newArray);
                    _version++;
                }
                else
                {
                    // The array from the pool wasn't any smaller than the one we already had,
                    // (we can only control minimum size) so return it and do nothing.
                    // If we create an exact-sized array not from the pool, we'll
                    // get an exception when returning it to the pool.
                    _pool.Return(newArray);
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

        /// <summary>
        ///     If the stack is not empty returns true and sets the <paramref name="result"/> parameter
        ///     to the value of the top object on the <see cref="PooledStack{T}"/> without removing it.
        ///     If the stack is empty, returns false and sets <paramref name="result"/> to the default
        ///     value for <typeparamref name="T"/>.
        /// </summary>
        /// <param name="result"></param>
        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            int size = _size - 1;
            T[] array = _array;

            if ((uint)size >= (uint)array.Length)
            {
                result = default!;
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
            if (_clearOnFree)
            {
                array[size] = default!;     // Free memory quicker.
            }
            return item;
        }

        /// <summary>
        ///     If the stack is not empty returns true and sets the <paramref name="result"/> parameter
        ///     to the value of the top object on the <see cref="PooledStack{T}"/> after removing it.
        ///     If the stack is empty, returns false and sets <paramref name="result"/> to the default
        ///     value for <typeparamref name="T"/>.
        /// </summary>
        /// <param name="result"></param>
        public bool TryPop([MaybeNullWhen(false)] out T result)
        {
            int size = _size - 1;
            T[] array = _array;

            if ((uint)size >= (uint)array.Length)
            {
                result = default!;
                return false;
            }

            _version++;
            _size = size;
            result = array[size];
            if (_clearOnFree)
            {
                array[size] = default!;     // Free memory quicker.
            }
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
            var newArray = _pool.Rent((_array.Length == 0) ? s_defaultCapacity : 2 * _array.Length);
            Array.Copy(_array, 0, newArray, 0, _size);
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

            var outArray = new T[_size];
            int i = 0;
            while (i < _size)
            {
                outArray[i] = _array[_size - i - 1];
                i++;
            }
            return outArray;
        }

        /// <summary>
        /// Reverses the stack. Useful when loading from saved state.
        /// </summary>
        public void Reverse()
        {
            Array.Reverse(_array, 0, _size);
        }

        private void ThrowForEmptyStack()
        {
            Debug.Assert(_size == 0);
            throw new InvalidOperationException("Stack was empty.");
        }

        private void ReturnArray(T[] replaceWith)
        {
            if (_array.Length > 0)
            {
                try
                {
                    _pool.Return(_array, clearArray: _clearOnFree);
                }
                catch (ArgumentException)
                {
                    // oh well, the array pool didn't like our array
                }
            }

            _array = replaceWith;
        }

        /// <summary>
        /// Returns the underlying storage to the pool and sets <see cref="PooledStack{T}.Count"/> to zero.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns the underlying storage to the pool and sets <see cref="PooledStack{T}.Count"/> to zero.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReturnArray(replaceWith: Array.Empty<T>());
                _size = 0;
                _version++;
            }
        }

        void IDeserializationCallback.OnDeserialization(object? sender)
        {
            // We can't serialize array pools, so deserialized PooledStacks will
            // have to use the shared pool, even if they were using a custom pool
            // before serialization.
            _pool = ArrayPool<T>.Shared;
        }

        /// <summary>
        /// Enumerates the <see cref="PooledStack{T}"/>.
        /// </summary>
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
                _currentElement = default!;
            }

            /// <summary>
            /// Disposes the enumerator.
            /// </summary>
            public void Dispose() => _index = -1;

            /// <summary>
            /// Advances the enumerator.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                bool retval;
                if (_version != _stack._version)
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
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
                    _currentElement = default!;
                return retval;
            }

            /// <summary>
            /// Returns the current item.
            /// </summary>
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

            object? IEnumerator.Current => Current;

            void IEnumerator.Reset()
            {
                if (_version != _stack._version)
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                _index = -2;
                _currentElement = default!;
            }
        }
    }
}
