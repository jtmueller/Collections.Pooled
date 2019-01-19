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
    public class Dict_Remove : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictRemove()
        {
            for (long i = 0; i < N; i++)
            {
                dict.Remove(items[i]);
            }
        }

        [Benchmark]
        public void PooledRemove()
        {
            for (long i = 0; i < N; i++)
            {
                pooled.Remove(items[i]);
            }
        }

        private PooledDictionary<long?, long?> pooled;
        private Dictionary<long?, long?> dict;
        private long?[] items;

        [Params(10, 100, 10000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            dict = new Dictionary<long?, long?>(N);
            pooled = new PooledDictionary<long?, long?>(N);
            items = new long?[N];

            for (long i = 0; i < N; ++i)
            {
                items[i] = i;
                dict.Add(i, i);
                pooled.Add(i, i);
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
