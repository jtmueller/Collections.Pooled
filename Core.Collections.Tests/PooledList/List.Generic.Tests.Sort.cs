// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.Tests;
using System.Linq;
using Xunit;

namespace Core.Collections.Tests
{
    /// <summary>
    /// Contains tests that ensure the correctness of the List class.
    /// </summary>
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        public static IEnumerable<object[]> ValidCollectionSizes_GreaterThanOne()
        {
            yield return new object[] { 2 };
            yield return new object[] { 20 };
        }

        #region Sort

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_WithoutDuplicates(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            IComparer<T> comparer = Comparer<T>.Default;
            list.Sort();
            Assert.All(Enumerable.Range(0, count - 2), i =>
            {
                Assert.True(comparer.Compare(list[i], list[i + 1]) < 0);
            });
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_WithDuplicates(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            list.Add(list[0]);
            IComparer<T> comparer = Comparer<T>.Default;
            list.Sort();
            Assert.All(Enumerable.Range(0, count - 2), i =>
            {
                Assert.True(comparer.Compare(list[i], list[i + 1]) <= 0);
            });
        }

        #endregion

        #region Sort(IComparer)

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_IComparer_WithoutDuplicates(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            IComparer<T> comparer = GetIComparer();
            list.Sort(comparer);
            Assert.All(Enumerable.Range(0, count - 2), i =>
            {
                Assert.True(comparer.Compare(list[i], list[i + 1]) < 0);
            });
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_IComparer_WithDuplicates(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            list.Add(list[0]);
            IComparer<T> comparer = GetIComparer();
            list.Sort(comparer);
            Assert.All(Enumerable.Range(0, count - 2), i =>
            {
                Assert.True(comparer.Compare(list[i], list[i + 1]) <= 0);
            });
        }

        #endregion

        #region Sort(Comparison)

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_Comparison_WithoutDuplicates(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            IComparer<T> iComparer = GetIComparer();
            Comparison<T> comparer = ((T first, T second) => { return iComparer.Compare(first, second); });
            list.Sort(comparer);
            Assert.All(Enumerable.Range(0, count - 2), i =>
            {
                Assert.True(iComparer.Compare(list[i], list[i + 1]) < 0);
            });
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_Comparison_WithDuplicates(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            list.Add(list[0]);
            IComparer<T> iComparer = GetIComparer();
            Comparison<T> comparer = ((T first, T second) => { return iComparer.Compare(first, second); });
            list.Sort(comparer);
            Assert.All(Enumerable.Range(0, count - 2), i =>
            {
                Assert.True(iComparer.Compare(list[i], list[i + 1]) <= 0);
            });
        }

        #endregion

        #region Sort(int, int, IComparer<T>)

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_intintIComparer_WithoutDuplicates(int count)
        {
            PooledList<T> unsortedList = GenericListFactory(count);
            IComparer<T> comparer = GetIComparer();
            for (int startIndex = 0; startIndex < count - 2; startIndex++)
                for (int sortCount = 1; sortCount < count - startIndex; sortCount++)
                {
                    using (var list = new PooledList<T>(unsortedList))
                    {
                        list.Sort(startIndex, sortCount + 1, comparer);
                        for (int i = startIndex; i < sortCount; i++)
                            Assert.InRange(comparer.Compare(list[i], list[i + 1]), int.MinValue, 0);
                    }
                }

            unsortedList.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes_GreaterThanOne))]
        public void Sort_intintIComparer_WithDuplicates(int count)
        {
            PooledList<T> unsortedList = GenericListFactory(count);
            IComparer<T> comparer = GetIComparer();
            unsortedList.Add(unsortedList[0]);
            for (int startIndex = 0; startIndex < count - 2; startIndex++)
                for (int sortCount = 2; sortCount < count - startIndex; sortCount++)
                {
                    using (var list = new PooledList<T>(unsortedList))
                    {
                        list.Sort(startIndex, sortCount + 1, comparer);
                        for (int i = startIndex; i < sortCount; i++)
                            Assert.InRange(comparer.Compare(list[i], list[i + 1]), int.MinValue, 1);
                    }
                }

            unsortedList.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Sort_intintIComparer_NegativeRange_ThrowsArgumentOutOfRangeException(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            (int, int)[] InvalidParameters = new[]
            {
                (-1,-1),
                (-1, 0),
                (-1, 1),
                (-1, 2),
                (-2, 0),
                (int.MinValue, 0),
                (0 ,-1),
                (0 ,-2),
                (0 , int.MinValue),
                (1 ,-1),
                (2 ,-1),
            };

            Assert.All(InvalidParameters, invalidSet =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => list.Sort(invalidSet.Item1, invalidSet.Item2, GetIComparer()));
            });

            list.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Sort_intintIComparer_InvalidRange_ThrowsArgumentException(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            var InvalidParameters = new[]
            {
                (count, 1),
                (count + 1, 0),
                (int.MaxValue, 0),
            };

            Assert.All(InvalidParameters, invalidSet =>
            {
                AssertExtensions.Throws<ArgumentException>(null, () => list.Sort(invalidSet.Item1, invalidSet.Item2, GetIComparer()));
            });

            list.Dispose();
        }

        #endregion
    }
}
