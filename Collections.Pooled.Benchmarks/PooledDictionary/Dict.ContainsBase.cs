using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [Config(typeof(BenchmarkConfig))]
    public abstract class DictContainsBase<T> : DictBase
    {
        protected PooledDictionary<T, T> pooled;
        protected Dictionary<T, T> dict;

        protected abstract T GetT(int i);
        protected virtual IEqualityComparer<T> Comparer => null;

        [Params(100, 1_000, 10_000)]
        public int N;

        protected virtual PooledDictionary<T, T> CreatePooled() => new PooledDictionary<T, T>(Comparer);

        [GlobalSetup]
        public virtual void GlobalSetup()
        {
            pooled = CreatePooled();
            dict = new Dictionary<T, T>(Comparer);

            for (int i = 0; i < N; i++)
            {
                T t = GetT(i);
                pooled.Add(t, t);
                dict.Add(t, t);
            }
        }

        [GlobalCleanup]
        public virtual void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
