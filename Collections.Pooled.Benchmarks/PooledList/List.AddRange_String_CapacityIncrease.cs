using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_AddRange_String_CapacityIncrease : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Array()
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
        public void Pooled_Array()
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
        public void List_Enumerable()
        {
            var list = new List<string>();

            for (int j = 0; j < addLoops; j++)
            {
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
                list.AddRange(SampleEnumerable());
            }
        }

        [Benchmark]
        public void Pooled_Enumerable()
        {
            var pooled = new PooledList<string>();

            for (int j = 0; j < addLoops; j++)
            {
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
                pooled.AddRange(SampleEnumerable());
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

        private IEnumerable<string> SampleEnumerable()
        {
            for (int i = 0; i < sampleSet.Length; i++)
            {
                yield return sampleSet[i];
            }
        }

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
