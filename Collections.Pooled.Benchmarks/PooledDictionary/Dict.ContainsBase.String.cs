using System;
using System.Buffers;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    public class DictContainsBase_String : DictContainsBase<string>
    {

#if NETCOREAPP3_1
        protected override string GetT(int seed)
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
#else
        protected override string GetT(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            var bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
#endif
    }
}
