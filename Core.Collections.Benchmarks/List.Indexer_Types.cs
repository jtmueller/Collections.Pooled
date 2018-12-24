using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [ClrJob, MemoryDiagnoser]
    public class List_Indexer_Types : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListIndexer_Int()
        {
            int item;
            for (int j = 0; j < N; ++j)
            {
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
            }
        }

        [Benchmark]
        public void PooledIndexer_Int()
        {
            int item;
            for (int j = 0; j < N; ++j)
            {
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
            }
        }

        [Benchmark]
        public void ListIndexer_String()
        {
            string item;
            for (int j = 0; j < N; ++j)
            {
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
            }
        }

        [Benchmark]
        public void PooledIndexer_String()
        {
            string item;
            for (int j = 0; j < N; ++j)
            {
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
            }
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;

        private const int N = 8192;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = CreateList(N);
            listString = listInt.ConvertAll(i => i.ToString());
            pooledInt = new PooledList<int>(listInt);
            pooledString = pooledInt.ConvertAll(i => i.ToString());
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
