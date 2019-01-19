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
    public class Dict_Indexer_get_ValueType : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictIndexer_get_ValueType()
        {
            int? item;
            for (int i = 0; i < N; ++i)
            {
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
                item = (int)dict[i];
            }
        }

        [Benchmark]
        public void PooledIndexer_get_ValueType()
        {
            int? item;
            for (int i = 0; i < N; ++i)
            {
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
                item = (int)pooled[i];
            }
        }

        private PooledDictionary<int?, int?> pooled;
        private Dictionary<int?, int?> dict;

        [Params(1024, 8192, 16384)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = new PooledDictionary<int?, int?>();
            for (int i = 0; i < N; i++)
                pooled.Add(i, i);

            dict = new Dictionary<int?, int?>();
            for (int i = 0; i < N; i++)
                dict.Add(i, i);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
