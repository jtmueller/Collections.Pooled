using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
#if NETCOREAPP3_0
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Dict_Constructors : DictBase
    {
        [Benchmark(Baseline = true)]
        public void Dict_Ctor()
        {
            for (int i = 0; i <= 500; i++)
            {
                new Dictionary<int, string>(N); new Dictionary<int, string>(N); new Dictionary<int, string>(N);
                new Dictionary<int, string>(N); new Dictionary<int, string>(N); new Dictionary<int, string>(N);
                new Dictionary<int, string>(N); new Dictionary<int, string>(N); new Dictionary<int, string>(N);
            }
        }

        [Benchmark]
        public void Pooled_Ctor()
        {
            for (int i = 0; i <= 500; i++)
            {
                new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose();
                new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose();
                new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose();
            }
        }

        [Params(0, 1024, 4096, 16384)]
        public int N;
    }
}
