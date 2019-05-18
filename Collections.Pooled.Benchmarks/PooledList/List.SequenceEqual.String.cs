using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_SequenceEqual_String : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List()
        {
            _ = listString.SequenceEqual(Equal ? stringArray : stringArrayNotEqual);
        }

        [Benchmark]
        public void Pooled()
        {
            _ = pooledString.SequenceEqual(Equal ? stringArray : stringArrayNotEqual);
        }

        [Benchmark]
        public void Pooled_Span()
        {
            _ = pooledString.Span.SequenceEqual(Equal ? stringArray : stringArrayNotEqual);
        }

        private string[] stringArray;
        private string[] stringArrayNotEqual;
        private List<string> listString;
        private PooledList<string> pooledString;

        [Params(64, 256, 1024)]
        public int N;

        [Params(true, false)]
        public bool Equal;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intArray = CreateArray(N);
            var intArrayNotEqual = new int[intArray.Length];
            Array.Copy(intArray, 0, intArrayNotEqual, 0, intArray.Length);
            intArrayNotEqual[N / 2] = ~17;
            stringArray = Array.ConvertAll(intArray, x => x.ToString());
            stringArrayNotEqual = Array.ConvertAll(intArrayNotEqual, x => x.ToString());
            listString = new List<string>(stringArray);
            pooledString = new PooledList<string>(stringArray);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledString?.Dispose();
        }
    }
}
