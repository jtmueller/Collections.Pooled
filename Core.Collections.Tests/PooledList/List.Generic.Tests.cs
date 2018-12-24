// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.Tests;
using System.Linq;
using Xunit;

namespace Core.Collections.Tests
{
    /// <summary>
    /// Contains tests that ensure the correctness of the List class.
    /// </summary>
    public abstract partial class List_Generic_Tests<T> : IList_Generic_Tests<T>
    {
        #region IList<T> Helper Methods

        protected override IList<T> GenericIListFactory()
        {
            return GenericListFactory();
        }

        protected override IList<T> GenericIListFactory(int count)
        {
            return GenericListFactory(count);
        }

        #endregion

        #region PooledList<T> Helper Methods

        protected virtual PooledList<T> GenericListFactory()
        {
            return new PooledList<T>();
        }

        protected virtual PooledList<T> GenericListFactory(int count)
        {
            IEnumerable<T> toCreateFrom = CreateEnumerable(EnumerableType.List, null, count, 0, 0);
            return new PooledList<T>(toCreateFrom);
        }

        protected virtual T[] GenericArrayFactory(int count)
        {
            IEnumerable<T> toCreateFrom = CreateEnumerable(EnumerableType.List, null, count, 0, 0);
            return toCreateFrom.ToArray();
        }

        protected void VerifyList(PooledList<T> list, PooledList<T> expectedItems)
        {
            Assert.Equal(expectedItems.Count, list.Count);

            //Only verify the indexer. List should be in a good enough state that we
            //do not have to verify consistency with any other method.
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.True(list[i] == null ? expectedItems[i] == null : list[i].Equals(expectedItems[i]));
            }
        }

        #endregion

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void CopyTo_ArgumentValidity(int count)
        {
            if (count > 0)
            {
                PooledList<T> list = GenericListFactory(count);
                AssertExtensions.Throws<ArgumentException>(null, () => list.CopyTo(new T[0]));
                list.Dispose();
            }
        }
    }
}
