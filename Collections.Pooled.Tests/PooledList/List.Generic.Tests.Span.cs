using System;
using Xunit;

namespace Collections.Pooled.Tests.PooledList
{
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        protected virtual void PopulateSpan(Span<T> span)
        {
            for (var i = 0; i < span.Length; i++)
            {
                span[i] = CreateT(i * 17);
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void ConstructWithArray(int length)
        {
            var arr = GenericArrayFactory(length);
            var list = new PooledList<T>(arr);
            Assert.Equal(arr.Length, list.Count);
            if (length > 0)
            {
                Assert.Equal(arr[0], list[0]);
                Assert.Equal(arr[arr.Length - 1], list[list.Count - 1]);
            }
            list.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void ConstructWithSpan(int length)
        {
            var span = GenericArrayFactory(length).AsSpan();
            var list = span.ToPooledList();
            Assert.Equal(span.Length, list.Count);
            if (length > 0)
            {
                Assert.Equal(span[0], list[0]);
                Assert.Equal(span[span.Length - 1], list[list.Count - 1]);
            }
            list.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void ConstructWithMemory(int length)
        {
            var memory = GenericArrayFactory(length).AsMemory();
            var list = memory.ToPooledList();
            Assert.Equal(memory.Length, list.Count);
            if (length > 0)
            {
                Assert.Equal(memory.Span[0], list[0]);
                Assert.Equal(memory.Span[memory.Length - 1], list[list.Count - 1]);
            }
            list.Dispose();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void SpanMatchesList(int length)
        {
            using (var list = GenericListFactory(length))
            {
                var span = list.Span;
                Assert.Equal(list.Count, span.Length);
                if (length > 0)
                {
                    Assert.Equal(list[0], span[0]);
                    Assert.Equal(list[list.Count - 1], span[span.Length - 1]);
                }
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void AddSpan(int length)
        {
            using (var list = GenericListFactory(length))
            {
                var origCount = list.Count;
                var newSpan = list.AddSpan(5);
                PopulateSpan(newSpan);

                Assert.Equal(origCount + newSpan.Length, list.Count);
                Assert.Equal(newSpan[0], list[origCount]);
                Assert.Equal(newSpan[newSpan.Length - 1], list[list.Count - 1]);
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void InsertSpan(int length)
        {
            using (var list = GenericListFactory(length))
            {
                var origCount = list.Count;
                var insertIndex = length < 2 ? 0 : 1;
                var newSpan = list.InsertSpan(insertIndex, 5);
                PopulateSpan(newSpan);

                Assert.Equal(origCount + newSpan.Length, list.Count);
                Assert.Equal(newSpan[0], list[insertIndex]);
                Assert.Equal(newSpan[newSpan.Length - 1], list[insertIndex + newSpan.Length - 1]);
            }
        }
    }
}
