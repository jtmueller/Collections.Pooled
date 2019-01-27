using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Set_Union : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Union()
        {
            hashSet.UnionWith(stuffToUnion);
        }

        [Benchmark]
        public void PooledSet_Union()
        {
            pooledSet.UnionWith(stuffToUnion);
        }

        private int[] startingElements;
        private int[] stuffToUnion;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(SetSize_Small, MaxStartSize)]
        public int CountToUnion;

        [Params(MaxStartSize, SetSize_Small, SetSize_Large)]
        public int InitialSetSize;

        [IterationSetup(Target = nameof(HashSet_Union))]
        public void HashIterationSetup()
        {
            hashSet.UnionWith(startingElements);
        }

        [IterationCleanup(Target = nameof(HashSet_Union))]
        public void HashIterationCleanup()
        {
            hashSet.Clear();
        }

        [IterationSetup(Target = nameof(PooledSet_Union))]
        public void PooledIterationSetup()
        {
            pooledSet.UnionWith(startingElements);
        }

        [IterationCleanup(Target = nameof(PooledSet_Union))]
        public void PooledIterationCleanup()
        {
            pooledSet.Clear();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToUnion = intGenerator.GenerateMixedSelection(startingElements, CountToUnion);

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
