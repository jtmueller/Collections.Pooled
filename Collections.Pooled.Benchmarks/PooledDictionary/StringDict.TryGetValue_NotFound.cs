using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
#if NETCOREAPP3_1
    [Config(typeof(NetCoreOnlyBenchmarkConfig))]
    public class StringDict_TryGetValue_NotFound : DictContainsBase_String
    {
        // These tests simulate looking up dictionary keys based on a substring of a larger string.
        // They aren't so much about the dictionary performance as lower allocation from not having to
        // allocate substrings when using the span-based key.

        [Benchmark(Baseline = true)]
        public void DictTryGetValue()
        {
            foreach (var key in keys.Split('|'))
            {
                dict.TryGetValue(key, out _);
            }
        }

        [Benchmark]
        public void PooledTryGetValue()
        {
            foreach (var key in keys.AsSpan().Split('|'))
            {
                stringDict.TryGetValue(key, out _);
            }
        }

        private string keys = null;
        private StringKeyedDictionary<string> stringDict;

        public override void GlobalSetup()
        {
            base.GlobalSetup();

            stringDict = (StringKeyedDictionary<string>)pooled;
            keys = string.Join('|', pooled.Keys.Select(k => k.GetHashCode().ToString()));
        }

        protected override PooledDictionary<string, string> CreatePooled() => new StringKeyedDictionary<string>(Comparer);
    }
#endif
}
