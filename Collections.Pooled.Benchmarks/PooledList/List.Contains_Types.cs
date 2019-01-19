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
    public class List_Contains_Types : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListContains_Int()
        {
            listInt.Contains(nonexistentInt);
            listInt.Contains(firstInt);
            listInt.Contains(middleInt);
            listInt.Contains(lastInt);
        }

        [Benchmark]
        public void PooledContains_Int()
        {
            pooledInt.Contains(nonexistentInt);
            pooledInt.Contains(firstInt);
            pooledInt.Contains(middleInt);
            pooledInt.Contains(lastInt);
        }

        [Benchmark]
        public void ListContains_String()
        {
            listString.Contains(nonexistentStr);
            listString.Contains(firstStr);
            listString.Contains(middleStr);
            listString.Contains(lastStr);
        }

        [Benchmark]
        public void PooledContains_String()
        {
            pooledString.Contains(nonexistentStr);
            pooledString.Contains(firstStr);
            pooledString.Contains(middleStr);
            pooledString.Contains(lastStr);
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;
        int nonexistentInt, firstInt, middleInt, lastInt;
        string nonexistentStr, firstStr, middleStr, lastStr;

        private const int N = 8192;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = CreateList(N);
            listString = listInt.ConvertAll(i => i.ToString());
            pooledInt = new PooledList<int>(listInt);
            pooledString = pooledInt.ConvertAll(i => i.ToString());

            nonexistentInt = -1;
            firstInt = 0;
            middleInt = N / 2;
            lastInt = N - 1;

            nonexistentStr = "foo";
            firstStr = 0.ToString();
            middleStr = (N / 2).ToString();
            lastStr = (N - 1).ToString();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
