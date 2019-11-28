// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Collections.Pooled.Tests
{
    #region Comparers and Equatables

    // Use parity only as a hashcode so as to have many collisions.
    [Serializable]
    public class BadIntEqualityComparer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y) => x == y;

        public int GetHashCode(int obj) => obj % 2;

        public override bool Equals(object obj) => obj is BadIntEqualityComparer; // Equal to all other instances of this type, not to anything else.

        public override int GetHashCode() => unchecked((int)0xC001CAFE); // Doesn't matter as long as its constant.
    }

    [Serializable]
    public class EquatableBackwardsOrder : IEquatable<EquatableBackwardsOrder>, IComparable<EquatableBackwardsOrder>, IComparable
    {
        private readonly int _value;

        public EquatableBackwardsOrder(int value)
        {
            _value = value;
        }

        public int CompareTo(EquatableBackwardsOrder other) //backwards from the usual integer ordering
            => other._value - _value;

        public override int GetHashCode() => _value;

        public override bool Equals(object obj)
        {
            var other = obj as EquatableBackwardsOrder;
            return other != null && Equals(other);
        }

        public bool Equals(EquatableBackwardsOrder other) => _value == other._value;

        int IComparable.CompareTo(object obj)
        {
            if (obj != null && obj.GetType() == typeof(EquatableBackwardsOrder))
            {
                return ((EquatableBackwardsOrder)obj)._value - _value;
            }
            else
            {
                return -1;
            }
        }
    }

    [Serializable]
    public class Comparer_SameAsDefaultComparer : IEqualityComparer<int>, IComparer<int>
    {
        public int Compare(int x, int y) => x - y;

        public bool Equals(int x, int y) => x == y;

        public int GetHashCode(int obj) => obj.GetHashCode();
    }

    [Serializable]
    public class Comparer_HashCodeAlwaysReturnsZero : IEqualityComparer<int>, IComparer<int>
    {
        public int Compare(int x, int y) => x - y;

        public bool Equals(int x, int y) => x == y;

        public int GetHashCode(int obj) => 0;
    }

    [Serializable]
    public class Comparer_ModOfInt : IEqualityComparer<int>, IComparer<int>
    {
        private readonly int _mod;

        public Comparer_ModOfInt(int mod)
        {
            _mod = mod;
        }

        public Comparer_ModOfInt()
        {
            _mod = 500;
        }

        public int Compare(int x, int y) => ((x % _mod) - (y % _mod));

        public bool Equals(int x, int y) => ((x % _mod) == (y % _mod));

        public int GetHashCode(int x) => (x % _mod);
    }

    [Serializable]
    public class Comparer_AbsOfInt : IEqualityComparer<int>, IComparer<int>
    {
        public int Compare(int x, int y) => Math.Abs(x) - Math.Abs(y);

        public bool Equals(int x, int y) => Math.Abs(x) == Math.Abs(y);

        public int GetHashCode(int x) => Math.Abs(x);
    }

    #endregion

    #region TestClasses

    [Serializable]
    public class SimpleObject : IEquatable<SimpleObject>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public bool Equals([AllowNull] SimpleObject other)
        {
            if (other is null)
                return false;

            return other.Name == Name && other.Age == Age;
        }

        public override bool Equals(object obj) => obj is SimpleObject so && Equals(so);

        public override int GetHashCode() => 17 ^ Age.GetHashCode() ^ (Name?.GetHashCode() ?? 0);
    }

    [Serializable]
    public struct SimpleInt : IStructuralComparable, IStructuralEquatable, IComparable, IComparable<SimpleInt>
    {
        private int _val;
        public SimpleInt(int t)
        {
            _val = t;
        }
        public int Val
        {
            get => _val;
            set => _val = value;
        }

        public int CompareTo(SimpleInt other) => other.Val - _val;

        public int CompareTo(object obj)
        {
            if (obj.GetType() == typeof(SimpleInt))
            {
                return ((SimpleInt)obj).Val - _val;
            }
            return -1;
        }

        public int CompareTo(object other, IComparer comparer)
        {
            if (other.GetType() == typeof(SimpleInt))
            {
                return ((SimpleInt)other).Val - _val;
            }

            return -1;
        }

        public bool Equals(object other, IEqualityComparer comparer)
        {
            if (other.GetType() == typeof(SimpleInt))
            {
                return ((SimpleInt)other).Val == _val;
            }

            return false;
        }

        public int GetHashCode(IEqualityComparer comparer) => comparer.GetHashCode(_val);
    }

    [Serializable]
    public class WrapStructural_Int : IEqualityComparer<int>, IComparer<int>
    {
        public int Compare(int x, int y) => StructuralComparisons.StructuralComparer.Compare(x, y);

        public bool Equals(int x, int y) => StructuralComparisons.StructuralEqualityComparer.Equals(x, y);

        public int GetHashCode(int obj) => StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
    }

    [Serializable]
    public class WrapStructural_SimpleInt : IEqualityComparer<SimpleInt>, IComparer<SimpleInt>
    {
        public int Compare(SimpleInt x, SimpleInt y) => StructuralComparisons.StructuralComparer.Compare(x, y);

        public bool Equals(SimpleInt x, SimpleInt y) => StructuralComparisons.StructuralEqualityComparer.Equals(x, y);

        public int GetHashCode(SimpleInt obj) => StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
    }

    public class GenericComparable : IComparable<GenericComparable>
    {
        private readonly int _value;

        public GenericComparable(int value)
        {
            _value = value;
        }

        public int CompareTo(GenericComparable other) => _value.CompareTo(other._value);
    }

    public class NonGenericComparable : IComparable
    {
        private readonly GenericComparable _inner;

        public NonGenericComparable(int value)
        {
            _inner = new GenericComparable(value);
        }

        public int CompareTo(object other) =>
            _inner.CompareTo(((NonGenericComparable)other)._inner);
    }

    public class BadlyBehavingComparable : IComparable<BadlyBehavingComparable>, IComparable
    {
        public int CompareTo(BadlyBehavingComparable other) => 1;

        public int CompareTo(object other) => -1;
    }

    public class MutatingComparable : IComparable<MutatingComparable>, IComparable
    {
        private int _state;

        public MutatingComparable(int initialState)
        {
            _state = initialState;
        }

        public int State => _state;

        public int CompareTo(object other) => _state++;

        public int CompareTo(MutatingComparable other) => _state++;
    }

    public static class ValueComparable
    {
        // Convenience method so the compiler can work its type inference magic.
        public static ValueComparable<T> Create<T>(T value) where T : IComparable<T> => new ValueComparable<T>(value);
    }

    public struct ValueComparable<T> : IComparable<ValueComparable<T>> where T : IComparable<T>
    {
        public ValueComparable(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public int CompareTo(ValueComparable<T> other) =>
            Value.CompareTo(other.Value);
    }

    public class Equatable : IEquatable<Equatable>
    {
        public Equatable(int value)
        {
            Value = value;
        }

        public int Value { get; }

        // Equals(object) is not implemented on purpose.
        // EqualityComparer is only supposed to call through to the strongly-typed Equals since we implement IEquatable.

        public bool Equals(Equatable other) => other != null && Value == other.Value;

        public override int GetHashCode() => Value;
    }

    public struct NonEquatableValueType
    {
        public NonEquatableValueType(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }

    public class DelegateEquatable : IEquatable<DelegateEquatable>
    {
        public DelegateEquatable()
        {
            EqualsWorker = _ => false;
        }

        public Func<DelegateEquatable, bool> EqualsWorker { get; set; }

        public bool Equals(DelegateEquatable other) => EqualsWorker(other);
    }

    public struct ValueDelegateEquatable : IEquatable<ValueDelegateEquatable>
    {
        public Func<ValueDelegateEquatable, bool> EqualsWorker { get; set; }

        public bool Equals(ValueDelegateEquatable other) => EqualsWorker(other);
    }

    #endregion
}
