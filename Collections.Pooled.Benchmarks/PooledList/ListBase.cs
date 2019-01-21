using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledList
{
    public abstract class ListBase
    {
        protected const int RAND_SEED = 24565653;

        protected static List<int> CreateList(int size)
        {
            var rand = new Random(RAND_SEED);
            var list = new List<int>(size);
            for (int i = 0; i < size; i++)
                list.Add(rand.Next());
            return list;
        }

        protected static PooledList<int> CreatePooled(int size)
        {
            var rand = new Random(RAND_SEED);
            var list = new PooledList<int>(size);
            for (int i = 0; i < size; i++)
                list.Add(rand.Next());
            return list;
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
    }
}
