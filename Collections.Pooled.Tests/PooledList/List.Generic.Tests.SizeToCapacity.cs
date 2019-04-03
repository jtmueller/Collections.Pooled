// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using Xunit;

namespace Collections.Pooled.Tests.PooledList
{
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        private class SingleArrayPool : ArrayPool<T>
        {
            private readonly T[] _items;

            public SingleArrayPool(T[] items)
            {
                _items = items;
            }

            public override T[] Rent(int minimumLength) => _items;

            public override void Return(T[] array, bool clearArray = false)
            {
            }
        }

        [Fact]
        public void SizeToCapacityCapacityCount()
        {
            var list = new PooledList<T>(13, true);
            Assert.InRange(list.Capacity, 13, list.Capacity);
            Assert.Equal(13, list.Count);
            list.Dispose();
        }

        [Fact]
        public void SizeToCapacityItemsSet()
        {
            var list = new PooledList<T>(13, true);
            var value = CreateT(42);
            list[12] = value;
            Assert.Equal(value, list[12]);
            list.Dispose();
        }

        [Fact]
        public void NoSizeToCapacityItemsSet()
        {
            var list = new PooledList<T>(13, false);
            var value = CreateT(42);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[12] = value);
            list.Dispose();
        }

        [Fact]
        public void SizeToCapacityClearModeNever()
        {
            var items = GenericArrayFactory(13);
            var value = CreateT(42);
            items[7] = value;
            var pool = new SingleArrayPool(items);

            var list = new PooledList<T>(13, ClearMode.Never, pool, true);

            Assert.Equal(value, list[7]);

            list.Dispose();
        } 

        [Fact]
        public void SizeToCapacityClearModeAlways()
        {
            var items = GenericArrayFactory(13);
            var value = CreateT(42);
            items[7] = value;
            items[items.Length-1] = value;
            var pool = new SingleArrayPool(items);

            var list = new PooledList<T>(13, ClearMode.Always, pool, true);

            Assert.Equal(default, list[7]);
            Assert.Equal(default, list[list.Count - 1]);

            list.Dispose();
        }               
    }
}