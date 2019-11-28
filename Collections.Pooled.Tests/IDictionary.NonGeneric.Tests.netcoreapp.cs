// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
#if NETCOREAPP
using System.Collections;
using Xunit;

namespace Collections.Pooled.Tests
{
    /// <summary>
    /// Contains tests that ensure the correctness of any class that implements the nongeneric
    /// IDictionary interface
    /// </summary>
    public abstract partial class IDictionary_NonGeneric_Tests : ICollection_NonGeneric_Tests
    {
        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void DictionaryEntry_Deconstruct(int size)
        {
            var dictionary = NonGenericIDictionaryFactory(size);

            // Assert.All is only supported for generic collections.
            foreach (DictionaryEntry entry in dictionary)
            {
                entry.Deconstruct(out object key, out object value);
                Assert.Equal(key, entry.Key);
                Assert.Equal(value, entry.Value);

                key = null;
                value = null;
                (key, value) = entry;
                Assert.Equal(key, entry.Key);
                Assert.Equal(value, entry.Value);
            }
        }
    }
}
#endif