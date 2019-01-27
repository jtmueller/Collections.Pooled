// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Collections.Pooled
{
    /// <summary>
    /// Implementation notes:
    /// This uses an array-based implementation similar to <see cref="Dictionary{TKey, TValue}"/>, using a buckets array
    /// to map hash values to the Slots array. Items in the Slots array that hash to the same value
    /// are chained together through the "next" indices. 
    /// 
    /// The capacity is always prime; so during resizing, the capacity is chosen as the next prime
    /// greater than double the last capacity. 
    /// 
    /// The underlying data structures are lazily initialized. Because of the observation that, 
    /// in practice, hashtables tend to contain only a few elements, the initial capacity is
    /// set very small (3 elements) unless the ctor with a collection is used.
    /// 
    /// The +/- 1 modifications in methods that add, check for containment, etc allow us to 
    /// distinguish a hash code of 0 from an uninitialized bucket. This saves us from having to 
    /// reset each bucket to -1 when resizing. See Contains, for example.
    /// 
    /// Set methods such as UnionWith, IntersectWith, ExceptWith, and SymmetricExceptWith modify
    /// this set.
    /// 
    /// Some operations can perform faster if we can assume "other" contains unique elements
    /// according to this equality comparer. The only times this is efficient to check is if
    /// other is a hashset. Note that checking that it's a hashset alone doesn't suffice; we
    /// also have to check that the hashset is using the same equality comparer. If other 
    /// has a different equality comparer, it will have unique elements according to its own
    /// equality comparer, but not necessarily according to ours. Therefore, to go these 
    /// optimized routes we check that other is a hashset using the same equality comparer.
    /// 
    /// A HashSet with no elements has the properties of the empty set. (See IsSubset, etc. for 
    /// special empty set checks.)
    /// 
    /// A couple of methods have a special case if other is this (e.g. SymmetricExceptWith). 
    /// If we didn't have these checks, we could be iterating over the set and modifying at
    /// the same time. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "By design")]
    [Serializable]
    public class PooledSet<T> : ICollection<T>, ISet<T>, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback, IDisposable
    {
        // store lower 31 bits of hash code
        private const int Lower31BitMask = 0x7FFFFFFF;
        // cutoff point, above which we won't do stackallocs. This corresponds to 100 integers.
        private const int StackAllocThreshold = 100;
        // when constructing a hashset from an existing collection, it may contain duplicates, 
        // so this is used as the max acceptable excess ratio of capacity to count. Note that
        // this is only used on the ctor and not to automatically shrink if the hashset has, e.g,
        // a lot of adds followed by removes. Users must explicitly shrink by calling TrimExcess.
        // This is set to 3 because capacity is acceptable as 2x rounded up to nearest prime.
        private const int ShrinkThreshold = 3;

        // constants for serialization
        private const string CapacityName = "Capacity"; // Do not rename (binary serialization)
        private const string ElementsName = "Elements"; // Do not rename (binary serialization)
        private const string ComparerName = "Comparer"; // Do not rename (binary serialization)
        private const string VersionName = "Version"; // Do not rename (binary serialization)

        private static readonly ArrayPool<int> s_bucketPool = ArrayPool<int>.Shared;
        private static readonly ArrayPool<Slot> s_slotPool = ArrayPool<Slot>.Shared;

        // WARNING:
        // It's important that the number of buckets be prime, and these arrays could exceed
        // that size as they come from ArrayPool. Be careful not to index past _size or bad
        // things will happen.
        // Alternatively, use the private properties Buckets and Slots, which slice the
        // arrays down to the correct length.
        private int[] _buckets;
        private Slot[] _slots;
        private int _size;

        private int _count;
        private int _lastIndex;
        private int _freeList;
        private IEqualityComparer<T> _comparer;
        private int _version;

        private SerializationInfo _siInfo; // temporary variable needed during deserialization

        #region Constructors

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet()
            : this(EqualityComparer<T>.Default)
        { }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            _comparer = comparer;
            _lastIndex = 0;
            _count = 0;
            _freeList = -1;
            _version = 0;
            _size = 0;
        }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(int capacity)
            : this(capacity, EqualityComparer<T>.Default)
        { }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(IEnumerable<T> collection)
            : this(collection, collection is PooledSet<T> ps ? ps.Comparer : EqualityComparer<T>.Default)
        { }

        /// <summary>
        /// Implementation Notes:
        /// Since resizes are relatively expensive (require rehashing), this attempts to minimize 
        /// the need to resize by setting the initial capacity based on size of collection. 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        public PooledSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : this(comparer)
        {
            if (collection == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
            }

            if (collection is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
            {
                CopyFrom(otherAsSet);
            }
            else
            {
                // to avoid excess resizes, first set size based on collection's count. Collection
                // may contain duplicates, so call TrimExcess if resulting hashset is larger than
                // threshold
                int suggestedCapacity = (collection is ICollection<T> coll) ? coll.Count : 0;
                Initialize(suggestedCapacity);

                UnionWith(collection);

                if (_count > 0 && _size / _count > ShrinkThreshold)
                {
                    TrimExcess();
                }
            }
        }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(T[] array)
            : this(array.AsSpan(), null)
        { }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(T[] array, IEqualityComparer<T> comparer)
            : this(array.AsSpan(), comparer)
        { }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(ReadOnlySpan<T> span)
            : this(span, null)
        { }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(ReadOnlySpan<T> span, IEqualityComparer<T> comparer)
            : this(comparer)
        {
            // to avoid excess resizes, first set size based on collection's count. Collection
            // may contain duplicates, so call TrimExcess if resulting hashset is larger than
            // threshold
            Initialize(span.Length);

            UnionWith(span);

            if (_count > 0 && _size / _count > ShrinkThreshold)
            {
                TrimExcess();
            }
        }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        protected PooledSet(SerializationInfo info, StreamingContext context)
        {
            // We can't do anything with the keys and values until the entire graph has been 
            // deserialized and we have a reasonable estimate that GetHashCode is not going to 
            // fail.  For the time being, we'll just cache this.  The graph is not valid until 
            // OnDeserialization has been called.
            _siInfo = info;
        }

        // Initializes the HashSet from another HashSet with the same element type and
        // equality comparer.
        private void CopyFrom(PooledSet<T> source)
        {
            int count = source._count;
            if (count == 0)
            {
                // As well as short-circuiting on the rest of the work done,
                // this avoids errors from trying to access otherAsHashSet._buckets
                // or otherAsHashSet._slots when they aren't initialized.
                return;
            }

            int capacity = _size = source._size;
            int threshold = HashHelpers.ExpandPrime(count + 1);

            if (threshold >= capacity)
            {
                _buckets = s_bucketPool.Rent(capacity);
                Array.Clear(_buckets, 0, _buckets.Length);
                Array.Copy(source._buckets, _buckets, capacity);
                _slots = s_slotPool.Rent(capacity);
                Array.Copy(source._slots, _slots, capacity);

                _lastIndex = source._lastIndex;
                _freeList = source._freeList;
            }
            else
            {
                int lastIndex = source._lastIndex;
                Slot[] slots = source._slots;
                Initialize(count);
                int index = 0;
                for (int i = 0; i < lastIndex; ++i)
                {
                    int hashCode = slots[i].hashCode;
                    if (hashCode >= 0)
                    {
                        AddValue(index, hashCode, slots[i].value);
                        ++index;
                    }
                }
                Debug.Assert(index == count);
                _lastIndex = index;
            }
            _count = count;
        }

        /// <summary>
        /// Creates a new instance of PooledSet.
        /// </summary>
        public PooledSet(int capacity, IEqualityComparer<T> comparer)
            : this(comparer)
        {
            if (capacity < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (capacity > 0)
            {
                Initialize(capacity);
            }
        }

        #endregion

        #region ICollection<T> methods

        /// <summary>
        /// Add item to this hashset. This is the explicit implementation of the <see cref="ICollection{T}"/>
        /// interface. The other Add method returns bool indicating whether item was added.
        /// </summary>
        /// <param name="item">item to add</param>
        void ICollection<T>.Add(T item)
            => AddIfNotPresent(item);

        /// <summary>
        /// Remove all items from this set. This clears the elements but not the underlying 
        /// buckets and slots array. Follow this call by TrimExcess to release these.
        /// </summary>
        public void Clear()
        {
            if (_lastIndex > 0)
            {
                Debug.Assert(_buckets != null, "_buckets was null but _lastIndex > 0");

                // clear the elements so that the gc can reclaim the references.
                // clear only up to _lastIndex for _slots 
                Array.Clear(_slots, 0, _lastIndex);
                Array.Clear(_buckets, 0, _buckets.Length);
                _lastIndex = 0;
                _count = 0;
                _freeList = -1;
            }
            _version++;
        }

        /// <summary>
        /// Checks if this hashset contains the item
        /// </summary>
        /// <param name="item">item to check for containment</param>
        /// <returns>true if item contained; false if not</returns>
        public bool Contains(T item)
        {
            if (_buckets != null)
            {
                int collisionCount = 0;
                int hashCode = InternalGetHashCode(item);
                Slot[] slots = _slots;
                // see note at "HashSet" level describing why "- 1" appears in for loop
                for (int i = _buckets[hashCode % _size] - 1; i >= 0; i = slots[i].next)
                {
                    if (slots[i].hashCode == hashCode && _comparer.Equals(slots[i].value, item))
                    {
                        return true;
                    }

                    if (collisionCount >= _size)
                    {
                        // The chain of entries forms a loop, which means a concurrent update has happened.
                        ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                    }
                    collisionCount++;
                }
            }
            // either _buckets is null or wasn't found
            return false;
        }

        /// <summary>
        /// Copy items in this hashset to array, starting at arrayIndex
        /// </summary>
        /// <param name="array">array to add items to</param>
        /// <param name="arrayIndex">index to start at</param>
        public void CopyTo(T[] array, int arrayIndex)
            => CopyTo(array, arrayIndex, _count);

        /// <summary>
        /// Remove item from this hashset
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>true if removed; false if not (i.e. if the item wasn't in the HashSet)</returns>
        public bool Remove(T item)
        {
            if (_buckets != null)
            {
                int hashCode = InternalGetHashCode(item);
                int bucket = hashCode % _size;
                int last = -1;
                int collisionCount = 0;
                Slot[] slots = _slots;
                for (int i = _buckets[bucket] - 1; i >= 0; last = i, i = slots[i].next)
                {
                    if (slots[i].hashCode == hashCode && _comparer.Equals(slots[i].value, item))
                    {
                        if (last < 0)
                        {
                            // first iteration; update buckets
                            _buckets[bucket] = slots[i].next + 1;
                        }
                        else
                        {
                            // subsequent iterations; update 'next' pointers
                            slots[last].next = slots[i].next;
                        }
                        slots[i].hashCode = -1;
#if NETCOREAPP2_1
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                        {
                            slots[i].value = default;
                        }
#else
                        slots[i].value = default;
#endif
                        slots[i].next = _freeList;

                        _count--;
                        _version++;
                        if (_count == 0)
                        {
                            _lastIndex = 0;
                            _freeList = -1;
                        }
                        else
                        {
                            _freeList = i;
                        }
                        return true;
                    }

                    if (collisionCount >= _size)
                    {
                        // The chain of entries forms a loop, which means a concurrent update has happened.
                        ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                    }
                    collisionCount++;
                }
            }
            // either _buckets is null or wasn't found
            return false;
        }

        /// <summary>
        /// Number of elements in this hashset
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Whether this is readonly
        /// </summary>
        bool ICollection<T>.IsReadOnly => false;

        #endregion

        #region IEnumerable methods

        /// <summary>
        /// Gets an enumerator with which to enumerate the set.
        /// </summary>
        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(this);

        #endregion

        #region ISerializable methods

        /// <summary>
        /// Gets object data for serialization.
        /// </summary>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
            }

            info.AddValue(VersionName, _version); // need to serialize version to avoid problems with serializing while enumerating
            info.AddValue(ComparerName, _comparer, typeof(IEqualityComparer<T>));
            info.AddValue(CapacityName, _buckets == null ? 0 : _size);

            if (_buckets != null)
            {
                T[] array = new T[_count];
                CopyTo(array);
                info.AddValue(ElementsName, array, typeof(T[]));
            }
        }

        #endregion

        #region IDeserializationCallback methods

        /// <summary>
        /// Deserialization callback.
        /// </summary>
        public virtual void OnDeserialization(object sender)
        {
            if (_siInfo == null)
            {
                // It might be necessary to call OnDeserialization from a container if the 
                // container object also implements OnDeserialization. We can return immediately
                // if this function is called twice. Note we set _siInfo to null at the end of this method.
                return;
            }

            int capacity = _siInfo.GetInt32(CapacityName);
            _comparer = (IEqualityComparer<T>)_siInfo.GetValue(ComparerName, typeof(IEqualityComparer<T>));
            _freeList = -1;

            if (capacity != 0)
            {
                Initialize(capacity);

                T[] array = (T[])_siInfo.GetValue(ElementsName, typeof(T[]));

                if (array == null)
                {
                    ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
                }

                // there are no resizes here because we already set capacity above
                for (int i = 0; i < array.Length; i++)
                {
                    AddIfNotPresent(array[i]);
                }
            }
            else
            {
                _buckets = null;
            }

            _version = _siInfo.GetInt32(VersionName);
            _siInfo = null;
        }

        #endregion

        #region HashSet methods

        /// <summary>
        /// Add item to this PooledSet. Returns bool indicating whether item was added (won't be 
        /// added if already present)
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if added, false if already present</returns>
        public bool Add(T item)
            => AddIfNotPresent(item);

        /// <summary>
        /// Searches the set for a given value and returns the equal value it finds, if any.
        /// </summary>
        /// <param name="equalValue">The value to search for.</param>
        /// <param name="actualValue">The value from the set that the search found, or the default value of <typeparamref name="T"/> when the search yielded no match.</param>
        /// <returns>A value indicating whether the search was successful.</returns>
        /// <remarks>
        /// This can be useful when you want to reuse a previously stored reference instead of 
        /// a newly constructed one (so that more sharing of references can occur) or to look up
        /// a value that has more complete data than the value you currently have, although their
        /// comparer functions indicate they are equal.
        /// </remarks>
        public bool TryGetValue(T equalValue, out T actualValue)
        {
            if (_buckets != null)
            {
                int i = InternalIndexOf(equalValue);
                if (i >= 0)
                {
                    actualValue = _slots[i].value;
                    return true;
                }
            }
            actualValue = default;
            return false;
        }

        /// <summary>
        /// Take the union of this HashSet with other. Modifies this set.
        /// </summary>
        /// <remarks>
        /// Implementation note: GetSuggestedCapacity (to increase capacity in advance avoiding 
        /// multiple resizes ended up not being useful in practice; quickly gets to the 
        /// point where it's a wasteful check.
        /// </remarks>
        /// <param name="other">enumerable with items to add</param>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            foreach (T item in other)
            {
                AddIfNotPresent(item);
            }
        }

        /// <summary>
        /// Take the union of this PooledSet with other. Modifies this set.
        /// </summary>
        /// <param name="other"></param>
        public void UnionWith(T[] other) => UnionWith(new ReadOnlySpan<T>(other));


        /// <summary>
        /// Take the union of this PooledSet with other. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to add</param>
        public void UnionWith(ReadOnlySpan<T> other)
        {
            for (int i = 0, len = other.Length; i < len; i++)
            {
                AddIfNotPresent(other[i]);
            }
        }

        /// <summary>
        /// Takes the intersection of this set with other. Modifies this set.
        /// </summary>
        /// <remarks>
        /// Implementation Notes: 
        /// We get better perf if other is a hashset using same equality comparer, because we 
        /// get constant contains check in other. Resulting cost is O(n1) to iterate over this.
        /// 
        /// If we can't go above route, iterate over the other and mark intersection by checking
        /// contains in this. Then loop over and delete any unmarked elements. Total cost is n2+n1. 
        /// 
        /// Attempts to return early based on counts alone, using the property that the 
        /// intersection of anything with the empty set is the empty set.
        /// </remarks>
        /// <param name="other">enumerable with items to add </param>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // intersection of anything with empty set is empty set, so return if count is 0
            if (_count == 0)
            {
                return;
            }

            // set intersecting with itself is the same set
            if (other == this)
            {
                return;
            }

            // if other is empty, intersection is empty set; remove all elements and we're done
            // can only figure this out if implements ICollection<T>. (IEnumerable<T> has no count)
            if (other is ICollection<T> otherAsCollection)
            {
                if (otherAsCollection.Count == 0)
                {
                    Clear();
                    return;
                }

                // faster if other is a hashset using same equality comparer; so check 
                // that other is a hashset using the same equality comparer.
                if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
                {
                    IntersectWithHashSetWithSameEC(otherAsSet);
                    return;
                }

                if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
                {
                    IntersectWithHashSetWithSameEC(otherAsHs);
                    return;
                }
            }

            IntersectWithEnumerable(other);
        }

        /// <summary>
        /// Takes the intersection of this set with other. Modifies this set.
        /// </summary>
        /// <remarks>
        /// Implementation Notes: 
        /// Iterate over the other and mark intersection by checking
        /// contains in this. Then loop over and delete any unmarked elements. Total cost is n2+n1. 
        /// 
        /// Attempts to return early based on counts alone, using the property that the 
        /// intersection of anything with the empty set is the empty set.
        /// </remarks>
        /// <param name="other">enumerable with items to add </param>
        public void IntersectWith(T[] other) => IntersectWith(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Takes the intersection of this set with other. Modifies this set.
        /// </summary>
        /// <remarks>
        /// Implementation Notes: 
        /// Iterate over the other and mark intersection by checking
        /// contains in this. Then loop over and delete any unmarked elements. Total cost is n2+n1. 
        /// 
        /// Attempts to return early based on counts alone, using the property that the 
        /// intersection of anything with the empty set is the empty set.
        /// </remarks>
        /// <param name="other">enumerable with items to add </param>
        public void IntersectWith(ReadOnlySpan<T> other)
        {
            // intersection of anything with empty set is empty set, so return if count is 0
            if (_count == 0)
            {
                return;
            }

            // if other is empty, intersection is empty set; remove all elements and we're done
            if (other.Length == 0)
            {
                Clear();
                return;
            }

            IntersectWithSpan(other);
        }

        /// <summary>
        /// Remove items in other from this set. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to remove</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // this is already the empty set; return
            if (_count == 0)
            {
                return;
            }

            // special case if other is this; a set minus itself is the empty set
            if (other == this)
            {
                Clear();
                return;
            }

            // remove every element in other from this
            foreach (T element in other)
            {
                Remove(element);
            }
        }

        /// <summary>
        /// Remove items in other from this set. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to remove</param>
        public void ExceptWith(T[] other) => ExceptWith(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Remove items in other from this set. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to remove</param>
        public void ExceptWith(ReadOnlySpan<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // this is already the empty set; return
            if (_count == 0)
            {
                return;
            }

            // remove every element in other from this
            for (int i = 0, len = other.Length; i < len; i++)
            {
                Remove(other[i]);
            }
        }

        /// <summary>
        /// Takes symmetric difference (XOR) with other and this set. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to XOR</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // if set is empty, then symmetric difference is other
            if (_count == 0)
            {
                UnionWith(other);
                return;
            }

            // special case this; the symmetric difference of a set with itself is the empty set
            if (other == this)
            {
                Clear();
                return;
            }

            // If other is a HashSet, it has unique elements according to its equality comparer,
            // but if they're using different equality comparers, then assumption of uniqueness
            // will fail. So first check if other is a hashset using the same equality comparer;
            // symmetric except is a lot faster and avoids bit array allocations if we can assume
            // uniqueness
            if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
            {
                SymmetricExceptWithUniqueHashSet(otherAsSet);
            }
            else if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
            {
                SymmetricExceptWithUniqueHashSet(otherAsHs);
            }
            else
            {
                SymmetricExceptWithEnumerable(other);
            }
        }

        /// <summary>
        /// Takes symmetric difference (XOR) with other and this set. Modifies this set.
        /// </summary>
        /// <param name="other">array with items to XOR</param>
        public void SymmetricExceptWith(T[] other) => SymmetricExceptWith(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Takes symmetric difference (XOR) with other and this set. Modifies this set.
        /// </summary>
        /// <param name="other">span with items to XOR</param>
        public void SymmetricExceptWith(ReadOnlySpan<T> other)
        {
            // if set is empty, then symmetric difference is other
            if (_count == 0)
            {
                UnionWith(other);
                return;
            }

            SymmetricExceptWithSpan(other);
        }

        /// <summary>
        /// Checks if this is a subset of other.
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it's a subset of anything, including the empty set
        /// 2. If other has unique elements according to this equality comparer, and this has more
        /// elements than other, then it can't be a subset.
        /// 
        /// Furthermore, if other is a hashset using the same equality comparer, we can use a 
        /// faster element-wise check.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a subset of other; false if not</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // The empty set is a subset of any set
            if (_count == 0)
            {
                return true;
            }

            // Set is always a subset of itself
            if (other == this)
            {
                return true;
            }

            // faster if other has unique elements according to this equality comparer; so check 
            // that other is a hashset using the same equality comparer.
            if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
            {
                // if this has more elements then it can't be a subset
                if (_count > otherAsSet.Count)
                {
                    return false;
                }

                // already checked that we're using same equality comparer. simply check that 
                // each element in this is contained in other.
                return IsSubsetOfHashSetWithSameEC(otherAsSet);
            }

            if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
            {
                // if this has more elements then it can't be a subset
                if (_count > otherAsHs.Count)
                {
                    return false;
                }

                // already checked that we're using same equality comparer. simply check that 
                // each element in this is contained in other.
                return IsSubsetOfHashSetWithSameEC(otherAsHs);
            }

            ElementCount result = CheckUniqueAndUnfoundElements(other, false);
            return (result.uniqueCount == _count && result.unfoundCount >= 0);
        }

        /// <summary>
        /// Checks if this is a subset of other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this is a subset of other; false if not</returns>
        public bool IsSubsetOf(T[] other) => IsSubsetOf(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Checks if this is a subset of other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this is a subset of other; false if not</returns>
        public bool IsSubsetOf(ReadOnlySpan<T> other)
        {
            // The empty set is a subset of any set
            if (_count == 0)
            {
                return true;
            }

            ElementCount result = CheckUniqueAndUnfoundElements(other, false);
            return (result.uniqueCount == _count && result.unfoundCount >= 0);
        }

        /// <summary>
        /// Checks if this is a proper subset of other (i.e. strictly contained in)
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it's a proper subset of a set that contains at least
        /// one element, but it's not a proper subset of the empty set.
        /// 2. If other has unique elements according to this equality comparer, and this has >=
        /// the number of elements in other, then this can't be a proper subset.
        /// 
        /// Furthermore, if other is a hashset using the same equality comparer, we can use a 
        /// faster element-wise check.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a proper subset of other; false if not</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // no set is a proper subset of itself.
            if (other == this)
            {
                return false;
            }

            if (other is ICollection<T> otherAsCollection)
            {
                // no set is a proper subset of an empty set
                if (otherAsCollection.Count == 0)
                {
                    return false;
                }

                // the empty set is a proper subset of anything but the empty set
                if (_count == 0)
                {
                    return otherAsCollection.Count > 0;
                }
                // faster if other is a hashset (and we're using same equality comparer)
                if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
                {
                    if (_count >= otherAsSet.Count)
                    {
                        return false;
                    }
                    // this has strictly less than number of items in other, so the following
                    // check suffices for proper subset.
                    return IsSubsetOfHashSetWithSameEC(otherAsSet);
                }
                if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
                {
                    // if this has more elements then it can't be a subset
                    if (_count > otherAsHs.Count)
                    {
                        return false;
                    }

                    // already checked that we're using same equality comparer. simply check that 
                    // each element in this is contained in other.
                    return IsSubsetOfHashSetWithSameEC(otherAsHs);
                }
            }

            ElementCount result = CheckUniqueAndUnfoundElements(other, false);
            return (result.uniqueCount == _count && result.unfoundCount > 0);
        }

        /// <summary>
        /// Checks if this is a proper subset of other (i.e. strictly contained in)
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it's a proper subset of a set that contains at least
        /// one element, but it's not a proper subset of the empty set.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a proper subset of other; false if not</returns>
        public bool IsProperSubsetOf(T[] other) => IsProperSubsetOf(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Checks if this is a proper subset of other (i.e. strictly contained in)
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it's a proper subset of a set that contains at least
        /// one element, but it's not a proper subset of the empty set.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a proper subset of other; false if not</returns>
        public bool IsProperSubsetOf(ReadOnlySpan<T> other)
        {
            // no set is a proper subset of an empty set
            if (other.Length == 0)
            {
                return false;
            }

            // the empty set is a proper subset of anything but the empty set
            if (_count == 0)
            {
                return other.Length > 0;
            }

            ElementCount result = CheckUniqueAndUnfoundElements(other, false);
            return (result.uniqueCount == _count && result.unfoundCount > 0);
        }

        /// <summary>
        /// Checks if this is a superset of other
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If other has no elements (it's the empty set), then this is a superset, even if this
        /// is also the empty set.
        /// 2. If other has unique elements according to this equality comparer, and this has less 
        /// than the number of elements in other, then this can't be a superset
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a superset of other; false if not</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // a set is always a superset of itself
            if (other == this)
            {
                return true;
            }

            // try to fall out early based on counts
            if (other is ICollection<T> otherAsCollection)
            {
                // if other is the empty set then this is a superset
                if (otherAsCollection.Count == 0)
                {
                    return true;
                }
                // try to compare based on counts alone if other is a hashset with
                // same equality comparer
                if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
                {
                    if (otherAsSet.Count > _count)
                    {
                        return false;
                    }
                }

                if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
                {
                    if (otherAsHs.Count > _count)
                    {
                        return false;
                    }
                }
            }

            return ContainsAllElements(other);
        }

        /// <summary>
        /// Checks if this is a superset of other
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If other has no elements (it's the empty set), then this is a superset, even if this
        /// is also the empty set.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a superset of other; false if not</returns>
        public bool IsSupersetOf(T[] other) => IsSupersetOf(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Checks if this is a superset of other
        /// </summary>
        /// <remarks>
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If other has no elements (it's the empty set), then this is a superset, even if this
        /// is also the empty set.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a superset of other; false if not</returns>
        public bool IsSupersetOf(ReadOnlySpan<T> other)
        {
            // if other is the empty set then this is a superset
            if (other.Length == 0)
            {
                return true;
            }

            return ContainsAllElements(other);
        }

        /// <summary>
        /// Checks if this is a proper superset of other (i.e. other strictly contained in this)
        /// </summary>
        /// <remarks>
        /// Implementation Notes: 
        /// This is slightly more complicated than IsSupersetOf because we have to keep track if there
        /// was at least one element not contained in other.
        /// 
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it can't be a proper superset of any set, even if 
        /// other is the empty set.
        /// 2. If other is an empty set and this contains at least 1 element, then this is a proper
        /// superset.
        /// 3. If other has unique elements according to this equality comparer, and other's count
        /// is greater than or equal to this count, then this can't be a proper superset
        /// 
        /// Furthermore, if other has unique elements according to this equality comparer, we can
        /// use a faster element-wise check.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a proper superset of other; false if not</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // the empty set isn't a proper superset of any set.
            if (_count == 0)
            {
                return false;
            }

            // a set is never a strict superset of itself
            if (other == this)
            {
                return false;
            }

            if (other is ICollection<T> otherAsCollection)
            {
                // if other is the empty set then this is a superset
                if (otherAsCollection.Count == 0)
                {
                    // note that this has at least one element, based on above check
                    return true;
                }
                // faster if other is a hashset with the same equality comparer
                if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
                {
                    if (otherAsSet.Count >= _count)
                    {
                        return false;
                    }
                    // now perform element check
                    return ContainsAllElements(otherAsSet);
                }

                if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
                {
                    if (otherAsHs.Count >= _count)
                    {
                        return false;
                    }
                    // now perform element check
                    return ContainsAllElements(otherAsHs);
                }
            }
            // couldn't fall out in the above cases; do it the long way
            ElementCount result = CheckUniqueAndUnfoundElements(other, true);
            return (result.uniqueCount < _count && result.unfoundCount == 0);
        }

        /// <summary>
        /// Checks if this is a proper superset of other (i.e. other strictly contained in this)
        /// </summary>
        /// <remarks>
        /// Implementation Notes: 
        /// This is slightly more complicated than IsSupersetOf because we have to keep track if there
        /// was at least one element not contained in other.
        /// 
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it can't be a proper superset of any set, even if 
        /// other is the empty set.
        /// 2. If other is an empty set and this contains at least 1 element, then this is a proper
        /// superset.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a proper superset of other; false if not</returns>
        public bool IsProperSupersetOf(T[] other) => IsProperSupersetOf(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Checks if this is a proper superset of other (i.e. other strictly contained in this)
        /// </summary>
        /// <remarks>
        /// Implementation Notes: 
        /// This is slightly more complicated than IsSupersetOf because we have to keep track if there
        /// was at least one element not contained in other.
        /// 
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it can't be a proper superset of any set, even if 
        /// other is the empty set.
        /// 2. If other is an empty set and this contains at least 1 element, then this is a proper
        /// superset.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns>true if this is a proper superset of other; false if not</returns>
        public bool IsProperSupersetOf(ReadOnlySpan<T> other)
        {
            // the empty set isn't a proper superset of any set.
            if (_count == 0)
            {
                return false;
            }

            if (other.Length == 0)
            {
                // note that this has at least one element, based on above check
                return true;
            }

            // couldn't fall out in the above cases; do it the long way
            ElementCount result = CheckUniqueAndUnfoundElements(other, true);
            return (result.uniqueCount < _count && result.unfoundCount == 0);
        }

        /// <summary>
        /// Checks if this set overlaps other (i.e. they share at least one item)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if these have at least one common element; false if disjoint</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            if (_count == 0)
            {
                return false;
            }

            // set overlaps itself
            if (other == this)
            {
                return true;
            }

            foreach (T element in other)
            {
                if (Contains(element))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if this set overlaps other (i.e. they share at least one item)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if these have at least one common element; false if disjoint</returns>
        public bool Overlaps(T[] other) => Overlaps(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Checks if this set overlaps other (i.e. they share at least one item)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if these have at least one common element; false if disjoint</returns>
        public bool Overlaps(ReadOnlySpan<T> other)
        {
            if (_count == 0)
            {
                return false;
            }

            for (int i = 0, len = other.Length; i < len; i++)
            {
                if (Contains(other[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if this and other contain the same elements. This is set equality: 
        /// duplicates and order are ignored
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
            }

            // a set is equal to itself
            if (other == this)
            {
                return true;
            }

            // faster if other is a hashset and we're using same equality comparer
            if (other is PooledSet<T> otherAsSet && AreEqualityComparersEqual(this, otherAsSet))
            {
                // attempt to return early: since both contain unique elements, if they have 
                // different counts, then they can't be equal
                if (_count != otherAsSet.Count)
                {
                    return false;
                }

                // already confirmed that the sets have the same number of distinct elements, so if
                // one is a superset of the other then they must be equal
                return ContainsAllElements(otherAsSet);
            }
            if (other is HashSet<T> otherAsHs && AreEqualityComparersEqual(this, otherAsHs))
            {
                // attempt to return early: since both contain unique elements, if they have 
                // different counts, then they can't be equal
                if (_count != otherAsHs.Count)
                {
                    return false;
                }

                // already confirmed that the sets have the same number of distinct elements, so if
                // one is a superset of the other then they must be equal
                return ContainsAllElements(otherAsHs);
            }
            else
            {
                if (other is ICollection<T> otherAsCollection)
                {
                    // if this count is 0 but other contains at least one element, they can't be equal
                    if (_count == 0 && otherAsCollection.Count > 0)
                    {
                        return false;
                    }
                }
                ElementCount result = CheckUniqueAndUnfoundElements(other, true);
                return (result.uniqueCount == _count && result.unfoundCount == 0);
            }
        }

        /// <summary>
        /// Checks if this and other contain the same elements. This is set equality: 
        /// duplicates and order are ignored
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool SetEquals(T[] other) => SetEquals(new ReadOnlySpan<T>(other));

        /// <summary>
        /// Checks if this and other contain the same elements. This is set equality: 
        /// duplicates and order are ignored
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool SetEquals(ReadOnlySpan<T> other)
        {
            // if this count is 0 but other contains at least one element, they can't be equal
            if (_count == 0 && other.Length > 0)
            {
                return false;
            }

            ElementCount result = CheckUniqueAndUnfoundElements(other, true);
            return (result.uniqueCount == _count && result.unfoundCount == 0);
        }

        /// <summary>
        /// Copies the set to the given array.
        /// </summary>
        public void CopyTo(T[] array) => CopyTo(array, 0, _count);

        /// <summary>
        /// Copies <paramref name="count"/> items of the set to the given array, starting 
        /// at <paramref name="arrayIndex"/> in the destination array.
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            // check array index valid index into array
            if (arrayIndex < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            // also throw if count less than 0
            if (count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            // will array, starting at arrayIndex, be able to hold elements? Note: not
            // checking arrayIndex >= array.Length (consistency with list of allowing
            // count of 0; subsequent check takes care of the rest)
            if (arrayIndex > array.Length || count > array.Length - arrayIndex)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            int numCopied = 0;
            for (int i = 0; i < _lastIndex && numCopied < count; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    array[arrayIndex + numCopied] = _slots[i].value;
                    numCopied++;
                }
            }
        }

        /// <summary>
        /// Copies the set to the given span.
        /// </summary>
        public void CopyTo(Span<T> span) => CopyTo(span, _count);

        /// <summary>
        /// Copies <paramref name="count"/> items from the set to the given span.
        /// </summary>
        public void CopyTo(Span<T> span, int count)
        {
            if (span.Length < _count || span.Length < count)
            {
                ThrowHelper.ThrowArgumentException_DestinationTooShort();
            }

            int numCopied = 0;
            for (int i = 0; i < _lastIndex && numCopied < count; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    span[numCopied] = _slots[i].value;
                    numCopied++;
                }
            }
        }

        /// <summary>
        /// Remove elements that match specified predicate. Returns the number of elements removed
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int RemoveWhere(Func<T, bool> match)
        {
            if (match == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
            }

            int numRemoved = 0;
            for (int i = 0; i < _lastIndex; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    // cache value in case delegate removes it
                    T value = _slots[i].value;
                    if (match(value))
                    {
                        // check again that remove actually removed it
                        if (Remove(value))
                        {
                            numRemoved++;
                        }
                    }
                }
            }
            return numRemoved;
        }

        /// <summary>
        /// Gets the IEqualityComparer that is used to determine equality of keys for 
        /// the HashSet.
        /// </summary>
        public IEqualityComparer<T> Comparer => _comparer;

        /// <summary>
        /// Ensures that the hash set can hold up to 'capacity' entries without any further expansion of its backing storage.
        /// </summary>
        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            int currentCapacity = _slots == null ? 0 : _size;
            if (currentCapacity >= capacity)
                return currentCapacity;
            if (_buckets == null)
                return Initialize(capacity);

            int newSize = HashHelpers.GetPrime(capacity);
            SetCapacity(newSize);
            return newSize;
        }

        /// <summary>
        /// Sets the capacity of this list to the size of the list (rounded up to nearest prime),
        /// unless count is 0, in which case we release references.
        /// 
        /// This method can be used to minimize a list's memory overhead once it is known that no
        /// new elements will be added to the list. To completely clear a list and release all 
        /// memory referenced by the list, execute the following statements:
        /// 
        /// list.Clear();
        /// list.TrimExcess(); 
        /// </summary>
        public void TrimExcess()
        {
            Debug.Assert(_count >= 0, "_count is negative");

            if (_count == 0)
            {
                // if count is zero, clear references
                ReturnArrays();
                _version++;
            }
            else
            {
                Debug.Assert(_buckets != null, "_buckets was null but _count > 0");

                // similar to IncreaseCapacity but moves down elements in case add/remove/etc
                // caused fragmentation
                int newSize = HashHelpers.GetPrime(_count);
                Slot[] newSlots = s_slotPool.Rent(newSize);
                int[] newBuckets = s_bucketPool.Rent(newSize);

                if (newSlots.Length >= _slots.Length || newBuckets.Length >= _buckets.Length)
                {
                    // ArrayPool treats "newSize" as a minimum - if it gives us arrays that are as-big or bigger
                    // that what we already have, we're not really trimming any excess and may as well quit.
                    // We can't manually create exact-sized arrays unless we track that we did and avoid returning
                    // them to the ArrayPool, as that would throw an exception.
                    s_slotPool.Return(newSlots);
                    s_bucketPool.Return(newBuckets);
                    return;
                }

                Array.Clear(newBuckets, 0, newBuckets.Length);

                // move down slots and rehash at the same time. newIndex keeps track of current 
                // position in newSlots array
                int newIndex = 0;
                for (int i = 0; i < _lastIndex; i++)
                {
                    if (_slots[i].hashCode >= 0)
                    {
                        newSlots[newIndex] = _slots[i];

                        // rehash
                        int bucket = newSlots[newIndex].hashCode % newSize;
                        newSlots[newIndex].next = newBuckets[bucket] - 1;
                        newBuckets[bucket] = newIndex + 1;

                        newIndex++;
                    }
                }

                Debug.Assert(newSize <= _size, "capacity increased after TrimExcess");

                _lastIndex = newIndex;
                ReturnArrays();
                _slots = newSlots;
                _buckets = newBuckets;
                _size = newSize;
                _freeList = -1;
                _version++;
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Used for deep equality of HashSet testing
        /// </summary>
        public static IEqualityComparer<PooledSet<T>> CreateSetComparer()
            => new PooledSetEqualityComparer<T>();

        /// <summary>
        /// Initializes buckets and slots arrays. Uses suggested capacity by finding next prime
        /// greater than or equal to capacity.
        /// </summary>
        /// <param name="capacity"></param>
        private int Initialize(int capacity)
        {
            Debug.Assert(_buckets == null, "Initialize was called but _buckets was non-null");

            _size = HashHelpers.GetPrime(capacity);
            _buckets = s_bucketPool.Rent(_size);
            Array.Clear(_buckets, 0, _buckets.Length);
            _slots = s_slotPool.Rent(_size);

            return _size;
        }

        /// <summary>
        /// Expand to new capacity. New capacity is next prime greater than or equal to suggested 
        /// size. This is called when the underlying array is filled. This performs no 
        /// defragmentation, allowing faster execution; note that this is reasonable since 
        /// AddIfNotPresent attempts to insert new elements in re-opened spots.
        /// </summary>
        private void IncreaseCapacity()
        {
            Debug.Assert(_buckets != null, "IncreaseCapacity called on a set with no elements");

            int newSize = HashHelpers.ExpandPrime(_count);
            if (newSize <= _count)
            {
                ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_HSCapacityOverflow);
            }

            // Able to increase capacity; copy elements to larger array and rehash
            SetCapacity(newSize);
        }

        /// <summary>
        /// Set the underlying buckets array to size newSize and rehash.  Note that newSize
        /// *must* be a prime.  It is very likely that you want to call IncreaseCapacity()
        /// instead of this method.
        /// </summary>
        private void SetCapacity(int newSize)
        {
            Debug.Assert(HashHelpers.IsPrime(newSize), "New size is not prime!");
            Debug.Assert(_buckets != null, "SetCapacity called on a set with no elements");

            int[] newBuckets;
            Slot[] newSlots;
            bool replaceArrays;

            // Because ArrayPool might have given us larger arrays than we asked for, see if we can 
            // use the existing capacity without actually resizing.
            if (_buckets?.Length >= newSize && _slots?.Length >= newSize)
            {
                Array.Clear(_buckets, 0, _buckets.Length);
                Array.Clear(_slots, _size, newSize - _size);
                newBuckets = _buckets;
                newSlots = _slots;
                replaceArrays = false;
            }
            else
            {
                newSlots = s_slotPool.Rent(newSize);
                newBuckets = s_bucketPool.Rent(newSize);

                Array.Clear(newBuckets, 0, newBuckets.Length);
                if (_slots != null)
                {
                    Array.Copy(_slots, 0, newSlots, 0, _lastIndex);
                }
                replaceArrays = true;
            }

            for (int i = 0; i < _lastIndex; i++)
            {
                int bucket = newSlots[i].hashCode % newSize;
                newSlots[i].next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }

            if (replaceArrays)
            {
                ReturnArrays();
                _slots = newSlots;
                _buckets = newBuckets;
            }

            _size = newSize;
        }

        private void ReturnArrays()
        {
            if (_slots?.Length > 0)
            {
#if NETCOREAPP2_1
                s_slotPool.Return(_slots, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
#else
                s_slotPool.Return(_slots, clearArray: true);
#endif
            }

            if (_buckets?.Length > 0)
            {
                s_bucketPool.Return(_buckets);
            }

            _slots = null;
            _buckets = null;
        }

        /// <summary>
        /// Adds value to HashSet if not contained already
        /// Returns true if added and false if already present
        /// </summary>
        /// <param name="value">value to find</param>
        /// <returns></returns>
        private bool AddIfNotPresent(T value)
        {
            if (_buckets == null)
            {
                Initialize(0);
            }

            int hashCode = InternalGetHashCode(value);
            int bucket = hashCode % _size;
            int collisionCount = 0;
            Slot[] slots = _slots;
            for (int i = _buckets[bucket] - 1; i >= 0; i = slots[i].next)
            {
                if (slots[i].hashCode == hashCode && _comparer.Equals(slots[i].value, value))
                {
                    return false;
                }

                if (collisionCount >= _size)
                {
                    // The chain of entries forms a loop, which means a concurrent update has happened.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }

            int index;
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = slots[index].next;
            }
            else
            {
                if (_lastIndex == _size)
                {
                    IncreaseCapacity();
                    // this will change during resize
                    slots = _slots;
                    bucket = hashCode % _size;
                }
                index = _lastIndex;
                _lastIndex++;
            }
            slots[index].hashCode = hashCode;
            slots[index].value = value;
            slots[index].next = _buckets[bucket] - 1;
            _buckets[bucket] = index + 1;
            _count++;
            _version++;

            return true;
        }

        // Add value at known index with known hash code. Used only
        // when constructing from another HashSet.
        private void AddValue(int index, int hashCode, T value)
        {
            int bucket = hashCode % _size;

#if DEBUG
            Debug.Assert(InternalGetHashCode(value) == hashCode);
            for (int i = _buckets[bucket] - 1; i >= 0; i = _slots[i].next)
            {
                Debug.Assert(!_comparer.Equals(_slots[i].value, value));
            }
#endif

            Debug.Assert(_freeList == -1);
            _slots[index].hashCode = hashCode;
            _slots[index].value = value;
            _slots[index].next = _buckets[bucket] - 1;
            _buckets[bucket] = index + 1;
        }

        /// <summary>
        /// Checks if this contains of other's elements. Iterates over other's elements and 
        /// returns false as soon as it finds an element in other that's not in this.
        /// Used by SupersetOf, ProperSupersetOf, and SetEquals.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool ContainsAllElements(IEnumerable<T> other)
        {
            foreach (T element in other)
            {
                if (!Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if this contains of other's elements. Iterates over other's elements and 
        /// returns false as soon as it finds an element in other that's not in this.
        /// Used by SupersetOf, ProperSupersetOf, and SetEquals.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool ContainsAllElements(ReadOnlySpan<T> other)
        {
            foreach (T element in other)
            {
                if (!Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Implementation Notes:
        /// If other is a hashset and is using same equality comparer, then checking subset is 
        /// faster. Simply check that each element in this is in other.
        /// 
        /// Note: if other doesn't use same equality comparer, then Contains check is invalid,
        /// which is why callers must take are of this.
        /// 
        /// If callers are concerned about whether this is a proper subset, they take care of that.
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool IsSubsetOfHashSetWithSameEC(PooledSet<T> other)
        {
            foreach (T item in this)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Implementation Notes:
        /// If other is a hashset and is using same equality comparer, then checking subset is 
        /// faster. Simply check that each element in this is in other.
        /// 
        /// Note: if other doesn't use same equality comparer, then Contains check is invalid,
        /// which is why callers must take are of this.
        /// 
        /// If callers are concerned about whether this is a proper subset, they take care of that.
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool IsSubsetOfHashSetWithSameEC(HashSet<T> other)
        {
            foreach (T item in this)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// If other is a hashset that uses same equality comparer, intersect is much faster 
        /// because we can use other's Contains
        /// </summary>
        /// <param name="other"></param>
        private void IntersectWithHashSetWithSameEC(PooledSet<T> other)
        {
            for (int i = 0; i < _lastIndex; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    T item = _slots[i].value;
                    if (!other.Contains(item))
                    {
                        Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// If other is a hashset that uses same equality comparer, intersect is much faster 
        /// because we can use other's Contains
        /// </summary>
        /// <param name="other"></param>
        private void IntersectWithHashSetWithSameEC(HashSet<T> other)
        {
            for (int i = 0; i < _lastIndex; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    T item = _slots[i].value;
                    if (!other.Contains(item))
                    {
                        Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// Iterate over other. If contained in this, mark an element in bit array corresponding to
        /// its position in _slots. If anything is unmarked (in bit array), remove it.
        /// 
        /// This attempts to allocate on the stack, if below StackAllocThreshold.
        /// </summary>
        /// <param name="other"></param>
        private unsafe void IntersectWithEnumerable(IEnumerable<T> other)
        {
            Debug.Assert(_buckets != null, "_buckets shouldn't be null; callers should check first");

            // keep track of current last index; don't want to move past the end of our bit array
            // (could happen if another thread is modifying the collection)
            int originalLastIndex = _lastIndex;
            int intArrayLength = BitHelper.ToIntArrayLength(originalLastIndex);

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = intArrayLength <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            // mark if contains: find index of in slots array and mark corresponding element in bit array
            foreach (T item in other)
            {
                int index = InternalIndexOf(item);
                if (index >= 0)
                {
                    bitHelper.MarkBit(index);
                }
            }

            // if anything unmarked, remove it. Perf can be optimized here if BitHelper had a 
            // FindFirstUnmarked method.
            for (int i = 0; i < originalLastIndex; i++)
            {
                if (_slots[i].hashCode >= 0 && !bitHelper.IsMarked(i))
                {
                    Remove(_slots[i].value);
                }
            }
        }

        /// <summary>
        /// Iterate over other. If contained in this, mark an element in bit array corresponding to
        /// its position in _slots. If anything is unmarked (in bit array), remove it.
        /// 
        /// This attempts to allocate on the stack, if below StackAllocThreshold.
        /// </summary>
        /// <param name="other"></param>
        private unsafe void IntersectWithSpan(ReadOnlySpan<T> other)
        {
            Debug.Assert(_buckets != null, "_buckets shouldn't be null; callers should check first");

            // keep track of current last index; don't want to move past the end of our bit array
            // (could happen if another thread is modifying the collection)
            int originalLastIndex = _lastIndex;
            int intArrayLength = BitHelper.ToIntArrayLength(originalLastIndex);

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = intArrayLength <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            // mark if contains: find index of in slots array and mark corresponding element in bit array
            for (int i = 0, len = other.Length; i < len; i++)
            {
                int index = InternalIndexOf(other[i]);
                if (index >= 0)
                {
                    bitHelper.MarkBit(index);
                }
            }

            // if anything unmarked, remove it. Perf can be optimized here if BitHelper had a 
            // FindFirstUnmarked method.
            for (int i = 0; i < originalLastIndex; i++)
            {
                if (_slots[i].hashCode >= 0 && !bitHelper.IsMarked(i))
                {
                    Remove(_slots[i].value);
                }
            }
        }

        /// <summary>
        /// Used internally by set operations which have to rely on bit array marking. This is like
        /// Contains but returns index in slots array. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int InternalIndexOf(T item)
        {
            Debug.Assert(_buckets != null, "_buckets was null; callers should check first");

            int collisionCount = 0;
            int hashCode = InternalGetHashCode(item);
            Slot[] slots = _slots;
            for (int i = _buckets[hashCode % _size] - 1; i >= 0; i = slots[i].next)
            {
                if ((slots[i].hashCode) == hashCode && _comparer.Equals(slots[i].value, item))
                {
                    return i;
                }

                if (collisionCount >= _size)
                {
                    // The chain of entries forms a loop, which means a concurrent update has happened.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }
            // wasn't found
            return -1;
        }

        /// <summary>
        /// if other is a set, we can assume it doesn't have duplicate elements, so use this
        /// technique: if can't remove, then it wasn't present in this set, so add.
        /// 
        /// As with other methods, callers take care of ensuring that other is a hashset using the
        /// same equality comparer.
        /// </summary>
        /// <param name="other"></param>
        private void SymmetricExceptWithUniqueHashSet(PooledSet<T> other)
        {
            foreach (T item in other)
            {
                if (!Remove(item))
                {
                    AddIfNotPresent(item);
                }
            }
        }

        /// <summary>
        /// if other is a set, we can assume it doesn't have duplicate elements, so use this
        /// technique: if can't remove, then it wasn't present in this set, so add.
        /// 
        /// As with other methods, callers take care of ensuring that other is a hashset using the
        /// same equality comparer.
        /// </summary>
        /// <param name="other"></param>
        private void SymmetricExceptWithUniqueHashSet(HashSet<T> other)
        {
            foreach (T item in other)
            {
                if (!Remove(item))
                {
                    AddIfNotPresent(item);
                }
            }
        }

        /// <summary>
        /// Implementation notes:
        /// 
        /// Used for symmetric except when other isn't a HashSet. This is more tedious because 
        /// other may contain duplicates. HashSet technique could fail in these situations:
        /// 1. Other has a duplicate that's not in this: HashSet technique would add then 
        /// remove it.
        /// 2. Other has a duplicate that's in this: HashSet technique would remove then add it
        /// back.
        /// In general, its presence would be toggled each time it appears in other. 
        /// 
        /// This technique uses bit marking to indicate whether to add/remove the item. If already
        /// present in collection, it will get marked for deletion. If added from other, it will
        /// get marked as something not to remove.
        ///
        /// </summary>
        /// <param name="other"></param>
        private unsafe void SymmetricExceptWithEnumerable(IEnumerable<T> other)
        {
            int originalLastIndex = _lastIndex;
            int intArrayLength = BitHelper.ToIntArrayLength(originalLastIndex);

            Span<int> itemsToRemoveSpan = stackalloc int[StackAllocThreshold / 2];
            BitHelper itemsToRemove = intArrayLength <= StackAllocThreshold / 2
                ? new BitHelper(itemsToRemoveSpan.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            Span<int> itemsAddedFromOtherSpan = stackalloc int[StackAllocThreshold / 2];
            BitHelper itemsAddedFromOther = intArrayLength <= StackAllocThreshold / 2
                ? new BitHelper(itemsAddedFromOtherSpan.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            foreach (T item in other)
            {
                bool added = AddOrGetLocation(item, out int location);
                if (added)
                {
                    // wasn't already present in collection; flag it as something not to remove
                    // *NOTE* if location is out of range, we should ignore. BitHelper will
                    // detect that it's out of bounds and not try to mark it. But it's 
                    // expected that location could be out of bounds because adding the item
                    // will increase _lastIndex as soon as all the free spots are filled.
                    itemsAddedFromOther.MarkBit(location);
                }
                else
                {
                    // already there...if not added from other, mark for remove. 
                    // *NOTE* Even though BitHelper will check that location is in range, we want 
                    // to check here. There's no point in checking items beyond originalLastIndex
                    // because they could not have been in the original collection
                    if (location < originalLastIndex && !itemsAddedFromOther.IsMarked(location))
                    {
                        itemsToRemove.MarkBit(location);
                    }
                }
            }

            // if anything marked, remove it
            for (int i = 0; i < originalLastIndex; i++)
            {
                if (itemsToRemove.IsMarked(i))
                {
                    Remove(_slots[i].value);
                }
            }
        }

        /// <summary>
        /// Implementation notes:
        /// 
        /// Used for symmetric except when other isn't a HashSet. This is more tedious because 
        /// other may contain duplicates. HashSet technique could fail in these situations:
        /// 1. Other has a duplicate that's not in this: HashSet technique would add then 
        /// remove it.
        /// 2. Other has a duplicate that's in this: HashSet technique would remove then add it
        /// back.
        /// In general, its presence would be toggled each time it appears in other. 
        /// 
        /// This technique uses bit marking to indicate whether to add/remove the item. If already
        /// present in collection, it will get marked for deletion. If added from other, it will
        /// get marked as something not to remove.
        ///
        /// </summary>
        /// <param name="other"></param>
        private unsafe void SymmetricExceptWithSpan(ReadOnlySpan<T> other)
        {
            int originalLastIndex = _lastIndex;
            int intArrayLength = BitHelper.ToIntArrayLength(originalLastIndex);

            Span<int> itemsToRemoveSpan = stackalloc int[StackAllocThreshold / 2];
            BitHelper itemsToRemove = intArrayLength <= StackAllocThreshold / 2
                ? new BitHelper(itemsToRemoveSpan.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            Span<int> itemsAddedFromOtherSpan = stackalloc int[StackAllocThreshold / 2];
            BitHelper itemsAddedFromOther = intArrayLength <= StackAllocThreshold / 2
                ? new BitHelper(itemsAddedFromOtherSpan.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            for (int i = 0, len = other.Length; i < len; i++)
            {
                bool added = AddOrGetLocation(other[i], out int location);
                if (added)
                {
                    // wasn't already present in collection; flag it as something not to remove
                    // *NOTE* if location is out of range, we should ignore. BitHelper will
                    // detect that it's out of bounds and not try to mark it. But it's 
                    // expected that location could be out of bounds because adding the item
                    // will increase _lastIndex as soon as all the free spots are filled.
                    itemsAddedFromOther.MarkBit(location);
                }
                else
                {
                    // already there...if not added from other, mark for remove. 
                    // *NOTE* Even though BitHelper will check that location is in range, we want 
                    // to check here. There's no point in checking items beyond originalLastIndex
                    // because they could not have been in the original collection
                    if (location < originalLastIndex && !itemsAddedFromOther.IsMarked(location))
                    {
                        itemsToRemove.MarkBit(location);
                    }
                }
            }

            // if anything marked, remove it
            for (int i = 0; i < originalLastIndex; i++)
            {
                if (itemsToRemove.IsMarked(i))
                {
                    Remove(_slots[i].value);
                }
            }
        }

        /// <summary>
        /// Add if not already in hashset. Returns an out param indicating index where added. This 
        /// is used by SymmetricExcept because it needs to know the following things:
        /// - whether the item was already present in the collection or added from other
        /// - where it's located (if already present, it will get marked for removal, otherwise
        /// marked for keeping)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool AddOrGetLocation(T value, out int location)
        {
            Debug.Assert(_buckets != null, "_buckets is null, callers should have checked");

            int hashCode = InternalGetHashCode(value);
            int bucket = hashCode % _size;
            int collisionCount = 0;
            Slot[] slots = _slots;
            for (int i = _buckets[bucket] - 1; i >= 0; i = slots[i].next)
            {
                if (slots[i].hashCode == hashCode && _comparer.Equals(slots[i].value, value))
                {
                    location = i;
                    return false; //already present
                }

                if (collisionCount >= _size)
                {
                    // The chain of entries forms a loop, which means a concurrent update has happened.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }
            int index;
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = slots[index].next;
            }
            else
            {
                if (_lastIndex == _size)
                {
                    IncreaseCapacity();
                    // this will change during resize
                    slots = _slots;
                    bucket = hashCode % _size;
                }
                index = _lastIndex;
                _lastIndex++;
            }
            slots[index].hashCode = hashCode;
            slots[index].value = value;
            slots[index].next = _buckets[bucket] - 1;
            _buckets[bucket] = index + 1;
            _count++;
            _version++;
            location = index;
            return true;
        }

        /// <summary>
        /// Determines counts that can be used to determine equality, subset, and superset. This
        /// is only used when other is an IEnumerable and not a HashSet. If other is a HashSet
        /// these properties can be checked faster without use of marking because we can assume 
        /// other has no duplicates.
        /// 
        /// The following count checks are performed by callers:
        /// 1. Equals: checks if unfoundCount = 0 and uniqueFoundCount = _count; i.e. everything 
        /// in other is in this and everything in this is in other
        /// 2. Subset: checks if unfoundCount >= 0 and uniqueFoundCount = _count; i.e. other may
        /// have elements not in this and everything in this is in other
        /// 3. Proper subset: checks if unfoundCount > 0 and uniqueFoundCount = _count; i.e
        /// other must have at least one element not in this and everything in this is in other
        /// 4. Proper superset: checks if unfound count = 0 and uniqueFoundCount strictly less
        /// than _count; i.e. everything in other was in this and this had at least one element
        /// not contained in other.
        /// 
        /// An earlier implementation used delegates to perform these checks rather than returning
        /// an ElementCount struct; however this was changed due to the perf overhead of delegates.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="returnIfUnfound">Allows us to finish faster for equals and proper superset
        /// because unfoundCount must be 0.</param>
        /// <returns></returns>
        private unsafe ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
        {
            ElementCount result;

            // need special case in case this has no elements. 
            if (_count == 0)
            {
                int numElementsInOther = 0;
                foreach (T item in other)
                {
                    numElementsInOther++;
                    // break right away, all we want to know is whether other has 0 or 1 elements
                    break;
                }
                result.uniqueCount = 0;
                result.unfoundCount = numElementsInOther;
                return result;
            }

            Debug.Assert((_buckets != null) && (_count > 0), "_buckets was null but count greater than 0");

            int originalLastIndex = _lastIndex;
            int intArrayLength = BitHelper.ToIntArrayLength(originalLastIndex);

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = intArrayLength <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            // count of items in other not found in this
            int unfoundCount = 0;
            // count of unique items in other found in this
            int uniqueFoundCount = 0;

            foreach (T item in other)
            {
                int index = InternalIndexOf(item);
                if (index >= 0)
                {
                    if (!bitHelper.IsMarked(index))
                    {
                        // item hasn't been seen yet
                        bitHelper.MarkBit(index);
                        uniqueFoundCount++;
                    }
                }
                else
                {
                    unfoundCount++;
                    if (returnIfUnfound)
                    {
                        break;
                    }
                }
            }

            result.uniqueCount = uniqueFoundCount;
            result.unfoundCount = unfoundCount;
            return result;
        }

        /// <summary>
        /// Determines counts that can be used to determine equality, subset, and superset. This
        /// is only used when other is an IEnumerable and not a HashSet. If other is a HashSet
        /// these properties can be checked faster without use of marking because we can assume 
        /// other has no duplicates.
        /// 
        /// The following count checks are performed by callers:
        /// 1. Equals: checks if unfoundCount = 0 and uniqueFoundCount = _count; i.e. everything 
        /// in other is in this and everything in this is in other
        /// 2. Subset: checks if unfoundCount >= 0 and uniqueFoundCount = _count; i.e. other may
        /// have elements not in this and everything in this is in other
        /// 3. Proper subset: checks if unfoundCount > 0 and uniqueFoundCount = _count; i.e
        /// other must have at least one element not in this and everything in this is in other
        /// 4. Proper superset: checks if unfound count = 0 and uniqueFoundCount strictly less
        /// than _count; i.e. everything in other was in this and this had at least one element
        /// not contained in other.
        /// 
        /// An earlier implementation used delegates to perform these checks rather than returning
        /// an ElementCount struct; however this was changed due to the perf overhead of delegates.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="returnIfUnfound">Allows us to finish faster for equals and proper superset
        /// because unfoundCount must be 0.</param>
        /// <returns></returns>
        private unsafe ElementCount CheckUniqueAndUnfoundElements(ReadOnlySpan<T> other, bool returnIfUnfound)
        {
            ElementCount result;

            // need special case in case this has no elements. 
            if (_count == 0)
            {
                result.uniqueCount = 0;
                result.unfoundCount = other.Length;
                return result;
            }

            Debug.Assert((_buckets != null) && (_count > 0), "_buckets was null but count greater than 0");

            int originalLastIndex = _lastIndex;
            int intArrayLength = BitHelper.ToIntArrayLength(originalLastIndex);

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = intArrayLength <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, intArrayLength), clear: true)
                : new BitHelper(new int[intArrayLength], clear: false);

            // count of items in other not found in this
            int unfoundCount = 0;
            // count of unique items in other found in this
            int uniqueFoundCount = 0;

            for (int i = 0, len = other.Length; i < len; i++)
            {
                int index = InternalIndexOf(other[i]);
                if (index >= 0)
                {
                    if (!bitHelper.IsMarked(index))
                    {
                        // item hasn't been seen yet
                        bitHelper.MarkBit(index);
                        uniqueFoundCount++;
                    }
                }
                else
                {
                    unfoundCount++;
                    if (returnIfUnfound)
                    {
                        break;
                    }
                }
            }

            result.uniqueCount = uniqueFoundCount;
            result.unfoundCount = unfoundCount;
            return result;
        }

        /// <summary>
        /// Internal method used for HashSetEqualityComparer. Compares set1 and set2 according 
        /// to specified comparer.
        /// 
        /// Because items are hashed according to a specific equality comparer, we have to resort
        /// to n^2 search if they're using different equality comparers.
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        internal static bool PooledSetEquals(PooledSet<T> set1, PooledSet<T> set2, IEqualityComparer<T> comparer)
        {
            // handle null cases first
            if (set1 == null)
            {
                return (set2 == null);
            }
            else if (set2 == null)
            {
                // set1 != null
                return false;
            }

            // all comparers are the same; this is faster
            if (AreEqualityComparersEqual(set1, set2))
            {
                if (set1.Count != set2.Count)
                {
                    return false;
                }
                // suffices to check subset
                foreach (T item in set2)
                {
                    if (!set1.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {  // n^2 search because items are hashed according to their respective ECs
                foreach (T set2Item in set2)
                {
                    bool found = false;
                    foreach (T set1Item in set1)
                    {
                        if (comparer.Equals(set2Item, set1Item))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Checks if equality comparers are equal. This is used for algorithms that can
        /// speed up if it knows the other item has unique elements. I.e. if they're using 
        /// different equality comparers, then uniqueness assumption between sets break.
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        private static bool AreEqualityComparersEqual(PooledSet<T> set1, PooledSet<T> set2)
        {
            return set1.Comparer.Equals(set2.Comparer);
        }

        /// <summary>
        /// Checks if equality comparers are equal. This is used for algorithms that can
        /// speed up if it knows the other item has unique elements. I.e. if they're using 
        /// different equality comparers, then uniqueness assumption between sets break.
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        private static bool AreEqualityComparersEqual(PooledSet<T> set1, HashSet<T> set2)
        {
            return set1.Comparer.Equals(set2.Comparer);
        }

        /// <summary>
        /// Workaround Comparers that throw ArgumentNullException for GetHashCode(null).
        /// </summary>
        /// <param name="item"></param>
        /// <returns>hash code</returns>
        private int InternalGetHashCode(T item)
        {
            if (item == null)
            {
                return 0;
            }
            return _comparer.GetHashCode(item) & Lower31BitMask;
        }

        /// <summary>
        /// Clears all values and returns internal arrays to the ArrayPool.
        /// </summary>
        public void Dispose()
        {
            ReturnArrays();
            _size = 0;
            _lastIndex = 0;
            _count = 0;
            _freeList = -1;
            _version++;
        }

        #endregion

        // used for set checking operations (using enumerables) that rely on counting
        internal struct ElementCount
        {
            internal int uniqueCount;
            internal int unfoundCount;
        }

        internal struct Slot
        {
            internal int hashCode;      // Lower 31 bits of hash code, -1 if unused
            internal int next;          // Index of next entry, -1 if last
            internal T value;
        }

        /// <summary>
        /// Enumerates the PooledSet.
        /// </summary>
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly PooledSet<T> _set;
            private int _index;
            private readonly int _version;
            private T _current;

            internal Enumerator(PooledSet<T> set)
            {
                _set = set;
                _index = 0;
                _version = set._version;
                _current = default;
            }

            void IDisposable.Dispose()
            {
            }

            /// <summary>
            /// Moves to the next item in the set.
            /// </summary>
            public bool MoveNext()
            {
                if (_version != _set._version)
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                while (_index < _set._lastIndex)
                {
                    if (_set._slots[_index].hashCode >= 0)
                    {
                        _current = _set._slots[_index].value;
                        _index++;
                        return true;
                    }
                    _index++;
                }
                _index = _set._lastIndex + 1;
                _current = default;
                return false;
            }

            /// <summary>
            /// Gets the current element in the set.
            /// </summary>
            public T Current => _current;

            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || _index == _set._lastIndex + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }
                    return Current;
                }
            }

            void IEnumerator.Reset()
            {
                if (_version != _set._version)
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                _index = 0;
                _current = default;
            }
        }
    }
}
