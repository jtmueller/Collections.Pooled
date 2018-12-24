using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Core.Collections.Benchmarks
{
#if NETCOREAPP2_2
    [CoreJob]
#elif NET472
    [ClrJob]
#endif
    [MemoryDiagnoser]
    public class List_AddRange_String_CapacityIncrease : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAddRange_String_CapacityIncrease()
        {
            var list = new List<string>();

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
        public void PooledAddRange_String_CapacityIncrease_Array()
        {
            var pooled = new PooledList<string>();

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
            var pooled = new PooledList<string>();

            var enumerable = (IEnumerable<string>)sampleSet;

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

        private string[] sampleSet;
        private int addLoops;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int samples = GetSampleLength();
            sampleSet = new string[samples];

            for (int i = 0; i < samples; i++)
            {
                sampleSet[i] = i.ToString();
            }

            addLoops = LARGE_SAMPLE_LENGTH / samples;
        }
    }
}
