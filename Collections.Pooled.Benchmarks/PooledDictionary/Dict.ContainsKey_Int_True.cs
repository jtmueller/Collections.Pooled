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
    public class Dict_ContainsKey_Int_True : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_Int_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(j);
        }

        [Benchmark]
        public void PooledContainsKey_Int_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(j);
        }

        protected override int GetT(int i) => i;
    }
}
