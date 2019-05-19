using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [Config(typeof(BenchmarkConfig))]
    public class Stack_Constructors : StackBase
    {
        [Benchmark(Baseline = true)]
        public void Stack_ICollection()
        {
            if (Type == StackType.Int)
            {
                Stack<int> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new Stack<int>(intList);
                }
            }
            else
            {
                Stack<string> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new Stack<string>(stringList);
                }
            }
        }

        [Benchmark]
        public void Pooled_ICollection()
        {
            if (Type == StackType.Int)
            {
                PooledStack<int> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new PooledStack<int>(intList);
                    stack.Dispose();
                }
            }
            else
            {
                PooledStack<string> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new PooledStack<string>(stringList);
                    stack.Dispose();
                }
            }
        }

        [Benchmark]
        public void Stack_IEnumerable()
        {
            if (Type == StackType.Int)
            {
                Stack<int> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new Stack<int>(IntEnumerable());
                }
            }
            else
            {
                Stack<string> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new Stack<string>(StringEnumerable());
                }
            }
        }

        [Benchmark]
        public void Pooled_IEnumerable()
        {
            if (Type == StackType.Int)
            {
                PooledStack<int> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new PooledStack<int>(IntEnumerable());
                    stack.Dispose();
                }
            }
            else
            {
                PooledStack<string> stack;
                for (int i = 0; i < 1000; i++)
                {
                    stack = new PooledStack<string>(StringEnumerable());
                    stack.Dispose();
                }
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return intArray[i];
        }

        private IEnumerable<string> StringEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return stringArray[i];
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [Params(StackType.Int, StackType.String)]
        public StackType Type;

        private int[] intArray;
        private string[] stringArray;
        private List<int> intList;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }

            intList = new List<int>(intArray);
            stringList = new List<string>(stringArray);
        }
    }
}
