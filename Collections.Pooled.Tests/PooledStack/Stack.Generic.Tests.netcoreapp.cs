// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledStack
{
    public abstract partial class Stack_Generic_Tests<T> : IGenericSharedAPI_Tests<T>
    {
        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TryPop_AllElements(int count)
        {
            PooledStack<T> stack = GenericStackFactory(count);
            List<T> elements = stack.ToList();
            foreach (T element in elements)
            {
                Assert.True(stack.TryPop(out T result));
                Assert.Equal(element, result);
            }
        }

        [Fact]
        public void Stack_Generic_TryPop_EmptyStack_ReturnsFalse()
        {
            Assert.False(new PooledStack<T>().TryPop(out T result));
            Assert.Equal(default, result);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TryPeek_AllElements(int count)
        {
            PooledStack<T> stack = GenericStackFactory(count);
            List<T> elements = stack.ToList();
            foreach (T element in elements)
            {
                Assert.True(stack.TryPeek(out T result));
                Assert.Equal(element, result);

                stack.Pop();
            }
        }

        [Fact]
        public void Stack_Generic_TryPeek_EmptyStack_ReturnsFalse()
        {
            Assert.False(new PooledStack<T>().TryPeek(out T result));
            Assert.Equal(default, result);
        }
    }
}
