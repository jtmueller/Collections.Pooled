// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Threading;

#if NETCOREAPP3_0
using System.Text.Json.Serialization;
#endif

namespace Collections.Pooled
{
    using static ClearModeUtil;

    /// <remarks>
    /// A <see cref="PooledDictionary{TKey,TValue}"/> can support multiple readers concurrently, as long as the collection is not modified. 
    /// Even so, enumerating through a collection is intrinsically not a thread-safe procedure. 
    /// In the rare case where an enumeration contends with write accesses, the collection must be locked during the entire enumeration. 
    /// To allow the collection to be accessed by multiple threads for reading and writing, you must implement your own synchronization. 
    /// </remarks>
    [DebuggerTypeProxy(typeof(IDictionaryDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
#if NETCOREAPP3_0
    [JsonConverter(typeof(PooledDictionaryJsonConverter))]
#endif
    public class PooledDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>,
        ISerializable, IDeserializationCallback, IDisposable
        where TKey : notnull
    {
        /// <summary>
        /// Used internally to control behavior of insertion into a <see cref="PooledDictionary{TKey, TValue}"/>.
        /// </summary>
        protected enum InsertionBehavior : byte
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

        protected struct Entry
        {
            // 0-based index of next entry in chain: -1 means end of chain
            // also encodes whether this entry _itself_ is part of the free list by changing sign and subtracting 3,
            // so -2 means end of free list, -3 means index 0 but on free list, -4 means index 1 but on free list, etc.
            public int next;
            public uint hashCode;
            public TKey key;           // Key of entry
            public TValue value;       // Value of entry
        }

        // constants for serialization
        private const string s_versionName = "Version"; // Do not rename (binary serialization)
        private const string s_hashSizeName = "HashSize"; // Do not rename (binary serialization). Must save buckets.Length
        private const string s_keyValuePairsName = "KeyValuePairs"; // Do not rename (binary serialization)
        private const string s_comparerName = "Comparer"; // Do not rename (binary serialization)
        private const string s_clearKeyName = "CK"; // Do not rename (binary serialization)
        private const string s_clearValueName = "CV"; // Do not rename (binary serialization)
        private const int s_startOfFreeList = -3;

        private static readonly ArrayPool<int> s_bucketPool = ArrayPool<int>.Shared;
        private static readonly ArrayPool<Entry> s_entryPool = ArrayPool<Entry>.Shared;

        // WARNING:
        // It's important that the number of buckets be prime, and these arrays could exceed
        // that size as they come from ArrayPool. Be careful not to index past _size or bad
        // things will happen.
        private int[] _buckets;
        private Entry[] _entries;
        private int _size;

        private int _count;
        private int _freeList;
        private int _freeCount;
        private int _version;
        protected IEqualityComparer<TKey>? _comparer;
        private KeyCollection? _keys;
        private ValueCollection? _values;
#pragma warning disable IDE0044
        private object? _syncRoot;
#pragma warning restore IDE0044
        private readonly bool _clearKeyOnFree;
        private readonly bool _clearValueOnFree;

        protected ReadOnlySpan<int> Buckets => new ReadOnlySpan<int>(_buckets);
        protected ReadOnlySpan<Entry> Entries => new ReadOnlySpan<Entry>(_entries);
        protected int Size => _size;

        #region Constructors

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary() : this(0, ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(ClearMode clearMode) : this(0, clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(int capacity) : this(capacity, ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(int capacity, ClearMode clearMode) : this(capacity, clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEqualityComparer<TKey> comparer) : this(0, ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(int capacity, IEqualityComparer<TKey>? comparer) : this(capacity, ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(ClearMode clearMode, IEqualityComparer<TKey>? comparer) : this(0, clearMode, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(int capacity, ClearMode clearMode, IEqualityComparer<TKey>? comparer)
        {
            if (capacity < 0) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            if (comparer != EqualityComparer<TKey>.Default)
            {
                _comparer = comparer;
            }

            _clearKeyOnFree = ShouldClear<TKey>(clearMode);
            _clearValueOnFree = ShouldClear<TValue>(clearMode);

            if (typeof(TKey) == typeof(string) && _comparer is null)
            {
                // To start, move off default comparer for string which is randomised
                _comparer = (IEqualityComparer<TKey>)NonRandomizedStringEqualityComparer.Default;
            }

            _buckets = Array.Empty<int>();
            _entries = Array.Empty<Entry>();

            if (capacity > 0)
            {
                Initialize(capacity);
            }
        }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IDictionary<TKey, TValue> dictionary, ClearMode clearMode) : this(dictionary, clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) : this(dictionary, ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IDictionary<TKey, TValue> dictionary, ClearMode clearMode, IEqualityComparer<TKey>? comparer) :
            this(dictionary?.Count ?? 0, clearMode, comparer)
        {
            if (dictionary is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);

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
                    if (entries[i].next >= -1)
                    {
                        TryInsert(entries[i].key, entries[i].value, InsertionBehavior.ThrowOnExisting);
                    }
                }
                return;
            }

            foreach (var pair in dictionary)
            {
                TryInsert(pair.Key, pair.Value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : this(collection, ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, ClearMode clearMode)
            : this(collection, clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : this(collection, ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, ClearMode clearMode, IEqualityComparer<TKey>? comparer) :
            this((collection as ICollection<KeyValuePair<TKey, TValue>>)?.Count ?? 0, clearMode, comparer)
        {
            if (collection is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);

            foreach (var pair in collection)
            {
                TryInsert(pair.Key, pair.Value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<(TKey key, TValue value)> collection)
            : this(collection, ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<(TKey key, TValue value)> collection, ClearMode clearMode)
            : this(collection, clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<(TKey key, TValue value)> collection, IEqualityComparer<TKey>? comparer)
            : this(collection, ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(IEnumerable<(TKey key, TValue value)> collection, ClearMode clearMode, IEqualityComparer<TKey>? comparer)
            : this((collection as ICollection<(TKey, TValue)>)?.Count ?? 0, clearMode, comparer)
        {
            if (collection is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);

            foreach (var (key, value) in collection)
            {
                TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary((TKey key, TValue value)[] array)
            : this(array.AsSpan(), ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary((TKey key, TValue value)[] array, ClearMode clearMode)
            : this(array.AsSpan(), clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary((TKey key, TValue value)[] array, IEqualityComparer<TKey>? comparer)
            : this(array.AsSpan(), ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary((TKey key, TValue value)[] array, ClearMode clearMode, IEqualityComparer<TKey>? comparer)
            : this(array.AsSpan(), clearMode, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(ReadOnlySpan<(TKey key, TValue value)> span)
            : this(span, ClearMode.Auto, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(ReadOnlySpan<(TKey key, TValue value)> span, ClearMode clearMode)
            : this(span, clearMode, null) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(ReadOnlySpan<(TKey key, TValue value)> span, IEqualityComparer<TKey>? comparer)
            : this(span, ClearMode.Auto, comparer) { }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        public PooledDictionary(ReadOnlySpan<(TKey key, TValue value)> span, ClearMode clearMode, IEqualityComparer<TKey>? comparer)
            : this(span.Length, clearMode, comparer)
        {
            foreach (var (key, value) in span)
            {
                TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        protected PooledDictionary(SerializationInfo info, StreamingContext _)
        {
            _clearKeyOnFree = (bool?)info.GetValue(s_clearKeyName, typeof(bool)) ?? ShouldClear<TKey>(ClearMode.Auto);
            _clearValueOnFree = (bool?)info.GetValue(s_clearValueName, typeof(bool)) ?? ShouldClear<TValue>(ClearMode.Auto);

            // We can't do anything with the keys and values until the entire graph has been deserialized
            // and we have a resonable estimate that GetHashCode is not going to fail.  For the time being,
            // we'll just cache this.  The graph is not valid until OnDeserialization has been called.
            HashHelpers.SerializationInfoTable.Add(this, info);
            _buckets = Array.Empty<int>();
            _entries = Array.Empty<Entry>();
        }

        #endregion

        /// <summary>
        /// The <see cref="IEqualityComparer{TKey}"/> used to compare keys in this dictionary.
        /// </summary>
        public virtual IEqualityComparer<TKey> Comparer
            => (_comparer is null || _comparer is NonRandomizedStringEqualityComparer)
                    ? EqualityComparer<TKey>.Default : _comparer;

        /// <summary>
        /// The number of items in the dictionary.
        /// </summary>
        public int Count => _count - _freeCount;

        /// <summary>
        /// Returns the ClearMode behavior for the collection, denoting whether values are
        /// cleared from internal arrays before returning them to the pool.
        /// </summary>
        public ClearMode KeyClearMode => _clearKeyOnFree ? ClearMode.Always : ClearMode.Never;

        /// <summary>
        /// Returns the ClearMode behavior for the collection, denoting whether values are
        /// cleared from internal arrays before returning them to the pool.
        /// </summary>
        public ClearMode ValueClearMode => _clearValueOnFree ? ClearMode.Always : ClearMode.Never;

        /// <summary>
        /// The keys in this dictionary.
        /// </summary>
        public KeyCollection Keys
        {
            get
            {
                if (_keys is null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                if (_keys is null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                if (_keys is null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        /// <summary>
        /// The values in this dictionary.
        /// </summary>
        public ValueCollection Values
        {
            get
            {
                if (_values is null) _values = new ValueCollection(this);
                return _values;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                if (_values is null) _values = new ValueCollection(this);
                return _values;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                if (_values is null) _values = new ValueCollection(this);
                return _values;
            }
        }

        /// <summary>
        /// Gets or sets an item in the dictionary by key.
        /// </summary>
        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0) return _entries[i].value;
                ThrowHelper.ThrowKeyNotFoundException(key);
                return default!;
            }
            set
            {
                bool modified = TryInsert(key, value, InsertionBehavior.OverwriteExisting);
                Debug.Assert(modified);
            }
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            bool modified = TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            Debug.Assert(modified); // If there was an existing key and the Add failed, an exception will already have been thrown.
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, an exception is thrown.
        /// </summary>
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            if (enumerable is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);

            if (enumerable is ICollection<KeyValuePair<TKey, TValue>> collection)
                EnsureCapacity(_count + collection.Count);

            foreach (var pair in enumerable)
            {
                TryInsert(pair.Key, pair.Value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, an exception is thrown.
        /// </summary>
        public void AddRange(IEnumerable<(TKey key, TValue value)> enumerable)
        {
            if (enumerable is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);

            if (enumerable is ICollection<KeyValuePair<TKey, TValue>> collection)
                EnsureCapacity(_count + collection.Count);

            foreach (var (key, value) in enumerable)
            {
                TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, an exception is thrown.
        /// </summary>
        public void AddRange(ReadOnlySpan<(TKey key, TValue value)> span)
        {
            EnsureCapacity(_count + span.Length);

            foreach (var (key, value) in span)
            {
                TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            }
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, an exception is thrown.
        /// </summary>
        public void AddRange((TKey key, TValue value)[] array)
            => AddRange(array.AsSpan());

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, the existing value is overwritten.
        /// </summary>
        public void SetRange(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            if (enumerable is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);

            foreach (var pair in enumerable)
            {
                TryInsert(pair.Key, pair.Value, InsertionBehavior.OverwriteExisting);
            }
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, the existing value is overwritten.
        /// </summary>
        public void SetRange(IEnumerable<(TKey key, TValue value)> enumerable)
        {
            if (enumerable is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.enumerable);

            foreach (var (key, value) in enumerable)
            {
                TryInsert(key, value, InsertionBehavior.OverwriteExisting);
            }
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, the existing value is overwritten.
        /// </summary>
        public void SetRange(ReadOnlySpan<(TKey key, TValue value)> span)
        {
            foreach (var (key, value) in span)
            {
                TryInsert(key, value, InsertionBehavior.OverwriteExisting);
            }
        }

        /// <summary>
        /// Adds a set of key-value pairs to the dictionary. If any of the keys are already
        /// present, the existing value is overwritten.
        /// </summary>
        public void SetRange((TKey key, TValue value)[] array)
            => SetRange(array.AsSpan());

        /// <summary>
        /// Adds a key/value pair to the dictionary. If the key is already present, the <paramref name="updater"/>
        /// function is called, with the key and the current value as parameters. The value returned from this function
        /// will overwrite the current value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="addValue"></param>
        /// <param name="updater"></param>
        public void AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updater)
        {
#pragma warning disable CS8717 // A member returning a [MaybeNull] value introduces a null value for a type parameter.
            if (TryGetValue(key, out var value))
            {
                var updatedValue = updater(key, value);
                TryInsert(key, updatedValue, InsertionBehavior.OverwriteExisting);
            }
            else
            {
                TryInsert(key, addValue, InsertionBehavior.ThrowOnExisting);
            }
#pragma warning restore CS8717
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary. If the key is not already present, the <paramref name="addValueFactory"/>
        /// function is called to generate the value for the key in question. 
        /// If the key is already present, the <paramref name="updater"/> function is called, with the key and
        /// the current value as parameters. The value returned from this function will overwrite the current value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="addValueFactory"></param>
        /// <param name="updater"></param>
        public void AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updater)
        {
#pragma warning disable CS8717 // A member returning a [MaybeNull] value introduces a null value for a type parameter.
            if (TryGetValue(key, out var value))
            {
                var updatedValue = updater(key, value);
                TryInsert(key, updatedValue, InsertionBehavior.OverwriteExisting);
            }
            else
            {
                var addValue = addValueFactory(key);
                TryInsert(key, addValue, InsertionBehavior.ThrowOnExisting);
            }
#pragma warning restore CS8717
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
            => Add(keyValuePair.Key, keyValuePair.Value);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);
            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(_entries[i].value, keyValuePair.Value))
            {
                return true;
            }
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);
            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(_entries[i].value, keyValuePair.Value))
            {
                Remove(keyValuePair.Key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        public void Clear()
        {
            int count = _count;
            if (count > 0)
            {
                Array.Clear(_buckets, 0, _size);

                _count = 0;
                _freeList = -1;
                _freeCount = 0;
                _size = 0;
                Array.Clear(_entries, 0, count);
                _version++;
            }
        }

        /// <summary>
        /// Returns true if the dictionary contains the given key.
        /// </summary>
        public bool ContainsKey(TKey key)
            => FindEntry(key) >= 0;

        /// <summary>
        /// Returns true if the dictionary contains the given value.
        /// </summary>
        public bool ContainsValue(TValue value)
        {
            var entries = _entries;
            if (value is null)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (entries[i].next >= -1 && entries[i].value is null)
                        return true;
                }
            }
            else
            {
                if (default(TValue)! != null)
                {
                    // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
                    for (int i = 0; i < _count; i++)
                    {
                        if (entries[i].next >= -1 && EqualityComparer<TValue>.Default.Equals(entries[i].value, value)) return true;
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
                        if (entries[i].next >= -1 && defaultComparer.Equals(entries[i].value, value)) return true;
                    }
                }
            }
            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if ((uint)index > (uint)array.Length)
            {
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            }

            if (array.Length - index < Count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            int count = _count;
            var entries = _entries;
            for (int i = 0; i < count; i++)
            {
                if (entries[i].next >= -1)
                {
                    array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                }
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public Enumerator GetEnumerator()
            => new Enumerator(this, Enumerator.KeyValuePair);

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => new Enumerator(this, Enumerator.KeyValuePair);

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            => GetObjectData(info, context);

        /// <summary>
        /// Allows child classes to add their own serialization data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
            }

            info.AddValue(s_versionName, _version);
            info.AddValue(s_comparerName, _comparer ?? EqualityComparer<TKey>.Default, typeof(IEqualityComparer<TKey>));
            info.AddValue(s_hashSizeName, _size); // This is the length of the bucket array
            info.AddValue(s_clearKeyName, _clearKeyOnFree);
            info.AddValue(s_clearValueName, _clearValueOnFree);

            if (_buckets != null)
            {
                var array = new KeyValuePair<TKey, TValue>[Count];
                CopyTo(array, 0);
                info.AddValue(s_keyValuePairsName, array, typeof(KeyValuePair<TKey, TValue>[]));
            }
        }

        private int FindEntry(TKey key)
        {
            if (key is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            int i = -1;
            int length = _size;
            if (length <= 0)
                return i;

            int[] buckets = _buckets;
            var entries = _entries;
            int collisionCount = 0;
            var comparer = _comparer;

            if (comparer is null)
            {
                uint hashCode = (uint)key.GetHashCode();
                // Value in _buckets is 1-based
                i = buckets[hashCode % (uint)length] - 1;

                if (default(TKey)! != null)
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
                            ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
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
                            ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                        }
                        collisionCount++;
                    } while (true);
                }
            }
            else
            {
                uint hashCode = (uint)comparer.GetHashCode(key);
                // Value in _buckets is 1-based
                i = buckets[hashCode % (uint)length] - 1;
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
                        ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                    }
                    collisionCount++;
                } while (true);
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

        protected bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
        {
            if (key is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (_buckets.Length == 0 || _size == 0)
            {
                Initialize(0);
            }

            var entries = _entries;
            var comparer = _comparer;
            int size = _size;

            uint hashCode = (uint)(comparer?.GetHashCode(key) ?? key.GetHashCode());

            int collisionCount = 0;
            ref int bucket = ref _buckets[hashCode % (uint)size];
            // Value in _buckets is 1-based
            int i = bucket - 1;

            if (comparer is null)
            {
                if (default(TKey)! != null)
                {
                    // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
                    do
                    {
                        // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                        // Test uint in if rather than loop condition to drop range check for following array access
                        if ((uint)i >= (uint)size)
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
                                ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
                            }

                            return false;
                        }

                        i = entries[i].next;
                        if (collisionCount >= size)
                        {
                            // The chain of entries forms a loop; which means a concurrent update has happened.
                            // Break out of the loop and throw, rather than looping forever.
                            ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
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
                        if ((uint)i >= (uint)size)
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
                                ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
                            }

                            return false;
                        }

                        i = entries[i].next;
                        if (collisionCount >= size)
                        {
                            // The chain of entries forms a loop; which means a concurrent update has happened.
                            // Break out of the loop and throw, rather than looping forever.
                            ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
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
                    if ((uint)i >= (uint)size)
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
                            ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
                        }

                        return false;
                    }

                    i = entries[i].next;
                    if (collisionCount >= size)
                    {
                        // The chain of entries forms a loop; which means a concurrent update has happened.
                        // Break out of the loop and throw, rather than looping forever.
                        ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
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
                if (count == size)
                {
                    Resize();
                    size = _size;
                    bucket = ref _buckets[hashCode % (uint)size];
                }
                index = count;
                _count = count + 1;
                entries = _entries;
            }

            ref Entry entry = ref entries[index];

            if (updateFreeList)
            {
                Debug.Assert((s_startOfFreeList - entries[_freeList].next) >= -1, "shouldn't overflow because `next` cannot underflow");
                _freeList = s_startOfFreeList - entries[_freeList].next;
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
            if (default(TKey)! is null && collisionCount > HashHelpers.HashCollisionThreshold && comparer is NonRandomizedStringEqualityComparer)
            {
                // If we hit the collision threshold we'll need to switch to the comparer which is using randomized string hashing
                // i.e. EqualityComparer<string>.Default.
                _comparer = null;
                Resize(size, true);
            }

            return true;
        }

        /// <summary>
        /// Called during deserialization.
        /// </summary>
        public virtual void OnDeserialization(object? sender)
        {
            HashHelpers.SerializationInfoTable.TryGetValue(this, out var siInfo);

            if (siInfo is null)
            {
                // We can return immediately if this function is called twice. 
                // Note we remove the serialization info from the table at the end of this method.
                return;
            }

            int realVersion = siInfo.GetInt32(s_versionName);
            int hashsize = siInfo.GetInt32(s_hashSizeName);
            _comparer = (IEqualityComparer<TKey>)siInfo.GetValue(s_comparerName, typeof(IEqualityComparer<TKey>))!;

            if (hashsize != 0)
            {
                Initialize(hashsize);

                var array = (KeyValuePair<TKey, TValue>[]?)siInfo.GetValue(s_keyValuePairsName, typeof(KeyValuePair<TKey, TValue>[]));

                if (array is null)
                {
                    ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
                }

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Key is null)
                    {
                        ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
                    }
                    Add(array[i].Key, array[i].Value);
                }
            }
            else
            {
                _buckets = Array.Empty<int>();
            }

            _version = realVersion;
            HashHelpers.SerializationInfoTable.Remove(this);
        }

        private void Resize()
            => Resize(HashHelpers.ExpandPrime(_count), false);

        private void Resize(int newSize, bool forceNewHashCodes)
        {
            // Value types never rehash
            Debug.Assert(!forceNewHashCodes || default(TKey)! is null);
            Debug.Assert(newSize >= _size);

            int[] buckets;
            Entry[] entries;
            bool replaceArrays;
            int count = _count;

            // Because ArrayPool might give us larger arrays than we asked for, see if we can 
            // use the existing capacity without actually resizing.
            if (_buckets.Length >= newSize && _entries.Length >= newSize)
            {
                Array.Clear(_buckets, 0, _buckets.Length);
                Array.Clear(_entries, _size, newSize - _size);
                buckets = _buckets;
                entries = _entries;
                replaceArrays = false;
            }
            else
            {
                buckets = s_bucketPool.Rent(newSize);
                entries = s_entryPool.Rent(newSize);

                Array.Clear(buckets, 0, buckets.Length);
                Array.Copy(_entries, 0, entries, 0, count);
                replaceArrays = true;
            }

            if (default(TKey)! is null && forceNewHashCodes)
            {
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].next >= -1)
                    {
                        Debug.Assert(_comparer is null);
                        entries[i].hashCode = (uint)entries[i].key.GetHashCode();
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (entries[i].next >= -1)
                {
                    uint bucket = entries[i].hashCode % (uint)newSize;
                    // Value in _buckets is 1-based
                    entries[i].next = buckets[bucket] - 1;
                    // Value in _buckets is 1-based
                    buckets[bucket] = i + 1;
                }
            }

            if (replaceArrays)
            {
                ReturnArrays();
                _buckets = buckets;
                _entries = entries;
            }
            _size = newSize;
        }

        // The overload Remove(TKey key, out TValue value) is a copy of this method with one additional
        // statement to copy the value for entry being removed into the output parameter.
        // Code has been intentionally duplicated for performance reasons.

        /// <summary>
        /// Removes the item with the given key.
        /// Returns true if the key was found and the value successfully removed.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>Returns true if the key was found and removed.</returns>
        public bool Remove(TKey key)
        {
            if (key is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            int[] buckets = _buckets;
            var entries = _entries;
            int collisionCount = 0;
            if (_size > 0)
            {
                uint hashCode = (uint)(_comparer?.GetHashCode(key) ?? key.GetHashCode());
                uint bucket = hashCode % (uint)_size;
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

                        Debug.Assert((s_startOfFreeList - _freeList) < 0, "shouldn't underflow because max hashtable length is MaxPrimeArrayLength = 0x7FEFFFFD(2146435069) _freelist underflow threshold 2147483646");
                        entry.next = s_startOfFreeList - _freeList;

                        if (_clearKeyOnFree)
                            entry.key = default!;
                        if (_clearValueOnFree)
                            entry.value = default!;
                        _freeList = i;
                        _freeCount++;
                        _version++;
                        return true;
                    }

                    last = i;
                    i = entry.next;
                    if (collisionCount >= _size)
                    {
                        // The chain of entries forms a loop; which means a concurrent update has happened.
                        // Break out of the loop and throw, rather than looping forever.
                        ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                    }
                    collisionCount++;
                }
            }
            return false;
        }

        // This overload is a copy of the overload Remove(TKey key) with one additional
        // statement to copy the value for entry being removed into the output parameter.
        // Code has been intentionally duplicated for performance reasons.

        /// <summary>
        /// Removes the item with the given key.
        /// If the key was found, returns true and sets the <paramref name="value"/> parameter to the removed value.
        /// If the key was not found, returns false and sets the <paramref name="value"/> parameter to the default value for the type.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The removed value</param>
        /// <returns>Returns true if the key was found and removed.</returns>
        public bool Remove(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (_size == 0)
            {
                value = default!;
                return false;
            }

            int[] buckets = _buckets;
            var entries = _entries;
            int collisionCount = 0;
            uint hashCode = (uint)(_comparer?.GetHashCode(key) ?? key.GetHashCode());
            uint bucket = hashCode % (uint)_size;
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

                    Debug.Assert((s_startOfFreeList - _freeList) < 0, "shouldn't underflow because max hashtable length is MaxPrimeArrayLength = 0x7FEFFFFD(2146435069) _freelist underflow threshold 2147483646");
                    entry.next = s_startOfFreeList - _freeList;

                    if (_clearKeyOnFree)
                        entry.key = default!;
                    if (_clearValueOnFree)
                        entry.value = default!;
                    _freeList = i;
                    _freeCount++;
                    _version++;
                    return true;
                }

                last = i;
                i = entry.next;
                if (collisionCount >= _size)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }
            value = default!;
            return false;
        }

        /// <summary>
        /// Attempts to get the value corresponding to the given key.
        /// If the key was found, returns true and sets the <paramref name="value"/> parameter to the corresponding value.
        /// If the key was not found, returns false and sets the <paramref name="value"/> to the default value for the type.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The returned value</param>
        /// <returns>True if the key was found, otherwise false.</returns>
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = _entries[i].value;
                return true;
            }
            value = default!;
            return false;
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => TryGetValue(key, out value!);
        bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => TryGetValue(key, out value!);

        /// <summary>
        /// Attempts to add the given key-value pair, if the key was not already present.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value to add</param>
        /// <returns>Returns true if the value was successfully added, false if the key was already present and the value was not added.</returns>
        public bool TryAdd(TKey key, TValue value)
            => TryInsert(key, value, InsertionBehavior.None);

        /// <summary>
        /// Gets the current value associated with the given key. If the key was not found,
        /// adds <paramref name="addValue"/> with the given key and returns it.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="addValue">The value to add if the key was not found</param>
        /// <returns>Either the current value or the just-added value.</returns>
        public TValue GetOrAdd(TKey key, TValue addValue)
        {
#pragma warning disable CS8717 // A member returning a [MaybeNull] value introduces a null value for a type parameter.
            if (TryGetValue(key, out var value))
                return value;
#pragma warning restore CS8717

            Add(key, addValue);
            return addValue;
        }

        /// <summary>
        /// Gets the current value associated with the given key. If the key was not found,
        /// runs <paramref name="valueFactory"/> and adds the result with the given key, returning the newly-added value.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="valueFactory">A function that will be used to generate a value to add if the key was not found</param>
        /// <returns>Either the current value or the just-added value.</returns>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
#pragma warning disable CS8717 // A member returning a [MaybeNull] value introduces a null value for a type parameter.
            if (TryGetValue(key, out var value))
                return value;
#pragma warning restore CS8717

            var addValue = valueFactory(key);
            Add(key, addValue);
            return addValue;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
            => CopyTo(array, index);

        void ICollection.CopyTo(Array array, int index)
        {
            if (array is null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            if (array.Rank != 1)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            if (array.GetLowerBound(0) != 0)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            if ((uint)index > (uint)array.Length)
                ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            if (array.Length - index < Count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

            if (array is KeyValuePair<TKey, TValue>[] pairs)
            {
                CopyTo(pairs, index);
            }
            else if (array is DictionaryEntry[] dictEntryArray)
            {
                var entries = _entries;
                for (int i = 0; i < _count; i++)
                {
                    if (entries[i].next >= -1)
                    {
                        dictEntryArray[index++] = new DictionaryEntry(entries[i].key, entries[i].value);
                    }
                }
            }
            else if (array is object[] objects)
            {
                try
                {
                    int count = _count;
                    var entries = _entries;
                    for (int i = 0; i < count; i++)
                    {
                        if (entries[i].next >= -1)
                        {
                            objects[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                        }
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }
            }
            else
            {
                ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
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
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            int currentCapacity = _size;
            if (currentCapacity >= capacity)
                return currentCapacity;
            _version++;
            if (_buckets.Length == 0 || _size == 0)
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

            var oldEntries = _entries;
            int[] oldBuckets = _buckets;
            int currentCapacity = oldEntries.Length;
            if (newSize >= currentCapacity)
                return;

            int oldCount = _count;
            _version++;
            Initialize(newSize);
            var entries = _entries;
            int[] buckets = _buckets;
            int count = 0;
            for (int i = 0; i < oldCount; i++)
            {
                uint hashCode = oldEntries[i].hashCode; // At this point, we know we have entries.
                if (oldEntries[i].next >= -1)
                {
                    ref Entry entry = ref entries[count];
                    entry = oldEntries[i];
                    uint bucket = hashCode % (uint)newSize;
                    // Value in _buckets is 1-based
                    entry.next = buckets[bucket] - 1; // If we get here, we have entries, therefore buckets is not null.
                    // Value in _buckets is 1-based
                    buckets[bucket] = count + 1;
                    count++;
                }
            }
            _count = count;
            _size = newSize;
            _freeCount = 0;
            s_bucketPool.Return(oldBuckets);
            s_entryPool.Return(oldEntries, clearArray: _clearKeyOnFree || _clearValueOnFree);
        }

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

        bool IDictionary.IsFixedSize => false;

        bool IDictionary.IsReadOnly => false;

        ICollection IDictionary.Keys => Keys;

        ICollection IDictionary.Values => Values;

        object? IDictionary.this[object key]
        {
            get
            {
                if (IsCompatibleKey(key))
                {
                    int i = FindEntry((TKey)key);
                    if (i >= 0)
                    {
                        return _entries[i].value;
                    }
                }
                return null;
            }
            set
            {
                if (key is null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
                }
                ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);

                try
                {
                    var tempKey = (TKey)key;
                    try
                    {
                        this[tempKey] = (TValue)value!;
                    }
                    catch (InvalidCastException)
                    {
                        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                    }
                }
                catch (InvalidCastException)
                {
                    ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
                }
            }
        }

        private void ReturnArrays()
        {
            if (_entries.Length > 0)
            {
                try
                {
                    s_entryPool.Return(_entries, clearArray: _clearKeyOnFree || _clearValueOnFree);
                }
                catch (ArgumentException)
                {
                    // oh well, the array pool didn't like our array
                }
            }

            if (_buckets.Length > 0)
            {
                try
                {
                    s_bucketPool.Return(_buckets);
                }
                catch (ArgumentException)
                {
                    // shucks
                }
            }

            _entries = Array.Empty<Entry>();
            _buckets = Array.Empty<int>();
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }
            return key is TKey;
        }

        void IDictionary.Add(object key, object? value)
        {
            if (key is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);

            try
            {
                var tempKey = (TKey)key;

                try
                {
                    Add(tempKey, (TValue)value!);
                }
                catch (InvalidCastException)
                {
                    ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                }
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
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

        /// <summary>
        /// Returns the underlying storage to the pool and sets the Count to zero.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns the underlying storage to the pool and sets the Count to zero.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReturnArrays();
                _count = 0;
                _size = 0;
                _freeList = -1;
                _freeCount = 0;
            }
        }

        /// <summary>
        /// Enumerates dictionary keys and values.
        /// </summary>
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

            /// <summary>
            /// Moves to the next item, returning false if there are no more items.
            /// </summary>
            public bool MoveNext()
            {
                if (_version != _dictionary._version)
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                // Use unsigned comparison since we set index to dictionary.count+1 when the enumeration ends.
                // dictionary.count+1 could be negative if dictionary.count is int.MaxValue
                while ((uint)_index < (uint)_dictionary._count)
                {
                    ref Entry entry = ref _dictionary._entries[_index++];

                    if (entry.next >= -1)
                    {
                        _current = new KeyValuePair<TKey, TValue>(entry.key, entry.value);
                        return true;
                    }
                }

                _index = _dictionary._count + 1;
                _current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            /// <summary>
            /// The current item.
            /// </summary>
            public KeyValuePair<TKey, TValue> Current => _current;

            void IDisposable.Dispose()
            {
            }

            object? IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
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
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
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
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
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
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return _current.Key;
                }
            }

            object? IDictionaryEnumerator.Value
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return _current.Value;
                }
            }
        }

        /// <summary>
        /// A collection of dictionary keys.
        /// </summary>
        [DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        public sealed class KeyCollection : ICollection<TKey>, ICollection, IReadOnlyCollection<TKey>
        {
            private readonly PooledDictionary<TKey, TValue> _dictionary;

            /// <summary>
            /// Creates an instance of a KeyCollection
            /// </summary>
            /// <param name="dictionary"></param>
            public KeyCollection(PooledDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            }

            /// <summary>
            /// Gets the enumerator.
            /// </summary>
            public Enumerator GetEnumerator()
                => new Enumerator(_dictionary);

            /// <summary>
            /// Copies the dictionary keys to an array.
            /// </summary>
            public void CopyTo(TKey[] array, int index)
            {
                if (array is null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);

                if (index < 0 || index > array.Length)
                    ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();

                if (array.Length - index < _dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                int count = _dictionary._count;
                var entries = _dictionary._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].next >= -1) array[index++] = entries[i].key;
                }
            }

            /// <summary>
            ///  Copies the dictionary keys to a span.
            /// </summary>
            /// <param name="span"></param>
            public void CopyTo(Span<TKey> span)
            {
                if (span.Length < _dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                int index = 0;
                int count = _dictionary._count;
                var entries = _dictionary._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].next >= -1) span[index++] = entries[i].key;
                }
            }

            /// <summary>
            /// The number of items in the collection.
            /// </summary>
            public int Count => _dictionary.Count;

            bool ICollection<TKey>.IsReadOnly => true;

            void ICollection<TKey>.Add(TKey item)
                => ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);

            void ICollection<TKey>.Clear()
                => ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);

            bool ICollection<TKey>.Contains(TKey item)
                => _dictionary.ContainsKey(item);

            bool ICollection<TKey>.Remove(TKey item)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
                return false;
            }

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
                => new Enumerator(_dictionary);

            IEnumerator IEnumerable.GetEnumerator()
                => new Enumerator(_dictionary);

            void ICollection.CopyTo(Array array, int index)
            {
                if (array is null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                if (array.Rank != 1)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                if (array.GetLowerBound(0) != 0)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                if ((uint)index > (uint)array.Length)
                    ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
                if (array.Length - index < _dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                if (array is TKey[] keys)
                {
                    CopyTo(keys, index);
                }
                else if (array is object[] objects)
                {
                    int count = _dictionary._count;
                    var entries = _dictionary._entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].next >= -1) objects[index++] = entries[i].key;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                    }
                }
                else
                {
                    ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }
            }

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;

            /// <summary>
            /// The enumerator.
            /// </summary>
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
                    _currentKey = default!;
                }

                void IDisposable.Dispose()
                {
                }

                /// <summary>
                /// Moves to the next value, returning false if there are no more values.
                /// </summary>
                /// <returns></returns>
                public bool MoveNext()
                {
                    if (_version != _dictionary._version)
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }

                    while ((uint)_index < (uint)_dictionary._count)
                    {
                        ref Entry entry = ref _dictionary._entries[_index++];

                        if (entry.next >= -1)
                        {
                            _currentKey = entry.key;
                            return true;
                        }
                    }

                    _index = _dictionary._count + 1;
                    _currentKey = default!;
                    return false;
                }

                /// <summary>
                /// The current value.
                /// </summary>
                public TKey Current => _currentKey;

                object? IEnumerator.Current
                {
                    get
                    {
                        if (_index == 0 || (_index == _dictionary._count + 1))
                        {
                            ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                        }

                        return _currentKey;
                    }
                }

                void IEnumerator.Reset()
                {
                    if (_version != _dictionary._version)
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }

                    _index = 0;
                    _currentKey = default!;
                }
            }
        }

        /// <summary>
        /// A collection of dicationary values.
        /// </summary>
        [DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        public sealed class ValueCollection : ICollection<TValue>, ICollection, IReadOnlyCollection<TValue>
        {
            private readonly PooledDictionary<TKey, TValue> _dictionary;

            /// <summary>
            /// Creates an instance of ValueCollection.
            /// </summary>
            public ValueCollection(PooledDictionary<TKey, TValue> dictionary)
            {
                if (dictionary is null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                }
                _dictionary = dictionary;
            }

            /// <summary>
            /// Gets the enumerator.
            /// </summary>
            public Enumerator GetEnumerator()
                => new Enumerator(_dictionary);

            /// <summary>
            /// Copies the dictionary values to an array.
            /// </summary>
            public void CopyTo(TValue[] array, int index)
            {
                if (array is null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);

                if (index < 0 || index > array.Length)
                    ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();

                if (array.Length - index < _dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                int count = _dictionary._count;
                var entries = _dictionary._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].next >= -1) array[index++] = entries[i].value;
                }
            }

            /// <summary>
            /// Copies the dictionary values to the given span.
            /// </summary>
            public void CopyTo(Span<TValue> span)
            {
                if (span.Length < _dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                int index = 0;
                int count = _dictionary._count;
                var entries = _dictionary._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].next >= -1) span[index++] = entries[i].value;
                }
            }

            /// <summary>
            /// The count of items.
            /// </summary>
            public int Count => _dictionary.Count;

            bool ICollection<TValue>.IsReadOnly => true;

            void ICollection<TValue>.Add(TValue item)
                => ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);

            bool ICollection<TValue>.Remove(TValue item)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
                return false;
            }

            void ICollection<TValue>.Clear()
                => ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);

            bool ICollection<TValue>.Contains(TValue item)
                => _dictionary.ContainsValue(item);

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
                => new Enumerator(_dictionary);

            IEnumerator IEnumerable.GetEnumerator()
                => new Enumerator(_dictionary);

            void ICollection.CopyTo(Array array, int index)
            {
                if (array is null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                if (array.Rank != 1)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                if (array.GetLowerBound(0) != 0)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                if ((uint)index > (uint)array.Length)
                    ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
                if (array.Length - index < _dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                if (array is TValue[] values)
                {
                    CopyTo(values, index);
                }
                else if (array is object[] objects)
                {
                    int count = _dictionary._count;
                    var entries = _dictionary._entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].next >= -1) objects[index++] = entries[i].value!;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                    }
                }
                else
                {
                    ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }
            }

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;

            /// <summary>
            /// A dictionary enumerator.
            /// </summary>
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
                    _currentValue = default!;
                }

                void IDisposable.Dispose()
                {
                }

                /// <summary>
                /// Moves to the next value, returning false if there are no more values.
                /// </summary>
                /// <returns></returns>
                public bool MoveNext()
                {
                    if (_version != _dictionary._version)
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }

                    while ((uint)_index < (uint)_dictionary._count)
                    {
                        ref Entry entry = ref _dictionary._entries[_index++];

                        if (entry.next >= -1)
                        {
                            _currentValue = entry.value;
                            return true;
                        }
                    }
                    _index = _dictionary._count + 1;
                    _currentValue = default!;
                    return false;
                }

                /// <summary>
                /// Returns the current value.
                /// </summary>
                public TValue Current => _currentValue;

                object? IEnumerator.Current
                {
                    get
                    {
                        if (_index == 0 || (_index == _dictionary._count + 1))
                        {
                            ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                        }

                        return _currentValue;
                    }
                }

                void IEnumerator.Reset()
                {
                    if (_version != _dictionary._version)
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }
                    _index = 0;
                    _currentValue = default!;
                }
            }
        }
    }
}
