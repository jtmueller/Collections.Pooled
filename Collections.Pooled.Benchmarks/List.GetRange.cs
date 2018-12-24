using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_GetRange : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListGetRange()
        {
            for (int i = 0; i < 5000; i++)
            {
                var range = list.GetRange(0, list.Count);
                range = list.GetRange(list.Count / 3, list.Count / 4);
                range = list.GetRange(list.Count / 2, list.Count / 5);
            }
        }

        [Benchmark]
        public void PooledGetRange()
        {
            for (int i = 0; i < 5000; i++)
            {
                var range = pooled.GetRange(0, pooled.Count);
                range = pooled.GetRange(pooled.Count / 3, pooled.Count / 4);
                range = pooled.GetRange(pooled.Count / 2, pooled.Count / 5);
            }
        }

        private PooledList<int> pooled;
        private List<int> list;

        public int N = 100_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = CreatePooled(N);
            list = new List<int>(pooled);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
