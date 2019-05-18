using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_Reverse : ListBase
    {
        [IterationSetup(Target = nameof(List_Int))]
        public void SetupListInt()
            => listInt = new List<int>(intItems);

        [Benchmark(Baseline = true)]
        public void List_Int()
        {
            listInt.Reverse();
        }

        [IterationSetup(Targets = new[] { nameof(Pooled_Int), nameof(Pooled_Span_Int) })]
        public void SetupPooledInt()
            => pooledInt = new PooledList<int>(intItems);

        [IterationCleanup(Targets = new[] { nameof(Pooled_Int), nameof(Pooled_Span_Int) })]
        public void CleanupPooledInt()
            => pooledInt?.Dispose();

        [Benchmark]
        public void Pooled_Int()
        {
            pooledInt.Reverse();
        }

        [Benchmark]
        public void Pooled_Span_Int()
        {
            pooledInt.Span.Reverse();
        }

        [IterationSetup(Target = nameof(List_String))]
        public void SetupListString()
            => listString = new List<string>(stringItems);

        [Benchmark]
        public void List_String()
        {
            listString.Reverse();
        }

        [IterationSetup(Targets = new[] { nameof(Pooled_String), nameof(Pooled_Span_String) })]
        public void SetupPooledString()
            => pooledString = new PooledList<string>(stringItems);

        [IterationCleanup(Targets = new[] { nameof(Pooled_String), nameof(Pooled_Span_String) })]
        public void CleanupPooledString()
            => pooledString?.Dispose();

        [Benchmark]
        public void Pooled_String()
        {
            pooledString.Reverse();
        }

        [Benchmark]
        public void Pooled_Span_String()
        {
            pooledString.Span.Reverse();
        }

        private List<int> listInt;
        private PooledList<int> pooledInt;
        private List<string> listString;
        private PooledList<string> pooledString;

        private int[] intItems;
        private string[] stringItems;

        [Params(100_000, 200_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intItems = CreatePooled(N).ToArray();
            stringItems = Array.ConvertAll(intItems, i => i.ToString());
        }
    }
}
