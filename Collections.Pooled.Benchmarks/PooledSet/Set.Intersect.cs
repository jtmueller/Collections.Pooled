using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Intersect : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            hashSet.IntersectWith(Kind switch
            {
                CollectionType.Set => hashSetToIntersect,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToIntersect,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
        }

        [Benchmark]
        public void PooledSet()
        {
            pooledSet.IntersectWith(Kind switch
            {
                CollectionType.Set => hashSetToIntersect,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToIntersect,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
        }

        private IEnumerable<int> GetEnum()
        {
            for (int i = 0; i < stuffToIntersect.Length; i++)
            {
                yield return stuffToIntersect[i];
            }
        }

        private int[] startingElements;
        private int[] stuffToIntersect;
        private HashSet<int> hashSet;
        private HashSet<int> hashSetToIntersect;
        private PooledSet<int> pooledSet;
        private PooledSet<int> pooledSetToIntersect;

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
            stuffToIntersect = intGenerator.GenerateMixedSelection(startingElements, N);

            hashSet = new HashSet<int>();
            hashSetToIntersect = new HashSet<int>(stuffToIntersect);
            pooledSet = new PooledSet<int>();
            pooledSetToIntersect = new PooledSet<int>(stuffToIntersect);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
            pooledSetToIntersect?.Dispose();
        }
    }
}