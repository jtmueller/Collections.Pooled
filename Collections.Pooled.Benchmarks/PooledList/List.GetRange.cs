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
    public class List_GetRange : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListGetRange()
        {
            int item;
            for (int i = 0; i < 5000; i++)
            {
                var range = list.GetRange(0, list.Count);
                foreach (var x in range)
                    item = x;
                range = list.GetRange(list.Count / 3, list.Count / 4);
                foreach (var x in range)
                    item = x;
                range = list.GetRange(list.Count / 2, list.Count / 5);
                foreach (var x in range)
                    item = x;
            }
        }

        [Benchmark]
        public void PooledGetRange()
        {
            int item;
            for (int i = 0; i < 5000; i++)
            {
                var range = pooled.GetRange(0, pooled.Count);
                foreach (var x in range)
                    item = x;
                range = pooled.GetRange(pooled.Count / 3, pooled.Count / 4);
                foreach (var x in range)
                    item = x;
                range = pooled.GetRange(pooled.Count / 2, pooled.Count / 5);
                foreach (var x in range)
                    item = x;
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
