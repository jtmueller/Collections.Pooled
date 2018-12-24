using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    public class List_Contains : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListContains()
        {
            for (int i = 0; i < 500; i++)
            {
                list.Contains(containedList); list.Contains(containedList); list.Contains(containedList); list.Contains(containedList);
                list.Contains(containedList); list.Contains(containedList); list.Contains(containedList); list.Contains(containedList);
                list.Contains(containedList); list.Contains(containedList); list.Contains(containedList); list.Contains(containedList);
            }
        }

        [Benchmark]
        public void PooledContains()
        {
            for (int i = 0; i < 500; i++)
            {
                pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled);
                pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled);
                pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled);
            }
        }

        private List<int> list;
        private PooledList<int> pooled;
        private int containedList;
        private int containedPooled;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
            pooled = CreatePooled(N);
            containedList = list[list.Count / 2];
            containedPooled = pooled[pooled.Count / 2];
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
