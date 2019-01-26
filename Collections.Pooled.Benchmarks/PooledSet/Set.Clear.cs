﻿using System;
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
    public class Set_Clear : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Clear()
        {
            hashSet.Clear();
        }

        [Benchmark]
        public void PooledSet_Clear()
        {
            pooledSet.Clear();
        }

        private int[] startingElements;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(SetSize_Small)]
        public int InitialSetSize;

        [IterationSetup(Target = nameof(HashSet_Clear))]
        public void HashIterationSetup()
        {
            hashSet.UnionWith(startingElements);
        }

        [IterationSetup(Target = nameof(PooledSet_Clear))]
        public void PooledIterationSetup()
        {
            pooledSet.UnionWith(startingElements);
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);

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
