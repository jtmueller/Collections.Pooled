using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Collections.Pooled
{
#if NETCOREAPP3_0
    /// <summary>
    /// <para>This is equivalend to a PooledDictionary&lt;string, T&gt; but also allows for lookups using
    /// a key of type ReadOnlySpan&lt;char&gt;. This allows for lookups based on substrings without
    /// the need to allocate substrings.</para>
    /// <para>
    /// NOTE: Custom string comparers are not supported. Only the 
    /// following non-default comparers are supported:
    /// <see cref="StringComparer.Ordinal"/>, <see cref="StringComparer.OrdinalIgnoreCase"/>,
    /// <see cref="StringComparer.InvariantCulture"/>, <see cref="StringComparer.InvariantCultureIgnoreCase"/>
    /// </para>
    /// </summary>
    [Serializable]
    public sealed class StringKeyedDictionary<T> : PooledDictionary<string, T>
    {
        private readonly StringComparison _stringComparison;

        public StringKeyedDictionary() : this(0, ClearMode.Auto, null) { }

        public StringKeyedDictionary(int capacity) : this(capacity, ClearMode.Auto, null) { }

        public StringKeyedDictionary(int capacity, IEqualityComparer<string>? comparer) : this(capacity, ClearMode.Auto, comparer) { }

        public StringKeyedDictionary(int capacity, ClearMode clearMode, IEqualityComparer<string>? comparer) 
            : base(capacity, clearMode, comparer)
        {
            var cmp = _comparer;
            if (cmp is null || ReferenceEquals(cmp, NonRandomizedStringEqualityComparer.Default) ||
                ReferenceEquals(cmp, StringComparer.Ordinal))
            {
                _stringComparison = StringComparison.Ordinal;
            }
            else if (ReferenceEquals(cmp, StringComparer.OrdinalIgnoreCase))
            {
                _stringComparison = StringComparison.OrdinalIgnoreCase;
            }
            else if (ReferenceEquals(cmp, StringComparer.InvariantCulture))
            {
                _stringComparison = StringComparison.InvariantCulture;
            }
            else if (ReferenceEquals(cmp, StringComparer.InvariantCultureIgnoreCase))
            {
                _stringComparison = StringComparison.InvariantCultureIgnoreCase;
            }
            else
            {
                throw new ArgumentException("Unsupported comparer: only Ordinal and InvariantCulture (and ignore-case versions) supported.", nameof(comparer));
            }
        }

        /// <summary>
        /// Creates a new instance of PooledDictionary.
        /// </summary>
        private StringKeyedDictionary(SerializationInfo info, StreamingContext ctx) : base(info, ctx)
        {
        }

        /// <summary>
        /// The <see cref="IEqualityComparer{TKey}"/> used to compare keys in this dictionary.
        /// </summary>
        public override IEqualityComparer<string> Comparer
            => (_comparer is null || _comparer is NonRandomizedStringEqualityComparer)
                    ? StringComparer.Ordinal : _comparer;

        /// <summary>
        /// Gets or sets an item in the dictionary by the key.
        /// </summary>
        public T this[ReadOnlySpan<char> key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0) return Entries[i].value;
                ThrowHelper.ThrowKeyNotFoundException(key.ToString());
                return default!;
            }
            set
            {
                bool modified = TryInsert(key.ToString(), value, InsertionBehavior.OverwriteExisting);
                Debug.Assert(modified);
            }
        }

        /// <summary>
        /// Attempts to get the value corresponding to the given key.
        /// If the key was found, returns true and sets the <paramref name="value"/> parameter to the corresponding value.
        /// If the key was not found, returns false and sets the <paramref name="value"/> to the default value for the type.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The returned value</param>
        /// <returns>True if the key was found, otherwise false.</returns>
        public bool TryGetValue(ReadOnlySpan<char> key, [MaybeNullWhen(false)] out T value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = Entries[i].value;
                return true;
            }
            value = default!;
            return false;
        }

        /// <summary>
        /// Returns true if the dictionary contains the given key.
        /// </summary>
        public bool ContainsKey(ReadOnlySpan<char> key)
            => FindEntry(key) >= 0;

        /// <summary>
        /// Gets the current value associated with the given key. If the key was not found,
        /// adds <paramref name="addValue"/> with the given key and returns it.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="addValue">The value to add if the key was not found</param>
        /// <returns>Either the current value or the just-added value.</returns>
        public T GetOrAdd(ReadOnlySpan<char> key, T addValue)
        {
#pragma warning disable CS8717 // A member returning a [MaybeNull] value introduces a null value for a type parameter.
            if (TryGetValue(key, out var value))
                return value;
#pragma warning restore CS8717

            Add(key.ToString(), addValue);
            return addValue;
        }

        /// <summary>
        /// Gets the current value associated with the given key. If the key was not found,
        /// runs <paramref name="valueFactory"/> and adds the result with the given key, returning the newly-added value.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="valueFactory">A function that will be used to generate a value to add if the key was not found</param>
        /// <returns>Either the current value or the just-added value.</returns>
        public T GetOrAdd(ReadOnlySpan<char> key, Func<string, T> valueFactory)
        {
#pragma warning disable CS8717 // A member returning a [MaybeNull] value introduces a null value for a type parameter.
            if (TryGetValue(key, out var value))
                return value;
#pragma warning restore CS8717

            var keyStr = key.ToString();
            var addValue = valueFactory(keyStr);
            Add(keyStr, addValue);
            return addValue;
        }

        private int FindEntry(ReadOnlySpan<char> key)
        {
            int i = -1;
            int length = Size;
            if (length <= 0)
                return i;
            var buckets = Buckets;
            var entries = Entries;
            int collisionCount = 0;
            var comparer = _comparer;
            var comparison = _stringComparison;

            if (comparer is null)
            {
                uint hashCode = (uint)string.GetHashCode(key, comparison);
                // Value in _buckets is 1-based
                i = buckets[(int)(hashCode % (uint)length)] - 1;


                // Object type: Shared Generic, EqualityComparer<TValue>.Default won't devirtualize
                // https://github.com/dotnet/coreclr/issues/17273
                // So cache in a local rather than get EqualityComparer per loop iteration
                do
                {
                    // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                    // Test in if to drop range check for following array access
                    if ((uint)i >= (uint)length || (entries[i].hashCode == hashCode && key.Equals(entries[i].key, comparison)))
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
                uint hashCode = comparer is NonRandomizedStringEqualityComparer nrsec
                    ? (uint)nrsec.GetHashCode(key)
                    : (uint)string.GetHashCode(key, comparison);
                // Value in _buckets is 1-based
                i = buckets[(int)(hashCode % (uint)length)] - 1;
                do
                {
                    // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                    // Test in if to drop range check for following array access
                    if ((uint)i >= (uint)length ||
                        (entries[i].hashCode == hashCode && key.Equals(entries[i].key, comparison)))
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
    }
#endif
}
