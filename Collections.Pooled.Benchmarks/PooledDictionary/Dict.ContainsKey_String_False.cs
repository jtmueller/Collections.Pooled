using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [CoreJob, ClrJob]
    public class Dict_ContainsKey_String_False : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_String_False()
        {
            bool result = false;
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(missingKey);
        }

        [Benchmark]
        public void PooledContainsKey_String_False()
        {
            bool result = false;
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(missingKey);
        }

        protected override string GetT(int i) => i.ToString();
    }
}
