using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_SequenceEqual_Int : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List()
        {
            _ = listInt.SequenceEqual(Equal ? intArray : intArrayNotEqual);
        }

        [Benchmark]
        public void Pooled()
        {
            _ = pooledInt.SequenceEqual(Equal ? intArray : intArrayNotEqual);
        }

        [Benchmark]
        public void Pooled_Span()
        {
            _ = pooledInt.Span.SequenceEqual(Equal ? intArray : intArrayNotEqual);
        }

        private int[] intArray;
        private int[] intArrayNotEqual;
        private List<int> listInt;
        private PooledList<int> pooledInt;

        [Params(64, 256, 1024)]
        public int N;

        [Params(true, false)]
        public bool Equal;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);
            intArrayNotEqual = new int[intArray.Length];
            Array.Copy(intArray, 0, intArrayNotEqual, 0, intArray.Length);
            intArrayNotEqual[N / 2] =~ 17;
            listInt = new List<int>(intArray);
            pooledInt = new PooledList<int>(listInt);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
        }
    }
}
