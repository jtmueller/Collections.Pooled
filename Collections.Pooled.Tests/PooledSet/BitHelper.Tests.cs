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
            Assert.False(bitHelper.IsMarked(bitHelper.FindFirstUnmarked()));
            Assert.Equal(start - 1, bitHelper.FindFirstUnmarked(start - 1));
            Assert.False(bitHelper.IsMarked(bitHelper.FindFirstUnmarked(start - 1)));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(start));
            Assert.False(bitHelper.IsMarked(bitHelper.FindFirstUnmarked(start)));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(start + 1));
            Assert.False(bitHelper.IsMarked(bitHelper.FindFirstUnmarked(start + 1)));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(end));
            Assert.False(bitHelper.IsMarked(bitHelper.FindFirstUnmarked(end)));
            Assert.Equal(end + 1, bitHelper.FindFirstUnmarked(end + 1));
            Assert.False(bitHelper.IsMarked(bitHelper.FindFirstUnmarked(end + 1)));
        }

        [Theory]
        [MemberData(nameof(BitCounts))]
        public void CanFindFirstMarked(int count)
        {
            if (count == 0) return;

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = count <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, count), clear: true)
                : new BitHelper(new int[count], clear: false);

            for (int i = count - 1; i >= 0; i--)
            {
                bitHelper.MarkBit(i);
                Assert.Equal(i, bitHelper.FindFirstMarked());
            }
        }

        [Theory]
        [MemberData(nameof(BitCounts))]
        public void CanFindFirstMarkedWithOffset(int count)
        {
            if (count < 5) return;

            Span<int> span = stackalloc int[StackAllocThreshold];
            BitHelper bitHelper = count <= StackAllocThreshold
                ? new BitHelper(span.Slice(0, count), clear: true)
                : new BitHelper(new int[count], clear: false);

            // flag a block of bits at each end
            int end1 = count / 3;
            int start2 = end1 * 2;
            for (int i = 0; i < end1; i++)
                bitHelper.MarkBit(i);
            for (int i = start2; i < count; i++)
                bitHelper.MarkBit(i);

            Assert.Equal(0, bitHelper.FindFirstMarked());
            Assert.True(bitHelper.IsMarked(bitHelper.FindFirstMarked()));
            Assert.Equal(end1 - 1, bitHelper.FindFirstMarked(end1 - 1));
            Assert.True(bitHelper.IsMarked(bitHelper.FindFirstMarked(end1 - 1)));
            Assert.Equal(start2, bitHelper.FindFirstMarked(end1));
            Assert.True(bitHelper.IsMarked(bitHelper.FindFirstMarked(end1)));
            Assert.Equal(start2, bitHelper.FindFirstMarked(end1 + 1));
            Assert.True(bitHelper.IsMarked(bitHelper.FindFirstMarked(end1 + 1)));
            Assert.Equal(start2 + 1, bitHelper.FindFirstMarked(start2 + 1));
            Assert.True(bitHelper.IsMarked(bitHelper.FindFirstMarked(start2 + 1)));
        }
    }
}
