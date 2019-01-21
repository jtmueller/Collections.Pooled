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
    public class Stack_Constructors : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackArrayConstructor_Int()
        {
            Stack<int> stack;
            for (int i = 0; i < 1000; i++)
                stack = new Stack<int>(intArray);
        }

        [Benchmark]
        public void PooledArrayConstructor_Int()
        {
            PooledStack<int> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new PooledStack<int>(intArray);
                stack.Dispose();
            }
        }

        [Benchmark]
        public void StackArrayConstructor_String()
        {
            Stack<string> stack;
            for (int i = 0; i < 1000; i++)
                stack = new Stack<string>(stringArray);
        }

        [Benchmark]
        public void PooledArrayConstructor_String()
        {
            PooledStack<string> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new PooledStack<string>(stringArray);
                stack.Dispose();
            }
        }

        [Benchmark]
        public void StackICollectionConstructor_Int()
        {
            Stack<int> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new Stack<int>(intList);
            }
        }

        [Benchmark]
        public void PooledICollectionConstructor_Int()
        {
            PooledStack<int> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new PooledStack<int>(intList);
                stack.Dispose();
            }
        }

        [Benchmark]
        public void StackICollectionConstructor_String()
        {
            Stack<string> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new Stack<string>(stringList);
            }
        }

        [Benchmark]
        public void PooledICollectionConstructor_String()
        {
            PooledStack<string> stack;
            for (int i = 0; i < 1000; i++)
            {
                stack = new PooledStack<string>(stringList);
                stack.Dispose();
            }
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        private int[] intArray;
        private string[] stringArray;
        private List<int> intList;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = new int[N];
            var rand = new Random(RAND_SEED);
            for (int i = 0; i < N; i++)
            {
                intArray[i] = rand.Next();
            }

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
