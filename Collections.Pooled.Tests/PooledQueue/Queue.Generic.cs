// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledQueue
{
    public class Queue_Generic_Tests_string : Queue_Generic_Tests<string>
    {
        protected override string CreateT(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }

    public class Queue_Generic_Tests_int : Queue_Generic_Tests<int>
    {
        protected override int CreateT(int seed)
        {
            Random rand = new Random(seed);
            return rand.Next();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void RemoveWhere_Removes_Evens(int count)
        {
            var queue = GenericQueueFactory(count);
            var evenCount = queue.Count(x => x % 2 == 0);
            queue.RemoveWhere(x => x % 2 == 0);
            Assert.Equal(count - evenCount, queue.Count);
            while (queue.Count > 0)
                Assert.True(queue.Dequeue() % 2 != 0, "Even value should not have been present!");
        }
    }
}
