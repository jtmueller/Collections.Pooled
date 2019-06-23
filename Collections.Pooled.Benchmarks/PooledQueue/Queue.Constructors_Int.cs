using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_Constructors_Int : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void Q_Coll()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new Queue<int>(intList);
            }
        }

        [Benchmark]
        public void Pool_Coll()
        {
            PooledQueue<int> queue;
            for (int i = 0; i < 1000; i++)
            {
                queue = new PooledQueue<int>(intList);
                queue.Dispose();
            }
        }

        [Benchmark]
        public void Q_Enum()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new Queue<int>(IntEnumerable());
            }
        }

        [Benchmark]
        public void Pool_Enum()
        {
            PooledQueue<int> queue;
            for (int i = 0; i < 1000; i++)
            {
                queue = new PooledQueue<int>(IntEnumerable());
                queue.Dispose();
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return intArray[i];
        }

        [Params(1_000, 10_000)]
        public int N;

        private int[] intArray;
        private List<int> intList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);
            intList = new List<int>(intArray);
        }
    }
}
