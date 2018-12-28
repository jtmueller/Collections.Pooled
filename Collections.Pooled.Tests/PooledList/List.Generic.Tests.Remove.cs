// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledList
{
    /// <summary>
    /// Contains tests that ensure the correctness of the List class.
    /// </summary>
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        #region RemoveAll(Pred<T>)

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void RemoveAll_AllElements(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            PooledList<T> beforeList = list.ToPooledList();
            int removedCount = list.RemoveAll((value) => { return true; });
            Assert.Equal(count, removedCount);
            Assert.Equal(0, list.Count);

            list.Dispose();
            beforeList.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void RemoveAll_NoElements(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            PooledList<T> beforeList = list.ToPooledList();
            int removedCount = list.RemoveAll((value) => { return false; });
            Assert.Equal(0, removedCount);
            Assert.Equal(count, list.Count);
            VerifyList(list, beforeList);

            list.Dispose();
            beforeList.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void RemoveAll_DefaultElements(int count)
        {
            PooledList<T> list = GenericListFactory(count);
            PooledList<T> beforeList = list.ToPooledList();
            bool EqualsDefaultElement(T value) { return default(T) == null ? value == null : default(T).Equals(value); }
            int expectedCount = beforeList.Count((value) => EqualsDefaultElement(value));
            int removedCount = list.RemoveAll(EqualsDefaultElement);
            Assert.Equal(expectedCount, removedCount);

            list.Dispose();
            beforeList.Dispose();
        }

        [Fact]
        public void RemoveAll_NullMatchPredicate()
        {
            AssertExtensions.Throws<ArgumentNullException>("match", () => new PooledList<T>().RemoveAll(null));
        }

        #endregion

        #region RemoveRange

        [Theory]
        [InlineData(10, 3, 3)]
        [InlineData(10, 0, 10)]
        [InlineData(10, 10, 0)]
        [InlineData(10, 5, 5)]
        [InlineData(10, 0, 5)]
        [InlineData(10, 1, 9)]
        [InlineData(10, 9, 1)]
        [InlineData(10, 2, 8)]
        [InlineData(10, 8, 2)]
        public void Remove_Range(int listLength, int index, int count)
        {
            PooledList<T> list = GenericListFactory(listLength);
            PooledList<T> beforeList = list.ToPooledList();

            list.RemoveRange(index, count);
            Assert.Equal(list.Count, listLength - count); //"Expected them to be the same."
            for (int i = 0; i < index; i++)
            {
                Assert.Equal(list[i], beforeList[i]); //"Expected them to be the same."
            }

            for (int i = index; i < count - (index + count); i++)
            {
                Assert.Equal(list[i], beforeList[i + count]); //"Expected them to be the same."
            }

            list.Dispose();
            beforeList.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void RemoveRange_InvalidParameters(int listLength)
        {
            if (listLength % 2 != 0)
                listLength++;
            PooledList<T> list = GenericListFactory(listLength);
            (int, int)[] InvalidParameters = new[]
            {
                (listLength     ,1             ),
                (listLength+1   ,0             ),
                (listLength+1   ,1             ),
                (listLength     ,2             ),
                (listLength/2   ,listLength/2+1),
                (listLength-1   ,2             ),
                (listLength-2   ,3             ),
                (1              ,listLength    ),
                (0              ,listLength+1  ),
                (1              ,listLength+1  ),
                (2              ,listLength    ),
                (listLength/2+1 ,listLength/2  ),
                (2              ,listLength-1  ),
                (3              ,listLength-2  ),
            };

            Assert.All(InvalidParameters, invalidSet =>
            {
                var (index, count) = invalidSet;
                //if (index >= 0 && count >= 0)
                    AssertExtensions.Throws<ArgumentOutOfRangeException>(null, () => list.RemoveRange(index, count));
            });

            list.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void RemoveRange_NegativeParameters(int listLength)
        {
            if (listLength % 2 != 0)
                listLength++;
            PooledList<T> list = GenericListFactory(listLength);
            (int, int)[] InvalidParameters = new[]
            {
                (-1,-1),
                (-1, 0),
                (-1, 1),
                (-1, 2),
                (0 ,-1),
                (1 ,-1),
                (2 ,-1),
            };

            Assert.All(InvalidParameters, invalidSet =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveRange(invalidSet.Item1, invalidSet.Item2));
            });

            list.Dispose();
        }

        #endregion
    }
}
