using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    public abstract class QueueBase
    {
        protected const int RAND_SEED = 24565653;

        protected static Queue<int> CreateQueue(int size)
        {
            var rand = new Random(RAND_SEED);
            var queue = new Queue<int>(size);
            for (int i = 0; i < size; i++)
                queue.Enqueue(rand.Next());
            return queue;
        }

        protected static PooledQueue<int> CreatePooled(int size)
        {
            var rand = new Random(RAND_SEED);
            var queue = new PooledQueue<int>(size);
            for (int i = 0; i < size; i++)
                queue.Enqueue(rand.Next());
            return queue;
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

        public enum QueueType
        {
            Int, String
        }
    }
}
