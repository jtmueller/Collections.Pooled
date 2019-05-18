using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class Set_Contains_True : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Contains_True()
        {
            bool present;
            foreach (int thing in subsetToCheck)
            {
                present = hashSet.Contains(thing);
            }
        }

        [Benchmark]
        public void PooledSet_Contains_True()
        {
            bool present;
            foreach (int thing in subsetToCheck)
            {
                present = pooledSet.Contains(thing);
            }
        }

        private int[] subsetToCheck;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(1, 100, 10000)]
        public int CountToCheck;

        [Params(SetSize_Large)]
        public int InitialSetSize;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            int[] startingElements = intGenerator.MakeNewTs(InitialSetSize);
            subsetToCheck = intGenerator.GenerateSelectionSubset(startingElements, CountToCheck);

            hashSet = new HashSet<int>(startingElements);
            pooledSet = new PooledSet<int>(startingElements);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
        }
    }
}
