// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Collections.Pooled.Tests.PooledDictionary
{
#if NETCOREAPP3_0
    public class DictionaryConcurrentAccessDetectionTests
    {
        private async Task DictionaryConcurrentAccessDetection<TKey, TValue>(PooledDictionary<TKey, TValue> dictionary, bool isValueType, object comparer, Action<PooledDictionary<TKey, TValue>> add, Action<PooledDictionary<TKey, TValue>> get, Action<PooledDictionary<TKey, TValue>> remove, Action<PooledDictionary<TKey, TValue>> removeOutParam)
        {
            var task = Task.Factory.StartNew(() =>
            {
                // Get the Dictionary into a corrupted state, as if it had been corrupted by concurrent access.
                // We this deterministically by clearing the _entries array using reflection;
                // this means that every Entry struct has a 'next' field of zero, which causes the infinite loop
                // that we want Dictionary to break out of
                var entriesField = dictionary.GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
                var entriesInstance = (Array)entriesField.GetValue(dictionary);
                var entryArray = (Array)Activator.CreateInstance(entriesInstance.GetType(), new object[] { ((ICollection)entriesInstance).Count });
                entriesField.SetValue(dictionary, entryArray);

                Assert.Equal(comparer, dictionary.GetType().GetField("_comparer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dictionary));
                Assert.Equal(isValueType, dictionary.GetType().GetGenericArguments()[0].IsValueType);
                string tsn = Assert.Throws<InvalidOperationException>(() => add(dictionary)).TargetSite.Name;
                Assert.Throws<InvalidOperationException>(() => add(dictionary));
                Assert.Throws<InvalidOperationException>(() => get(dictionary));
                Assert.Throws<InvalidOperationException>(() => remove(dictionary));
                Assert.Throws<InvalidOperationException>(() => removeOutParam(dictionary));
            }, TaskCreationOptions.LongRunning);

            // If Dictionary regresses, we do not want to hang here indefinitely
            Assert.True((await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(60))) == task) && task.IsCompletedSuccessfully);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(typeof(CustomEqualityComparerInt32ValueType))]
        public async Task DictionaryConcurrentAccessDetection_ValueTypeKey(Type comparerType)
        {
            IEqualityComparer<int> customComparer = null;

            using var dic = comparerType == null ?
                new PooledDictionary<int, int>() :
                new PooledDictionary<int, int>((customComparer = (IEqualityComparer<int>)Activator.CreateInstance(comparerType)));

            dic.Add(1, 1);

            await DictionaryConcurrentAccessDetection(dic,
                typeof(int).IsValueType,
                customComparer,
                add: d => d.Add(1, 1),
                get: d => { int v = d[1]; },
                remove: d => d.Remove(1),
                removeOutParam: d => d.Remove(1, out int value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(typeof(CustomEqualityComparerDummyRefType))]
        public async Task DictionaryConcurrentAccessDetection_ReferenceTypeKey(Type comparerType)
        {
            IEqualityComparer<DummyRefType> customComparer = null;

            using var dic = comparerType == null ?
                new PooledDictionary<DummyRefType, DummyRefType>() :
                new PooledDictionary<DummyRefType, DummyRefType>((customComparer = (IEqualityComparer<DummyRefType>)Activator.CreateInstance(comparerType)));

            var keyValueSample = new DummyRefType() { Value = 1 };

            dic.Add(keyValueSample, keyValueSample);

            await DictionaryConcurrentAccessDetection(dic,
                typeof(DummyRefType).IsValueType,
                customComparer,
                add: d => d.Add(keyValueSample, keyValueSample),
                get: d => { var v = d[keyValueSample]; },
                remove: d => d.Remove(keyValueSample),
                removeOutParam: d => d.Remove(keyValueSample, out var value));
        }
    }

    // We use a custom type instead of string because string use optimized comparer https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/Collections/Generic/Dictionary.cs#L79
    // We want to test case with _comparer = null
    internal class DummyRefType
    {
        public int Value { get; set; }
        public override bool Equals(object obj) => ((DummyRefType)obj).Equals(Value);

        public override int GetHashCode() => Value.GetHashCode();
    }

    internal class CustomEqualityComparerDummyRefType : EqualityComparer<DummyRefType>
    {
        public override bool Equals(DummyRefType x, DummyRefType y) => x.Value == y.Value;

        public override int GetHashCode(DummyRefType obj) => obj.GetHashCode();
    }

    internal class CustomEqualityComparerInt32ValueType : EqualityComparer<int>
    {
        public override bool Equals(int x, int y) => Default.Equals(x, y);

        public override int GetHashCode(int obj) => Default.GetHashCode(obj);
    }
#endif
}
