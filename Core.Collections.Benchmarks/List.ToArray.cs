using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [CoreJob, MemoryDiagnoser]
    public class List_ToArray : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListToArray()
        {
            for (int i = 0; i < 10000; i++)
                list.ToArray();
        }

        [Benchmark]
        public void PooledToArray()
        {
            for (int i = 0; i < 10000; i++)
                pooled.ToArray();
        }

        private List<int> list;
        private PooledList<int> pooled;

        [Params(1000, 10000, 100000)]
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
