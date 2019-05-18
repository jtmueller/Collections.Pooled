using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_Add : DictBase
    {
        [IterationSetup(Target = nameof(DictAdd))]
        public void SetupDict()
            => dictCopy = new Dictionary<int, int>(dict);

        [Benchmark(Baseline = true)]
        public void DictAdd()
        {
            for (int i = 0; i <= 20000; i++)
            {
                dictCopy.Add(i * 10 + 1, 0); dictCopy.Add(i * 10 + 2, 0); dictCopy.Add(i * 10 + 3, 0);
                dictCopy.Add(i * 10 + 4, 0); dictCopy.Add(i * 10 + 5, 0); dictCopy.Add(i * 10 + 6, 0);
                dictCopy.Add(i * 10 + 7, 0); dictCopy.Add(i * 10 + 8, 0); dictCopy.Add(i * 10 + 9, 0);
            }
        }

        [IterationSetup(Target = nameof(PooledAdd))]
        public void SetupPooled()
            => pooledCopy = new PooledDictionary<int, int>(dict);

        [IterationCleanup(Target = nameof(PooledAdd))]
        public void CleanupPooled()
            => pooledCopy?.Dispose();

        [Benchmark]
        public void PooledAdd()
        {
            for (int i = 0; i <= 20000; i++)
            {
                pooledCopy.Add(i * 10 + 1, 0); pooledCopy.Add(i * 10 + 2, 0); pooledCopy.Add(i * 10 + 3, 0);
                pooledCopy.Add(i * 10 + 4, 0); pooledCopy.Add(i * 10 + 5, 0); pooledCopy.Add(i * 10 + 6, 0);
                pooledCopy.Add(i * 10 + 7, 0); pooledCopy.Add(i * 10 + 8, 0); pooledCopy.Add(i * 10 + 9, 0);
            }
        }

        private PooledDictionary<int, int> dict;
        private PooledDictionary<int, int> pooledCopy;
        private Dictionary<int, int> dictCopy;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            dict = CreatePooled(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            dict?.Dispose();
        }
    }
}
