using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class Set_Contains_False : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Contains_False()
        {
            bool present;
            for (int i = 0; i < CountToCheck; i++)
            {
                present = hashSet.Contains(missingValue);
            }
        }

        [Benchmark]
        public void PooledSet_Contains_False()
        {
            bool present;
            for (int i = 0; i < CountToCheck; i++)
            {
                present = pooledSet.Contains(missingValue);
            }
        }

        private readonly int missingValue = InstanceCreators.IntGenerator_MaxValue + 1;
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
