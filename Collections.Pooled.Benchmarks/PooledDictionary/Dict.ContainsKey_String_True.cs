using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
#if NETCOREAPP3_0
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    public class Dict_ContainsKey_String_True : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_String_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(sampleKeys[j]);
        }

        [Benchmark]
        public void PooledContainsKey_String_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(sampleKeys[j]);
        }

        protected override string GetT(int i) => i.ToString();

        private string[] sampleKeys;

        public override void GlobalSetup()
        {
            base.GlobalSetup();
            sampleKeys = dict.Keys.ToArray();
        }
    }
}
