using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Reverse : ListBase
    {
        [IterationSetup(Target = nameof(L_Int))]
        public void SetupListInt()
            => listInt = new List<int>(intItems);

        [Benchmark(Baseline = true)]
        public void L_Int()
        {
            listInt.Reverse();
        }

        [IterationSetup(Targets = new[] { nameof(P_Int), nameof(P_Span_Int) })]
        public void SetupPooledInt()
            => pooledInt = new PooledList<int>(intItems);

        [IterationCleanup(Targets = new[] { nameof(P_Int), nameof(P_Span_Int) })]
        public void CleanupPooledInt()
            => pooledInt?.Dispose();

        [Benchmark]
        public void P_Int()
        {
            pooledInt.Reverse();
        }

        [Benchmark]
        public void P_Span_Int()
        {
            pooledInt.Span.Reverse();
        }

        [IterationSetup(Target = nameof(L_Str))]
        public void SetupListString()
            => listString = new List<string>(stringItems);

        [Benchmark]
        public void L_Str()
        {
            listString.Reverse();
        }

        [IterationSetup(Targets = new[] { nameof(P_Str), nameof(P_Span_Str) })]
        public void SetupPooledString()
            => pooledString = new PooledList<string>(stringItems);

        [IterationCleanup(Targets = new[] { nameof(P_Str), nameof(P_Span_Str) })]
        public void CleanupPooledString()
            => pooledString?.Dispose();

        [Benchmark]
        public void P_Str()
        {
            pooledString.Reverse();
        }

        [Benchmark]
        public void P_Span_Str()
        {
            pooledString.Span.Reverse();
        }

        private List<int> listInt;
        private PooledList<int> pooledInt;
        private List<string> listString;
        private PooledList<string> pooledString;

        private int[] intItems;
        private string[] stringItems;

        public const int N = 100_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intItems = CreatePooled(N).ToArray();
            stringItems = Array.ConvertAll(intItems, i => i.ToString());
        }
    }
}
