using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public class Dict_ContainsKey_Int_True : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void Dict_ContainsKey()
        {
            for (int j = 0; j < N; j++)
                _ = dict.ContainsKey(j);
        }

        [Benchmark]
        public void Pooled_ContainsKey()
        {
            for (int j = 0; j < N; j++)
                _ = pooled.ContainsKey(j);
        }

        protected override int GetT(int i) => i;
    }
}
