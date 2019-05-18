using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_ContainsValue_Int_True : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void Dict_Contains()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsValue(j);
        }

        [Benchmark]
        public void Pooled_Contains()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = pooled.ContainsValue(j);
        }

        protected override int GetT(int i) => i;
    }
}
