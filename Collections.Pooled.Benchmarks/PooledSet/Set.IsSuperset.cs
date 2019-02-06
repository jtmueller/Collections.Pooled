using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
#if NETCOREAPP3_0
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Set_IsSuperset : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_IsSuperset_Hashset()
        {
            hashSet.IsSupersetOf(hashSetToCheck);
        }

        [Benchmark]
        public void PooledSet_IsSuperset_PooledSet()
        {
            pooledSet.IsSupersetOf(pooledSetToCheck);
        }

        [Benchmark]
        public void HashSet_IsSuperset_Enum()
        {
            hashSet.IsSupersetOf(GetEnum());
        }

        [Benchmark]
        public void PooledSet_IsSuperset_Enum()
        {
            pooledSet.IsSupersetOf(GetEnum());
        }

        [Benchmark]
        public void HashSet_IsSuperset_Array()
        {
            hashSet.IsSupersetOf(stuffToCheck);
        }

        [Benchmark]
        public void PooledSet_IsSuperset_Array()
        {
            pooledSet.IsSupersetOf(stuffToCheck);
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

        [Params(MaxStartSize, SetSize_Small)]
        public int InitialSetSize;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            int[] startingElements = intGenerator.MakeNewTs(MaxStartSize);

            stuffToCheck = intGenerator.GenerateSelectionSubset(startingElements, InitialSetSize);

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