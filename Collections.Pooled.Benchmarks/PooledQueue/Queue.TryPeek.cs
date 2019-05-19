using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_TryPeek : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void QueueTryPeek()
        {
            if (Type == QueueType.Int)
            {
                for (int i = 0; i <= N; i++)
                {
                    _ = intQueue.TryPeek(out _);
                }
            }
            else
            {
                for (int i = 0; i <= N; i++)
                {
                    _ = stringQueue.TryPeek(out _);
                }
            }
        }

        [Benchmark]
        public void PooledTryPeek()
        {
            if (Type == QueueType.Int)
            {
                for (int i = 0; i <= N; i++)
                {
                    _ = intPooled.TryPeek(out _);
                }
            }
            else
            {
                for (int i = 0; i <= N; i++)
                {
                    _ = stringPooled.TryPeek(out _);
                }
            }
        }

        [Params(10000, 100000, 1000000)]
        public int N;

        [Params(QueueType.Int, QueueType.String)]
        public QueueType Type;

        [Params(true, false)]
        public bool EmptyQueue;

        private Queue<int> intQueue;
        private Queue<string> stringQueue;
        private PooledQueue<int> intPooled;
        private PooledQueue<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (EmptyQueue)
            {
                intQueue = new Queue<int>();
                stringQueue = new Queue<string>();
                intPooled = new PooledQueue<int>();
                stringPooled = new PooledQueue<string>();
            }

            numbers = CreateArray(N);

            if (Type == QueueType.String)
            {
                strings = Array.ConvertAll(numbers, x => x.ToString());
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (EmptyQueue)
                return;

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
            if (EmptyQueue)
                return;

            intPooled?.Dispose();
            stringPooled?.Dispose();
        }
    }
}
