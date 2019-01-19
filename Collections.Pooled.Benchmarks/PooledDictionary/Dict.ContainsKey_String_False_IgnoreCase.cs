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
    public class Dict_ContainsKey_String_False_IgnoreCase : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_String_False_IgnoreCase()
        {
            bool result = false;
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(missingKey);
        }

        [Benchmark]
        public void PooledContainsKey_String_False_IgnoreCase()
        {
            bool result = false;
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(missingKey);
        }

        protected override string GetT(int i) => i.ToString();

        protected override IEqualityComparer<string> Comparer 
            => StringComparer.OrdinalIgnoreCase;
    }
}
