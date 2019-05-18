using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_IndexOf_ValueType : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_IndexOf()
        {
            list.IndexOf(nonexistentItem);
            list.IndexOf(firstItem);
            list.IndexOf(middleItem);
            list.IndexOf(lastItem);
        }

        [Benchmark]
        public void Pooled_IndexOf()
        {
            pooled.IndexOf(nonexistentItem);
            pooled.IndexOf(firstItem);
            pooled.IndexOf(middleItem);
            pooled.IndexOf(lastItem);
        }

        private List<int> list;
        private PooledList<int> pooled;
        int nonexistentItem, firstItem, middleItem, lastItem;

        //[Params(1_000, 10_000, 100_000)]
        //public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int N = 8192;
            list = new List<int>(N);
            pooled = new PooledList<int>(N);

            for (int i = 0; i < N; i++)
            {
                list.Add(i);
                pooled.Add(i);
            }

            nonexistentItem = -1;
            firstItem = 0;
            middleItem = pooled.Count / 2;
            lastItem = pooled.Count - 1;
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
