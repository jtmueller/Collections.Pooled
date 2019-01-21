using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    public abstract class StackBase
    {
        protected const int RAND_SEED = 24565653;

        protected static Stack<int> CreateStack(int size)
        {
            var rand = new Random(RAND_SEED);
            var stack = new Stack<int>();
            for (int i = 0; i < size; i++)
                stack.Push(rand.Next());
            return stack;
        }

        protected static PooledStack<int> CreatePooled(int size)
        {
            var rand = new Random(RAND_SEED);
            var stack = new PooledStack<int>();
            for (int i = 0; i < size; i++)
                stack.Push(rand.Next());
            return stack;
        }
    }
}
