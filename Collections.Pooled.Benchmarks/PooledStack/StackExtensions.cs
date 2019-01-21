using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledStack
{
#if NET472
    // extension methods to make up for missing methods in .NET Standard 2.0
    internal static class StackExtensions
    {
        public static bool TryPop<T>(this Stack<T> stack, out T result)
        {
            if (stack.Count == 0)
            {
                result = default;
                return false;
            }

            result = stack.Pop();
            return true;
        }

        public static bool TryPeek<T>(this Stack<T> stack, out T result)
        {
            if (stack.Count == 0)
            {
                result = default;
                return false;
            }

            result = stack.Peek();
            return true;
        }
    }
#endif
}
