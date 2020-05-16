using System;
using System.Collections.Generic;
using Xunit;

namespace Collections.Pooled.Tests.StringKeyedDictionary
{
#if NETCOREAPP3_0
    /// <summary>
    /// Contains tests that ensure the correctness of the PooledDictionary class.
    /// </summary>
    public abstract partial class StringDictionary_Generic_Tests<TValue> : IDictionary_Generic_Tests<string, TValue>
    {
        public override bool SupportsJson => true;
        public override Type CollectionType => typeof(StringKeyedDictionary<TValue>);

        #region StringKeyedDictionary<TValue> Helper Methods

        protected override IDictionary<string, TValue> GenericIDictionaryFactory()
        {
            return RegisterForDispose(new StringKeyedDictionary<TValue>());
        }

        protected override Type ICollection_Generic_CopyTo_IndexLargerThanArrayCount_ThrowType => typeof(ArgumentOutOfRangeException);

        protected override void AddToCollection(ICollection<KeyValuePair<string, TValue>> collection, int numberOfItemsToAdd)
        {
            int seed = 9600;

            //var comparer = GetIEqualityComparer();
            var dict = (IDictionary<string, TValue>)collection;

            while (dict.Count < numberOfItemsToAdd)
            {
                var toAdd = CreateT(seed++);
                dict[toAdd.Key] = toAdd.Value;
            }
        }

        private static readonly int[] _sizes = new[] { 0, 1, 5, 75 };
        private static readonly StringComparer[] _comparers = new[] {
            null, StringComparer.Ordinal, StringComparer.OrdinalIgnoreCase,
            StringComparer.InvariantCulture, StringComparer.InvariantCultureIgnoreCase };

        public static IEnumerable<object[]> SizesAndComparers()
        {
            foreach (var comparer in _comparers)
            {
                foreach (var size in _sizes)
                {
                    yield return new object[] { size, comparer };
                }
            }
        }

        #endregion

        // Indexer
        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_Indexer(int count, IEqualityComparer<string> comparer)
        {
            using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
            AddToCollection(dictionary, count);
            foreach (var key in dictionary.Keys)
            {
                Assert.Equal(dictionary[key], dictionary[key.AsSpan()]);
            }

            var newKey = CreateTKey(count);
            var newValue = CreateTValue(count + 500);
            dictionary[newKey.AsSpan()] = newValue;
            Assert.Equal(dictionary[newKey], newValue);
            Assert.Equal(dictionary[newKey.AsSpan()], newValue);
        }

        #region TryGetValue

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_TryGetValue_ValidKeyNotContainedInDictionary(int count, IEqualityComparer<string> comparer)
        {
            using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
            AddToCollection(dictionary, count);
            var missingKey = GetNewKey(dictionary).AsSpan();
            TValue value = CreateTValue(5123);
            Assert.False(dictionary.TryGetValue(missingKey, out TValue outValue));
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_TryGetValue_ValidKeyContainedInDictionary(int count, IEqualityComparer<string> comparer)
        {
            if (!IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                string missingKey = GetNewKey(dictionary);
                TValue value = CreateTValue(5123);
                dictionary.TryAdd(missingKey, value);
                Assert.True(dictionary.TryGetValue(missingKey.AsSpan(), out TValue outValue));
                Assert.Equal(value, outValue);
            }
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_TryGetValue_DefaulstringContainedInDictionary(int count, IEqualityComparer<string> comparer)
        {
            if (DefaultValueAllowed && !IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                string missingKey = default;
                TValue value = CreateTValue(5123);
                dictionary.TryAdd(missingKey, value);
                Assert.True(dictionary.TryGetValue(missingKey.AsSpan(), out TValue outValue));
                Assert.Equal(value, outValue);
            }
        }

        #endregion

        #region ContainsKey

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_ContainsKey_ValidKeyNotContainedInDictionary(int count, IEqualityComparer<string> comparer)
        {
            if (!IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                var missingKey = GetNewKey(dictionary).AsSpan();
                Assert.False(dictionary.ContainsKey(missingKey));
            }
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_ContainsKey_ValidKeyContainedInDictionary(int count, IEqualityComparer<string> comparer)
        {
            if (!IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                string missingKey = GetNewKey(dictionary);
                dictionary.Add(missingKey, CreateTValue(34251));
                Assert.True(dictionary.ContainsKey(missingKey.AsSpan()));
            }
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_ContainsKey_DefaultKeyContainedInDictionary(int count, IEqualityComparer<string> comparer)
        {
            if (DefaultValueAllowed && !IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                string missingKey = default;
                if (!dictionary.ContainsKey(missingKey))
                    dictionary.Add(missingKey, CreateTValue(5341));
                Assert.True(dictionary.ContainsKey(missingKey.AsSpan()));
            }
        }

        #endregion

        #region GetOrAdd

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_Generic_GetOrAdd_ValidKeyNotContainedInDictionary_Value(int count, IEqualityComparer<string> comparer)
        {
            using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
            AddToCollection(dictionary, count);
            var missingKey = GetNewKey(dictionary).AsSpan();
            TValue value = CreateTValue(5123);
            Assert.Equal(value, dictionary.GetOrAdd(missingKey, value));
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_Generic_GetOrAdd_ValidKeyNotContainedInDictionary_Factory(int count, IEqualityComparer<string> comparer)
        {
            using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
            AddToCollection(dictionary, count);
            var missingKey = GetNewKey(dictionary).AsSpan();
            TValue value = CreateTValue(5123);
            Assert.Equal(value, dictionary.GetOrAdd(missingKey, _ => CreateTValue(5123)));
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_Generic_GetOrAdd_ValidKeyContainedInDictionary_Value(int count, IEqualityComparer<string> comparer)
        {
            if (!IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                var missingKey = GetNewKey(dictionary);
                TValue value = CreateTValue(5123);
                dictionary.TryAdd(missingKey, value);
                Assert.Equal(value, dictionary.GetOrAdd(missingKey.AsSpan(), value));
            }
        }

        [Theory]
        [MemberData(nameof(SizesAndComparers))]
        public void StringDictionary_Generic_GetOrAdd_ValidKeyContainedInDictionary_Factory(int count, IEqualityComparer<string> comparer)
        {
            if (!IsReadOnly)
            {
                using var dictionary = new StringKeyedDictionary<TValue>(count, comparer);
                AddToCollection(dictionary, count);
                var missingKey = GetNewKey(dictionary);
                TValue value = CreateTValue(5123);
                dictionary.TryAdd(missingKey, value);
                Assert.Equal(value, dictionary.GetOrAdd(missingKey.AsSpan(), _ => CreateTValue(5123)));
            }
        }

        #endregion
    }
#endif
}
