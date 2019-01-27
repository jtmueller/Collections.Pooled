﻿using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Set_Except : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Except_Hashset()
        {
            hashSet.ExceptWith(hashSetToExcept);
        }

        [Benchmark]
        public void PooledSet_Except_PooledSet()
        {
            pooledSet.ExceptWith(pooledSetToExcept);
        }

        [Benchmark]
        public void HashSet_Except_Enum()
        {
            hashSet.ExceptWith(GetEnum());
        }

        [Benchmark]
        public void PooledSet_Except_Enum()
        {
            pooledSet.ExceptWith(GetEnum());
        }

        [Benchmark]
        public void HashSet_Except_Array()
        {
            hashSet.ExceptWith(stuffToExcept);
        }

        [Benchmark]
        public void PooledSet_Except_Array()
        {
            pooledSet.ExceptWith(stuffToExcept);
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