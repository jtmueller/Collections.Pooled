using System;
using System.Buffers;
using System.Collections.Generic;

namespace Collections.Pooled.Tests.StringKeyedDictionary
{
#if NETCOREAPP3_1
    public class StringDictionary_Tests_string : StringDictionary_Generic_Tests<string>
    {
        protected override KeyValuePair<string, string> CreateT(int seed)
            => new KeyValuePair<string, string>(CreateTKey(seed), CreateTKey(seed + 500));

        protected override string CreateTKey(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            byte[] pooled = stringLength < 33 ? null : ArrayPool<byte>.Shared.Rent(stringLength);
            try
            {
                Span<byte> bytes = pooled ?? stackalloc byte[stringLength];
                rand.NextBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
            finally
            {
                if (pooled is object)
                    ArrayPool<byte>.Shared.Return(pooled);
            }
        }

        protected override string CreateTValue(int seed) => CreateTKey(seed);
    }

    public class StringDictionary_Tests_int : StringDictionary_Generic_Tests<int>
    {
        protected override KeyValuePair<string, int> CreateT(int seed)
            => new KeyValuePair<string, int>(CreateTKey(seed), CreateTValue(seed + 500));

        protected override string CreateTKey(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            byte[] pooled = stringLength < 33 ? null : ArrayPool<byte>.Shared.Rent(stringLength);
            try
            {
                Span<byte> bytes = pooled ?? stackalloc byte[stringLength];
                rand.NextBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
            finally
            {
                if (pooled is object)
                    ArrayPool<byte>.Shared.Return(pooled);
            }
        }

        protected override int CreateTValue(int seed) => new Random(seed).Next();
    }
#endif
}
