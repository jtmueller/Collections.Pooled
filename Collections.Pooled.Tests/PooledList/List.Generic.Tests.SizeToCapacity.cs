// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using Xunit;

namespace Collections.Pooled.Tests.PooledList
{
    public abstract partial class List_Generic_Tests_SizeToCapacity
    {
        private class SingleArrayPool : ArrayPool<int>
        {
            private readonly int[] items;

            public SingleArrayPool(int[] items)
            {
                this.items = items;
            }

            public override int[] Rent(int minimumLength) => items;

            public override void Return(int[] array, bool clearArray = false)
            {
            }
        }

        [Fact]
        public void SizeToCapacityCapacityCount()
        {
            var list = new PooledList<int>(13, true);
            Assert.InRange(list.Capacity, 13, list.Capacity);
            Assert.Equal(13, list.Count);
        }

        [Fact]
        public void SizeToCapacityItemsSet()
        {
            var list = new PooledList<int>(13, true);
            list[12] = 42;
            Assert.Equal(42, list[12]);
        }

        [Fact]
        public void NoSizeToCapacityItemsSet()
        {
            var list = new PooledList<int>(13, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[12] = 42);
        }

        [Fact]
        public void SizeToCapacityClearModeNever()
        {
            var items = new int[13];
            items[7] = 42;
            var pool = new SingleArrayPool(items);

            var list = new PooledList<int>(13, ClearMode.Never, pool, true);

            Assert.Equal(42, list[7]);
        } 

        [Fact]
        public void SizeToCapacityClearModeAlways()
        {
            var items = new int[13];
            items[7] = 42;
            items[items.Length-1] = 42;
            var pool = new SingleArrayPool(items);

            var list = new PooledList<int>(13, ClearMode.Always, pool, true);

            Assert.Equal(0, list[7]);
            Assert.Equal(0, list[list.Count - 1]);
        }               
    }
}