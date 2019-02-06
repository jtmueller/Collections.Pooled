using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
#if NETCOREAPP3_0
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_AddRange : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAddRangeICollection()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new List<int>();
                emptyList.AddRange(list);
            }
        }

        [Benchmark]
        public void PooledAddRangeICollection()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new PooledList<int>();
                emptyList.AddRange(list);
                emptyList.Dispose();
            }
        }

        [Benchmark]
        public void ListAddRangeIEnumerable()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new List<int>();
                emptyList.AddRange(IntEnumerable());
            }
        }

        [Benchmark]
        public void PooledAddRangeIEnumerable()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new PooledList<int>();
                emptyList.AddRange(IntEnumerable());
                emptyList.Dispose();
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i=0; i < N; i++)
                yield return list[i];
        }

        private List<int> list;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
        }
    }
}
