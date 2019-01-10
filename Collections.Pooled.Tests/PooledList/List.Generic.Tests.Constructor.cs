// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Xunit;

namespace Collections.Pooled.Tests.PooledList
{
    /// <summary>
    /// Contains tests that ensure the correctness of the List class.
    /// </summary>
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        [Fact]
        public void Constructor_Default()
        {
            PooledList<T> list = new PooledList<T>();
            Assert.Equal(0, list.Capacity); //"Expected capacity of list to be the same as given."
            Assert.Empty(list); //"Do not expect anything to be in the list."
            Assert.False(((IList<T>)list).IsReadOnly); //"List should not be readonly"
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(100)]
        public void Constructor_Capacity(int capacity)
        {
            PooledList<T> list = new PooledList<T>(capacity);
            Assert.True(capacity <= list.Capacity); //"Expected capacity of list to be at least the same as given."
            Assert.Empty(list); //"Do not expect anything to be in the list."
            Assert.False(((IList<T>)list).IsReadOnly); //"List should not be readonly"
            list.Dispose();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_NegativeCapacity_ThrowsArgumentOutOfRangeException(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PooledList<T>(capacity));
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
        public void Constructor_IEnumerable(EnumerableType enumerableType, int listLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            IEnumerable<T> enumerable = CreateEnumerable(enumerableType, null, enumerableLength, 0, numberOfDuplicateElements);
            PooledList<T> list = new PooledList<T>(enumerable);
            PooledList<T> expected = enumerable.ToPooledList();

            Assert.Equal(enumerableLength, list.Count); //"Number of items in list do not match the number of items given."

            for (int i = 0; i < enumerableLength; i++)
                Assert.Equal(expected[i], list[i]); //"Expected object in item array to be the same as in the list"

            Assert.False(((IList<T>)list).IsReadOnly); //"List should not be readonly"
            list.Dispose();
            expected.Dispose();
        }
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters

        [Fact]
        public void Constructo_NullIEnumerable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { PooledList<T> _list = new PooledList<T>((IEnumerable<T>)null); }); //"Expected ArgumentnUllException for null items"
        }
    }
}
