using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Span : ListBase
    {
        [Benchmark]
        public void PooledSpan()
        {
            for (int i = 0; i < 10000; i++)
            {
                pooled.Span[0] = 1;
            }
        }

        [Benchmark(Baseline = true)]
        public void PooledIndex()
        {
            for (int i = 0; i < 10000; i++)
            {
                pooled[0] = 1;
            }
        }

        // make _items public to run this
        //[Benchmark]
        //public void Pooled_public_items()
        //{
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        pooled._items[0] = 1;
        //    }
        //}

        private PooledList<int> pooled;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = CreatePooled(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}