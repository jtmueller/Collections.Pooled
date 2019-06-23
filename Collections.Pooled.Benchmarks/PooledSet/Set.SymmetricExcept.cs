using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_SymmetricExcept : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet()
        {
            hashSet.SymmetricExceptWith(Kind switch
            {
                CollectionType.Set => hashSetToExcept,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToExcept,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
        }

        [Benchmark]
        public void PooledSet()
        {
            pooledSet.SymmetricExceptWith(Kind switch
            {
                CollectionType.Set => pooledSetToExcept,
                CollectionType.Enumerable => GetEnum(),
                CollectionType.Array => stuffToExcept,
                _ => throw new InvalidOperationException("Not a valid collection type")
            });
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
            stuffToExcept = intGenerator.GenerateMixedSelection(startingElements, N);

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