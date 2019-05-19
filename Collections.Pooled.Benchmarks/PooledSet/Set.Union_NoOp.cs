using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Union_NoOp : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            hashSet.UnionWith(stuffToUnion);
        }

        [Benchmark]
        public void PooledSet()
        {
            pooledSet.UnionWith(stuffToUnion);
        }

        private int[] stuffToUnion;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(SetSize_Small, MaxStartSize)]
        public int CountToUnion;

        [Params(MaxStartSize, SetSize_Small)]
        public int InitialSetSize;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            int[] startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToUnion = intGenerator.GenerateMixedSelection(startingElements, CountToUnion);

            hashSet = new HashSet<int>(startingElements);
            hashSet.UnionWith(stuffToUnion);
            pooledSet = new PooledSet<int>(startingElements);
            pooledSet.UnionWith(stuffToUnion);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
        }
    }
}
