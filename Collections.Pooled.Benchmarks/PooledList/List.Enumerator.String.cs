using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Enumerator_String : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List()
        {
            string item;
            foreach (string x in listString)
            {
                item = x;
            }
        }

        [Benchmark]
        public void Pooled()
        {
            string item;
            foreach (string x in pooledString)
            {
                item = x;
            }
        }

        [Benchmark]
        public void Pooled_Span()
        {
            string item;
            foreach (string x in pooledString.Span)
            {
                item = x;
            }
        }

        private List<string> listString;
        private PooledList<string> pooledString;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intArray = CreateArray(N);
            listString = new List<string>(intArray.Select(x => x.ToString()));
            pooledString = new PooledList<string>(listString);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledString?.Dispose();
        }
    }
}
