using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class Queue_Dequeue : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void QueueDequeue()
        {
            if (Type == QueueType.Int)
            {
                int result;
                for (int i = 0; i < N; i++)
                {
                    result = intQueue.Dequeue();
                }
            }
            else
            {
                string result;
                for (int i = 0; i < N; i++)
                {
                    result = stringQueue.Dequeue();
                }
            }
        }

        [Benchmark]
        public void PooledDequeue()
        {
            if (Type == QueueType.Int)
            {
                int result;
                for (int i = 0; i < N; i++)
                {
                    result = intPooled.Dequeue();
                }
            }
            else
            {
                string result;
                for (int i = 0; i < N; i++)
                {
                    result = stringPooled.Dequeue();
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
