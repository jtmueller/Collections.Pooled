using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    // TODO: some assembly-binding redirect bug related to System.Buffers
    // prevents us from running CoreJob with a CLR host, or ClrJob with a Core host.
    // When this is resolved, should change all the tests to run both job types at the same time.

#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Set_Remove : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Remove()
        {
            foreach (int thing in stuffToRemove)
            {
                hashSet.Remove(thing);
            }
        }

        [Benchmark]
        public void PooledSet_Remove()
        {
            foreach (int thing in stuffToRemove)
            {
                pooledSet.Remove(thing);
            }
        }

        private int[] startingElements;
        private int[] stuffToRemove;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(1, 100, 10000)]
        public int CountToRemove;

        [Params(SetSize_Large)]
        public int InitialSetSize;

        [IterationSetup(Target = nameof(HashSet_Remove))]
        public void HashIterationSetup()
        {
            hashSet = new HashSet<int>(startingElements);
        }

        [IterationSetup(Target = nameof(PooledSet_Remove))]
        public void PooledIterationSetup()
        {
            pooledSet = new PooledSet<int>(startingElements);
        }

        [IterationCleanup(Target = nameof(PooledSet_Remove))]
        public void PooledIterationCleanup()
        {
            pooledSet?.Dispose();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToRemove = intGenerator.GenerateSelectionSubset(startingElements, CountToRemove);
        }
    }
}
