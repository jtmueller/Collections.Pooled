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
    public class Dict_ContainsValue_Int_True : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictContainsValue_Int_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsValue(j);
        }

        [Benchmark]
        public void PooledContainsValue_Int_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = pooled.ContainsValue(j);
        }

        private PooledDictionary<int, int> pooled;
        private Dictionary<int, int> dict;

        [Params(1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = new PooledDictionary<int, int>();
            for (int i = 0; i < N; i++)
                pooled.Add(i, i);

            dict = new Dictionary<int, int>();
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
