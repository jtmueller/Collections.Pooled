using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class Stack_Peek : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackPeek()
        {
            if (Type == StackType.Int)
            {
                for (int i = 0; i < N; i++)
                {
                    _ = intStack.Peek();
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    _ = stringStack.Peek();
                }
            }
        }

        [Benchmark]
        public void PooledPeek()
        {
            if (Type == StackType.Int)
            {
                for (int i = 0; i < N; i++)
                {
                    _ = intPooled.Peek();
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    _ = stringPooled.Peek();
                }
            }
        }

        [Params(10000, 100000, 1000000)]
        public int N;

        [Params(StackType.Int, StackType.String)]
        public StackType Type;

        private Stack<int> intStack;
        private Stack<string> stringStack;
        private PooledStack<int> intPooled;
        private PooledStack<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            numbers = CreateArray(N);

            if (Type == StackType.String)
            {
                strings = Array.ConvertAll(numbers, x => x.ToString());
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (Type == StackType.Int)
            {
                intStack = new Stack<int>(numbers);
                intPooled = new PooledStack<int>(numbers);
            }
            else
            {
                stringStack = new Stack<string>(strings);
                stringPooled = new PooledStack<string>(strings);
            }
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            intPooled?.Dispose();
            stringPooled?.Dispose();
        }
    }
}
