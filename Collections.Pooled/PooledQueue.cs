// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*=============================================================================
**
**
** Purpose: A circular-array implementation of a generic queue.
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
    /// A simple Queue of generic objects.  Internally it is implemented as a 
    /// circular buffer, so Enqueue can be O(n).  Dequeue is O(1).
    /// </summary>
    /// <typeparam name="T">The type to store in the queue.</typeparam>
    [DebuggerTypeProxy(typeof(QueueDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class PooledQueue<T> : IEnumerable<T>, ICollection, IReadOnlyCollection<T>, IDisposable
    {
        private static readonly ArrayPool<T> s_pool = ArrayPool<T>.Shared;

        private T[] _array;
        private int _head;       // The index from which to dequeue if the queue isn't empty.
        private int _tail;       // The index at which to enqueue if the queue isn't full.
        private int _size;       // Number of elements.
        private int _version;
        private object _syncRoot;

        private const int MinimumGrow = 4;
        private const int GrowFactor = 200;  // double each time

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledQueue{T}"/> class that is empty and has the default initial capacity.
        /// </summary>
        public PooledQueue()
        {
            _array = Array.Empty<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledQueue{T}"/> class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public PooledQueue(int capacity)
        {
            if (capacity < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity,
                    ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }
            _array = s_pool.Rent(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledQueue{T}"/> class that contains elements copied from the specified 
        /// collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        public PooledQueue(IEnumerable<T> enumerable)
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
                        if (_size != _array.Length) _tail = _size;
                    }
                    break;

                default:
                    using (var list = new PooledList<T>(enumerable))
                    {
                        _array = s_pool.Rent(list.Count);
                        list.Span.CopyTo(_array);
                        _size = list.Count;
                        if (_size != _array.Length) _tail = _size;
                    }
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledQueue{T}"/> class that contains elements copied from the specified 
        /// array and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        public PooledQueue(T[] array) : this(array.AsSpan()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledQueue{T}"/> class that contains elements copied from the specified 
        /// span and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        public PooledQueue(ReadOnlySpan<T> span)
        {
            _array = s_pool.Rent(span.Length);
            span.CopyTo(_array);
            _size = span.Length;
            if (_size != _array.Length) _tail = _size;
        }

        /// <summary>
        /// The number of items in the queue.
        /// </summary>
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
        /// Removes all objects from the queue.
        /// </summary>
        public void Clear()
        {
            if (_size != 0)
            {
#if NETCOREAPP2_1
                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                {
                    if (_head < _tail)
                    {
                        Array.Clear(_array, _head, _size);
                    }
                    else
                    {
                        Array.Clear(_array, _head, _array.Length - _head);
                        Array.Clear(_array, 0, _tail);
                    }
                }
#else
                if (_head < _tail)
                {
                    Array.Clear(_array, _head, _size);
                }
                else
                {
                    Array.Clear(_array, _head, _array.Length - _head);
                    Array.Clear(_array, 0, _tail);
                }
#endif

                _size = 0;
            }

            _head = 0;
            _tail = 0;
            _version++;
        }

        /// <summary>
        /// CopyTo copies a collection into an Array, starting at a particular
        /// index into the array.
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if ((uint)arrayIndex > (uint)array.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex,
                    ExceptionResource.ArgumentOutOfRange_Index);
            }

            int arrayLen = array.Length;
            if (arrayLen - arrayIndex < _size)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
            }

            int numToCopy = _size;
            if (numToCopy == 0) return;

            int firstPart = Math.Min(_array.Length - _head, numToCopy);
            Array.Copy(_array, _head, array, arrayIndex, firstPart);
            numToCopy -= firstPart;
            if (numToCopy > 0)
            {
                Array.Copy(_array, 0, array, arrayIndex + _array.Length - _head, numToCopy);
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (array.Rank != 1)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Rank_MultiDimNotSupported,
                    ExceptionArgument.array);
            }

            if (array.GetLowerBound(0) != 0)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound,
                    ExceptionArgument.array);
            }

            int arrayLen = array.Length;
            if ((uint)index > (uint)arrayLen)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index,
                    ExceptionResource.ArgumentOutOfRange_Index);
            }

            if (arrayLen - index < _size)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
            }

            int numToCopy = _size;
            if (numToCopy == 0) return;

            try
            {
                int firstPart = (_array.Length - _head < numToCopy) ? _array.Length - _head : numToCopy;
                Array.Copy(_array, _head, array, index, firstPart);
                numToCopy -= firstPart;

                if (numToCopy > 0)
                {
                    Array.Copy(_array, 0, array, index + _array.Length - _head, numToCopy);
                }
            }
            catch (ArrayTypeMismatchException)
            {
                ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
            }
        }

        /// <summary>
        /// Adds <paramref name="item"/> to the tail of the queue.
        /// </summary>
        public void Enqueue(T item)
        {
            if (_size == _array.Length)
            {
                int newcapacity = (int)(_array.Length * (long)GrowFactor / 100);
                if (newcapacity < _array.Length + MinimumGrow)
                {
                    newcapacity = _array.Length + MinimumGrow;
                }
                SetCapacity(newcapacity);
            }

            _array[_tail] = item;
            MoveNext(ref _tail);
            _size++;
            _version++;
        }

        /// <summary>
        /// GetEnumerator returns an IEnumerator over this Queue.  This
        /// Enumerator will support removing.
        /// </summary>
        public Enumerator GetEnumerator()
            => new Enumerator(this);

        /// <internalonly/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(this);

        /// <summary>
        /// Removes the object at the head of the queue and returns it. If the queue
        /// is empty, this method throws an 
        /// <see cref="InvalidOperationException"/>.
        /// </summary>
        public T Dequeue()
        {
            int head = _head;
            T[] array = _array;

            if (_size == 0)
            {
                ThrowForEmptyQueue();
            }

            T removed = array[head];
#if NETCOREAPP2_1
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                array[head] = default;
            }
#else
            array[head] = default;
#endif
            MoveNext(ref _head);
            _size--;
            _version++;
            return removed;
        }

        public bool TryDequeue(out T result)
        {
            int head = _head;
            T[] array = _array;

            if (_size == 0)
            {
                result = default;
                return false;
            }

            result = array[head];
#if NETCOREAPP2_1
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                array[head] = default;
            }
#else
            array[head] = default;
#endif
            MoveNext(ref _head);
            _size--;
            _version++;
            return true;
        }

        /// <summary>
        /// Returns the object at the head of the queue. The object remains in the
        /// queue. If the queue is empty, this method throws an 
        /// <see cref="InvalidOperationException"/>.
        /// </summary>
        public T Peek()
        {
            if (_size == 0)
            {
                ThrowForEmptyQueue();
            }

            return _array[_head];
        }

        public bool TryPeek(out T result)
        {
            if (_size == 0)
            {
                result = default;
                return false;
            }

            result = _array[_head];
            return true;
        }

        /// <summary>
        /// Returns true if the queue contains at least one object equal to item.
        /// Equality is determined using <see cref="EqualityComparer{T}.Default"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            if (_size == 0)
            {
                return false;
            }

            if (_head < _tail)
            {
                return Array.IndexOf(_array, item, _head, _size) >= 0;
            }

            // We've wrapped around. Check both partitions, the least recently enqueued first.
            return
                Array.IndexOf(_array, item, _head, _array.Length - _head) >= 0 ||
                Array.IndexOf(_array, item, 0, _tail) >= 0;
        }

        // TODO: Add a RemoveWhere method.

        /// <summary>
        /// Iterates over the objects in the queue, returning an array of the
        /// objects in the Queue, or an empty array if the queue is empty.
        /// The order of elements in the array is first in to last in, the same
        /// order produced by successive calls to Dequeue.
        /// </summary>
        public T[] ToArray()
        {
            if (_size == 0)
            {
                return Array.Empty<T>();
            }

            T[] arr = new T[_size];

            if (_head < _tail)
            {
                Array.Copy(_array, _head, arr, 0, _size);
            }
            else
            {
                Array.Copy(_array, _head, arr, 0, _array.Length - _head);
                Array.Copy(_array, 0, arr, _array.Length - _head, _tail);
            }

            return arr;
        }

        // PRIVATE Grows or shrinks the buffer to hold capacity objects. Capacity
        // must be >= _size.
        private void SetCapacity(int capacity)
        {
            T[] newarray = s_pool.Rent(capacity);
            if (_size > 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_array, _head, newarray, 0, _size);
                }
                else
                {
                    Array.Copy(_array, _head, newarray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, newarray, _array.Length - _head, _tail);
                }
            }

            ReturnArray(replaceWith: newarray);
            _head = 0;
            _tail = (_size == newarray.Length) ? 0 : _size;
            _version++;
        }

        // Increments the index wrapping it if necessary.
        private void MoveNext(ref int index)
        {
            // It is tempting to use the remainder operator here but it is actually much slower
            // than a simple comparison and a rarely taken branch.
            // JIT produces better code than with ternary operator ?:
            int tmp = index + 1;
            if (tmp == _array.Length)
            {
                tmp = 0;
            }
            index = tmp;
        }

        private void ThrowForEmptyQueue()
        {
            Debug.Assert(_size == 0);
            throw new InvalidOperationException("Queue is empty.");
        }

        public void TrimExcess()
        {
            int threshold = (int)(_array.Length * 0.9);
            if (_size < threshold)
            {
                SetCapacity(_size);
            }
        }

        private void ReturnArray(T[] replaceWith)
        {
            if (_array.Length > 0)
            {
#if NETCOREAPP2_1
                s_pool.Return(_array, clearArray: RuntimeHelpers.IsReferenceOrContainsReferences<T>());
#else
                s_pool.Return(_array, clearArray: true);
#endif
            }
            _array = replaceWith;
        }

        public void Dispose()
        {
            ReturnArray(replaceWith: Array.Empty<T>());
            _head = _tail = _size = 0;
            _version++;
        }

        // Implements an enumerator for a Queue.  The enumerator uses the
        // internal version number of the list to ensure that no modifications are
        // made to the list while an enumeration is in progress.
        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "not an expected scenario")]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly PooledQueue<T> _q;
            private readonly int _version;
            private int _index;   // -1 = not started, -2 = ended/disposed
            private T _currentElement;

            internal Enumerator(PooledQueue<T> q)
            {
                _q = q;
                _version = q._version;
                _index = -1;
                _currentElement = default;
            }

            public void Dispose()
            {
                _index = -2;
                _currentElement = default;
            }

            public bool MoveNext()
            {
                if (_version != _q._version)
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();

                if (_index == -2)
                    return false;

                _index++;

                if (_index == _q._size)
                {
                    // We've run past the last element
                    _index = -2;
                    _currentElement = default;
                    return false;
                }

                // Cache some fields in locals to decrease code size
                T[] array = _q._array;
                int capacity = array.Length;

                // _index represents the 0-based index into the queue, however the queue
                // doesn't have to start from 0 and it may not even be stored contiguously in memory.

                int arrayIndex = _q._head + _index; // this is the actual index into the queue's backing array
                if (arrayIndex >= capacity)
                {
                    // NOTE: Originally we were using the modulo operator here, however
                    // on Intel processors it has a very high instruction latency which
                    // was slowing down the loop quite a bit.
                    // Replacing it with simple comparison/subtraction operations sped up
                    // the average foreach loop by 2x.

                    arrayIndex -= capacity; // wrap around if needed
                }

                _currentElement = array[arrayIndex];
                return true;
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
                if (_index == -1)
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumNotStarted();
                else
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumEnded();
            }

            object IEnumerator.Current => Current;

            void IEnumerator.Reset()
            {
                if (_version != _q._version)
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                _index = -1;
                _currentElement = default;
            }
        }
    }
}
