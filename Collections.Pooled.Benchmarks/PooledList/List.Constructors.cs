using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_Constructors : ListBase
    {
        [Benchmark(Baseline=true)]
        public void ListICollectionConstructor()
        {
            if (Type == ListType.Int)
            {
                List<int> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new List<int>(intList);
                }
            }
            else
            {
                List<string> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new List<string>(stringList);
                }
            }
        }

        [Benchmark]
        public void PooledICollectionConstructor()
        {
            if (Type == ListType.Int)
            {
                PooledList<int> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new PooledList<int>(intList);
                    list.Dispose();
                }
            }
            else
            {
                PooledList<string> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new PooledList<string>(stringList);
                    list.Dispose();
                }
            }
        }

        [Benchmark]
        public void ListIEnumerableConstructor()
        {
            if (Type == ListType.Int)
            {
                List<int> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new List<int>(IntEnumerable());
                }
            }
            else
            {
                List<string> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new List<string>(StringEnumerable());
                }
            }
        }

        [Benchmark]
        public void PooledIEnumerableConstructor()
        {
            if (Type == ListType.Int)
            {
                PooledList<int> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new PooledList<int>(IntEnumerable());
                    list.Dispose();
                }
            }
            else
            {
                PooledList<string> list;
                for (int i = 0; i < 1000; i++)
                {
                    list = new PooledList<string>(StringEnumerable());
                    list.Dispose();
                }
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return intArray[i];
        }

        private IEnumerable<string> StringEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return stringArray[i];
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [Params(ListType.Int, ListType.String)]
        public ListType Type;

        private int[] intArray;
        private string[] stringArray;
        private List<int> intList;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }

            intList = new List<int>(intArray);
            stringList = new List<string>(stringArray);
        }

        public enum ListType
        {
            Int, String
        }
    }
}
