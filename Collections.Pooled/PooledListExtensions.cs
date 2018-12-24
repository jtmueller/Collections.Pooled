using System;
using System.Collections.Generic;

namespace Collections.Pooled
{
    public static class PooledListExtensions
    {
        public static PooledList<T> ToPooledList<T>(this IEnumerable<T> items)
            => new PooledList<T>(items);

        public static PooledList<T> ToPooledList<T>(this T[] array)
            => new PooledList<T>(array.AsSpan());

        public static PooledList<T> ToPooledList<T>(this ReadOnlySpan<T> span)
            => new PooledList<T>(span);

        public static PooledList<T> ToPooledList<T>(this Span<T> span)
            => new PooledList<T>(span);

        public static PooledList<T> ToPooledList<T>(this ReadOnlyMemory<T> memory)
            => new PooledList<T>(memory.Span);

        public static PooledList<T> ToPooledList<T>(this Memory<T> memory)
            => new PooledList<T>(memory.Span);
    }
}
