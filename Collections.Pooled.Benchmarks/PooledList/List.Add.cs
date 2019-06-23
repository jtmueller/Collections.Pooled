using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
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

        [Params(100, 1000, 10_000)]
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
