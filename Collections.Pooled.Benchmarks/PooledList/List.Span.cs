using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_Span : ListBase
    {
        [Benchmark]
        public void PooledSpan()
        {
            for (int i = 0; i < 10000; i++)
            {
                var span = pooled.Span;
                span[0] = 1;
            }
        }

        [Benchmark]
        public void PooledIndex()
        {
            for (int i = 0; i < 10000; i++)
            {
                pooled[0] = 1;
            }
        }

        // mark _items public to run this
        // [Benchmark]
        // public void Pooled_public_items()
        // {
        //     for (int i = 0; i < 10000; i++)
        //     {
        //         pooled._items[0] = 1;
        //     }                
        // }                

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
