using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Add_Types : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Int()
        {
            var list = new List<int>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
            }
        }

        [Benchmark]
        public void Pooled_Int()
        {
            var list = new PooledList<int>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
            }
            list.Dispose();
        }

        [Benchmark]
        public void List_String()
        {
            var list = new List<string>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
            }
        }

        [Benchmark]
        public void Pooled_String()
        {
            var list = new PooledList<string>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
            }
            list.Dispose();
        }

        [Params(256, 512, 2048)]
        public int N;

        private readonly string stringToAdd = "foo";
    }
}
