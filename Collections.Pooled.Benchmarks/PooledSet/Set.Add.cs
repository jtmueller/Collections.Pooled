using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Add : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            foreach (int thing in stuffToAdd)
            {
                hashSet.Add(thing);
            }
        }

        [Benchmark]
        public void PooledSet()
        {
            foreach (int thing in stuffToAdd)
            {
                pooledSet.Add(thing);
            }
        }

        private int[] startingElements;
        private int[] stuffToAdd;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(100, 1_000, 10_000)]
        public int N;

        [Params(0, SetSize_Large)]
        public int InitSize;

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
            startingElements = intGenerator.MakeNewTs(InitSize);
            stuffToAdd = intGenerator.MakeNewTs(N);
        }
    }
}
