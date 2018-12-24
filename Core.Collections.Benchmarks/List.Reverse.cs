using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [CoreJob]
    public class List_Reverse : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListReverse_Int()
        {
            var list = new List<int>(listInt);
            list.Reverse();
        }

        [Benchmark]
        public void PooledReverse_Int()
        {
            var list = new PooledList<int>(listInt);
            list.Reverse();
            list.Dispose();
        }

        [Benchmark]
        public void ListReverse_String()
        {
            var list = new List<string>(listString);
            list.Reverse();
        }

        [Benchmark]
        public void PooledReverse_String()
        {
            var list = new PooledList<string>(listString);
            list.Reverse();
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
