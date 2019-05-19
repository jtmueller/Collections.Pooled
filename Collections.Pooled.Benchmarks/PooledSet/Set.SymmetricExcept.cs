using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_SymmetricExcept : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Hashset()
        {
            hashSet.SymmetricExceptWith(hashSetToExcept);
        }

        [Benchmark]
        public void PooledSet_PooledSet()
        {
            pooledSet.SymmetricExceptWith(pooledSetToExcept);
        }

        [Benchmark]
        public void HashSet_Enum()
        {
            hashSet.SymmetricExceptWith(GetEnum());
        }

        [Benchmark]
        public void PooledSet_Enum()
        {
            pooledSet.SymmetricExceptWith(GetEnum());
        }

        [Benchmark]
        public void HashSet_Array()
        {
            hashSet.SymmetricExceptWith(stuffToExcept);
        }

        [Benchmark]
        public void PooledSet_Array()
        {
            pooledSet.SymmetricExceptWith(stuffToExcept);
        }

        private IEnumerable<int> GetEnum()
        {
            for (int i = 0; i < stuffToExcept.Length; i++)
            {
                yield return stuffToExcept[i];
            }
        }

        private int[] startingElements;
        private int[] stuffToExcept;
        private HashSet<int> hashSet;
        private HashSet<int> hashSetToExcept;
        private PooledSet<int> pooledSet;
        private PooledSet<int> pooledSetToExcept;

        [Params(MaxStartSize, SetSize_Small)]
        public int CountToIntersect;

        [Params(SetSize_Large)]
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
            stuffToExcept = intGenerator.GenerateMixedSelection(startingElements, CountToIntersect);

            hashSet = new HashSet<int>();
            hashSetToExcept = new HashSet<int>(stuffToExcept);
            pooledSet = new PooledSet<int>();
            pooledSetToExcept = new PooledSet<int>(stuffToExcept);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
            pooledSetToExcept?.Dispose();
        }
    }
}