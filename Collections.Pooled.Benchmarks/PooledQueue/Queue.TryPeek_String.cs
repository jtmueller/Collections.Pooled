using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_TryPeek_String : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void QueueTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = stringQueue.TryPeek(out _);
            }
        }

        [Benchmark]
        public void PooledTryPeek()
        {
            for (int i = 0; i <= N; i++)
            {
                _ = stringPooled.TryPeek(out _);
            }
        }

        [Params(10_000, 100_000)]
        public int N;

        [Params(true, false)]
        public bool EmptyQueue;

        private Queue<string> stringQueue;
        private PooledQueue<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (EmptyQueue)
            {
                stringQueue = new Queue<string>();
                stringPooled = new PooledQueue<string>();
            }

            numbers = CreateArray(N);
            strings = Array.ConvertAll(numbers, x => x.ToString());
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (EmptyQueue)
                return;

            stringQueue = new Queue<string>(strings);
            stringPooled = new PooledQueue<string>(strings);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            if (EmptyQueue)
                return;

            stringPooled?.Dispose();
        }
    }
}
