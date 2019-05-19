using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_Constructors : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void Queue_ICollection()
        {
            if (Type == QueueType.Int)
            {
                for (int i = 0; i < 1000; i++)
                {
                    _ = new Queue<int>(intList);
                }
            }
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    _ = new Queue<string>(stringList);
                }
            }
        }

        [Benchmark]
        public void Pooled_ICollection()
        {
            if (Type == QueueType.Int)
            {
                PooledQueue<int> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new PooledQueue<int>(intList);
                    queue.Dispose();
                }
            }
            else
            {
                PooledQueue<string> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new PooledQueue<string>(stringList);
                    queue.Dispose();
                }
            }
        }

        [Benchmark]
        public void Queue_IEnumerable()
        {
            if (Type == QueueType.Int)
            {
                for (int i = 0; i < 1000; i++)
                {
                    _ = new Queue<int>(IntEnumerable());
                }
            }
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    _ = new Queue<string>(StringEnumerable());
                }
            }
        }

        [Benchmark]
        public void Pooled_IEnumerable()
        {
            if (Type == QueueType.Int)
            {
                PooledQueue<int> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new PooledQueue<int>(IntEnumerable());
                    queue.Dispose();
                }
            }
            else
            {
                PooledQueue<string> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new PooledQueue<string>(StringEnumerable());
                    queue.Dispose();
                }
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return intArray[i];
        }

        private IEnumerable<string> StringEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return stringArray[i];
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [Params(QueueType.Int, QueueType.String)]
        public QueueType Type;

        private int[] intArray;
        private string[] stringArray;
        private List<int> intList;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }

            intList = new List<int>(intArray);
            stringList = new List<string>(stringArray);
        }
    }
}
