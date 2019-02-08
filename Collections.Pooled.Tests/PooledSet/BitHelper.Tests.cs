using System;
using System.Collections.Generic;
using Xunit;

namespace Collections.Pooled.Tests.PooledSet
{
    public class BitHelperTests
    {
        const int StackAllocThreshold = 100;

        public static IEnumerable<object[]> BitCounts()
        {
            yield return new object[] { 0 };
            yield return new object[] { 1 };
            yield return new object[] { 5 };
            yield return new object[] { 75 };
            yield return new object[] { 117 };
            yield return new object[] { 965 };
        }

        [Theory]
        [MemberData(nameof(BitCounts))]
        public void CanMarkBits(int count)
        {
            if (count == 0) return;

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = count <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, count), clear: true)
                : new BitHelper(new int[count], clear: false);

            // mark every other bit
            for (int i = 0; i < count; i += 2)
                bitHelper.MarkBit(i);

            for (int i = 0; i < count; i++)
            {
                Assert.Equal(i % 2 == 0, bitHelper.IsMarked(i));
            }
        }

        [Theory]
        [MemberData(nameof(BitCounts))]
        public void CanFindFirstUnmarked(int count)
        {
            if (count == 0) return;

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = count <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, count), clear: true)
                : new BitHelper(new int[count], clear: false);

            for (int i = 0; i < count; i++)
            {
                Assert.Equal(i, bitHelper.FindFirstUnmarked());
                bitHelper.MarkBit(i);
            }

            Assert.Equal(count, bitHelper.FindFirstUnmarked());
        }

        [Theory]
        [MemberData(nameof(BitCounts))]
        public void CanFindFirstUnmarkedWithOffset(int count)
        {
            if (count < 5) return;

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = count <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, count), clear: true)
                : new BitHelper(new int[count], clear: false);

            // flag a block of bits in the middle
            int start = count / 3;
            int end = start * 2;
            for (int i = start; i <= end; i++)
                bitHelper.MarkBit(i);

            Assert.Equal(0, bitHelper.FindFirstUnmarked());
            Assert.Equal(start - 1, bitHelper.FindFirstUnmarked(start - 1));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(start));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(start + 1));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(end));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(end + 1));
        }
    }
}
