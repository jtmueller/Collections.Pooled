using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class Set_IsSubset : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_IsSubset_Hashset()
        {
            hashSet.IsSubsetOf(hashSetToCheck);
        }

        [Benchmark]
        public void PooledSet_IsSubset_PooledSet()
        {
            pooledSet.IsSubsetOf(pooledSetToCheck);
        }

        [Benchmark]
        public void HashSet_IsSubset_Enum()
        {
            hashSet.IsSubsetOf(GetEnum());
        }

        [Benchmark]
        public void PooledSet_IsSubset_Enum()
        {
            pooledSet.IsSubsetOf(GetEnum());
        }

        [Benchmark]
        public void HashSet_IsSubset_Array()
        {
            hashSet.IsSubsetOf(stuffToCheck);
        }

        [Benchmark]
        public void PooledSet_IsSubset_Array()
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