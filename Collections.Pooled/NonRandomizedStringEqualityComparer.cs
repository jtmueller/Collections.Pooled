// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Collections.Pooled
{
    /// <summary>
    /// NonRandomizedStringEqualityComparer is the comparer used by default with the PooledDictionary.
    /// We use NonRandomizedStringEqualityComparer as default comparer as it doesnt use the randomized string hashing which 
    /// keeps the performance not affected till we hit collision threshold and then we switch to the comparer which is using 
    /// randomized string hashing.
    /// </summary>
    [Serializable] // Required for compatibility with .NET Core 2.0 as we exposed the NonRandomizedStringEqualityComparer inside the serialization blob
    public sealed class NonRandomizedStringEqualityComparer : EqualityComparer<string>, ISerializable
    {
        private static readonly int s_empyStringHashCode = string.Empty.GetHashCode();

        internal static new IEqualityComparer<string> Default { get; } = new NonRandomizedStringEqualityComparer();

        private NonRandomizedStringEqualityComparer() { }

        // This is used by the serialization engine.
        private NonRandomizedStringEqualityComparer(SerializationInfo information, StreamingContext context) { }

        public sealed override bool Equals(string x, string y) => string.Equals(x, y);

        public sealed override int GetHashCode(string str) 
            => str is null ? 0 : str.Length == 0 ? s_empyStringHashCode : GetNonRandomizedHashCode(str);

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.SetType(typeof(NonRandomizedStringEqualityComparer));
        }

        // Use this if and only if 'Denial of Service' attacks are not a concern (i.e. never used for free-form user input),
        // or are otherwise mitigated.
        // This code was ported from an internal method on String, which relied on private members to get the char* pointer.
        private static unsafe int GetNonRandomizedHashCode(string str)
        {
            ReadOnlySpan<char> chars = str.AsSpan();
            fixed (char* src = chars)
            {
                Debug.Assert(src[chars.Length] == '\0', "src[this.Length] == '\\0'");
                Debug.Assert(((int)src) % 4 == 0, "Managed string should start at 4 bytes boundary");

                uint hash1 = (5381 << 16) + 5381;
                uint hash2 = hash1;

                uint* ptr = (uint*)src;
                int length = chars.Length;

                while (length > 2)
                {
                    length -= 4;
                    // Where length is 4n-1 (e.g. 3,7,11,15,19) this additionally consumes the null terminator
                    hash1 = (((hash1 << 5) | (hash1 >> 27)) + hash1) ^ ptr[0];
                    hash2 = (((hash2 << 5) | (hash2 >> 27)) + hash2) ^ ptr[1];
                    ptr += 2;
                }

                if (length > 0)
                {
                    // Where length is 4n-3 (e.g. 1,5,9,13,17) this additionally consumes the null terminator
                    hash2 = (((hash2 << 5) | (hash2 >> 27)) + hash2) ^ ptr[0];
                }

                return (int)(hash1 + (hash2 * 1566083941));
            }
        }
    }
}
