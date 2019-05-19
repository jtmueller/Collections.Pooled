using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_IsSubset : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Hashset()
        {
            hashSet.IsSubsetOf(hashSetToCheck);
        }

        [Benchmark]
        public void PooledSet_PooledSet()
        {
            pooledSet.IsSubsetOf(pooledSetToCheck);
        }

        [Benchmark]
        public void HashSet_Enum()
        {
            hashSet.IsSubsetOf(GetEnum());
        }

        [Benchmark]
        public void PooledSet_Enum()
        {
            pooledSet.IsSubsetOf(GetEnum());
        }

        [Benchmark]
        public void HashSet_Array()
        {
            hashSet.IsSubsetOf(stuffToCheck);
        }

        [Benchmark]
        public void PooledSet__Array()
        {
            pooledSet.IsSubsetOf(stuffToCheck);
        }

        private IEnumerable<int> GetEnum()
        {
            for (int i = 0; i < stuffToCheck.Length; i++)
            {
                yield return stuffToCheck[i];
            }
        }

        private int[] stuffToCheck;
        private HashSet<int> hashSet;
        private HashSet<int> hashSetToCheck;
        private PooledSet<int> pooledSet;
        private PooledSet<int> pooledSetToCheck;

        [Params(SetSize_Small, SetSize_Large)]
        public int InitialSetSize;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            int[] startingElements = intGenerator.MakeNewTs(InitialSetSize);

            stuffToCheck = intGenerator.GenerateMixedSelection(startingElements, InitialSetSize);

            hashSet = new HashSet<int>(startingElements);
            hashSetToCheck = new HashSet<int>(stuffToCheck);
            pooledSet = new PooledSet<int>(startingElements);
            pooledSetToCheck = new PooledSet<int>(stuffToCheck);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
            pooledSetToCheck?.Dispose();
        }
    }
}