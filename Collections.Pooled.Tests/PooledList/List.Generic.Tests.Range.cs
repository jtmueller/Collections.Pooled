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
    }
#endif
}
