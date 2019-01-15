// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace Collections.Pooled
{
    /// <summary>
    /// Used internally to control behavior of insertion into a <see cref="PooledDictionary{TKey, TValue}"/>.
    /// </summary>
    internal enum InsertionBehavior : byte
    {
        /// <summary>
        /// The default insertion behavior.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that an existing entry with the same key should be overwritten if encountered.
        /// </summary>
        OverwriteExisting = 1,

        /// <summary>
        /// Specifies that if an existing entry with the same key is encountered, an exception should be thrown.
        /// </summary>
        ThrowOnExisting = 2
    }

    /// <remarks>
    /// A PooledDictionary<TKey,TValue> can support multiple readers concurrently, as long as the collection is not modified. 
    /// Even so, enumerating through a collection is intrinsically not a thread-safe procedure. 
    /// In the rare case where an enumeration contends with write accesses, the collection must be locked during the entire enumeration. 
    /// To allow the collection to be accessed by multiple threads for reading and writing, you must implement your own synchronization. 
    /// <remarks/>
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class PooledDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>,
        ISerializable, IDeserializationCallback, IDisposable
    {
        private struct Entry
        {
            public int hashCode;    // Lower 31 bits of hash code, -1 if unused
            public int next;        // Index of next entry, -1 if last
            public TKey key;        // Key of entry
            public TValue value;    // Value of entry
        }

        // constants for serialization
        private const string VersionName = "Version"; // Do not rename (binary serialization)
        private const string HashSizeName = "HashSize"; // Do not rename (binary serialization). Must save buckets.Length
        private const string KeyValuePairsName = "KeyValuePairs"; // Do not rename (binary serialization)
        private const string ComparerName = "Comparer"; // Do not rename (binary serialization)

        private static readonly ArrayPool<int> s_bucketPool = ArrayPool<int>.Shared;
        private static readonly ArrayPool<Entry> s_entryPool = ArrayPool<Entry>.Shared;

        // DO NOT USE THE FOLLOWING ARRAYS DIRECTLY:
        // It's important that the number of buckets be prime, and these arrays could exceed
        // that size as they come from ArrayPool. Use the private properties Buckets and Entries, which slice the
        // arrays down to the correct length.
        private int[] _buckets;
        private Entry[] _entries;

        private int _count;
        private int _size;
        private int _freeList;
        private int _freeCount;
        private int _version;
        private IEqualityComparer<TKey> _comparer;
        private KeyCollection _keys;
        private ValueCollection _values;
        private object _syncRoot;

        public PooledDictionary() : this(0, null) { }

        public PooledDictionary(int capacity) : this(capacity, null) { }

        public PooledDictionary(IEqualityComparer<TKey> comparer) : this(0, comparer) { }

        public PooledDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            if (capacity > 0) Initialize(capacity);
            if (comparer != EqualityComparer<TKey>.Default)
            {
                _comparer = comparer;
            }

            if (typeof(TKey) == typeof(string) && _comparer == null)
            {
                // To start, move off default comparer for string which is randomised
                _comparer = (IEqualityComparer<TKey>)NonRandomizedStringEqualityComparer.Default;
            }
        }

        public PooledDictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, null) { }

        public PooledDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) :
            this(dictionary?.Count ?? 0, comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            // It is likely that the passed-in dictionary is PooledDictionary<TKey,TValue>. When this is the case,
            // avoid the enumerator allocation and overhead by looping through the entries array directly.
            // We only do this when dictionary is PooledDictionary<TKey,TValue> and not a subclass, to maintain
            // back-compat with subclasses that may have overridden the enumerator behavior.
            if (dictionary is PooledDictionary<TKey, TValue> pooled)
            {
                int count = pooled._count;
                var entries = pooled._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0)
                    {
                        Add(entries[i].key, entries[i].value);
                    }
                }
                return;
            }

            foreach (var pair in dictionary)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public PooledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : this(collection, null) { }

        public PooledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) :
            this((collection as ICollection<KeyValuePair<TKey, TValue>>)?.Count ?? 0, comparer)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var pair in collection)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public PooledDictionary(IEnumerable<(TKey key, TValue value)> collection) : this(collection, null) { }

        public PooledDictionary(IEnumerable<(TKey key, TValue value)> collection, IEqualityComparer<TKey> comparer) :
            this((collection as ICollection<(TKey, TValue)>)?.Count ?? 0, comparer)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var (key, value) in collection)
            {
                Add(key, value);
            }
        }

        public PooledDictionary((TKey key, TValue value)[] array, IEqualityComparer<TKey> comparer) : this(array.AsSpan(), comparer) { }

        public PooledDictionary((TKey key, TValue value)[] array) : this(array.AsSpan()) { }

        public PooledDictionary(ReadOnlySpan<(TKey key, TValue value)> span) : this(span, null) { }

        public PooledDictionary(ReadOnlySpan<(TKey key, TValue value)> span, IEqualityComparer<TKey> comparer) :
            this(span.Length, comparer)
        {
            foreach (var (key, value) in span)
            {
                Add(key, value);
            }
        }

        protected PooledDictionary(SerializationInfo info, StreamingContext context)
        {
            // We can't do anything with the keys and values until the entire graph has been deserialized
            // and we have a resonable estimate that GetHashCode is not going to fail.  For the time being,
            // we'll just cache this.  The graph is not valid until OnDeserialization has been called.
            HashHelpers.SerializationInfoTable.Add(this, info);
        }

        private Span<int> Buckets => _buckets.AsSpan(0, _size);

        private Span<Entry> Entries => _entries.AsSpan(0, _size);

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return (_comparer == null || _comparer is NonRandomizedStringEqualityComparer) ? EqualityComparer<TKey>.Default : _comparer;
            }
        }

        public int Count => _count - _freeCount;

        public KeyCollection Keys
        {
            get
            {
                if (_keys == null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                if (_keys == null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                if (_keys == null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        public ValueCollection Values
        {
            get
            {
                if (_values == null) _values = new ValueCollection(this);
                return _values;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                if (_values == null) _values = new ValueCollection(this);
                return _values;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                if (_values == null) _values = new ValueCollection(this);
                return _values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0) return _entries[i].value;
                throw new KeyNotFoundException($"Key not found: '{key}'.");
            }
            set
            {
                bool modified = TryInsert(key, value, InsertionBehavior.OverwriteExisting);
                Debug.Assert(modified);
            }
        }

        public void Add(TKey key, TValue value)
        {
            bool modified = TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            Debug.Assert(modified); // If there was an existing key and the Add failed, an exception will already have been thrown.
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            if (enumerable is ICollection<KeyValuePair<TKey, TValue>> collection)
                EnsureCapacity(_count + collection.Count);

            foreach (var pair in enumerable)
            {
                TryInsert(pair.Key, pair.Value, InsertionBehavior.ThrowOnExisting);
            }
        }

        public void AddRange(IEnumerable<(TKey key, TValue value)> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            if (enumerable is ICollection<KeyValuePair<TKey, TValue>> collection)
                EnsureCapacity(_count + collection.Count);

            foreach (var (key, value) in enumerable)
            {
                TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            }
        }

        public void AddRange(ReadOnlySpan<(TKey key, TValue value)> span)
        {
            EnsureCapacity(_count + span.Length);

            foreach (var (key, value) in span)
            {
                TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            }
        }

        public void AddRange((TKey key, TValue value)[] array) 
            => AddRange(array.AsSpan());

        public void AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updater)
        {
            if (TryGetValue(key, out TValue value))
            {
                var updatedValue = updater(key, value);
                TryInsert(key, updatedValue, InsertionBehavior.OverwriteExisting);
            }
            else
            {
                TryInsert(key, addValue, InsertionBehavior.ThrowOnExisting);
            }
        }

        public void AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updater)
        {
            if (TryGetValue(key, out TValue value))
            {
                var updatedValue = updater(key, value);
                TryInsert(key, updatedValue, InsertionBehavior.OverwriteExisting);
            }
            else
            {
                var addValue = addValueFactory(key);
                TryInsert(key, addValue, InsertionBehavior.ThrowOnExisting);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
            => Add(keyValuePair.Key, keyValuePair.Value);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);
            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(Entries[i].value, keyValuePair.Value))
            {
                return true;
            }
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);
            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(Entries[i].value, keyValuePair.Value))
            {
                Remove(keyValuePair.Key);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            int count = _count;
            if (count > 0)
            {
                Buckets.Clear();

                _count = 0;
                _freeList = -1;
                _freeCount = 0;
                Entries.Slice(0, count).Clear();
                _size = 0;
                _version++;
            }
        }

        public bool ContainsKey(TKey key)
            => FindEntry(key) >= 0;

        public bool ContainsValue(TValue value)
        {
            var entries = Entries;
            if (value == null)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (entries[i].hashCode >= 0 && entries[i].value == null) return true;
                }
            }
            else
            {
                if (default(TValue) != null)
                {
                    // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
                    for (int i = 0; i < _count; i++)
                    {
                        if (entries[i].hashCode >= 0 && EqualityComparer<TValue>.Default.Equals(entries[i].value, value)) return true;
                    }
                }
                else
                {
                    // Object type: Shared Generic, EqualityComparer<TValue>.Default won't devirtualize
                    // https://github.com/dotnet/coreclr/issues/17273
                    // So cache in a local rather than get EqualityComparer per loop iteration
                    var defaultComparer = EqualityComparer<TValue>.Default;
                    for (int i = 0; i < _count; i++)
                    {
                        if (entries[i].hashCode >= 0 && defaultComparer.Equals(entries[i].value, value)) return true;
                    }
                }
            }
            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if ((uint)index > (uint)array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (array.Length - index < Count)
                throw new ArgumentException("Destination array too small.");

            int count = _count;
            var entries = Entries;
            for (int i = 0; i < count; i++)
            {
                if (entries[i].hashCode >= 0)
                {
                    array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                }
            }
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this, Enumerator.KeyValuePair);

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => new Enumerator(this, Enumerator.KeyValuePair);

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(VersionName, _version);
            info.AddValue(ComparerName, _comparer ?? EqualityComparer<TKey>.Default, typeof(IEqualityComparer<TKey>));
            info.AddValue(HashSizeName, _size); // This is the length of the bucket array

            if (_buckets != null)
            {
                var array = new KeyValuePair<TKey, TValue>[Count];
                CopyTo(array, 0);
                info.AddValue(KeyValuePairsName, array, typeof(KeyValuePair<TKey, TValue>[]));
            }
        }

        private int FindEntry(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int i = -1;
            var buckets = _buckets;
            var entries = _entries;
            int collisionCount = 0;
            int length = _size;
            if (length > 0)
            {
                IEqualityComparer<TKey> comparer = _comparer;

                if (comparer == null)
                {
                    int hashCode = key.GetHashCode() & 0x7FFFFFFF;
                    // Value in _buckets is 1-based
                    i = buckets[hashCode % length] - 1;
                    if (default(TKey) != null)
                    {
                        // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
                        do
                        {
                            // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                            // Test in if to drop range check for following array access
                            if ((uint)i >= (uint)length || (entries[i].hashCode == hashCode && EqualityComparer<TKey>.Default.Equals(entries[i].key, key)))
                            {
                                break;
                            }

                            i = entries[i].next;
                            if (collisionCount >= length)
                            {
                                // The chain of entries forms a loop; which means a concurrent update has happened.
                                // Break out of the loop and throw, rather than looping forever.
                                throw new InvalidOperationException("Concurrent operations are not supported.");
                            }
                            collisionCount++;
                        } while (true);
                    }
                    else
                    {
                        // Object type: Shared Generic, EqualityComparer<TValue>.Default won't devirtualize
                        // https://github.com/dotnet/coreclr/issues/17273
                        // So cache in a local rather than get EqualityComparer per loop iteration
                        var defaultComparer = EqualityComparer<TKey>.Default;
                        do
                        {
                            // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                            // Test in if to drop range check for following array access
                            if ((uint)i >= (uint)length || (entries[i].hashCode == hashCode && defaultComparer.Equals(entries[i].key, key)))
                            {
                                break;
                            }

                            i = entries[i].next;
                            if (collisionCount >= length)
                            {
                                // The chain of entries forms a loop; which means a concurrent update has happened.
                                // Break out of the loop and throw, rather than looping forever.
                                throw new InvalidOperationException("Concurrent operations are not supported.");
                            }
                            collisionCount++;
                        } while (true);
                    }
                }
                else
                {
                    int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                    // Value in _buckets is 1-based
                    i = buckets[hashCode % length] - 1;
                    do
                    {
                        // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                        // Test in if to drop range check for following array access
                        if ((uint)i >= (uint)length ||
                            (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key)))
                        {
                            break;
                        }

                        i = entries[i].next;
                        if (collisionCount >= length)
                        {
                            // The chain of entries forms a loop; which means a concurrent update has happened.
                            // Break out of the loop and throw, rather than looping forever.
                            throw new InvalidOperationException("Concurrent operations are not supported.");
                        }
                        collisionCount++;
                    } while (true);
                }
            }

            return i;
        }

        private int Initialize(int capacity)
        {
            _size = HashHelpers.GetPrime(capacity);
            _freeList = -1;
            _buckets = s_bucketPool.Rent(_size);
            Array.Clear(_buckets, 0, _buckets.Length);
            _entries = s_entryPool.Rent(_size);

            return _size;
        }

        private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (_buckets == null || _size == 0)
            {
                Initialize(0);
            }

            var entries = Entries;
            var buckets = Buckets;
            var comparer = _comparer;

            int hashCode = ((comparer == null) ? key.GetHashCode() : comparer.GetHashCode(key)) & 0x7FFFFFFF;

            int collisionCount = 0;
            ref int bucket = ref buckets[hashCode % buckets.Length];
            // Value in _buckets is 1-based
            int i = bucket - 1;

            if (comparer == null)
            {
                if (default(TKey) != null)
                {
                    // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
                    do
                    {
                        // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                        // Test uint in if rather than loop condition to drop range check for following array access
                        if ((uint)i >= (uint)entries.Length)
                        {
                            break;
                        }

                        if (entries[i].hashCode == hashCode && EqualityComparer<TKey>.Default.Equals(entries[i].key, key))
                        {
                            if (behavior == InsertionBehavior.OverwriteExisting)
                            {
                                entries[i].value = value;
                                _version++;
                                return true;
                            }

                            if (behavior == InsertionBehavior.ThrowOnExisting)
                            {
                                throw new ArgumentException("An element with the same key already exists in the dictionary.");
                            }

                            return false;
                        }

                        i = entries[i].next;
                        if (collisionCount >= entries.Length)
                        {
                            // The chain of entries forms a loop; which means a concurrent update has happened.
                            // Break out of the loop and throw, rather than looping forever.
                            throw new InvalidOperationException("Concurrent operations are not supported.");
                        }
                        collisionCount++;
                    } while (true);
                }
                else
                {
                    // Object type: Shared Generic, EqualityComparer<TValue>.Default won't devirtualize
                    // https://github.com/dotnet/coreclr/issues/17273
                    // So cache in a local rather than get EqualityComparer per loop iteration
                    var defaultComparer = EqualityComparer<TKey>.Default;
                    do
                    {
                        // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                        // Test uint in if rather than loop condition to drop range check for following array access
                        if ((uint)i >= (uint)entries.Length)
                        {
                            break;
                        }

                        if (entries[i].hashCode == hashCode && defaultComparer.Equals(entries[i].key, key))
                        {
                            if (behavior == InsertionBehavior.OverwriteExisting)
                            {
                                entries[i].value = value;
                                _version++;
                                return true;
                            }

                            if (behavior == InsertionBehavior.ThrowOnExisting)
                            {
                                throw new ArgumentException("An element with the same key already exists in the dictionary.");
                            }

                            return false;
                        }

                        i = entries[i].next;
                        if (collisionCount >= entries.Length)
                        {
                            // The chain of entries forms a loop; which means a concurrent update has happened.
                            // Break out of the loop and throw, rather than looping forever.
                            throw new InvalidOperationException("Concurrent operations are not supported.");
                        }
                        collisionCount++;
                    } while (true);
                }
            }
            else
            {
                do
                {
                    // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                    // Test uint in if rather than loop condition to drop range check for following array access
                    if ((uint)i >= (uint)entries.Length)
                    {
                        break;
                    }

                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                    {
                        if (behavior == InsertionBehavior.OverwriteExisting)
                        {
                            entries[i].value = value;
                            _version++;
                            return true;
                        }

                        if (behavior == InsertionBehavior.ThrowOnExisting)
                        {
                            throw new ArgumentException("An element with the same key already exists in the dictionary.");
                        }

                        return false;
                    }

                    i = entries[i].next;
                    if (collisionCount >= entries.Length)
                    {
                        // The chain of entries forms a loop; which means a concurrent update has happened.
                        // Break out of the loop and throw, rather than looping forever.
                        throw new InvalidOperationException("Concurrent operations are not supported.");
                    }
                    collisionCount++;
                } while (true);

            }

            bool updateFreeList = false;
            int index;
            if (_freeCount > 0)
            {
                index = _freeList;
                updateFreeList = true;
                _freeCount--;
            }
            else
            {
                int count = _count;
                if (count == entries.Length)
                {
                    Resize();
                    buckets = Buckets;
                    entries = Entries;
                    bucket = ref buckets[hashCode % buckets.Length];
                }
                index = count;
                _count = count + 1;
                entries = Entries;
            }

            ref Entry entry = ref entries[index];

            if (updateFreeList)
            {
                _freeList = entry.next;
            }
            entry.hashCode = hashCode;
            // Value in _buckets is 1-based
            entry.next = bucket - 1;
            entry.key = key;
            entry.value = value;
            // Value in _buckets is 1-based
            bucket = index + 1;
            _version++;

            // Value types never rehash
            if (default(TKey) == null && collisionCount > HashHelpers.HashCollisionThreshold && comparer is NonRandomizedStringEqualityComparer)
            {
                // If we hit the collision threshold we'll need to switch to the comparer which is using randomized string hashing
                // i.e. EqualityComparer<string>.Default.
                _comparer = null;
                Resize(entries.Length, true);
            }

            return true;
        }

        public virtual void OnDeserialization(object sender)
        {
            HashHelpers.SerializationInfoTable.TryGetValue(this, out SerializationInfo siInfo);

            if (siInfo == null)
            {
                // We can return immediately if this function is called twice. 
                // Note we remove the serialization info from the table at the end of this method.
                return;
            }

            int realVersion = siInfo.GetInt32(VersionName);
            int hashsize = siInfo.GetInt32(HashSizeName);
            _comparer = (IEqualityComparer<TKey>)siInfo.GetValue(ComparerName, typeof(IEqualityComparer<TKey>));

            if (hashsize != 0)
            {
                Initialize(hashsize);

                var array = (KeyValuePair<TKey, TValue>[])
                    siInfo.GetValue(KeyValuePairsName, typeof(KeyValuePair<TKey, TValue>[]));

                if (array == null)
                {
                    throw new SerializationException("Serialized PooledDictionary missing data.");
                }

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Key == null)
                    {
                        throw new SerializationException("Serialized PooledDictionary had null key.");
                    }
                    Add(array[i].Key, array[i].Value);
                }
            }
            else
            {
                _buckets = null;
            }

            _version = realVersion;
            HashHelpers.SerializationInfoTable.Remove(this);
        }

        private void Resize()
            => Resize(HashHelpers.ExpandPrime(_count), false);

        private void Resize(int newSize, bool forceNewHashCodes)
        {
            // Value types never rehash
            Debug.Assert(!forceNewHashCodes || default(TKey) == null);
            Debug.Assert(newSize >= _size);

            // Because ArrayPool might give us larger arrays than we asked for, see if we can 
            // use the existing capacity without actually resizing.
            if (_buckets.Length >= newSize && _entries.Length >= newSize)
            {
                _size = newSize;
                return;
            }

            int[] buckets = s_bucketPool.Rent(newSize);
            Array.Clear(buckets, 0, buckets.Length);
            Entry[] entries = s_entryPool.Rent(newSize);

            int count = _count;
            Array.Copy(_entries, 0, entries, 0, count);

            if (default(TKey) == null && forceNewHashCodes)
            {
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0)
                    {
                        Debug.Assert(_comparer == null);
                        entries[i].hashCode = (entries[i].key.GetHashCode() & 0x7FFFFFFF);
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (entries[i].hashCode >= 0)
                {
                    int bucket = entries[i].hashCode % newSize;
                    // Value in _buckets is 1-based
                    entries[i].next = buckets[bucket] - 1;
                    // Value in _buckets is 1-based
                    buckets[bucket] = i + 1;
                }
            }

            s_bucketPool.Return(_buckets);
            _buckets = buckets;
            s_entryPool.Return(_entries);
            _entries = entries;
            _size = newSize;
        }

        // The overload Remove(TKey key, out TValue value) is a copy of this method with one additional
        // statement to copy the value for entry being removed into the output parameter.
        // Code has been intentionally duplicated for performance reasons.
        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var buckets = Buckets;
            var entries = Entries;
            int collisionCount = 0;
            if (_size > 0)
            {
                int hashCode = (_comparer?.GetHashCode(key) ?? key.GetHashCode()) & 0x7FFFFFFF;
                int bucket = hashCode % buckets.Length;
                int last = -1;
                // Value in buckets is 1-based
                int i = buckets[bucket] - 1;
                while (i >= 0)
                {
                    ref Entry entry = ref entries[i];

                    if (entry.hashCode == hashCode && (_comparer?.Equals(entry.key, key) ?? EqualityComparer<TKey>.Default.Equals(entry.key, key)))
                    {
                        if (last < 0)
                        {
                            // Value in buckets is 1-based
                            buckets[bucket] = entry.next + 1;
                        }
                        else
                        {
                            entries[last].next = entry.next;
                        }
                        entry.hashCode = -1;
                        entry.next = _freeList;

#if NETCOREAPP2_1
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
                        {
                            entry.key = default;
                        }
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
                        {
                            entry.value = default;
                        }
#else
                        entry.key = default;
                        entry.value = default;
#endif
                        _freeList = i;
                        _freeCount++;
                        _version++;
                        return true;
                    }

                    last = i;
                    i = entry.next;
                    if (collisionCount >= entries.Length)
                    {
                        // The chain of entries forms a loop; which means a concurrent update has happened.
                        // Break out of the loop and throw, rather than looping forever.
                        throw new InvalidOperationException("Concurrent operations are not supported.");
                    }
                    collisionCount++;
                }
            }
            return false;
        }

        // This overload is a copy of the overload Remove(TKey key) with one additional
        // statement to copy the value for entry being removed into the output parameter.
        // Code has been intentionally duplicated for performance reasons.
        public bool Remove(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var buckets = Buckets;
            var entries = Entries;
            int collisionCount = 0;
            int hashCode = (_comparer?.GetHashCode(key) ?? key.GetHashCode()) & 0x7FFFFFFF;
            int bucket = hashCode % buckets.Length;
            int last = -1;
            // Value in buckets is 1-based
            int i = buckets[bucket] - 1;
            while (i >= 0)
            {
                ref Entry entry = ref entries[i];

                if (entry.hashCode == hashCode && (_comparer?.Equals(entry.key, key) ?? EqualityComparer<TKey>.Default.Equals(entry.key, key)))
                {
                    if (last < 0)
                    {
                        // Value in buckets is 1-based
                        buckets[bucket] = entry.next + 1;
                    }
                    else
                    {
                        entries[last].next = entry.next;
                    }

                    value = entry.value;

                    entry.hashCode = -1;
                    entry.next = _freeList;

#if NETCOREAPP2_1
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
                        {
                            entry.key = default;
                        }
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
                        {
                            entry.value = default;
                        }
#else
                    entry.key = default;
                    entry.value = default;
#endif
                    _freeList = i;
                    _freeCount++;
                    return true;
                }

                last = i;
                i = entry.next;
                if (collisionCount >= entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    throw new InvalidOperationException("Concurrent operations are not supported.");
                }
                collisionCount++;
            }
            value = default;
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = _entries[i].value;
                return true;
            }
            value = default;
            return false;
        }

        public bool TryAdd(TKey key, TValue value)
            => TryInsert(key, value, InsertionBehavior.None);

        public TValue GetOrAdd(TKey key, TValue addValue)
        {
            if (TryGetValue(key, out TValue value))
                return value;

            Add(key, addValue);
            return addValue;
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            if (TryGetValue(key, out TValue value))
                return value;

            var addValue = valueFactory(key);
            Add(key, addValue);
            return addValue;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
            => CopyTo(array, index);

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (array.Rank != 1)
                throw new ArgumentException("Destination array must be one-dimensional.");
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("Destination array must be zero-based.");
            if ((uint)index > (uint)array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < Count)
                throw new ArgumentException("Destination array too small.");

            if (array is KeyValuePair<TKey, TValue>[] pairs)
            {
                CopyTo(pairs, index);
            }
            else if (array is object[] objects)
            {
                try
                {
                    int count = _count;
                    var entries = Entries;
                    for (int i = 0; i < count; i++)
                    {
                        if (entries[i].hashCode >= 0)
                        {
                            objects[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                        }
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException("Invalid array type.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid array type.");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(this, Enumerator.KeyValuePair);

        /// <summary>
        /// Ensures that the dictionary can hold up to 'capacity' entries without any further expansion of its backing storage
        /// </summary>
        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));
            int currentCapacity = _size;
            if (currentCapacity >= capacity)
                return currentCapacity;
            _version++;
            if (_buckets == null || _size == 0)
                return Initialize(capacity);
            int newSize = HashHelpers.GetPrime(capacity);
            Resize(newSize, forceNewHashCodes: false);
            return newSize;
        }

        /// <summary>
        /// Sets the capacity of this dictionary to what it would be if it had been originally initialized with all its entries
        /// 
        /// This method can be used to minimize the memory overhead 
        /// once it is known that no new elements will be added. 
        /// 
        /// To allocate minimum size storage array, execute the following statements:
        /// 
        /// dictionary.Clear();
        /// dictionary.TrimExcess();
        /// </summary>
        public void TrimExcess()
            => TrimExcess(Count);

        /// <summary>
        /// Sets the capacity of this dictionary to hold up 'capacity' entries without any further expansion of its backing storage
        /// 
        /// This method can be used to minimize the memory overhead 
        /// once it is known that no new elements will be added. 
        /// </summary>
        public void TrimExcess(int capacity)
        {
            if (capacity < Count)
                throw new ArgumentOutOfRangeException(nameof(capacity));
            int newSize = HashHelpers.GetPrime(capacity);

            Entry[] oldEntries = _entries;
            int[] oldBuckets = _buckets;
            int currentCapacity = oldEntries == null ? 0 : oldEntries.Length;
            if (newSize >= currentCapacity)
                return;

            int oldCount = _count;
            _version++;
            Initialize(newSize);
            var entries = _entries;
            var buckets = _buckets;
            int count = 0;
            for (int i = 0; i < oldCount; i++)
            {
                int hashCode = oldEntries[i].hashCode;
                if (hashCode >= 0)
                {
                    ref Entry entry = ref entries[count];
                    entry = oldEntries[i];
                    int bucket = hashCode % newSize;
                    // Value in _buckets is 1-based
                    entry.next = buckets[bucket] - 1;
                    // Value in _buckets is 1-based
                    buckets[bucket] = count + 1;
                    count++;
                }
            }
            _count = count;
            _freeCount = 0;
            s_bucketPool.Return(oldBuckets);
            s_entryPool.Return(oldEntries);
        }

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

        bool IDictionary.IsFixedSize => false;

        bool IDictionary.IsReadOnly => false;

        ICollection IDictionary.Keys => Keys;

        ICollection IDictionary.Values => Values;

        object IDictionary.this[object key]
        {
            get
            {
                if (IsCompatibleKey(key))
                {
                    int i = FindEntry((TKey)key);
                    if (i >= 0)
                    {
                        return Entries[i].value;
                    }
                }
                return null;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                if (value == null && default(TValue) != null)
                    throw new ArgumentNullException("Value cannot be null");

                try
                {
                    TKey tempKey = (TKey)key;
                    try
                    {
                        this[tempKey] = (TValue)value;
                    }
                    catch (InvalidCastException)
                    {
                        throw new ArgumentNullException($"Wrong value type. Expected {typeof(TValue).Name}.");
                    }
                }
                catch (InvalidCastException)
                {
                    throw new ArgumentNullException($"Wrong key type. Expected {typeof(TKey).Name}.");
                }
            }
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return key is TKey;
        }

        void IDictionary.Add(object key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null && default(TValue) != null)
                throw new ArgumentNullException("Value cannot be null");

            try
            {
                TKey tempKey = (TKey)key;

                try
                {
                    Add(tempKey, (TValue)value);
                }
                catch (InvalidCastException)
                {
                    throw new ArgumentNullException($"Wrong value type. Expected {typeof(TValue).Name}.");
                }
            }
            catch (InvalidCastException)
            {
                throw new ArgumentNullException($"Wrong key type. Expected {typeof(TKey).Name}.");
            }
        }

        bool IDictionary.Contains(object key)
        {
            if (IsCompatibleKey(key))
            {
                return ContainsKey((TKey)key);
            }

            return false;
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
            => new Enumerator(this, Enumerator.DictEntry);

        void IDictionary.Remove(object key)
        {
            if (IsCompatibleKey(key))
            {
                Remove((TKey)key);
            }
        }

        public void Dispose()
        {
            if (_buckets != null)
            {
                s_bucketPool.Return(_buckets);
                _buckets = null;
            }
            if (_entries != null)
            {
                s_entryPool.Return(_entries);
                _entries = null;
            }
            _count = 0;
            _size = 0;
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator
        {
            private readonly PooledDictionary<TKey, TValue> _dictionary;
            private readonly int _version;
            private int _index;
            private KeyValuePair<TKey, TValue> _current;
            private readonly int _getEnumeratorRetType;  // What should Enumerator.Current return?

            internal const int DictEntry = 1;
            internal const int KeyValuePair = 2;

            internal Enumerator(PooledDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
            {
                _dictionary = dictionary;
                _version = dictionary._version;
                _index = 0;
                _getEnumeratorRetType = getEnumeratorRetType;
                _current = new KeyValuePair<TKey, TValue>();
            }

            public bool MoveNext()
            {
                if (_version != _dictionary._version)
                {
                    throw new InvalidOperationException("PooledDictionary was modified during enumeration.");
                }

                // Use unsigned comparison since we set index to dictionary.count+1 when the enumeration ends.
                // dictionary.count+1 could be negative if dictionary.count is int.MaxValue
                while ((uint)_index < (uint)_dictionary._count)
                {
                    ref Entry entry = ref _dictionary.Entries[_index++];

                    if (entry.hashCode >= 0)
                    {
                        _current = new KeyValuePair<TKey, TValue>(entry.key, entry.value);
                        return true;
                    }
                }

                _index = _dictionary._count + 1;
                _current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            public KeyValuePair<TKey, TValue> Current => _current;

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new InvalidOperationException("Invalid enumeration, index was out of range.");
                    }

                    if (_getEnumeratorRetType == DictEntry)
                    {
                        return new DictionaryEntry(_current.Key, _current.Value);
                    }
                    else
                    {
                        return new KeyValuePair<TKey, TValue>(_current.Key, _current.Value);
                    }
                }
            }

            void IEnumerator.Reset()
            {
                if (_version != _dictionary._version)
                {
                    throw new InvalidOperationException("PooledDictionary was modified during enumeration.");
                }

                _index = 0;
                _current = new KeyValuePair<TKey, TValue>();
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new InvalidOperationException("Invalid enumeration, index was out of range.");
                    }

                    return new DictionaryEntry(_current.Key, _current.Value);
                }
            }

            object IDictionaryEnumerator.Key
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new InvalidOperationException("Invalid enumeration, index was out of range.");
                    }

                    return _current.Key;
                }
            }

            object IDictionaryEnumerator.Value
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new InvalidOperationException("Invalid enumeration, index was out of range.");
                    }

                    return _current.Value;
                }
            }
        }

        [DebuggerDisplay("Count = {Count}")]
        public sealed class KeyCollection : ICollection<TKey>, ICollection, IReadOnlyCollection<TKey>
        {
            private PooledDictionary<TKey, TValue> _dictionary;

            public KeyCollection(PooledDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            }

            public Enumerator GetEnumerator()
                => new Enumerator(_dictionary);

            public void CopyTo(TKey[] array, int index)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));

                if (index < 0 || index > array.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (array.Length - index < _dictionary.Count)
                    throw new ArgumentException("Destination array too small.");

                int count = _dictionary._count;
                var entries = _dictionary.Entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0) array[index++] = entries[i].key;
                }
            }

            public int Count => _dictionary.Count;

            bool ICollection<TKey>.IsReadOnly => true;

            void ICollection<TKey>.Add(TKey item)
                => throw new NotSupportedException();

            void ICollection<TKey>.Clear()
                => throw new NotSupportedException();

            bool ICollection<TKey>.Contains(TKey item)
                => _dictionary.ContainsKey(item);

            bool ICollection<TKey>.Remove(TKey item)
                => throw new NotSupportedException();

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
                => new Enumerator(_dictionary);

            IEnumerator IEnumerable.GetEnumerator()
                => new Enumerator(_dictionary);

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));
                if (array.Rank != 1)
                    throw new ArgumentException("Destination array must be one-dimensional.");
                if (array.GetLowerBound(0) != 0)
                    throw new ArgumentException("Destination array must be zero-based.");
                if ((uint)index > (uint)array.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));
                if (array.Length - index < _dictionary.Count)
                    throw new ArgumentException("Destination array too small.");

                if (array is TKey[] keys)
                {
                    CopyTo(keys, index);
                }
                else
                {
                    if (!(array is object[] objects))
                    {
                        throw new ArgumentException("Invalid array type.");
                    }

                    int count = _dictionary._count;
                    var entries = _dictionary.Entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].hashCode >= 0) objects[index++] = entries[i].key;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new ArgumentException("Invalid array type.");
                    }
                }
            }

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;

            public struct Enumerator : IEnumerator<TKey>, IEnumerator
            {
                private readonly PooledDictionary<TKey, TValue> _dictionary;
                private int _index;
                private readonly int _version;
                private TKey _currentKey;

                internal Enumerator(PooledDictionary<TKey, TValue> dictionary)
                {
                    _dictionary = dictionary;
                    _version = dictionary._version;
                    _index = 0;
                    _currentKey = default;
                }

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new InvalidOperationException("PooledDictionary was modified during enumeration.");
                    }

                    while ((uint)_index < (uint)_dictionary._count)
                    {
                        ref Entry entry = ref _dictionary.Entries[_index++];

                        if (entry.hashCode >= 0)
                        {
                            _currentKey = entry.key;
                            return true;
                        }
                    }

                    _index = _dictionary._count + 1;
                    _currentKey = default;
                    return false;
                }

                public TKey Current => _currentKey;

                object IEnumerator.Current
                {
                    get
                    {
                        if (_index == 0 || (_index == _dictionary._count + 1))
                        {
                            throw new InvalidOperationException("Invalid enumeration, index was out of range.");
                        }

                        return _currentKey;
                    }
                }

                void IEnumerator.Reset()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new InvalidOperationException("PooledDictionary was modified during enumeration.");
                    }

                    _index = 0;
                    _currentKey = default;
                }
            }
        }

        [DebuggerDisplay("Count = {Count}")]
        public sealed class ValueCollection : ICollection<TValue>, ICollection, IReadOnlyCollection<TValue>
        {
            private PooledDictionary<TKey, TValue> _dictionary;

            public ValueCollection(PooledDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            }

            public Enumerator GetEnumerator()
                => new Enumerator(_dictionary);

            public void CopyTo(TValue[] array, int index)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));

                if (index < 0 || index > array.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (array.Length - index < _dictionary.Count)
                    throw new ArgumentException("Destination array is too small.");

                int count = _dictionary._count;
                var entries = _dictionary.Entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0) array[index++] = entries[i].value;
                }
            }

            public int Count => _dictionary.Count;

            bool ICollection<TValue>.IsReadOnly => true;

            void ICollection<TValue>.Add(TValue item)
                => throw new NotSupportedException();

            bool ICollection<TValue>.Remove(TValue item)
                => throw new NotSupportedException();

            void ICollection<TValue>.Clear()
                => throw new NotSupportedException();

            bool ICollection<TValue>.Contains(TValue item)
                => _dictionary.ContainsValue(item);

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
                => new Enumerator(_dictionary);

            IEnumerator IEnumerable.GetEnumerator()
                => new Enumerator(_dictionary);

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));
                if (array.Rank != 1)
                    throw new ArgumentException("Destination array must be one-dimensional.");
                if (array.GetLowerBound(0) != 0)
                    throw new ArgumentException("Destination array must be zero-based.");
                if ((uint)index > (uint)array.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));
                if (array.Length - index < _dictionary.Count)
                    throw new ArgumentException("Destination array too small.");

                if (array is TValue[] values)
                {
                    CopyTo(values, index);
                }
                else if (array is object[] objects)
                {
                    int count = _dictionary._count;
                    var entries = _dictionary.Entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].hashCode >= 0) objects[index++] = entries[i].value;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new ArgumentException("Invalid array type.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid array type.");
                }
            }

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;

            public struct Enumerator : IEnumerator<TValue>, IEnumerator
            {
                private readonly PooledDictionary<TKey, TValue> _dictionary;
                private int _index;
                private readonly int _version;
                private TValue _currentValue;

                internal Enumerator(PooledDictionary<TKey, TValue> dictionary)
                {
                    _dictionary = dictionary;
                    _version = dictionary._version;
                    _index = 0;
                    _currentValue = default;
                }

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new InvalidOperationException("PooledDictionary was modified during enumeration.");
                    }

                    while ((uint)_index < (uint)_dictionary._count)
                    {
                        ref Entry entry = ref _dictionary.Entries[_index++];

                        if (entry.hashCode >= 0)
                        {
                            _currentValue = entry.value;
                            return true;
                        }
                    }
                    _index = _dictionary._count + 1;
                    _currentValue = default;
                    return false;
                }

                public TValue Current => _currentValue;

                object IEnumerator.Current
                {
                    get
                    {
                        if (_index == 0 || (_index == _dictionary._count + 1))
                        {
                            throw new InvalidOperationException("Invalid enumeration, index was out of range.");
                        }

                        return _currentValue;
                    }
                }

                void IEnumerator.Reset()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new InvalidOperationException("PooledDictionary was modified during enumeration.");
                    }
                    _index = 0;
                    _currentValue = default;
                }
            }
        }
    }
}
