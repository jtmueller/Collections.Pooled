using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Constructors_Int : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Collection()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new List<int>(intList);
            }
        }

        [Benchmark]
        public void Pooled_Collection()
        {
            PooledList<int> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<int>(intList);
                list.Dispose();
            }
        }

        [Benchmark]
        public void List_Enumerable()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new List<int>(IntEnumerable());
            }
        }

        [Benchmark]
        public void Pooled_Enumerable()
        {
            PooledList<int> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<int>(IntEnumerable());
                list.Dispose();
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
            {
                yield return intArray[i];
            }
        }

        [Params(100, 1_000, 10_000)]
        public int N;

        private int[] intArray;
        private List<int> intList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);
            intList = new List<int>(intArray);
        }
    }
}
