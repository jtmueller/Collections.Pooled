using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [Config(typeof(BenchmarkConfig))]
    public class Stack_Constructors_Int : StackBase
    {
        [Benchmark(Baseline = true)]
        public void Stack()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new Stack<int>(Kind switch
                {
                    CollectionType.Collection => intList,
                    CollectionType.Enumerable => IntEnumerable(),
                    _ => throw new InvalidOperationException("Unsupported collection type.")
                });
            }
        }

        [Benchmark]
        public void PooledStack()
        {
            PooledStack<int> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new PooledStack<int>(Kind switch
                {
                    CollectionType.Collection => intList,
                    CollectionType.Enumerable => IntEnumerable(),
                    _ => throw new InvalidOperationException("Unsupported collection type.")
                });
                stack.Dispose();
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return intArray[i];
        }

        [Params(1_000, 10_000)]
        public int N;

        [Params(CollectionType.Enumerable, CollectionType.Collection)]
        public CollectionType Kind;

        private int[] intArray;
        private List<int> intList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);
            intList = new List<int>(intArray);
        }
    }
}
