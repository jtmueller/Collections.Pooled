using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_ToArray : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListToArray()
        {
            for (int i = 0; i < 10000; i++)
                _ = list.ToArray();
        }

        [Benchmark]
        public void PooledToArray()
        {
            for (int i = 0; i < 10000; i++)
                _ = pooled.ToArray();
        }

        private List<int> list;
        private PooledList<int> pooled;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
            pooled = CreatePooled(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
