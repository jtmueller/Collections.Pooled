// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Collections.Pooled.Tests.PooledDictionary
{
    public class Dictionary_Generic_Tests_Keys : ICollection_Generic_Tests<string>
    {
        public override bool SupportsJson => false;
        public override Type CollectionType => typeof(PooledDictionary<string, string>.KeyCollection);

        protected override bool DefaultValueAllowed => false;
        protected override bool DuplicateValuesAllowed => false;
        protected override bool IsReadOnly => true;
        protected override bool SupportsSerialization => false;
        protected override IEnumerable<ModifyEnumerable> GetModifyEnumerables(ModifyOperation operations)
            => new List<ModifyEnumerable>();

        protected override ICollection<string> GenericICollectionFactory()
        {
            var dict = new PooledDictionary<string, string>();
            RegisterForDispose(dict);
            return dict.Keys;
        }

        protected override ICollection<string> GenericICollectionFactory(int count)
        {
            var dict = new PooledDictionary<string, string>();
            RegisterForDispose(dict);
            int seed = 13453;
            for (int i = 0; i < count; i++)
                dict[CreateT(seed++)] = CreateT(seed++);
            return dict.Keys;
        }

#if NETCOREAPP3_0
        protected override string CreateT(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            using var bytesHandle = MemoryPool<byte>.Shared.Rent(stringLength);
            var bytes = bytesHandle.Memory.Span[..stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
#else
        protected override string CreateT(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            var bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
#endif

        protected override Type ICollection_Generic_CopyTo_IndexLargerThanArrayCount_ThrowType => typeof(ArgumentOutOfRangeException);

        [Fact]
        public void Dictionary_Generic_KeyCollection_Constructor_NullDictionary()
        {
            Assert.Throws<ArgumentNullException>(() => new PooledDictionary<string, string>.KeyCollection(null));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_KeyCollection_GetEnumerator(int count)
        {
            var dictionary = new PooledDictionary<string, string>();
            RegisterForDispose(dictionary);
            int seed = 13453;
            while (dictionary.Count < count)
                dictionary[CreateT(seed++)] = CreateT(seed++);
            dictionary.Keys.GetEnumerator();
        }
    }

    public class Dictionary_Generic_Tests_Keys_AsICollection : ICollection_NonGeneric_Tests
    {
        public override bool SupportsJson => false;
        public override Type CollectionType => typeof(PooledDictionary<string, string>.KeyCollection);

        protected override bool NullAllowed => false;
        protected override bool DuplicateValuesAllowed => false;
        protected override bool IsReadOnly => true;
        protected override bool Enumerator_Current_UndefinedOperation_Throws => true;
        protected override Type ICollection_NonGeneric_CopyTo_ArrayOfEnumType_ThrowType => typeof(ArgumentException);
        protected override bool SupportsSerialization => false;

        protected override Type ICollection_NonGeneric_CopyTo_IndexLargerThanArrayCount_ThrowType => typeof(ArgumentOutOfRangeException);

        protected override ICollection NonGenericICollectionFactory()
        {
            var dict = new PooledDictionary<string, string>();
            RegisterForDispose(dict);
            return dict.Keys;
        }

        protected override ICollection NonGenericICollectionFactory(int count)
        {
            var dict = new PooledDictionary<string, string>();
            RegisterForDispose(dict);
            int seed = 13453;
            for (int i = 0; i < count; i++)
                dict.Add(CreateT(seed++), CreateT(seed++));
            return dict.Keys;
        }

        private string CreateT(int seed)
        {
            int stringLength = seed % 10 + 5;
            var rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        protected override void AddToCollection(ICollection collection, int numberOfItemsToAdd)
        {
            Debug.Assert(false);
        }

        protected override IEnumerable<ModifyEnumerable> GetModifyEnumerables(ModifyOperation operations) => new List<ModifyEnumerable>();

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Dictionary_Generic_KeyCollection_CopyTo_ExactlyEnoughSpaceInTypeCorrectArray(int count)
        {
            ICollection collection = NonGenericICollectionFactory(count);
            string[] array = new string[count];
            collection.CopyTo(array, 0);
            int i = 0;
            foreach (object obj in collection)
                Assert.Equal(array[i++], obj);
        }
    }
}
