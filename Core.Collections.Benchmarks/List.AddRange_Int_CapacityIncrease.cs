using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
    [CoreJob, MemoryDiagnoser]
    public class List_AddRange_Int_CapacityIncrease : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAddRange_Int_CapacityIncrease()
        {
            var list = new List<int>();

            for (int j = 0; j < addLoops; j++)
            {
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
                list.AddRange(sampleSet);
            }
        }

        [Benchmark]
        public void PooledAddRange_Int_CapacityIncrease_Array()
        {
            var pooled = new PooledList<int>();

            for (int j = 0; j < addLoops; j++)
            {
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
                pooled.AddRange(sampleSet);
            }

            pooled.Dispose();
        }

        [Benchmark]
        public void PooledAddRange_Int_CapacityIncrease_Enumerable()
        {
            var pooled = new PooledList<int>();

            var enumerable = (IEnumerable<int>)sampleSet;

            for (int j = 0; j < addLoops; j++)
            {
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
                pooled.AddRange(enumerable);
            }

            pooled.Dispose();
        }

        private int GetSampleLength()
        {
            if (LargeSets)
                return LARGE_SAMPLE_LENGTH;
            else
                return SMALL_SAMPLE_LENGTH;
        }

        private const int LARGE_SAMPLE_LENGTH = 10000;
        private const int SMALL_LOOPS = 1000;
        private const int SMALL_SAMPLE_LENGTH = LARGE_SAMPLE_LENGTH / SMALL_LOOPS;

        [Params(true, false)]
        public bool LargeSets;

        private int[] sampleSet;
        private int addLoops;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int samples = GetSampleLength();
            sampleSet = new int[samples];

            for (int i = 0; i < samples; i++)
            {
                sampleSet[i] = i;
            }

            addLoops = LARGE_SAMPLE_LENGTH / samples;
        }
    }
}
