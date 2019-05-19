using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [Config(typeof(BenchmarkConfig))]
    public class Set_Intersect : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Hashset()
        {
            hashSet.IntersectWith(hashSetToIntersect);
        }

        [Benchmark]
        public void PooledSet_PooledSet()
        {
            pooledSet.IntersectWith(pooledSetToIntersect);
        }

        [Benchmark]
        public void HashSet_Enum()
        {
            hashSet.IntersectWith(GetEnum());
        }

        [Benchmark]
        public void PooledSet_Enum()
        {
            pooledSet.IntersectWith(GetEnum());
        }

        [Benchmark]
        public void HashSet_Array()
        {
            hashSet.IntersectWith(stuffToIntersect);
        }

        [Benchmark]
        public void PooledSet_Array()
        {
            pooledSet.IntersectWith(stuffToIntersect);
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
            stuffToIntersect = intGenerator.GenerateMixedSelection(startingElements, CountToIntersect);

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