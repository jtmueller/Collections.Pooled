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
    public class List_Clear : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ClearList()
        {
            for (int i = 0; i < 5000; i++)
                listLists[i].Clear();
        }

        [Benchmark]
        public void ClearPooled()
        {
            for (int i = 0; i < 5000; i++)
                pooledLists[i].Clear();
        }

        private PooledList<int>[] pooledLists;
        private List<int>[] listLists;

        [IterationSetup(Target = nameof(ClearList))]
        public void ListIterSetup()
        {
            listLists = new List<int>[5000];
            for (int i = 0; i < 5000; i++)
                listLists[i] = new List<int>(list);
        }

        [IterationCleanup(Target = nameof(ClearList))]
        public void ListIterCleanup()
        {
            listLists = null;
        }

        [IterationSetup(Target = nameof(ClearPooled))]
        public void PooledIterSetup()
        {
            pooledLists = new PooledList<int>[5000];
            for (int i = 0; i < 5000; i++)
                pooledLists[i] = new PooledList<int>(list);
        }

        [IterationCleanup(Target = nameof(ClearPooled))]
        public void PooledIterCleanup()
        {
            if (pooledLists != null)
            {
                foreach (var pl in pooledLists)
                    pl.Dispose();
            }
            pooledLists = null;
        }

        private PooledList<int> list;

        [Params(10_000, 100_000)]
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
