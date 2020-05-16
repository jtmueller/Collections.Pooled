// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections.Generic;

namespace Collections.Pooled.Tests.PooledDictionary
{
    public class Dictionary_Generic_Tests_string_string : Dictionary_Generic_Tests<string, string>
    {
        protected override KeyValuePair<string, string> CreateT(int seed) => new KeyValuePair<string, string>(CreateTKey(seed), CreateTKey(seed + 500));

#if NETCOREAPP3_1
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
#else
        protected override string CreateTKey(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            var bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
#endif

        protected override string CreateTValue(int seed) => CreateTKey(seed);
    }

    public class Dictionary_Generic_Tests_int_int : Dictionary_Generic_Tests<int, int>
    {
        public override bool SupportsJson => false;
        protected override bool DefaultValueAllowed => true;

        protected override KeyValuePair<int, int> CreateT(int seed)
        {
            var rand = new Random(seed);
            return new KeyValuePair<int, int>(rand.Next(), rand.Next());
        }

        protected override int CreateTKey(int seed)
        {
            var rand = new Random(seed);
            return rand.Next();
        }

        protected override int CreateTValue(int seed) => CreateTKey(seed);
    }

    public class Dictionary_Generic_Tests_SimpleInt_int_With_Comparer_WrapStructural_SimpleInt : Dictionary_Generic_Tests<SimpleInt, int>
    {
        public override bool SupportsJson => false;
        protected override bool DefaultValueAllowed => true;

        public override IEqualityComparer<SimpleInt> GetKeyIEqualityComparer() => new WrapStructural_SimpleInt();

        public override IComparer<SimpleInt> GetKeyIComparer() => new WrapStructural_SimpleInt();

        protected override SimpleInt CreateTKey(int seed)
        {
            var rand = new Random(seed);
            return new SimpleInt(rand.Next());
        }

        protected override int CreateTValue(int seed)
        {
            var rand = new Random(seed);
            return rand.Next();
        }

        protected override KeyValuePair<SimpleInt, int> CreateT(int seed) => new KeyValuePair<SimpleInt, int>(CreateTKey(seed), CreateTValue(seed));
    }

    public class Dictionary_Generic_Tests_string_SimpleObject : Dictionary_Generic_Tests<string, SimpleObject>
    {
        public override bool SupportsJson => true;
        protected override bool DefaultValueAllowed => false;

        public override IEqualityComparer<string> GetKeyIEqualityComparer() => StringComparer.Ordinal;

        public override IComparer<string> GetKeyIComparer() => StringComparer.Ordinal;

        protected override string CreateTKey(int seed) => CreateString(seed);

        protected override SimpleObject CreateTValue(int seed)
        {
            var rand = new Random(seed);
            return new SimpleObject
            {
                Name = CreateString(rand.Next()),
                Age = rand.Next()
            };
        }

#if NETCOREAPP3_1
        protected string CreateString(int seed)
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
        protected string CreateString(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            var bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
#endif

        protected override KeyValuePair<string, SimpleObject> CreateT(int seed) => new KeyValuePair<string, SimpleObject>(CreateTKey(seed), CreateTValue(seed));
    }
}
