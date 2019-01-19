using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    public abstract class DictContainsBase<T> : DictBase
    {
        protected PooledDictionary<T, T> pooled;
        protected Dictionary<T, T> dict;

        protected abstract T GetT(int i);
        protected virtual IEqualityComparer<T> Comparer => null;

        [Params(1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public virtual void GlobalSetup()
        {
            pooled = new PooledDictionary<T, T>(Comparer);
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
