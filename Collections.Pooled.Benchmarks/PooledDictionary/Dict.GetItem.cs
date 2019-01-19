using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    public class Dict_GetItem : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictGetItem()
        {
            int retrieved;
            for (int i = 0; i <= 10000; i++)
            {
                retrieved = dict[1]; retrieved = dict[2]; retrieved = dict[3];
                retrieved = dict[4]; retrieved = dict[5]; retrieved = dict[6];
                retrieved = dict[7]; retrieved = dict[8]; retrieved = dict[9];
            }
        }

        [Benchmark]
        public void PooledGetItem()
        {
            int retrieved;
            for (int i = 0; i <= 10000; i++)
            {
                retrieved = pooled[1]; retrieved = pooled[2]; retrieved = pooled[3];
                retrieved = pooled[4]; retrieved = pooled[5]; retrieved = pooled[6];
                retrieved = pooled[7]; retrieved = pooled[8]; retrieved = pooled[9];
            }
        }

        private PooledDictionary<int, int> pooled;
        private Dictionary<int, int> dict;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = CreatePooled(N);
            for (int i = 1; i <= 9; i++)
                pooled.Add(i, 0);

            dict = CreateDictionary(N);
            for (int i = 1; i <= 9; i++)
                dict.Add(i, 0);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
