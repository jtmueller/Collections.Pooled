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
    public class Queue_Constructors : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void QueueICollectionConstructor()
        {
            if (Type == QueueType.Int)
            {
                Queue<int> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new Queue<int>(intList);
                }
            }
            else
            {
                Queue<string> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new Queue<string>(stringList);
                }
            }
        }

        [Benchmark]
        public void PooledICollectionConstructor()
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
        public void QueueIEnumerableConstructor()
        {
            if (Type == QueueType.Int)
            {
                Queue<int> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new Queue<int>(IntEnumerable());
                }
            }
            else
            {
                Queue<string> queue;
                for (int i = 0; i < 1000; i++)
                {
                    queue = new Queue<string>(StringEnumerable());
                }
            }
        }

        [Benchmark]
        public void PooledIEnumerableConstructor()
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
