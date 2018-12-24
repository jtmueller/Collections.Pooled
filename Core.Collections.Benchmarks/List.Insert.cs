using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_Insert : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListInsert_Int()
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
        public void PooledInsert_Int()
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
        public void ListInsert_String()
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
        public void PooledInsert_String()
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

        [Params(1_000, 10_000, 100_000)]
        public int N;
    }
}
