﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Collections.Pooled.Tests.PooledStack
{
    public class Stack_Generic_Tests_string : Stack_Generic_Tests<string>
    {
        protected override string CreateT(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }


        protected override bool RemoveWherePredicate(string item)
            => item.GetHashCode() % 2 == 0;
    }

    public class Stack_Generic_Tests_int : Stack_Generic_Tests<int>
    {
        protected override int CreateT(int seed)
        {
            Random rand = new Random(seed);
            return rand.Next();
        }

        protected override bool RemoveWherePredicate(int item)
            => item % 2 == 0;
    }
}
