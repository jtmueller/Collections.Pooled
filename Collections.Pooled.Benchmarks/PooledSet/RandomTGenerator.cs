using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    // Generates T instances for use with HashSet perf tests
    internal sealed class RandomTGenerator<T>
    {
        private const int RAND_SEED = 24565653;

        private static readonly Random s_sequenceGenerator;
        private static readonly Random s_elementGenerator;

        private readonly Instantiate _instantiate;

        public delegate T Instantiate(Random elementGenerator, int i);

        static RandomTGenerator()
        {
            s_sequenceGenerator = new Random(RAND_SEED);
            s_elementGenerator = new Random(s_sequenceGenerator.Next());
        }

        public RandomTGenerator(Instantiate i)
        {
            _instantiate = i;
        }

        public T[] MakeNewTs(int count)
        {
            T[] results = new T[count];
            for (int i = 0; i < count; i++)
            {
                results[i] = _instantiate(s_elementGenerator, i);
            }
            return results;
        }

        public T[] GenerateSelectionSubset(T[] allObjects, int numToSelect)
        {
            T[] sequence = new T[numToSelect];
            int max = allObjects.Length - 1;
            for (int i = 0; i < numToSelect; i++)
            {
                int nextInt = s_sequenceGenerator.Next(0, max);
                sequence[i] = allObjects[nextInt];
            }
            return sequence;
        }

        public T[] GenerateMixedSelection(T[] allObjects, int numElements)
        {
            T[] sequence = new T[numElements];
            int max = allObjects.Length - 1;
            for (int i = 0; i < numElements; i++)
            {
                if (i % 2 == 0)
                {
                    int nextInt = s_sequenceGenerator.Next(0, max);
                    sequence[i] = allObjects[nextInt];
                }
                else
                {
                    sequence[i] = _instantiate(s_elementGenerator, i);
                }
            }
            return sequence;
        }
    }

    // Wrapper class for delegates capable of creating new instances of different types
    internal static class InstanceCreators
    {
        public static int IntGenerator_MinValue
        {
            get { return -50000; }
        }

        public static int IntGenerator_MaxValue
        {
            get { return 50000; }
        }

        public static int IntGenerator(Random generator, int i)
        {
            return generator.Next(IntGenerator_MinValue, IntGenerator_MaxValue);
        }

        public static DummyClass DummyGenerator(Random generator, int i)
        {
            return new DummyClass(string.Format("Dummy{0}", i));
        }
    }

    #region DummyClass / helpers
    // DummyClass is used for HashSet of object tests
    public class DummyClass
    {
        private static readonly Random s_nameRand;
        private static readonly Random s_percentRand;

        private string _id;
        private string _name;
        private double _percent;

        // initialize random generators
        static DummyClass()
        {
            s_nameRand = new Random();
            s_percentRand = new Random();
        }

        public DummyClass(string id)
        {
            _id = id;
            _name = RandomString();
            _percent = s_percentRand.NextDouble();
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Percent
        {
            get { return _percent; }
            set { _percent = value; }
        }

        public override string ToString()
        {
            return _id;
        }

        // Generates a random-ish string 
        private static string RandomString()
        {
            int size = s_nameRand.Next(0, 100);

            var builder = new StringBuilder(size);
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * s_nameRand.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }

    // Dummy equality comparer for some DummyClass test cases where we want
    // DummyClass value equality instead of reference
    public class DummyClassEqualityComparer : IEqualityComparer<DummyClass>
    {
        // Don't worry about boundary cases like null, etc; this is just for DummyClass test cases below
        public bool Equals(DummyClass x, DummyClass y)
        {
            return x.Id.Equals(y.Id) && x.Name.Equals(y.Name) && x.Percent.Equals(y.Percent);
        }

        public int GetHashCode(DummyClass obj)
        {
            return obj.Id.GetHashCode() | obj.Name.GetHashCode() | obj.Percent.GetHashCode();
        }
    }
    #endregion
}
