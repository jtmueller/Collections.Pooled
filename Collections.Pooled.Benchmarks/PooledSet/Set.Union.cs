using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Union : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Hashset()
        {
            hashSet.UnionWith(hashSetToUnion);
        }

        [Benchmark]
        public void PooledSet_PooledSet()
        {
            pooledSet.UnionWith(pooledSetToUnion);
        }

        [Benchmark]
        public void HashSet_Enum()
        {
            hashSet.UnionWith(GetEnum());
        }

        [Benchmark]
        public void PooledSet_Enum()
        {
            pooledSet.UnionWith(GetEnum());
        }

        [Benchmark]
        public void HashSet_Array()
        {
            hashSet.UnionWith(stuffToUnion);
        }

        [Benchmark]
        public void PooledSet_Array()
        {
            pooledSet.UnionWith(stuffToUnion);
        }

        private IEnumerable<int> GetEnum()
        {
            for (int i = 0; i < stuffToUnion.Length; i++)
            {
                yield return stuffToUnion[i];
            }
        }

        private int[] startingElements;
        private int[] stuffToUnion;
        private HashSet<int> hashSet;
        private HashSet<int> hashSetToUnion;
        private PooledSet<int> pooledSet;
        private PooledSet<int> pooledSetToUnion;

        [Params(SetSize_Small, MaxStartSize)]
        public int CountToUnion;

        [Params(SetSize_Small, SetSize_Large)]
        public int InitialSetSize;

        [IterationSetup]
        public void IterationSetup()
        {
            hashSet.UnionWith(startingElements);
            pooledSet.UnionWith(startingElements);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            hashSet.Clear();
            pooledSet.Clear();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToUnion = intGenerator.GenerateMixedSelection(startingElements, CountToUnion);

            hashSet = new HashSet<int>();
            hashSetToUnion = new HashSet<int>(stuffToUnion);
            pooledSet = new PooledSet<int>();
            pooledSetToUnion = new PooledSet<int>(stuffToUnion);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
            pooledSetToUnion?.Dispose();
        }
    }
}
