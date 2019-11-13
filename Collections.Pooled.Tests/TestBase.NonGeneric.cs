// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.Pooled.Tests
{
    /// <summary>
    /// Provides a base set of nongeneric operations that are used by all other testing interfaces.
    /// </summary>
    public abstract class TestBase : IDisposable
    {
        private readonly PooledList<IDisposable> _disposables = new PooledList<IDisposable>();

        #region Helper Methods

        public static IEnumerable<object[]> ValidCollectionSizes()
        {
            yield return new object[] { 0 };
            yield return new object[] { 1 };
            yield return new object[] { 5 };
            yield return new object[] { 75 };
        }

        protected T RegisterForDispose<T>(T obj)
        {
            if (obj is IDisposable disposable)
                _disposables.Add(disposable);
            return obj;
        }

        protected void RegisterForDispose<T>(params T[] objects)
        {
            if (objects.Length == 0)
                return;

            _disposables.AddRange(objects.OfType<IDisposable>());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposables.ForEach(d => d?.Dispose());
                _disposables.Dispose();
            }
        }

        public enum EnumerableType
        {
            HashSet,
            SortedSet,
            List,
            Queue,
            Lazy,
        };

        [Flags]
        public enum ModifyOperation
        {
            None = 0,
            Add = 1,
            Insert = 2,
            Remove = 4,
            Clear = 8
        }

        #endregion
    }
}
