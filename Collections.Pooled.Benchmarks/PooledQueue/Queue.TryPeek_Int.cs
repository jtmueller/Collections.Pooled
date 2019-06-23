using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_TryPeek_Int : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void QueueTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = intQueue.TryPeek(out _);
            }

        }

        [Benchmark]
        public void PooledTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = intPooled.TryPeek(out _);
            }
        }

        [Params(10_000, 100_000)]
        public int N;

        [Params(true, false)]
        public bool EmptyQueue;

        private Queue<int> intQueue;
        private PooledQueue<int> intPooled;
        private int[] numbers;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (EmptyQueue)
            {
                intQueue = new Queue<int>();
                intPooled = new PooledQueue<int>();
            }

            numbers = CreateArray(N);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (EmptyQueue)
                return;

            intQueue = new Queue<int>(numbers);
            intPooled = new PooledQueue<int>(numbers);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            if (EmptyQueue)
                return;

            intPooled?.Dispose();
        }
    }
}
