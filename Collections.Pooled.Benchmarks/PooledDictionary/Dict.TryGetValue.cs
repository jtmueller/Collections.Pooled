using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_TryGetValue : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictTryGetValue()
        {
            for (int i = 0; i <= 1000; i++)
            {
                dict.TryGetValue(key, out _); dict.TryGetValue(key, out _);
                dict.TryGetValue(key, out _); dict.TryGetValue(key, out _);
                dict.TryGetValue(key, out _); dict.TryGetValue(key, out _);
                dict.TryGetValue(key, out _); dict.TryGetValue(key, out _);
            }
        }

        [Benchmark]
        public void PooledTryGetValue()
        {
            for (int i = 0; i <= 1000; i++)
            {
                pooled.TryGetValue(key, out _); pooled.TryGetValue(key, out _);
                pooled.TryGetValue(key, out _); pooled.TryGetValue(key, out _);
                pooled.TryGetValue(key, out _); pooled.TryGetValue(key, out _);
                pooled.TryGetValue(key, out _); pooled.TryGetValue(key, out _);
            }
        }

        private int key = 374068;
        private PooledDictionary<int, int> pooled;
        private Dictionary<int, int> dict;

        [Params(100, 1_000, 10_000)]
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
