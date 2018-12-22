using System.Collections.Generic;

namespace Core.Collections
{
    public static class PooledListExtensions
    {
        public static PooledList<T> ToPooledList<T>(this IEnumerable<T> items)
            => new PooledList<T>(items);
    }
}
