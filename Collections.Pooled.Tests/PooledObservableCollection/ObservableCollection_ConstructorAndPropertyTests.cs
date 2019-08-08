// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Collections.Pooled.Tests.PooledObservableCollection
{
    /// <summary>
    /// Tests the public properties and constructor in ObservableCollection<T>.
    /// </summary>
    public class ConstructorAndPropertyTests
    {
        /// <summary>
        /// Tests that the parameterless constructor works.
        /// </summary>
        [Fact]
        public static void ParameterlessConstructorTest()
        {
            using var col = new PooledObservableCollection<string>();
            Assert.Equal(0, col.Count);
            Assert.Empty(col);
        }

        /// <summary>
        /// Tests that the IEnumerable constructor can various IEnumerables with items.
        /// </summary>
        [Theory]
        [MemberData(nameof(Collections))]
        public static void IEnumerableConstructorTest(IEnumerable<string> collection)
        {
            using var actual = new PooledObservableCollection<string>(collection);
            Assert.Equal(collection, actual);
        }

        [Theory]
        [MemberData(nameof(Collections))]
        public static void IEnumerableConstructorTest_MakesCopy(IEnumerable<string> collection)
        {
            using var oc = new ObservableCollectionSubclass<string>(collection);
            Assert.NotNull(oc.InnerList);
            Assert.NotSame(collection, oc.InnerList);
        }

        public static readonly object[][] Collections =
        {
            new object[] { new string[] { "one", "two", "three" } },
            new object[] { new List<string> { "one", "two", "three" } },
            new object[] { new PooledCollection<string> { "one", "two", "three" } },
            new object[] { Enumerable.Range(1, 3).Select(i => i.ToString()) },
            new object[] { CreateIteratorCollection() }
        };

        private static IEnumerable<string> CreateIteratorCollection()
        {
            yield return "one";
            yield return "two";
            yield return "three";
        }

        /// <summary>
        /// Tests that the IEnumerable constructor can take an empty IEnumerable.
        /// </summary>
        [Fact]
        public static void IEnumerableConstructorTest_Empty()
        {
            using var col = new PooledObservableCollection<string>(new string[] { });
            Assert.Equal(0, col.Count);
            Assert.Empty(col);
        }

        /// <summary>
        /// Tests that ArgumentNullException is thrown when given a null IEnumerable.
        /// </summary>
        [Fact]
        public static void IEnumerableConstructorTest_Negative() 
            => AssertExtensions.Throws<ArgumentNullException>("collection", () => new PooledObservableCollection<string>(null));

        /// <summary>
        /// Tests that an item can be set using the index.
        /// </summary>
        [Fact]
        public static void ItemTestSet()
        {
            using var col = new PooledObservableCollection<Guid>(new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });
            for (int i = 0; i < col.Count; ++i)
            {
                var guid = Guid.NewGuid();
                col[i] = guid;
                Assert.Equal(guid, col[i]);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(3, Int32.MinValue)]
        [InlineData(3, -1)]
        [InlineData(3, 3)]
        [InlineData(3, 4)]
        [InlineData(3, Int32.MaxValue)]
        public static void ItemTestSet_Negative_InvalidIndex(int size, int index)
        {
            using var col = new PooledObservableCollection<int>(new int[size]);
            AssertExtensions.Throws<ArgumentOutOfRangeException>("index", () => col[index]);
        }

        // ICollection<T>.IsReadOnly
        [Fact]
        public static void IsReadOnlyTest()
        {
            using var col = new PooledObservableCollection<Guid>(new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });
            Assert.False(((ICollection<Guid>)col).IsReadOnly);
        }

        [Fact]
        public static void DebuggerAttributeTests()
        {
            using var col = new PooledObservableCollection<int>(new[] { 1, 2, 3, 4 });
            DebuggerAttributes.ValidateDebuggerDisplayReferences(col);
            var info = DebuggerAttributes.ValidateDebuggerTypeProxyProperties(col);
            var itemProperty = info.Properties.Single(pr => pr.GetCustomAttribute<DebuggerBrowsableAttribute>().State == DebuggerBrowsableState.RootHidden);
            int[] items = itemProperty.GetValue(info.Instance) as int[];
            Assert.Equal(col, items);
        }

        [Fact]
        public static void DebuggerAttribute_NullCollection_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<TargetInvocationException>(() => DebuggerAttributes.ValidateDebuggerTypeProxyProperties(typeof(PooledObservableCollection<int>), null));
            var argumentNullException = Assert.IsType<ArgumentNullException>(ex.InnerException);
        }

        private partial class ObservableCollectionSubclass<T> : PooledObservableCollection<T>
        {
            public ObservableCollectionSubclass(IEnumerable<T> collection) : base(collection) { }

            public PooledList<T> InnerList => (PooledList<T>)base.Items;
        }

        /// <summary>
        /// Tests that ArgumentNullException is thrown when given a null IEnumerable.
        /// </summary>
        [Fact]
        public static void ListConstructorTest_Negative() => AssertExtensions.Throws<ArgumentNullException>("collection", () => new PooledObservableCollection<string>(null));

        [Fact]
        public static void ListConstructorTest()
        {
            var collection = new List<string> { "one", "two", "three" };
            using var actual = new PooledObservableCollection<string>(collection);
            Assert.Equal(collection, actual);
        }

        [Fact]
        public static void ListConstructorTest_MakesCopy()
        {
            var collection = new List<string> { "one", "two", "three" };
            using var oc = new ObservableCollectionSubclass<string>(collection);
            Assert.NotNull(oc.InnerList);
            Assert.NotSame(collection, oc.InnerList);
        }

        private partial class ObservableCollectionSubclass<T> : PooledObservableCollection<T>
        {
            public ObservableCollectionSubclass(List<T> list) : base(list) { }
        }
    }
}
