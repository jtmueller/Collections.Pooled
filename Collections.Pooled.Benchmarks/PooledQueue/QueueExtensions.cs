using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
#if NET472
    // extension methods to make up for missing methods in .NET Standard 2.0
    internal static class QueueExtensions
    {
        public static bool TryDequeue<T>(this Queue<T> queue, out T result)
        {
            if (queue.Count == 0)
            {
                result = default;
                return false;
            }

            result = queue.Dequeue();
            return true;
        }

        public static bool TryPeek<T>(this Queue<T> queue, out T result)
        {
            if (queue.Count == 0)
            {
                result = default;
                return false;
            }

            result = queue.Peek();
            return true;
        }
    }
#endif
}
