using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_Constructors : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListArrayConstructor_Int()
        {
            List<int> list;
            for (int i = 0; i < 1000; i++)
                list = new List<int>(intArray);
        }

        [Benchmark]
        public void PooledArrayConstructor_Int()
        {
            PooledList<int> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<int>(intArray);
                list.Dispose();
            }
        }

        [Benchmark]
        public void ListArrayConstructor_String()
        {
            List<string> list;
            for (int i = 0; i < 1000; i++)
                list = new List<string>(stringArray);
        }

        [Benchmark]
        public void PooledArrayConstructor_String()
        {
            PooledList<string> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<string>(stringArray);
                list.Dispose();
            }
        }

        [Benchmark]
        public void ListICollectionConstructor_Int()
        {
            List<int> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new List<int>(intArray);
            }
        }

        [Benchmark]
        public void PooledICollectionConstructor_Int()
        {
            PooledList<int> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<int>(intArray);
                list.Dispose();
            }
        }

        [Benchmark]
        public void ListICollectionConstructor_String()
        {
            List<string> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new List<string>(stringList);
            }
        }

        [Benchmark]
        public void PooledICollectionConstructor_String()
        {
            PooledList<string> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<string>(stringList);
                list.Dispose();
            }
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        private int[] intArray;
        private string[] stringArray;
        private List<int> intList;
        private List<string> stringList;

        [GlobalSetup]
        public void ArraySetup()
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
