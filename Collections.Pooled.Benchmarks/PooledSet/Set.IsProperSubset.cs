using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_IsProperSubset : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            hashSet.IsProperSubsetOf(Kind switch
            {
                CollectionType.Set => hashSetToCheck,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToCheck,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
        }

        [Benchmark]
        public void PooledSet()
        {
            pooledSet.IsProperSubsetOf(Kind switch
            {
                CollectionType.Set => pooledSetToCheck,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToCheck,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
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

        public const int InitialSetSize = SetSize_Small;

        [Params(CollectionType.Set, CollectionType.Enumerable, CollectionType.Array)]
        public CollectionType Kind;

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