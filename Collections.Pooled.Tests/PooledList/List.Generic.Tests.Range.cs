using System;
using Xunit;

namespace Collections.Pooled.Tests.PooledList
{
#if NETCOREAPP3_0
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void TestRanges(int length)
        {
            if (length < 2) return;

            using var list = GenericListFactory(length);

            Assert.Equal(list[list.Count - 1], list[^1]);

            var firstHalf = list[..length / 2];
            var secondHalf = list[firstHalf.Length..];
            var wholeThing = list[..];

            Assert.Equal(list.Count, wholeThing.Length);
            Assert.Equal(wholeThing.Length, firstHalf.Length + secondHalf.Length);
            Assert.Equal(list[0], firstHalf[0]);
            Assert.NotEqual(list[0], secondHalf[0]);
            Assert.Equal(list[^1], secondHalf[^1]);
            Assert.Equal(list[^1], wholeThing[^1]);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void GetRange_int(int length)
        {
            if (length < 2) return;

            using var list = GenericListFactory(length);

            var range = list.GetRange(1);
            Assert.Equal(list[1], range[0]);
            Assert.Equal(list[^1], range[^1]);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void GetRange_Index(int length)
        {
            if (length < 2) return;

            using var list = GenericListFactory(length);

            var range = list.GetRange((Index)1);
            Assert.Equal(list[1], range[0]);
            Assert.Equal(list[^1], range[^1]);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void GetRange_int_int(int length)
        {
            if (length < 2) return;

            using var list = GenericListFactory(length);

            var range = list.GetRange(1, length - 1);
            Assert.Equal(list[1], range[0]);
            Assert.Equal(list[^1], range[^1]);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void GetRange_Range(int length)
        {
            if (length < 2) return;

            using var list = GenericListFactory(length);

            var range = list.GetRange(1..^1);
            Assert.Equal(list[1], range[0]);
            Assert.Equal(list[^2], range[^1]);
        }
    }
#endif
}
