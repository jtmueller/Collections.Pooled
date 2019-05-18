using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class List_Reverse : ListBase
    {
        [IterationSetup(Target = nameof(ListReverse_Int))]
        public void SetupListInt()
            => listInt = new List<int>(intItems);

        [Benchmark(Baseline = true)]
        public void ListReverse_Int()
        {
            listInt.Reverse();
        }

        [IterationSetup(Target = nameof(PooledReverse_Int))]
        public void SetupPooledInt()
            => pooledInt = new PooledList<int>(intItems);

        [IterationCleanup(Target = nameof(PooledReverse_Int))]
        public void CleanupPooledInt()
            => pooledInt?.Dispose();

        [Benchmark]
        public void PooledReverse_Int()
        {
            pooledInt.Reverse();
        }

        [IterationSetup(Target = nameof(ListReverse_String))]
        public void SetupListString()
            => listString = new List<string>(stringItems);

        [Benchmark]
        public void ListReverse_String()
        {
            listString.Reverse();
        }

        [IterationSetup(Target = nameof(PooledReverse_String))]
        public void SetupPooledString()
            => pooledString = new PooledList<string>(stringItems);

        [IterationCleanup(Target = nameof(PooledReverse_String))]
        public void CleanupPooledString()
            => pooledString?.Dispose();

        [Benchmark]
        public void PooledReverse_String()
        {
            pooledString.Reverse();
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
