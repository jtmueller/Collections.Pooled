using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [Config(typeof(BenchmarkConfig))]
    public class Stack_TryPeek_String : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = stringStack.TryPeek(out _);
            }
        }

        [Benchmark]
        public void PooledTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = stringPooled.TryPeek(out _);
            }
        }

        [Params(10_000, 100_000)]
        public int N;

        [Params(true, false)]
        public bool EmptyStack;

        private Stack<string> stringStack;
        private PooledStack<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (EmptyStack)
            {
                stringStack = new Stack<string>();
                stringPooled = new PooledStack<string>();
            }

            numbers = CreateArray(N);

            strings = Array.ConvertAll(numbers, x => x.ToString());
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (EmptyStack)
                return;

            stringStack = new Stack<string>(strings);
            stringPooled = new PooledStack<string>(strings);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            if (EmptyStack)
                return;

            stringPooled?.Dispose();
        }
    }
}
