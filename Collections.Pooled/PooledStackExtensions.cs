using System;
using System.Collections.Generic;

namespace Collections.Pooled
{
    /// <summary>
    /// Extension methods for creating <see cref="PooledStack{T}"/> instances.
    /// </summary>
    public static class PooledStackExtensions
    {
        public static PooledStack<T> ToPooledStack<T>(this IEnumerable<T> items)
            => new PooledStack<T>(items);

        public static PooledStack<T> ToPooledStack<T>(this T[] array)
            => new PooledStack<T>(array.AsSpan());

        public static PooledStack<T> ToPooledStack<T>(this ReadOnlySpan<T> span)
            => new PooledStack<T>(span);

        public static PooledStack<T> ToPooledStack<T>(this Span<T> span)
            => new PooledStack<T>(span);

        public static PooledStack<T> ToPooledStack<T>(this ReadOnlyMemory<T> memory)
            => new PooledStack<T>(memory.Span);

        public static PooledStack<T> ToPooledStack<T>(this Memory<T> memory)
            => new PooledStack<T>(memory.Span);
    }
}
