﻿using System;
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
    public class List_AddRange : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAddRange()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new List<int>();
                emptyList.AddRange(list);
            }
        }

        [Benchmark]
        public void PooledAddRange()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new PooledList<int>();
                emptyList.AddRange(list);
                emptyList.Dispose();
            }
        }

        private PooledList<int> list;

        [Params(1_000, 10_000, 100_000, 1_000_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreatePooled(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            list?.Dispose();
        }
    }
}
