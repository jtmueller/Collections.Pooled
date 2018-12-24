using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [CoreJob]
    public class List_Sort : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListSort_Int()
        {
            var list = new List<int>(listInt);
            list.Sort();
        }

        [Benchmark]
        public void PooledSort_Int()
        {
            var list = new PooledList<int>(listInt);
            list.Sort();
            list.Dispose();
        }

        [Benchmark]
        public void ListSort_String()
        {
            var list = new List<string>(listString);
            list.Sort();
        }

        [Benchmark]
        public void PooledSort_String()
        {
            var list = new PooledList<string>(listString);
            list.Sort();
            list.Dispose();
        }

        private List<int> listInt;
        private List<string> listString;

        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = CreateList(N);
            listString = listInt.ConvertAll(i => i.ToString());
        }
    }
}
