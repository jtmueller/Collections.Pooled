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
    [MemoryDiagnoser]
    public class Dict_TryGetValue : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictTryGetValue()
        {
            int retrieved;
            for (int i = 0; i <= 1000; i++)
            {
                dict.TryGetValue(key, out retrieved); dict.TryGetValue(key, out retrieved);
                dict.TryGetValue(key, out retrieved); dict.TryGetValue(key, out retrieved);
                dict.TryGetValue(key, out retrieved); dict.TryGetValue(key, out retrieved);
                dict.TryGetValue(key, out retrieved); dict.TryGetValue(key, out retrieved);
            }
        }

        [Benchmark]
        public void PooledTryGetValue()
        {
            int retrieved;
            for (int i = 0; i <= 1000; i++)
            {
                pooled.TryGetValue(key, out retrieved); pooled.TryGetValue(key, out retrieved);
                pooled.TryGetValue(key, out retrieved); pooled.TryGetValue(key, out retrieved);
                pooled.TryGetValue(key, out retrieved); pooled.TryGetValue(key, out retrieved);
                pooled.TryGetValue(key, out retrieved); pooled.TryGetValue(key, out retrieved);
            }
        }

        private int key = 374068;
        private PooledDictionary<int, int> pooled;
        private Dictionary<int, int> dict;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = CreatePooled(N);
            dict = CreateDictionary(N);

            // needs a specific seed to prevent key collision with TestData
            dict.Add(key, 12);
            pooled.Add(key, 12);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
