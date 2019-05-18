using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class List_Sort : ListBase
    {
        [IterationSetup(Target = nameof(ListSort_Int))]
        public void SetupListInt()
            => listInt = new List<int>(intItems);

        [Benchmark(Baseline = true)]
        public void ListSort_Int()
        {
            listInt.Sort();
        }

        [IterationSetup(Target = nameof(PooledSort_Int))]
        public void SetupPooledInt()
            => pooledInt = new PooledList<int>(intItems);

        [IterationCleanup(Target = nameof(PooledSort_Int))]
        public void CleanupPooledInt()
            => pooledInt?.Dispose();

        [Benchmark]
        public void PooledSort_Int()
        {
            pooledInt.Sort();
        }

        [IterationSetup(Target = nameof(ListSort_String))]
        public void SetupListString()
            => listString = new List<string>(stringItems);

        [Benchmark]
        public void ListSort_String()
        {
            listString.Sort();
        }

        [IterationSetup(Target = nameof(PooledSort_String))]
        public void SetupPooledString()
            => pooledString = new PooledList<string>(stringItems);

        [IterationCleanup(Target = nameof(PooledSort_String))]
        public void CleanupPooledString()
            => pooledString?.Dispose();

        [Benchmark]
        public void PooledSort_String()
        {
            pooledString.Sort();
        }

        private List<int> listInt;
        private PooledList<int> pooledInt;
        private List<string> listString;
        private PooledList<string> pooledString;

        private int[] intItems;
        private string[] stringItems;

        [Params(100_000, 200_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intItems = CreatePooled(N).ToArray();
            stringItems = Array.ConvertAll(intItems, i => i.ToString());
        }
    }
}
