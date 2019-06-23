using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    public abstract class SetBase
    {
        protected const int RAND_SEED = 24565653;
        protected const int SetSize_Large = 8_000_000;
        protected const int SetSize_Small = 320_000;
        protected const int MaxStartSize = 32_000;

        protected static HashSet<int> CreateSet(int size)
        {
            var rand = new Random(RAND_SEED);
            var set = new HashSet<int>(size);
            for (int i = 0; i < size; i++)
                set.Add(rand.Next());
            return set;
        }

        protected static PooledSet<int> CreatePooled(int size)
        {
            var rand = new Random(RAND_SEED);
            var set = new PooledSet<int>(size);
            for (int i = 0; i < size; i++)
                set.Add(rand.Next());
            return set;
        }

        protected static int[] CreateArray(int size)
        {
            var rand = new Random(RAND_SEED);
            int[] output = new int[size];
            for (int i = 0; i < size; i++)
            {
                output[i] = rand.Next();
            }
            return output;
        }

        public enum CollectionType
        {
            Set,
            Enumerable,
            Array
        }
    }
}
