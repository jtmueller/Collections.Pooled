using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Stack_Peek : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackPeek()
        {
            if (Type == StackType.Int)
            {
                int result;
                for (int i = 0; i < N; i++)
                {
                    result = intStack.Peek();
                }
            }
            else
            {
                string result;
                for (int i = 0; i < N; i++)
                {
                    result = stringStack.Peek();
                }
            }
        }

        [Benchmark]
        public void PooledPeek()
        {
            if (Type == StackType.Int)
            {
                int result;
                for (int i = 0; i < N; i++)
                {
                    result = intPooled.Peek();
                }
            }
            else
            {
                string result;
                for (int i = 0; i < N; i++)
                {
                    result = stringPooled.Peek();
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
