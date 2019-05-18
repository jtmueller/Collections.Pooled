using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Enumerator_Int : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List()
        {
            int item;
            foreach (int x in listInt)
            {
                item = x;
            }
        }

        [Benchmark]
        public void Pooled()
        {
            int item;
            foreach (int x in pooledInt)
            {
                item = x;
            }
        }

        [Benchmark]
        public void Pooled_Span()
        {
            int item;
            foreach (int x in pooledInt.Span)
            {
                item = x;
            }
        }

        private List<int> listInt;
        private PooledList<int> pooledInt;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = CreateList(N);
            pooledInt = new PooledList<int>(listInt);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
        }
    }
}
