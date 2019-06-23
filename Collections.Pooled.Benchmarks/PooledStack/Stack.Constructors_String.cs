using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [Config(typeof(BenchmarkConfig))]
    public class Stack_Constructors_String : StackBase
    {
        [Benchmark(Baseline = true)]
        public void Stack()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new Stack<string>(Kind switch
                {
                    CollectionType.Collection => stringList,
                    CollectionType.Enumerable => StringEnumerable(),
                    _ => throw new InvalidOperationException("Unsupported collection type.")
                });
            }
        }

        [Benchmark]
        public void PooledStack()
        {
            PooledStack<string> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new PooledStack<string>(Kind switch
                {
                    CollectionType.Collection => stringList,
                    CollectionType.Enumerable => StringEnumerable(),
                    _ => throw new InvalidOperationException("Unsupported collection type.")
                });
                stack.Dispose();
            }
        }

        private IEnumerable<string> StringEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return stringArray[i];
        }

        [Params(1_000, 10_000)]
        public int N;

        [Params(CollectionType.Enumerable, CollectionType.Collection)]
        public CollectionType Kind;

        private string[] stringArray;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int[] intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }

            stringList = new List<string>(stringArray);
        }
    }
}
