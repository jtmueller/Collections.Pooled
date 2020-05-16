using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_ContainsValue_String_False : DictContainsBase_String
    {
        [Benchmark(Baseline = true)]
        public void Dict_Contains()
        {
            string missingValue = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                _ = dict.ContainsValue(missingValue);
        }

        [Benchmark]
        public void Pooled_Contains()
        {
            string missingValue = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                _ = pooled.ContainsValue(missingValue);
        }

    }
}
