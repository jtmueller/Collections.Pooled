using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Clear : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            hashSet.Clear();
        }

        [Benchmark]
        public void PooledSet()
        {
            pooledSet.Clear();
        }

        private int[] startingElements;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(SetSize_Small)]
        public int InitialSetSize;

        [IterationSetup(Target = nameof(HashSet))]
        public void HashIterationSetup()
        {
            hashSet.UnionWith(startingElements);
        }

        [IterationSetup(Target = nameof(PooledSet))]
        public void PooledIterationSetup()
        {
            pooledSet.UnionWith(startingElements);
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);

            hashSet = new HashSet<int>();
            pooledSet = new PooledSet<int>();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
        }
    }
}
