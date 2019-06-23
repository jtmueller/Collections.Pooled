using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [Config(typeof(BenchmarkConfig))]
    public class List_AddRange_Int_CapacityIncrease : ListBase
    {
        [Benchmark(Baseline = true)]
        public void List_Array()
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
        public void List_Enumerable()
        {
            var list = new List<int>();

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
        public void Pooled_Array()
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
        public void Pooled_Enumerable()
        {
            var pooled = new PooledList<int>();

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

        private const int LARGE_SAMPLE_LENGTH = 10_000;
        private const int SMALL_LOOPS = 1_000;
        private const int SMALL_SAMPLE_LENGTH = LARGE_SAMPLE_LENGTH / SMALL_LOOPS;

        [Params(true, false)]
        public bool LargeSets;

        private int[] sampleSet;
        private int addLoops;

        private IEnumerable<int> SampleEnumerable()
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
            sampleSet = new int[samples];

            for (int i = 0; i < samples; i++)
            {
                sampleSet[i] = i;
            }

            addLoops = LARGE_SAMPLE_LENGTH / samples;
        }
    }
}
