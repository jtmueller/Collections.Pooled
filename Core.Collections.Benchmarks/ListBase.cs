using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Collections.Benchmarks
{
    public abstract class ListBase
    {
        protected const int RAND_SEED = 24565653;

        protected static List<int> CreateList(int size)
        {
            var rand = new Random(RAND_SEED);
            var list = new List<int>();
            for (int i = 0; i < size; i++)
                list.Add(rand.Next());
            return list;
        }

        protected static PooledList<int> CreatePooled(int size)
        {
            var rand = new Random(RAND_SEED);
            var list = new PooledList<int>();
            for (int i = 0; i < size; i++)
                list.Add(rand.Next());
            return list;
        }
    }
}
