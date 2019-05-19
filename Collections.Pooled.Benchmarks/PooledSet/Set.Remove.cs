using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Remove : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            foreach (int thing in stuffToRemove)
            {
                hashSet.Remove(thing);
            }
        }

        [Benchmark]
        public void PooledSet()
        {
            foreach (int thing in stuffToRemove)
            {
                pooledSet.Remove(thing);
            }
        }

        private int[] startingElements;
        private int[] stuffToRemove;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(1, 100, 10000)]
        public int CountToRemove;

        [Params(SetSize_Large)]
        public int InitialSetSize;

        [IterationSetup(Target = nameof(HashSet))]
        public void HashIterationSetup()
        {
            hashSet = new HashSet<int>(startingElements);
        }

        [IterationSetup(Target = nameof(PooledSet))]
        public void PooledIterationSetup()
        {
            pooledSet = new PooledSet<int>(startingElements);
        }

        [IterationCleanup(Target = nameof(PooledSet))]
        public void PooledIterationCleanup()
        {
            pooledSet?.Dispose();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToRemove = intGenerator.GenerateSelectionSubset(startingElements, CountToRemove);
        }
    }
}
