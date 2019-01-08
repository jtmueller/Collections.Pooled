using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    // TODO: some assembly-binding redirect bug related to System.Buffers
    // prevents us from running CoreJob with a CLR host, or ClrJob with a Core host.
    // When this is resolved, should change all the tests to run both job types at the same time.

#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_Add : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAdd()
        {
            var copyList = new List<int>(list);
            for (int i = 0; i < N; i++)
            {
                copyList.Add(123555); copyList.Add(123555); copyList.Add(123555); copyList.Add(123555);
                copyList.Add(123555); copyList.Add(123555); copyList.Add(123555); copyList.Add(123555);
            }
        }

        [Benchmark]
        public void PooledAdd()
        {
            var copyList = new PooledList<int>(list);
            for (int i = 0; i < N; i++)
            {
                copyList.Add(123555); copyList.Add(123555); copyList.Add(123555); copyList.Add(123555);
                copyList.Add(123555); copyList.Add(123555); copyList.Add(123555); copyList.Add(123555);
            }
            copyList.Dispose();
        }

        private PooledList<int> list;

        [Params(10_000, 100_000, 1_000_000)]
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
