// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledDictionary
{
    /// <summary>
    /// Contains tests that ensure the correctness of the Dictionary class.
    /// </summary>
    public abstract partial class Dictionary_Generic_Tests<TKey, TValue> : IDictionary_Generic_Tests<TKey, TValue>
    {
        #region Remove(TKey)

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_RemoveKey_ValidKeyNotContainedInDictionary(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            var missingKey = GetNewKey(dictionary);

            Assert.False(dictionary.Remove(missingKey, out var value));
            Assert.Equal(count, dictionary.Count);
            Assert.Equal(default(TValue), value);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_RemoveKey_ValidKeyContainedInDictionary(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            var missingKey = GetNewKey(dictionary);
            var inValue = CreateTValue(count);

            dictionary.Add(missingKey, inValue);
            Assert.True(dictionary.Remove(missingKey, out var outValue));
            Assert.Equal(count, dictionary.Count);
            Assert.Equal(inValue, outValue);
            Assert.False(dictionary.TryGetValue(missingKey, out outValue));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_RemoveKey_DefaultKeyNotContainedInDictionary(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            TValue outValue;

            if (DefaultValueAllowed)
            {
                var missingKey = default(TKey);
                while (dictionary.ContainsKey(missingKey))
                {
                    dictionary.Remove(missingKey);
                }

                Assert.False(dictionary.Remove(missingKey, out outValue));
                Assert.Equal(default(TValue), outValue);
            }
            else
            {
                var initValue = CreateTValue(count);
                outValue = initValue;
                Assert.Throws<ArgumentNullException>(() => dictionary.Remove(default(TKey), out outValue));
                Assert.Equal(initValue, outValue);
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_RemoveKey_DefaultKeyContainedInDictionary(int count)
        {
            if (DefaultValueAllowed)
            {
                var dictionary = (PooledDictionary<TKey, TValue>)(GenericIDictionaryFactory(count));
                var missingKey = default(TKey);

                dictionary.TryAdd(missingKey, default(TValue));
                Assert.True(dictionary.Remove(missingKey, out var value));
            }
        }

        [Fact]
        public void Dictionary_Generic_Remove_RemoveFirstEnumerationContinues()
        {
            var dict = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(3);
            using (var enumerator = dict.GetEnumerator())
            {
                enumerator.MoveNext();
                var key = enumerator.Current.Key;
                enumerator.MoveNext();
                dict.Remove(key);
                Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
            }
        }

        [Fact]
        public void Dictionary_Generic_Remove_RemoveCurrentEnumerationContinues()
        {
            var dict = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(3);
            using (var enumerator = dict.GetEnumerator())
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                dict.Remove(enumerator.Current.Key);
                Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
            }
        }

        [Fact]
        public void Dictionary_Generic_Remove_RemoveLastEnumerationFinishes()
        {
            var dict = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(3);
            TKey key = default;
            using (var enumerator = dict.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    key = enumerator.Current.Key;
                }
            }
            using (var enumerator = dict.GetEnumerator())
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                dict.Remove(key);
                Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
            }
        }

        #endregion

        #region EnsureCapacity

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void EnsureCapacity_Generic_RequestingLargerCapacity_DoesInvalidateEnumeration(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)(GenericIDictionaryFactory(count));
            int capacity = dictionary.EnsureCapacity(0);
            var enumerator = dictionary.GetEnumerator();

            dictionary.EnsureCapacity(capacity + 1); // Verify EnsureCapacity does invalidate enumeration

            Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        }

        [Fact]
        public void EnsureCapacity_Generic_NegativeCapacityRequested_Throws()
        {
            var dictionary = new PooledDictionary<TKey, TValue>();
            AssertExtensions.Throws<ArgumentOutOfRangeException>("capacity", () => dictionary.EnsureCapacity(-1));
        }

        [Fact]
        public void EnsureCapacity_Generic_DictionaryNotInitialized_RequestedZero_ReturnsZero()
        {
            var dictionary = new PooledDictionary<TKey, TValue>();
            Assert.Equal(0, dictionary.EnsureCapacity(0));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void EnsureCapacity_Generic_DictionaryNotInitialized_RequestedNonZero_CapacityIsSetToAtLeastTheRequested(int requestedCapacity)
        {
            var dictionary = new PooledDictionary<TKey, TValue>();
            Assert.InRange(dictionary.EnsureCapacity(requestedCapacity), requestedCapacity, Int32.MaxValue);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        public void EnsureCapacity_Generic_RequestedCapacitySmallerThanCurrent_CapacityUnchanged(int currentCapacity)
        {
            PooledDictionary<TKey, TValue> dictionary;

            // assert capacity remains the same when ensuring a capacity smaller or equal than existing
            for (int i = 0; i <= currentCapacity; i++)
            {
                dictionary = new PooledDictionary<TKey, TValue>(currentCapacity);
                Assert.Equal(currentCapacity, dictionary.EnsureCapacity(i));
            }
        }

        [Theory]
        [InlineData(7)]
        public void EnsureCapacity_Generic_ExistingCapacityRequested_SameValueReturned(int capacity)
        {
            var dictionary = new PooledDictionary<TKey, TValue>(capacity);
            Assert.Equal(capacity, dictionary.EnsureCapacity(capacity));

            dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(capacity);
            Assert.Equal(capacity, dictionary.EnsureCapacity(capacity));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void EnsureCapacity_Generic_EnsureCapacityCalledTwice_ReturnsSameValue(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            int capacity = dictionary.EnsureCapacity(0);
            Assert.Equal(capacity, dictionary.EnsureCapacity(0));

            dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            capacity = dictionary.EnsureCapacity(count);
            Assert.Equal(capacity, dictionary.EnsureCapacity(count));

            dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            capacity = dictionary.EnsureCapacity(count + 1);
            Assert.Equal(capacity, dictionary.EnsureCapacity(count + 1));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(7)]
        public void EnsureCapacity_Generic_DictionaryNotEmpty_RequestedSmallerThanCount_ReturnsAtLeastSizeOfCount(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            Assert.InRange(dictionary.EnsureCapacity(count - 1), count, Int32.MaxValue);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(20)]
        public void EnsureCapacity_Generic_DictionaryNotEmpty_SetsToAtLeastTheRequested(int count)
        {
            var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);

            // get current capacity
            int currentCapacity = dictionary.EnsureCapacity(0);

            // assert we can update to a larger capacity
            int newCapacity = dictionary.EnsureCapacity(currentCapacity * 2);
            Assert.InRange(newCapacity, currentCapacity * 2, Int32.MaxValue);
        }

        [Fact]
        public void EnsureCapacity_Generic_CapacityIsSetToPrimeNumberLargerOrEqualToRequested()
        {
            var dictionary = new PooledDictionary<TKey, TValue>();
            Assert.Equal(17, dictionary.EnsureCapacity(17));

            dictionary = new PooledDictionary<TKey, TValue>();
            Assert.Equal(17, dictionary.EnsureCapacity(15));

            dictionary = new PooledDictionary<TKey, TValue>();
            Assert.Equal(17, dictionary.EnsureCapacity(13));
        }

        #endregion

        #region TrimExcess

        [Fact]
        public void TrimExcess_Generic_NegativeCapacity_Throw()
        {
            using var dictionary = new PooledDictionary<TKey, TValue>();
            AssertExtensions.Throws<ArgumentOutOfRangeException>("capacity", () => dictionary.TrimExcess(-1));
        }

        [Theory]
        [InlineData(20)]
        [InlineData(23)]
        public void TrimExcess_Generic_CapacitySmallerThanCount_Throws(int suggestedCapacity)
        {
            var dictionary = new PooledDictionary<TKey, TValue>();
            dictionary.Add(GetNewKey(dictionary), CreateTValue(0));
            AssertExtensions.Throws<ArgumentOutOfRangeException>("capacity", () => dictionary.TrimExcess(0));
            dictionary.Dispose();

            dictionary = new PooledDictionary<TKey, TValue>(suggestedCapacity);
            dictionary.Add(GetNewKey(dictionary), CreateTValue(0));
            AssertExtensions.Throws<ArgumentOutOfRangeException>("capacity", () => dictionary.TrimExcess(0));
            dictionary.Dispose();
        }

        [Fact]
        public void TrimExcess_Generic_LargeInitialCapacity_TrimReducesSize()
        {
            using var dictionary = new PooledDictionary<TKey, TValue>(20);
            dictionary.TrimExcess(7);
            Assert.Equal(7, dictionary.EnsureCapacity(0));
        }

        [Theory]
        [InlineData(20)]
        [InlineData(23)]
        public void TrimExcess_Generic_TrimToLargerThanExistingCapacity_DoesNothing(int suggestedCapacity)
        {
            var dictionary = new PooledDictionary<TKey, TValue>();
            int capacity = dictionary.EnsureCapacity(0);
            dictionary.TrimExcess(suggestedCapacity);
            Assert.Equal(capacity, dictionary.EnsureCapacity(0));
            dictionary.Dispose();

            dictionary = new PooledDictionary<TKey, TValue>(suggestedCapacity / 2);
            capacity = dictionary.EnsureCapacity(0);
            dictionary.TrimExcess(suggestedCapacity);
            Assert.Equal(capacity, dictionary.EnsureCapacity(0));
            dictionary.Dispose();
        }

        [Fact]
        public void TrimExcess_Generic_DictionaryNotInitialized_CapacityRemainsAsMinPossible()
        {
            using var dictionary = new PooledDictionary<TKey, TValue>();
            Assert.Equal(0, dictionary.EnsureCapacity(0));
            dictionary.TrimExcess();
            Assert.Equal(0, dictionary.EnsureCapacity(0));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(85)]
        [InlineData(89)]
        public void TrimExcess_Generic_ClearThenTrimNonEmptyDictionary_SetsCapacityTo3(int count)
        {
            using var dictionary = (PooledDictionary<TKey, TValue>)GenericIDictionaryFactory(count);
            Assert.Equal(count, dictionary.Count);
            // The smallest possible capacity size after clearing a dictionary is 3
            dictionary.Clear();
            dictionary.TrimExcess();
            Assert.Equal(3, dictionary.EnsureCapacity(0));
        }

        [Theory]
        [InlineData(85)]
        [InlineData(89)]
        public void TrimExcess_NoArguments_TrimsToAtLeastCount(int count)
        {
            using var dictionary = new PooledDictionary<int, int>(20);
            for (int i = 0; i < count; i++)
            {
                dictionary.Add(i, 0);
            }
            dictionary.TrimExcess();
            Assert.InRange(dictionary.EnsureCapacity(0), count, Int32.MaxValue);
        }

        [Theory]
        [InlineData(85)]
        [InlineData(89)]
        public void TrimExcess_WithArguments_OnDictionaryWithManyElementsRemoved_TrimsToAtLeastRequested(int finalCount)
        {
            const int InitToFinalRatio = 10;
            int initialCount = InitToFinalRatio * finalCount;
            using var dictionary = new PooledDictionary<int, int>(initialCount);
            Assert.InRange(dictionary.EnsureCapacity(0), initialCount, Int32.MaxValue);
            for (int i = 0; i < initialCount; i++)
            {
                dictionary.Add(i, 0);
            }
            for (int i = 0; i < initialCount - finalCount; i++)
            {
                dictionary.Remove(i);
            }
            for (int i = InitToFinalRatio; i > 0; i--)
            {
                dictionary.TrimExcess(i * finalCount);
                Assert.InRange(dictionary.EnsureCapacity(0), i * finalCount, Int32.MaxValue);
            }
        }

        [Theory]
        [InlineData(1000, 900, 5000, 85, 89)]
        [InlineData(1000, 400, 5000, 85, 89)]
        [InlineData(1000, 900, 500, 85, 89)]
        [InlineData(1000, 400, 500, 85, 89)]
        [InlineData(1000, 400, 500, 1, 3)]
        public void TrimExcess_NoArgument_TrimAfterEachBulkAddOrRemove_TrimsToAtLeastCount(int initialCount, int numRemove, int numAdd, int newCount, int newCapacity)
        {
            var random = new Random(32);
            using var dictionary = new PooledDictionary<int, int>();
            dictionary.TrimExcess();
            Assert.InRange(dictionary.EnsureCapacity(0), dictionary.Count, Int32.MaxValue);

            int[] initialKeys = new int[initialCount];
            for (int i = 0; i < initialCount; i++)
            {
                initialKeys[i] = i;
            }
            random.Shuffle(initialKeys);
            foreach (int key in initialKeys)
            {
                dictionary.Add(key, 0);
            }
            dictionary.TrimExcess();
            Assert.InRange(dictionary.EnsureCapacity(0), dictionary.Count, Int32.MaxValue);

            random.Shuffle(initialKeys);
            for (int i = 0; i < numRemove; i++)
            {
                dictionary.Remove(initialKeys[i]);
            }
            dictionary.TrimExcess();
            Assert.InRange(dictionary.EnsureCapacity(0), dictionary.Count, Int32.MaxValue);

            int[] moreKeys = new int[numAdd];
            for (int i = 0; i < numAdd; i++)
            {
                moreKeys[i] = i + initialCount;
            }
            random.Shuffle(moreKeys);
            foreach (int key in moreKeys)
            {
                dictionary.Add(key, 0);
            }
            int currentCount = dictionary.Count;
            dictionary.TrimExcess();
            Assert.InRange(dictionary.EnsureCapacity(0), currentCount, Int32.MaxValue);

            int[] existingKeys = new int[currentCount];
            Array.Copy(initialKeys, numRemove, existingKeys, 0, initialCount - numRemove);
            Array.Copy(moreKeys, 0, existingKeys, initialCount - numRemove, numAdd);
            random.Shuffle(existingKeys);
            for (int i = 0; i < currentCount - newCount; i++)
            {
                dictionary.Remove(existingKeys[i]);
            }
            dictionary.TrimExcess();
            int finalCapacity = dictionary.EnsureCapacity(0);
            Assert.InRange(finalCapacity, newCount, initialCount);
            Assert.Equal(newCapacity, finalCapacity);
        }

        [Fact]
        public void TrimExcess_DictionaryHasElementsChainedWithSameHashcode_Success()
        {
            using var dictionary = new PooledDictionary<string, int>(7);
            for (int i = 0; i < 4; i++)
            {
                dictionary.Add(i.ToString(), 0);
            }
            string[] s_64bit = new string[] { "95e85f8e-67a3-4367-974f-dd24d8bb2ca2", "eb3d6fe9-de64-43a9-8f58-bddea727b1ca" };
            string[] s_32bit = new string[] { "25b1f130-7517-48e3-96b0-9da44e8bfe0e", "ba5a3625-bc38-4bf1-a707-a3cfe2158bae" };
            string[] chained = (Environment.Is64BitProcess ? s_64bit : s_32bit).ToArray();
            dictionary.Add(chained[0], 0);
            dictionary.Add(chained[1], 0);
            for (int i = 0; i < 4; i++)
            {
                dictionary.Remove(i.ToString());
            }
            dictionary.TrimExcess(3);
            Assert.Equal(2, dictionary.Count);
            Assert.True(dictionary.TryGetValue(chained[0], out int _));
            Assert.True(dictionary.TryGetValue(chained[1], out _));
        }

        [Fact]
        public void TrimExcess_Generic_DoesInvalidateEnumeration()
        {
            using var dictionary = new PooledDictionary<TKey, TValue>(20);
            var enumerator = dictionary.GetEnumerator();

            dictionary.TrimExcess(7); // Verify TrimExcess does invalidate enumeration

            Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        }

        #endregion
    }
}
