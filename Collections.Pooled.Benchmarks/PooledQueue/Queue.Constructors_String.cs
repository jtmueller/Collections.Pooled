using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledQueue
{
    [Config(typeof(BenchmarkConfig))]
    public class Queue_Constructors_String : QueueBase
    {
        [Benchmark(Baseline = true)]
        public void Q_Coll()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new Queue<string>(stringList);
            }
        }

        [Benchmark]
        public void Pool_Coll()
        {
            PooledQueue<string> queue;
            for (int i = 0; i < 1000; i++)
            {
                queue = new PooledQueue<string>(stringList);
                queue.Dispose();
            }
        }

        [Benchmark]
        public void Q_Enum()
        {
            for (int i = 0; i < 1000; i++)
            {
                _ = new Queue<string>(StringEnumerable());
            }
        }

        [Benchmark]
        public void Pool_Enum()
        {
            PooledQueue<string> queue;
            for (int i = 0; i < 1000; i++)
            {
                queue = new PooledQueue<string>(StringEnumerable());
                queue.Dispose();
            }
        }

        private IEnumerable<string> StringEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return stringArray[i];
        }

        [Params(1_000, 10_000)]
        public int N;

        private string[] stringArray;
        private List<string> stringList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int[] intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }

            stringList = new List<string>(stringArray);
        }
    }
}
