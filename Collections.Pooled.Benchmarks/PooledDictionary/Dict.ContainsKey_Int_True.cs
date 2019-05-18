using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [CoreJob, ClrJob]
    public class Dict_ContainsKey_Int_True : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_Int_True()
        {
            for (int j = 0; j < N; j++)
                _ = dict.ContainsKey(j);
        }

        [Benchmark]
        public void PooledContainsKey_Int_True()
        {
            for (int j = 0; j < N; j++)
                _ = pooled.ContainsKey(j);
        }

        protected override int GetT(int i) => i;
    }
}
