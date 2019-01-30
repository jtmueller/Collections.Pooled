// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledQueue
{
    public abstract partial class Queue_Generic_Tests<T> : IGenericSharedAPI_Tests<T>
    {
        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Queue_Generic_TryDequeue_AllElements(int count)
        {
            PooledQueue<T> queue = GenericQueueFactory(count);
            List<T> elements = queue.ToList();
            foreach (T element in elements)
            {
                Assert.True(queue.TryDequeue(out T result));
                Assert.Equal(element, result);
            }
        }

        [Fact]
        public void Queue_Generic_TryDequeue_EmptyQueue_ReturnsFalse()
        {
            Assert.False(new PooledQueue<T>().TryDequeue(out T result));
            Assert.Equal(default, result);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Queue_Generic_TryPeek_AllElements(int count)
        {
            PooledQueue<T> queue = GenericQueueFactory(count);
            List<T> elements = queue.ToList();
            foreach (T element in elements)
            {
                Assert.True(queue.TryPeek(out T result));
                Assert.Equal(element, result);

                queue.Dequeue();
            }
        }

        [Fact]
        public void Queue_Generic_TryPeek_EmptyQueue_ReturnsFalse()
        {
            Assert.False(new PooledQueue<T>().TryPeek(out T result));
            Assert.Equal(default, result);
        }
    }
}
