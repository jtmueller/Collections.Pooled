using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_BinarySearch : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Int()
        {
            for (int j = 0; j < N; j++)
                _ = listInt.BinarySearch(listInt[j], intComparer);
        }

        [Benchmark]
        public void Pooled_Int()
        {
            for (int j = 0; j < N; j++)
                _ = pooledInt.BinarySearch(pooledInt[j], intComparer);
        }

        [Benchmark]
        public void List_String()
        {
            for (int j = 0; j < N; j++)
                _ = listString.BinarySearch(listString[j], stringComparer);
        }

        [Benchmark]
        public void Pooled_String()
        {
            for (int j = 0; j < N; j++)
                _ = pooledString.BinarySearch(pooledString[j], stringComparer);
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;
        private readonly IComparer<int> intComparer = Comparer<int>.Default;
        private readonly IComparer<string> stringComparer = Comparer<string>.Default;

        [Params(100, 1000)]
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
