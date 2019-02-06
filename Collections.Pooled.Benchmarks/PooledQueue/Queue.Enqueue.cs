using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
#if NETCOREAPP3_0
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Queue_Enqueue : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void QueueEnqueue()
        {
            if (Type == QueueType.Int)
            {
                var queue = new Queue<int>();
                for (int i = 0; i < N; i++)
                {
                    queue.Enqueue(intArray[i]);
                }
            }
            else
            {
                var queue = new Queue<string>();
                for (int i = 0; i < N; i++)
                {
                    queue.Enqueue(stringArray[i]);
                }
            }
        }

        [Benchmark]
        public void PooledEnqueue()
        {
            if (Type == QueueType.Int)
            {
                using var queue = new PooledQueue<int>();
                for (int i = 0; i < N; i++)
                {
                    queue.Enqueue(intArray[i]);
                }
            }
            else
            {
                using var queue = new PooledQueue<string>();
                for (int i = 0; i < N; i++)
                {
                    queue.Enqueue(stringArray[i]);
                }
            }
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [Params(QueueType.Int, QueueType.String)]
        public QueueType Type;

        private int[] intArray;
        private string[] stringArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }
        }
    }
}
