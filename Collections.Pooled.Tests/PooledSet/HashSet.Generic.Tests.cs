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
    }
}
