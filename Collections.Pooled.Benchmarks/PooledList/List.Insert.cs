using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Insert : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Int()
        {
            var list = new List<int>();
            for (int j = 0; j < N; ++j)
            {
                list.Insert(0, j); // Insert at the begining of the list
                list.Insert(j / 2, j); // Insert in the middle of the list
                list.Insert(list.Count, j); // Insert at the end of the list
            }
        }

        [Benchmark]
        public void Pooled_Int()
        {
            var list = new PooledList<int>();
            for (int j = 0; j < N; ++j)
            {
                list.Insert(0, j); // Insert at the begining of the list
                list.Insert(j / 2, j); // Insert in the middle of the list
                list.Insert(list.Count, j); // Insert at the end of the list
            }
            list.Dispose();
        }

        [Benchmark]
        public void List_String()
        {
            var list = new List<string>();
            for (int j = 0; j < N; ++j)
            {
                list.Insert(0, stringToAdd); // Insert at the begining of the list
                list.Insert(j / 2, stringToAdd); // Insert in the middle of the list
                list.Insert(list.Count, stringToAdd); // Insert at the end of the list
            }
        }

        [Benchmark]
        public void Pooled_String()
        {
            var list = new PooledList<string>();
            for (int j = 0; j < N; ++j)
            {
                list.Insert(0, stringToAdd); // Insert at the begining of the list
                list.Insert(j / 2, stringToAdd); // Insert in the middle of the list
                list.Insert(list.Count, stringToAdd); // Insert at the end of the list
            }
            list.Dispose();
        }

        private readonly string stringToAdd = "foo";

        [Params(1_000, 10_000)]
        public int N;
    }
}
