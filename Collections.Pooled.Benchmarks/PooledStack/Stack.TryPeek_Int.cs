using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [Config(typeof(BenchmarkConfig))]
    public class Stack_TryPeek_Int : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = intStack.TryPeek(out _);
            }
        }

        [Benchmark]
        public void PooledTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = intPooled.TryPeek(out _);
            }
        }

        [Params(10_000, 100_000)]
        public int N;

        [Params(true, false)]
        public bool EmptyStack;

        private Stack<int> intStack;
        private PooledStack<int> intPooled;
        private int[] numbers;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (EmptyStack)
            {
                intStack = new Stack<int>();
                intPooled = new PooledStack<int>();
            }

            numbers = CreateArray(N);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (EmptyStack)
                return;

            intStack = new Stack<int>(numbers);
            intPooled = new PooledStack<int>(numbers);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            if (EmptyStack)
                return;

            intPooled?.Dispose();
        }
    }
}
