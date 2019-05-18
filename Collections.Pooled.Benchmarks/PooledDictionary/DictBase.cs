using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    public abstract class DictBase
    {
        protected const int RAND_SEED = 837322;

        protected static Dictionary<int, int> CreateDictionary(int size)
        {
            var rand = new Random(RAND_SEED);
            var dict = new Dictionary<int, int>();
            while (dict.Count < size)
            {
                int key = rand.Next(500000, int.MaxValue);
#if NETCOREAPP3_0
                dict.TryAdd(key, 0);
#else
                if (!dict.ContainsKey(key))
                    dict.Add(key, 0);
#endif
            }
            return dict;
        }

        protected static PooledDictionary<int, int> CreatePooled(int size)
        {
            var rand = new Random(RAND_SEED);
            var dict = new PooledDictionary<int, int>();
            while (dict.Count < size)
            {
                int key = rand.Next(500000, int.MaxValue);
                dict.TryAdd(key, 0);
            }
            return dict;
        }
    }
}
