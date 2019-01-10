using System;
using System.Collections;

namespace Collections.Pooled.Tests.PooledDictionary
{
    public class Dictionary_NonGeneric_Tests : IDictionary_NonGeneric_Tests
    {
        protected override IDictionary NonGenericIDictionaryFactory()
        {
            var dict = new PooledDictionary<int, string>();
            RegisterForDispose(dict);
            return dict;
        }

        protected override object CreateTKey(int seed)
        {
            Random rand = new Random(seed);
            return rand.Next();
        }

        protected override object CreateTValue(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
