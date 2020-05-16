// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Collections.Pooled
{
    /// <summary>
    /// Equality comparer for hashsets of hashsets
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class PooledSetEqualityComparer<T> : IEqualityComparer<PooledSet<T>>
    {
        private readonly IEqualityComparer<T> _comparer;

        public PooledSetEqualityComparer()
        {
            _comparer = EqualityComparer<T>.Default;
        }

        // using _comparer to keep equals properties intact; don't want to choose one of the comparers
        public bool Equals(PooledSet<T>? x, PooledSet<T>? y)
        {
            return PooledSet<T>.PooledSetEquals(x, y, _comparer);
        }

        public int GetHashCode(PooledSet<T> obj)
        {
#if NETSTANDARD2_1 || NETCOREAPP3_0
            var hashCode = new HashCode();
            if (obj is object)
            {
                foreach (T t in obj)
                {
                    hashCode.Add(t, _comparer);
                }
            }
            return hashCode.ToHashCode();
#else
            int hashCode = 0;
            if (obj != null)
            {
                foreach (T t in obj)
                {
                    if (t != null)
                    {
                        hashCode ^= (_comparer.GetHashCode(t) & 0x7FFFFFFF);
                    }
                }
            } // else returns hashcode of 0 for null hashsets
            return hashCode;
#endif
        }

        // Equals method for the comparer itself. 
        public override bool Equals(object? obj)
        {
            if (obj is PooledSetEqualityComparer<T> comparer)
            {
                return _comparer == comparer._comparer;
            }
            else if (obj is IEqualityComparer<T> ieq)
            {
                return _comparer == ieq;
            }
            return false;
        }

        public override int GetHashCode() => _comparer.GetHashCode();
    }
}

