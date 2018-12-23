using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [CoreJob, MemoryDiagnoser]
    public class List_Constructors : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListArrayConstructor()
        {
            for (int i = 0; i < 1000; i++)
                new List<int>(array);
        }

        [Benchmark]
        public void PooledArrayConstructor()
        {
            for (int i = 0; i < 1000; i++)
            {
                var list = new PooledList<int>(array);
                list.Dispose();
            }
        }

        [Params(1000, 10000, 100000)]
        public int N;

        private int[] array;

        [GlobalSetup]
        public void ArraySetup()
        {
            array = new int[N];
            var rand = new Random(RAND_SEED);
            for (int i = 0; i < N; i++)
            {
                array[i] = rand.Next();
            }
        }
    }
}
