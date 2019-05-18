using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [CoreJob, ClrJob]
    public class StringHashing : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DefaultStringHash()
        {
            var comparer = EqualityComparer<string>.Default;
            int hash;
            for (int i = 0; i < N; i++)
            {
                hash = comparer.GetHashCode(strings[i]);
            }
        }

        [Benchmark]
        public void NonRandomizedStringHash()
        {
            var comparer = NonRandomizedStringEqualityComparer.Default;
            int hash;
            for (int i = 0; i < N; i++)
            {
                hash = comparer.GetHashCode(strings[i]);
            }
        }

        private string[] strings;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int seed = RAND_SEED;
            strings = new string[N];
            for (int i = 0; i < N; i++)
            {
                strings[i] = CreateString(seed++);
            }
        }

        private string CreateString(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            var bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
