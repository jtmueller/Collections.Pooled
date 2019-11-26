// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Collections.Pooled.Tests")]

namespace Collections.Pooled
{
    internal ref struct BitHelper
    {
        private const int s_intSize = sizeof(int) * 8;
        private readonly Span<int> _span;

        internal BitHelper(Span<int> span, bool clear)
        {
            if (clear)
            {
                span.Clear();
            }
            _span = span;
        }

        internal void MarkBit(int bitPosition)
        {
            int bitArrayIndex = bitPosition / s_intSize;
            if ((uint)bitArrayIndex < (uint)_span.Length)
            {
                _span[bitArrayIndex] |= (1 << (bitPosition % s_intSize));
            }
        }

        internal bool IsMarked(int bitPosition)
        {
            int bitArrayIndex = bitPosition / s_intSize;
            return
                (uint)bitArrayIndex < (uint)_span.Length &&
                (_span[bitArrayIndex] & (1 << (bitPosition % s_intSize))) != 0;
        }

        internal int FindFirstUnmarked(int startPosition = 0)
        {
            int i = startPosition;
            for (int bi = i / s_intSize; (uint)bi < (uint)_span.Length; bi = ++i / s_intSize)
            {
                if (_span[bi] == 0 || (_span[bi] & (1 << (i % s_intSize))) == 0)
                    return i;
            }
            return -1;
        }

        internal int FindFirstMarked(int startPosition = 0)
        {
            int i = startPosition;
            for (int bi = i / s_intSize; (uint)bi < (uint)_span.Length; bi = ++i / s_intSize)
            {
                if (_span[bi] == 0)
                {
                    if (bi++ == _span.Length)
                        break;
                    // skip ahead to the next int boundary
                    i = (bi * s_intSize) - 1;
                    continue;
                }

                if ((_span[bi] & (1 << (i % s_intSize))) != 0)
                    return i;
            }
            return -1;
        }

        /// <summary>How many ints must be allocated to represent n bits. Returns (n+31)/32, but avoids overflow.</summary>
        internal static int ToIntArrayLength(int n) => n > 0 ? ((n - 1) / s_intSize + 1) : 0;
    }
}
