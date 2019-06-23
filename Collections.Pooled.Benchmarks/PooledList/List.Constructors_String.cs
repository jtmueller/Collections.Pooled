using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Constructors_String : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Collection()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new List<string>(stringList);
            }
        }

        [Benchmark]
        public void Pooled_Collection()
        {
            PooledList<string> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<string>(stringList);
                list.Dispose();
            }
        }

        [Benchmark]
        public void List_Enumerable()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new List<string>(StringEnumerable());
            }
        }

        [Benchmark]
        public void Pooled_Enumerable()
        {
            PooledList<string> list;
            for (int i = 0; i < 1000; i++)
            {
                list = new PooledList<string>(StringEnumerable());
                list.Dispose();
            }
        }

        private IEnumerable<string> StringEnumerable()
        {
            for (int i = 0; i < N; i++)
            {
                yield return stringArray[i];
            }
        }

        [Params(100, 1_000, 10_000)]
        public int N;

        private string[] stringArray;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int[] intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }

            stringList = new List<string>(stringArray);
        }
    }
}
