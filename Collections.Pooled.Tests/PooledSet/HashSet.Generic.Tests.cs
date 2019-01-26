// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledSet
{
    /// <summary>
    /// Contains tests that ensure the correctness of the HashSet class.
    /// </summary>
    public abstract partial class HashSet_Generic_Tests<T> : ISet_Generic_Tests<T>
    {
        #region ISet<T> Helper Methods

        protected override bool ResetImplemented => true;

        protected override ISet<T> GenericISetFactory()
        {
            var set = new PooledSet<T>();
            RegisterForDispose(set);
            return set;
        }

        #endregion

        #region Constructors

        private static IEnumerable<int> NonSquares(int limit)
        {
            for (int i = 0; i != limit; ++i)
            {
                int root = (int)Math.Sqrt(i);
                if (i != root * root)
                    yield return i;
            }
        }

        [Fact]
        public void HashSet_Generic_Constructor()
        {
            PooledSet<T> set = new PooledSet<T>();
            Assert.Empty(set);
        }

        [Fact]
        public void HashSet_Generic_Constructor_IEqualityComparer()
        {
            IEqualityComparer<T> comparer = GetIEqualityComparer();
            PooledSet<T> set = new PooledSet<T>(comparer);
            if (comparer == null)
                Assert.Equal(EqualityComparer<T>.Default, set.Comparer);
            else
                Assert.Equal(comparer, set.Comparer);
        }

        [Fact]
        public void HashSet_Generic_Constructor_NullIEqualityComparer()
        {
            IEqualityComparer<T> comparer = null;
            PooledSet<T> set = new PooledSet<T>(comparer);
            if (comparer == null)
                Assert.Equal(EqualityComparer<T>.Default, set.Comparer);
            else
                Assert.Equal(comparer, set.Comparer);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void HashSet_Generic_Constructor_IEnumerable(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            IEnumerable<T> enumerable = CreateEnumerable(enumerableType, null, enumerableLength, 0, numberOfDuplicateElements);
            PooledSet<T> set = new PooledSet<T>(enumerable);
            Assert.True(set.SetEquals(enumerable));
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void HashSet_Generic_Constructor_Span(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            IEnumerable<T> enumerable = CreateEnumerable(enumerableType, null, enumerableLength, 0, numberOfDuplicateElements);
            var span = enumerable.ToArray().AsSpan();
            PooledSet<T> set = new PooledSet<T>(span);
            Assert.True(set.SetEquals(enumerable));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_Constructor_IEnumerable_WithManyDuplicates(int count)
        {
            IEnumerable<T> items = CreateEnumerable(EnumerableType.List, null, count, 0, 0);
            PooledSet<T> hashSetFromDuplicates = new PooledSet<T>(Enumerable.Range(0, 40).SelectMany(i => items).ToArray());
            PooledSet<T> hashSetFromNoDuplicates = new PooledSet<T>(items);
            RegisterForDispose(hashSetFromDuplicates, hashSetFromNoDuplicates);
            Assert.True(hashSetFromNoDuplicates.SetEquals(hashSetFromDuplicates));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_Constructor_HashSet_SparselyFilled(int count)
        {
            PooledSet<T> source = (PooledSet<T>)CreateEnumerable(EnumerableType.HashSet, null, count, 0, 0);
            List<T> sourceElements = source.ToList();
            foreach (int i in NonSquares(count))
                source.Remove(sourceElements[i]);// Unevenly spaced survivors increases chance of catching any spacing-related bugs.


            PooledSet<T> set = new PooledSet<T>(source, GetIEqualityComparer());
            RegisterForDispose(set);
            Assert.True(set.SetEquals(source));
        }

        [Fact]
        public void HashSet_Generic_Constructor_IEnumerable_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new PooledSet<T>((IEnumerable<T>)null));
            Assert.Throws<ArgumentNullException>(() => new PooledSet<T>((IEnumerable<T>)null, EqualityComparer<T>.Default));
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void HashSet_Generic_Constructor_IEnumerable_IEqualityComparer(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            IEnumerable<T> enumerable = CreateEnumerable(enumerableType, null, enumerableLength, 0, 0);
            PooledSet<T> set = new PooledSet<T>(enumerable, GetIEqualityComparer());
            RegisterForDispose(set);
            Assert.True(set.SetEquals(enumerable));
        }

        #endregion

        #region RemoveWhere

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_RemoveWhere_AllElements(int setLength)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
            int removedCount = set.RemoveWhere((value) => { return true; });
            Assert.Equal(setLength, removedCount);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_RemoveWhere_NoElements(int setLength)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
            int removedCount = set.RemoveWhere((value) => { return false; });
            Assert.Equal(0, removedCount);
            Assert.Equal(setLength, set.Count);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_RemoveWhere_NewObject(int setLength) // Regression Dev10_624201
        {
            object[] array = new object[2];
            object obj = new object();
            PooledSet<object> set = new PooledSet<object>();
            RegisterForDispose(set);

            set.Add(obj);
            set.Remove(obj);
            foreach (object o in set) { }
            set.CopyTo(array, 0, 2);
            set.RemoveWhere((element) => { return false; });
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_RemoveWhere_NullMatchPredicate(int setLength)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
            Assert.Throws<ArgumentNullException>(() => set.RemoveWhere(null));
        }

        #endregion

        #region TrimExcess

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_TrimExcess_OnValidSetThatHasntBeenRemovedFrom(int setLength)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
            set.TrimExcess();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_TrimExcess_Repeatedly(int setLength)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
            List<T> expected = set.ToList();
            set.TrimExcess();
            set.TrimExcess();
            set.TrimExcess();
            Assert.True(set.SetEquals(expected));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_TrimExcess_AfterRemovingOneElement(int setLength)
        {
            if (setLength > 0)
            {
                PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
                List<T> expected = set.ToList();
                T elementToRemove = set.ElementAt(0);

                set.TrimExcess();
                Assert.True(set.Remove(elementToRemove));
                expected.Remove(elementToRemove);
                set.TrimExcess();

                Assert.True(set.SetEquals(expected));
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_TrimExcess_AfterClearingAndAddingSomeElementsBack(int setLength)
        {
            if (setLength > 0)
            {
                PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
                set.TrimExcess();
                set.Clear();
                set.TrimExcess();
                Assert.Equal(0, set.Count);

                AddToCollection(set, setLength / 10);
                set.TrimExcess();
                Assert.Equal(setLength / 10, set.Count);
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_TrimExcess_AfterClearingAndAddingAllElementsBack(int setLength)
        {
            if (setLength > 0)
            {
                PooledSet<T> set = (PooledSet<T>)GenericISetFactory(setLength);
                set.TrimExcess();
                set.Clear();
                set.TrimExcess();
                Assert.Equal(0, set.Count);

                AddToCollection(set, setLength);
                set.TrimExcess();
                Assert.Equal(setLength, set.Count);
            }
        }

        #endregion

        #region CopyTo

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_CopyTo_NegativeCount_ThrowsArgumentOutOfRangeException(int count)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(count);
            T[] arr = new T[count];
            Assert.Throws<ArgumentOutOfRangeException>(() => set.CopyTo(arr, 0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => set.CopyTo(arr, 0, int.MinValue));
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void HashSet_Generic_CopyTo_NoIndexDefaultsToZero(int count)
        {
            PooledSet<T> set = (PooledSet<T>)GenericISetFactory(count);
            T[] arr1 = new T[count];
            T[] arr2 = new T[count];
            set.CopyTo(arr1);
            set.CopyTo(arr2, 0);
            Assert.True(arr1.SequenceEqual(arr2));
        }

        #endregion

        #region CreateSetComparer

        [Fact]
        public void SetComparer_SetEqualsTests()
        {
            List<T> objects = new List<T>() { CreateT(1), CreateT(2), CreateT(3), CreateT(4), CreateT(5), CreateT(6) };

            var set = new PooledSet<PooledSet<T>>()
            {
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] }),
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] })
            };
            RegisterForDispose(set);

            var noComparerSet = new PooledSet<PooledSet<T>>()
            {
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] }),
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] })
            };
            RegisterForDispose(noComparerSet);

            var comparerSet1 = new PooledSet<PooledSet<T>>(PooledSet<T>.CreateSetComparer())
            {
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] }),
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] })
            };
            RegisterForDispose(comparerSet1);

            var comparerSet2 = new PooledSet<PooledSet<T>>(PooledSet<T>.CreateSetComparer())
            {
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] }),
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] })
            };
            RegisterForDispose(comparerSet2);

            Assert.False(noComparerSet.SetEquals(set));
            Assert.True(comparerSet1.SetEquals(set));
            Assert.True(comparerSet2.SetEquals(set));
        }

        [Fact]
        public void SetComparer_SequenceEqualTests()
        {
            List<T> objects = new List<T>() { CreateT(1), CreateT(2), CreateT(3), CreateT(4), CreateT(5), CreateT(6) };

            var set = new PooledSet<PooledSet<T>>()
            {
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] }),
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] })
            };
            RegisterForDispose(set);

            var noComparerSet = new PooledSet<PooledSet<T>>()
            {
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] }),
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] })
            };
            RegisterForDispose(noComparerSet);

            var comparerSet = new PooledSet<PooledSet<T>>(PooledSet<T>.CreateSetComparer())
            {
                RegisterForDispose(new PooledSet<T> { objects[0], objects[1], objects[2] }),
                RegisterForDispose(new PooledSet<T> { objects[3], objects[4], objects[5] })
            };
            RegisterForDispose(comparerSet);

            Assert.False(noComparerSet.SequenceEqual(set));
            Assert.True(noComparerSet.SequenceEqual(set, PooledSet<T>.CreateSetComparer()));
            Assert.False(comparerSet.SequenceEqual(set));
        }

        #endregion

        [Fact]
        public void CanBeCastedToISet()
        {
            PooledSet<T> set = new PooledSet<T>();
            RegisterForDispose(set);
            ISet <T> iset = (set as ISet<T>);
            Assert.NotNull(iset);
        }

        #region Set Validation: Spans

        private bool SpanContains(Span<T> span, T value, IEqualityComparer<T> comparer)
        {
            foreach (T x in span)
            {
                if (comparer.Equals(x, value))
                    return true;
            }
            return false;
        }

        private void Validate_ExceptWith(PooledSet<T> set, Span<T> span)
        {
            if (set.Count == 0)
            {
                set.ExceptWith(span);
                Assert.Equal(0, set.Count);
            }
            else
            {
                PooledSet<T> expected = new PooledSet<T>(set, set.Comparer);
                RegisterForDispose(expected);
                foreach (T element in span)
                    expected.Remove(element);
                set.ExceptWith(span);
                Assert.Equal(expected.Count, set.Count);
                Assert.True(expected.SetEquals(set));
            }
        }

        private void Validate_IntersectWith(PooledSet<T> set, Span<T> span)
        {
            if (set.Count == 0 || span.Length == 0)
            {
                set.IntersectWith(span);
                Assert.Equal(0, set.Count);
            }
            else
            {
                IEqualityComparer<T> comparer = set.Comparer;
                PooledSet<T> expected = new PooledSet<T>(comparer);
                RegisterForDispose(expected);
                foreach (T value in set)
                    if (SpanContains(span, value, comparer))
                        expected.Add(value);
                set.IntersectWith(span);
                Assert.Equal(expected.Count, set.Count);
                Assert.True(expected.SetEquals(set));
            }
        }

        private void Validate_IsProperSubsetOf(PooledSet<T> set, Span<T> span)
        {
            bool setContainsValueNotInEnumerable = false;
            bool enumerableContainsValueNotInSet = false;
            IEqualityComparer<T> comparer = set.Comparer;
            foreach (T value in set) // Every value in Set must be in Enumerable
            {
                if (!SpanContains(span, value, comparer))
                {
                    setContainsValueNotInEnumerable = true;
                    break;
                }
            }
            foreach (T value in span) // Enumerable must contain at least one value not in Set
            {
                if (!set.Contains(value))
                {
                    enumerableContainsValueNotInSet = true;
                    break;
                }
            }
            Assert.Equal(!setContainsValueNotInEnumerable && enumerableContainsValueNotInSet, set.IsProperSubsetOf(span));
        }

        private void Validate_IsProperSupersetOf(PooledSet<T> set, Span<T> span)
        {
            bool isProperSuperset = true;
            bool setContainsElementsNotInEnumerable = false;
            IEqualityComparer<T> comparer = set.Comparer;
            foreach (T value in span)
            {
                if (!set.Contains(value))
                {
                    isProperSuperset = false;
                    break;
                }
            }
            foreach (T value in set)
            {
                if (!SpanContains(span, value, comparer))
                {
                    setContainsElementsNotInEnumerable = true;
                    break;
                }
            }
            isProperSuperset = isProperSuperset && setContainsElementsNotInEnumerable;
            Assert.Equal(isProperSuperset, set.IsProperSupersetOf(span));
        }

        private void Validate_IsSubsetOf(PooledSet<T> set, Span<T> span)
        {
            IEqualityComparer<T> comparer = set.Comparer;
            foreach (T value in set)
                if (!SpanContains(span, value, comparer))
                {
                    Assert.False(set.IsSubsetOf(span));
                    return;
                }
            Assert.True(set.IsSubsetOf(span));
        }

        private void Validate_IsSupersetOf(PooledSet<T> set, Span<T> span)
        {
            foreach (T value in span)
                if (!set.Contains(value))
                {
                    Assert.False(set.IsSupersetOf(span));
                    return;
                }
            Assert.True(set.IsSupersetOf(span));
        }

        private void Validate_Overlaps(PooledSet<T> set, Span<T> span)
        {
            foreach (T value in span)
            {
                if (set.Contains(value))
                {
                    Assert.True(set.Overlaps(span));
                    return;
                }
            }
            Assert.False(set.Overlaps(span));
        }

        private void Validate_SetEquals(PooledSet<T> set, Span<T> span)
        {
            IEqualityComparer<T> comparer = set.Comparer;
            foreach (T value in set)
            {
                if (!SpanContains(span, value, comparer))
                {
                    Assert.False(set.SetEquals(span));
                    return;
                }
            }
            foreach (T value in span)
            {
                if (!set.Contains(value, comparer))
                {
                    Assert.False(set.SetEquals(span));
                    return;
                }
            }
            Assert.True(set.SetEquals(span));
        }

        private void Validate_SymmetricExceptWith(PooledSet<T> set, Span<T> span)
        {
            IEqualityComparer<T> comparer = set.Comparer;
            PooledSet<T> expected = new PooledSet<T>(comparer);
            RegisterForDispose(expected);
            foreach (T element in span)
                if (!set.Contains(element))
                    expected.Add(element);
            foreach (T element in set)
                if (!SpanContains(span, element, comparer))
                    expected.Add(element);
            set.SymmetricExceptWith(span);
            Assert.Equal(expected.Count, set.Count);
            Assert.True(expected.SetEquals(set));
        }

        private void Validate_UnionWith(PooledSet<T> set, Span<T> span)
        {
            IEqualityComparer<T> comparer = set.Comparer;
            PooledSet<T> expected = new PooledSet<T>(set, comparer);
            RegisterForDispose(expected);
            foreach (T element in span)
                if (!set.Contains(element))
                    expected.Add(element);
            set.UnionWith(span);
            Assert.Equal(expected.Count, set.Count);
            Assert.True(expected.SetEquals(set));
        }

        #endregion

        #region Set Function tests: Span

        private Span<T> CreateSpan(EnumerableType type, IEnumerable<T> enumerableToMatchTo, int count, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            IEnumerable<T> enumerable = CreateEnumerable(type, enumerableToMatchTo, count, numberOfMatchingElements, numberOfDuplicateElements);
            return enumerable.ToArray().AsSpan();
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_ExceptWith(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_ExceptWith(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_IntersectWith(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_IntersectWith(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_IsProperSubsetOf(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_IsProperSubsetOf(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_IsProperSupersetOf(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_IsProperSupersetOf(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_IsSubsetOf(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_IsSubsetOf(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_IsSupersetOf(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_IsSupersetOf(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_Overlaps(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_Overlaps(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_SetEquals(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_SetEquals(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_SymmetricExceptWith(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_SymmetricExceptWith(set, span);
        }

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void PooledSet_Span_UnionWith(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var set = (PooledSet<T>)GenericISetFactory(setLength);
            Span<T> span = CreateSpan(enumerableType, set, enumerableLength, numberOfMatchingElements, numberOfDuplicateElements);
            Validate_UnionWith(set, span);
        }

        #endregion
    }
}
