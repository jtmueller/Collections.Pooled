using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [CoreJob, MemoryDiagnoser]
    public class List_Enumerator : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListEnumerate()
        {
            for (int i = 0; i < 10000; i++)
                foreach (var element in list) { }
        }

        [Benchmark]
        public void PooledEnumerate()
        {
            for (int i = 0; i < 10000; i++)
                foreach (var element in pooled) { }
        }

        [Benchmark]
        public void PooledSpanEnumerate()
        {
            for (int i = 0; i < 10000; i++)
                foreach (var element in pooled.Span) { }
        }

        private List<int> list;
        private PooledList<int> pooled;

        [Params(1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
            pooled = CreatePooled(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
