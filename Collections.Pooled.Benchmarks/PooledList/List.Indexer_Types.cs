using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Indexer_Types : ListBase
    {
        [Benchmark(Baseline = true)]
        public void L_Int()
        {
            for (int j = 0; j < N; ++j)
            {
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
                _ = listInt[j];
            }
        }

        [Benchmark]
        public void P_Int()
        {
            for (int j = 0; j < N; ++j)
            {
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
                _ = pooledInt[j];
            }
        }

        [Benchmark]
        public void P_Span_Int()
        {
            var span = pooledInt.Span;
            for (int j = 0; j < N; ++j)
            {
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
            }
        }

        [Benchmark]
        public void L_Str()
        {
            for (int j = 0; j < N; ++j)
            {
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
                _ = listString[j];
            }
        }

        [Benchmark]
        public void P_Str()
        {
            for (int j = 0; j < N; ++j)
            {
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
                _ = pooledString[j];
            }
        }

        [Benchmark]
        public void P_Span_Str()
        {
            var span = pooledString.Span;
            for (int j = 0; j < N; ++j)
            {
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
                _ = span[j];
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
            pooledString = new PooledList<string>(listString);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
