using System;
using System.Collections;

namespace Collections.Pooled.Tests.PooledDictionary
{
    public class Dictionary_NonGeneric_Tests : IDictionary_NonGeneric_Tests
    {
        public override bool SupportsJson => true;
        public override Type CollectionType => typeof(PooledDictionary<string, int>);

        protected override IDictionary NonGenericIDictionaryFactory()
        {
            var dict = new PooledDictionary<string, string>();
            RegisterForDispose(dict);
            return dict;
        }

        protected override object CreateTValue(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        protected override object CreateTKey(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
