using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class Dict_GetKeys : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictGetKeys()
        {
            IEnumerable<int> result;
            for (int i = 0; i <= 20000; i++)
            {
                result = dict.Keys; result = dict.Keys; result = dict.Keys;
                result = dict.Keys; result = dict.Keys; result = dict.Keys;
                result = dict.Keys; result = dict.Keys; result = dict.Keys;
            }
        }

        [Benchmark]
        public void PooledGetKeys()
        {
            IEnumerable<int> result;
            for (int i = 0; i <= 20000; i++)
            {
                result = pooled.Keys; result = pooled.Keys; result = pooled.Keys;
                result = pooled.Keys; result = pooled.Keys; result = pooled.Keys;
                result = pooled.Keys; result = pooled.Keys; result = pooled.Keys;
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
            dict = CreateDictionary(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
