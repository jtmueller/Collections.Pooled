using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Union : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            hashSet.UnionWith(Kind switch
            {
                CollectionType.Set => hashSetToUnion,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToUnion,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
        }

        [Benchmark]
        public void PooledSet()
        {
            pooledSet.UnionWith(Kind switch
            {
                CollectionType.Set => pooledSetToUnion,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToUnion,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
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

        public const int N = MaxStartSize;
        public const int InitialSetSize = SetSize_Large;

        [Params(CollectionType.Set, CollectionType.Enumerable, CollectionType.Array)]
        public CollectionType Kind;

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
            stuffToUnion = intGenerator.GenerateMixedSelection(startingElements, N);

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
