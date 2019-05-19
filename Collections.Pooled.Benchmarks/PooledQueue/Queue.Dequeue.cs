using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_Dequeue : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void Queue()
        {
            if (Type == QueueType.Int)
            {
                for (int i = 0; i < N; i++)
                {
                    _ = intQueue.Dequeue();
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    _ = stringQueue.Dequeue();
                }
            }
        }

        [Benchmark]
        public void Pooled()
        {
            if (Type == QueueType.Int)
            {
                for (int i = 0; i < N; i++)
                {
                    _ = intPooled.Dequeue();
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    _ = stringPooled.Dequeue();
                }
            }
        }

        [Params(10000, 100000, 1000000)]
        public int N;

        [Params(QueueType.Int, QueueType.String)]
        public QueueType Type;

        private Queue<int> intQueue;
        private Queue<string> stringQueue;
        private PooledQueue<int> intPooled;
        private PooledQueue<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            numbers = CreateArray(N);

            if (Type == QueueType.String)
            {
                strings = Array.ConvertAll(numbers, x => x.ToString());
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (Type == QueueType.Int)
            {
                intQueue = new Queue<int>(numbers);
                intPooled = new PooledQueue<int>(numbers);
            }
            else
            {
                stringQueue = new Queue<string>(strings);
                stringPooled = new PooledQueue<string>(strings);
            }
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            intPooled?.Dispose();
            stringPooled?.Dispose();
        }
    }
}
