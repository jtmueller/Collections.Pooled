// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests.PooledStack
{
    /// <summary>
    /// Contains tests that ensure the correctness of the Stack class.
    /// </summary>
    public abstract partial class Stack_Generic_Tests<T> : IGenericSharedAPI_Tests<T>
    {
        #region PooledStack<T> Helper Methods

        #region IGenericSharedAPI<T> Helper Methods

        protected PooledStack<T> GenericStackFactory() => RegisterForDispose(new PooledStack<T>());

        protected PooledStack<T> GenericStackFactory(int count)
        {
            var stack = RegisterForDispose(new PooledStack<T>(count));
            int seed = count * 34;
            for (int i = 0; i < count; i++)
            {
                stack.Push(CreateT(seed++));
            }

            return stack;
        }

        protected override Type IGenericSharedAPI_CopyTo_IndexLargerThanArrayCount_ThrowType => typeof(ArgumentOutOfRangeException);

        #endregion

        protected override IEnumerable<T> GenericIEnumerableFactory() => GenericStackFactory();

        protected override IEnumerable<T> GenericIEnumerableFactory(int count) => GenericStackFactory(count);

        protected override int Count(IEnumerable<T> enumerable) => ((PooledStack<T>)enumerable).Count;
        protected override void Add(IEnumerable<T> enumerable, T value) => ((PooledStack<T>)enumerable).Push(value);
        protected override void Clear(IEnumerable<T> enumerable) => ((PooledStack<T>)enumerable).Clear();
        protected override bool Contains(IEnumerable<T> enumerable, T value) => ((PooledStack<T>)enumerable).Contains(value);
        protected override void CopyTo(IEnumerable<T> enumerable, T[] array, int index) => ((PooledStack<T>)enumerable).CopyTo(array, index);
        protected override bool Remove(IEnumerable<T> enumerable) { ((PooledStack<T>)enumerable).Pop(); return true; }
        protected override bool Enumerator_Current_UndefinedOperation_Throws => true;

        #endregion

        #region Constructor

        [Fact]
        public void Stack_Generic_Constructor_InitialValues()
        {
            var stack = new PooledStack<T>();
            Assert.Equal(0, stack.Count);
            Assert.Equal(0, stack.ToArray().Length);
            Assert.NotNull(((ICollection)stack).SyncRoot);
        }

        #endregion

        #region Constructor_IEnumerable

        [Theory]
        [MemberData(nameof(EnumerableTestData))]
        public void Stack_Generic_Constructor_IEnumerable(EnumerableType enumerableType, int setLength, int enumerableLength, int numberOfMatchingElements, int numberOfDuplicateElements)
        {
            var enumerable = CreateEnumerable(enumerableType, null, enumerableLength, 0, numberOfDuplicateElements);
            using var stack = new PooledStack<T>(enumerable);
            Assert.Equal(enumerable.ToArray().Reverse(), stack.ToArray());
        }

        [Fact]
        public void Stack_Generic_Constructor_IEnumerable_Null_ThrowsArgumentNullException() => AssertExtensions.Throws<ArgumentNullException>("enumerable", () => new PooledStack<T>((IEnumerable<T>)null));

        #endregion

        #region Constructor_Capacity

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_Constructor_int(int count)
        {
            using var stack = new PooledStack<T>(count);
            Assert.Equal(Array.Empty<T>(), stack.ToArray());
        }

        [Fact]
        public void Stack_Generic_Constructor_int_Negative_ThrowsArgumentOutOfRangeException()
        {
            AssertExtensions.Throws<ArgumentOutOfRangeException>("capacity", () => new PooledStack<T>(-1));
            AssertExtensions.Throws<ArgumentOutOfRangeException>("capacity", () => new PooledStack<T>(Int32.MinValue));
        }

        #endregion

        #region Pop

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_Pop_AllElements(int count)
        {
            var stack = GenericStackFactory(count);
            var elements = stack.ToList();
            foreach (var element in elements)
            {
                Assert.Equal(element, stack.Pop());
            }
        }

        [Fact]
        public void Stack_Generic_Pop_OnEmptyStack_ThrowsInvalidOperationException() => Assert.Throws<InvalidOperationException>(() => new PooledStack<T>().Pop());

        #endregion

        #region RemoveWhere

        protected abstract bool RemoveWherePredicate(T item);

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_RemoveWhere(int count)
        {
            var stack = GenericStackFactory(count);
            int startingCount = stack.Count;
            var expected = stack.Where(x => !RemoveWherePredicate(x)).ToPooledList();
            stack.RemoveWhere(RemoveWherePredicate);
            Assert.Equal(expected.Count, stack.Count);
            Assert.Equal(expected, stack);
            expected.Dispose();
        }

        #endregion

        #region ToArray

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_ToArray(int count)
        {
            var stack = GenericStackFactory(count);
            Assert.Equal(Enumerable.ToArray(stack), stack.ToArray());
        }

        #endregion

        #region Peek

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_Peek_AllElements(int count)
        {
            var stack = GenericStackFactory(count);
            var elements = stack.ToList();
            foreach (var element in elements)
            {
                Assert.Equal(element, stack.Peek());
                stack.Pop();
            }
        }

        [Fact]
        public void Stack_Generic_Peek_OnEmptyStack_ThrowsInvalidOperationException() => Assert.Throws<InvalidOperationException>(() => new PooledStack<T>().Peek());

        #endregion

        #region TrimExcess

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TrimExcess_OnValidStackThatHasntBeenRemovedFrom(int count)
        {
            var stack = GenericStackFactory(count);
            stack.TrimExcess();
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TrimExcess_Repeatedly(int count)
        {
            var stack = GenericStackFactory(count);
            var expected = stack.ToList();
            stack.TrimExcess();
            stack.TrimExcess();
            stack.TrimExcess();
            Assert.Equal(expected, stack);
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TrimExcess_AfterRemovingOneElement(int count)
        {
            if (count > 0)
            {
                var stack = GenericStackFactory(count);
                var expected = stack.ToList();
                var elementToRemove = stack.ElementAt(0);

                stack.TrimExcess();
                stack.Pop();
                expected.RemoveAt(0);
                stack.TrimExcess();

                Assert.Equal(expected, stack);
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TrimExcess_AfterClearingAndAddingSomeElementsBack(int count)
        {
            if (count > 0)
            {
                var stack = GenericStackFactory(count);
                stack.TrimExcess();
                stack.Clear();
                stack.TrimExcess();
                Assert.Equal(0, stack.Count);

                AddToCollection(stack, count / 10);
                stack.TrimExcess();
                Assert.Equal(count / 10, stack.Count);
            }
        }

        [Theory]
        [MemberData(nameof(ValidCollectionSizes))]
        public void Stack_Generic_TrimExcess_AfterClearingAndAddingAllElementsBack(int count)
        {
            if (count > 0)
            {
                var stack = GenericStackFactory(count);
                stack.TrimExcess();
                stack.Clear();
                stack.TrimExcess();
                Assert.Equal(0, stack.Count);

                AddToCollection(stack, count);
                stack.TrimExcess();
                Assert.Equal(count, stack.Count);
            }
        }

        #endregion
    }
}
