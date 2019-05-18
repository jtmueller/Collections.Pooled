using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [CoreJob, ClrJob]
    [MemoryDiagnoser]
    public class List_AddRange_Int_NoCapacityIncrease : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAddRange_Int_NoCapacityIncrease()
        {
            //Clear the list without changing its capacity, so that when more data is added to the list its
            //capacity will not need to increase.
            list.RemoveRange(0, startingCapacity);

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
        public void PooledAddRange_Int_NoCapacityIncrease_Array()
        {
            //Clear the list without changing its capacity, so that when more data is added to the list its
            //capacity will not need to increase.
            pooled.RemoveRange(0, startingCapacity);

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
        }

        [Benchmark]
        public void PooledAddRange_Int_NoCapacityIncrease_Enumerable()
        {
            //Clear the list without changing its capacity, so that when more data is added to the list its
            //capacity will not need to increase.
            pooled.RemoveRange(0, startingCapacity);

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
        private List<int> list;
        private PooledList<int> pooled;
        private int addLoops, startingCapacity;

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

            // create lists big enough to hold 17 copies of the sample set
            startingCapacity = 17 * samples * addLoops;
            list = new List<int>(startingCapacity);
            pooled = new PooledList<int>(startingCapacity);

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
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
