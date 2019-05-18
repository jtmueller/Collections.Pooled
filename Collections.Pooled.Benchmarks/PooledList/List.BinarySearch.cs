using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class List_BinarySearch : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListBinarySearch_Int()
        {
            int result = 0;
            for (int j = 0; j < N; j++)
                result = listInt.BinarySearch(listInt[j], intComparer);
        }

        [Benchmark]
        public void PooledBinarySearch_Int()
        {
            int result = 0;
            for (int j = 0; j < N; j++)
                result = pooledInt.BinarySearch(pooledInt[j], intComparer);
        }

        [Benchmark]
        public void ListBinarySearch_String()
        {
            int result = 0;
            for (int j = 0; j < N; j++)
                result = listString.BinarySearch(listString[j], stringComparer);
        }

        [Benchmark]
        public void PooledBinarySearch_String()
        {
            int result = 0;
            for (int j = 0; j < N; j++)
                result = pooledString.BinarySearch(pooledString[j], stringComparer);
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;
        private readonly IComparer<int> intComparer = Comparer<int>.Default;
        private readonly IComparer<string> stringComparer = Comparer<string>.Default;

        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = new List<int>();
            for (int i = 0; i < N; i++)
            {
                listInt.Add(i);
            }

            listString = listInt.ConvertAll(i => i.ToString());
            pooledInt = new PooledList<int>(listInt);
            pooledString = pooledInt.ConvertAll(i => i.ToString());
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
