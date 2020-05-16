// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledDictionary
{
    /// <summary>
    /// Contains tests that ensure the correctness of the PooledDictionary class.
    /// </summary>
    public abstract partial class Dictionary_Generic_Tests<TKey, TValue> : IDictionary_Generic_Tests<TKey, TValue>
    {
        public override bool SupportsJson => true;
        public override Type CollectionType => typeof(PooledDictionary<TKey, TValue>);

        #region IDictionary<TKey, TValue Helper Methods

        protected override IDictionary<TKey, TValue> GenericIDictionaryFactory()
        {
            var dict = new PooledDictionary<TKey, TValue>();
            RegisterForDispose(dict);
            return dict;
        }

        protected override Type ICollection_Generic_CopyTo_IndexLargerThanArrayCount_ThrowType => typeof(ArgumentOutOfRangeException);

        protected override void AddToCollection(ICollection<KeyValuePair<TKey, TValue>> collection, int numberOfItemsToAdd)
        {
            int seed = 9600;

            //var comparer = GetIEqualityComparer();
            var dict = (IDictionary<TKey, TValue>)collection;

            while (dict.Count < numberOfItemsToAdd)
            {
                var toAdd = CreateT(seed++);
                dict[toAdd.Key] = toAdd.Value;
            }
        }

        #endregion

        #region Constructors

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_Constructor_IDictionary(int count)
        {
            IDictionary<TKey, TValue> source = GenericIDictionaryFactory(count);
            var copied = new PooledDictionary<TKey, TValue>(source);
            Assert.Equal(source, copied);
            copied.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_Constructor_IDictionary_IEqualityComparer(int count)
        {
            IEqualityComparer<TKey> comparer = GetKeyIEqualityComparer();
            IDictionary<TKey, TValue> source = GenericIDictionaryFactory(count);
            var copied = new PooledDictionary<TKey, TValue>(source, comparer);
            Assert.Equal(source, copied);
            Assert.Equal(comparer, copied.Comparer);
            copied.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_Constructor_IEqualityComparer(int count)
        {
            IEqualityComparer<TKey> comparer = GetKeyIEqualityComparer();
            IDictionary<TKey, TValue> source = GenericIDictionaryFactory(count);
            var copied = new PooledDictionary<TKey, TValue>(source, comparer);
            Assert.Equal(source, copied);
            Assert.Equal(comparer, copied.Comparer);
            copied.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_Constructor_int(int count)
        {
            IDictionary<TKey, TValue> dictionary = new PooledDictionary<TKey, TValue>(count);
            RegisterForDispose(dictionary);
            Assert.Equal(0, dictionary.Count);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_Constructor_int_IEqualityComparer(int count)
        {
            IEqualityComparer<TKey> comparer = GetKeyIEqualityComparer();
            var dictionary = new PooledDictionary<TKey, TValue>(count, comparer);
            Assert.Equal(0, dictionary.Count);
            Assert.Equal(comparer, dictionary.Comparer);
            dictionary.Dispose();
        }

        #endregion

        #region ContainsValue

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_ContainsValue_NotPresent(int count)
        {
            PooledDictionary<TKey, TValue> dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            int seed = 4315;
            TValue notPresent = CreateTValue(seed++);
            while (dictionary.Values.Contains(notPresent))
                notPresent = CreateTValue(seed++);
            Assert.False(dictionary.ContainsValue(notPresent));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_ContainsValue_Present(int count)
        {
            PooledDictionary<TKey, TValue> dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            int seed = 4315;
            KeyValuePair<TKey, TValue> notPresent = CreateT(seed++);
            while (dictionary.Contains(notPresent))
                notPresent = CreateT(seed++);
            dictionary.Add(notPresent.Key, notPresent.Value);
            Assert.True(dictionary.ContainsValue(notPresent.Value));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_ContainsValue_DefaultValueNotPresent(int count)
        {
            PooledDictionary<TKey, TValue> dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            Assert.False(dictionary.ContainsValue(default));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_ContainsValue_DefaultValuePresent(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            int seed = 4315;
            TKey notPresent = CreateTKey(seed++);
            while (dictionary.ContainsKey(notPresent))
                notPresent = CreateTKey(seed++);
            dictionary.Add(notPresent, default);
            Assert.True(dictionary.ContainsValue(default));
        }

        #endregion

        #region IReadOnlyPooledDictionary<TKey, TValue>.Keys

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void IReadOnlyDictionary_Generic_Keys_ContainsAllCorrectKeys(int count)
        {
            IDictionary<TKey, TValue> dictionary = GenericIDictionaryFactory(count);
            IEnumerable<TKey> expected = dictionary.Select((pair) => pair.Key);
            IEnumerable<TKey> keys = ((IReadOnlyDictionary<TKey, TValue>)dictionary).Keys;
            Assert.True(expected.SequenceEqual(keys));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void IReadOnlyDictionary_Generic_Values_ContainsAllCorrectValues(int count)
        {
            IDictionary<TKey, TValue> dictionary = GenericIDictionaryFactory(count);
            IEnumerable<TValue> expected = dictionary.Select((pair) => pair.Value);
            IEnumerable<TValue> values = ((IReadOnlyDictionary<TKey, TValue>)dictionary).Values;
            Assert.True(expected.SequenceEqual(values));
        }

        #endregion

        #region GetOrAdd

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_GetOrAdd_ValidKeyNotContainedInDictionary_Value(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            TKey missingKey = GetNewKey(dictionary);
            TValue value = CreateTValue(5123);
            Assert.Equal(value, dictionary.GetOrAdd(missingKey, value));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_GetOrAdd_ValidKeyNotContainedInDictionary_Factory(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            TKey missingKey = GetNewKey(dictionary);
            TValue value = CreateTValue(5123);
            Assert.Equal(value, dictionary.GetOrAdd(missingKey, _ => CreateTValue(5123)));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_GetOrAdd_ValidKeyContainedInDictionary_Value(int count)
        {
            if (!IsReadOnly)
            {
                var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
                TKey missingKey = GetNewKey(dictionary);
                TValue value = CreateTValue(5123);
                dictionary.TryAdd(missingKey, value);
                Assert.Equal(value, dictionary.GetOrAdd(missingKey, value));
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_GetOrAdd_ValidKeyContainedInDictionary_Factory(int count)
        {
            if (!IsReadOnly)
            {
                var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
                TKey missingKey = GetNewKey(dictionary);
                TValue value = CreateTValue(5123);
                dictionary.TryAdd(missingKey, value);
                Assert.Equal(value, dictionary.GetOrAdd(missingKey, _ => CreateTValue(5123)));
            }
        }

        #endregion
    }
}
