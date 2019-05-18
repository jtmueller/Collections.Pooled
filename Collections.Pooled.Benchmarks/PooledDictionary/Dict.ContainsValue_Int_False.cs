using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_ContainsValue_Int_False : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void Dict_Contains()
        {
            bool result = false;
            int missingValue = N;   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = dict.ContainsValue(missingValue);
        }

        [Benchmark]
        public void Pooled_Contains()
        {
            bool result = false;
            int missingValue = N;   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = pooled.ContainsValue(missingValue);
        }

        protected override int GetT(int i) => i;
    }
}
