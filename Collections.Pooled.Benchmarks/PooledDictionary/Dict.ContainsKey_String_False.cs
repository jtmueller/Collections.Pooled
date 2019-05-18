using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_ContainsKey_String_False : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void Dict_ContainsKey()
        {
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                _ = dict.ContainsKey(missingKey);
        }

        [Benchmark]
        public void Pooled_ContainsKey()
        {
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                _ = pooled.ContainsKey(missingKey);
        }

        protected override string GetT(int i) => i.ToString();
    }
}
