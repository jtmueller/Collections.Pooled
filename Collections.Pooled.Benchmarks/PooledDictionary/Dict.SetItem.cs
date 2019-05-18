using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_SetItem : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictSetItem()
        {
            for (int i = 0; i <= 10000; i++)
            {
                dict[1] = 0; dict[2] = 0; dict[3] = 0;
                dict[4] = 0; dict[5] = 0; dict[6] = 0;
                dict[7] = 0; dict[8] = 0; dict[9] = 0;
            }
        }

        [Benchmark]
        public void PooledSetItem()
        {
            for (int i = 0; i <= 10000; i++)
            {
                pooled[1] = 0; pooled[2] = 0; pooled[3] = 0;
                pooled[4] = 0; pooled[5] = 0; pooled[6] = 0;
                pooled[7] = 0; pooled[8] = 0; pooled[9] = 0;
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
