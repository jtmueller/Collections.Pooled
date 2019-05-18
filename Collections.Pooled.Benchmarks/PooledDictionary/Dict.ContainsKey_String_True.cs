using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_ContainsKey_String_True : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void Dict_Contains()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(sampleKeys[j]);
        }

        [Benchmark]
        public void Pooled_Contains()
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
