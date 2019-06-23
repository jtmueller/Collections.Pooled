using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_BinarySearch : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List()
        {
            if (Type == ListType.Int)
            {
                for (int j = 0; j < N; j++)
                    _ = listInt.BinarySearch(listInt[j]);
            }
            else
            {
                for (int j = 0; j < N; j++)
                    _ = listString.BinarySearch(listString[j]);
            }
        }

        [Benchmark]
        public void PooledList()
        {
            if (Type == ListType.Int)
            {
                for (int j = 0; j < N; j++)
                    _ = pooledInt.BinarySearch(pooledInt[j]);
            }
            else
            {
                for (int j = 0; j < N; j++)
                    _ = pooledString.BinarySearch(pooledString[j]);
            }
        }

        [Benchmark]
        public void PooledList_Span()
        {
            if (Type == ListType.Int)
            {
                for (int j = 0; j < N; j++)
                    _ = pooledInt.Span.BinarySearch(pooledInt[j]);
            }
            else
            {
                for (int j = 0; j < N; j++)
                    _ = pooledString.Span.BinarySearch(pooledString[j]);
            }
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;

        [Params(100, 1000)]
        public int N;

        [Params(ListType.Int, ListType.String)]
        public ListType Type;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = new List<int>();
            for (int i = 0; i < N; i++)
            {
                listInt.Add(i);
            }

            listString = listInt.ConvertAll(i => i.ToString());
            listString.Sort();
            pooledInt = new PooledList<int>(listInt);
            pooledString = pooledInt.ConvertAll(i => i.ToString());
            pooledString.Sort();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
