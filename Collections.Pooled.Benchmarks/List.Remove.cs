using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_Remove : ListBase
    {
        [IterationSetup(Target = nameof(ListRemove_Int))]
        public void SetupListInt() 
            => listInt = new List<int>(intItems);

        [Benchmark(Baseline = true)]
        public void ListRemove_Int()
        {
            int start = 0;
            int middle = N / 3;
            int end = N - 1;

            for (int j = 0; j < N / 3; j++)
            {
                listInt.Remove(-1);
                listInt.Remove(intItems[start]);
                listInt.Remove(intItems[middle]);
                listInt.Remove(intItems[end]);

                ++start;
                ++middle;
                --end;
            }
        }

        [IterationSetup(Target = nameof(PooledRemove_Int))]
        public void SetupPooledInt()
            => pooledInt = new PooledList<int>(intItems);

        [IterationCleanup(Target = nameof(PooledRemove_Int))]
        public void CleanupPooledInt()
            => pooledInt?.Dispose();

        [Benchmark]
        public void PooledRemove_Int()
        {
            int start = 0;
            int middle = N / 3;
            int end = N - 1;

            for (int j = 0; j < N / 3; j++)
            {
                pooledInt.Remove(-1);
                pooledInt.Remove(intItems[start]);
                pooledInt.Remove(intItems[middle]);
                pooledInt.Remove(intItems[end]);

                ++start;
                ++middle;
                --end;
            }
        }

        [IterationSetup(Target = nameof(ListRemove_String))]
        public void SetupListString() 
            => listString = new List<string>(stringItems);

        [Benchmark]
        public void ListRemove_String()
        {
            int start = 0;
            int middle = N / 3;
            int end = N - 1;

            for (int j = 0; j < N / 3; j++)
            {
                listString.Remove("-1");
                listString.Remove(stringItems[start]);
                listString.Remove(stringItems[middle]);
                listString.Remove(stringItems[end]);

                ++start;
                ++middle;
                --end;
            }
        }

        [IterationSetup(Target = nameof(PooledRemove_String))]
        public void SetupPooledString()
            => pooledString = new PooledList<string>(stringItems);

        [IterationCleanup(Target = nameof(PooledRemove_String))]
        public void CleanupPooledString()
            => pooledString?.Dispose();

        [Benchmark]
        public void PooledRemove_String()
        {
            int start = 0;
            int middle = N / 3;
            int end = N - 1;

            for (int j = 0; j < N / 3; j++)
            {
                pooledString.Remove("-1");
                pooledString.Remove(stringItems[start]);
                pooledString.Remove(stringItems[middle]);
                pooledString.Remove(stringItems[end]);

                ++start;
                ++middle;
                --end;
            }
        }

        private List<int> listInt;
        private PooledList<int> pooledInt;
        private List<string> listString;
        private PooledList<string> pooledString;

        private int[] intItems;
        private string[] stringItems;

        [Params(3000, 10000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intItems = CreatePooled(N).ToArray();
            stringItems = Array.ConvertAll(intItems, i => i.ToString());
        }
    }
}
