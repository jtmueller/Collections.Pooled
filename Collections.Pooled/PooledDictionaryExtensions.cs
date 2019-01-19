using System;
using System.Collections.Generic;

namespace Collections.Pooled
{
    /// <summary>
    /// Extension methods for creating <see cref="PooledDictionary{TKey, TValue}"/> instances.
    /// </summary>
    public static class PooledDictionaryExtensions
    {
        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IEnumerable{TSource}"/> according to specified 
        /// key selector and element selector functions, as well as a comparer.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer = null)
        {
            var dict = new PooledDictionary<TKey, TValue>((source as ICollection<TSource>)?.Count ?? 0, comparer);
            foreach (var item in source)
            {
                dict.Add(keySelector(item), valueSelector(item));
            }
            return dict;
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="ReadOnlySpan{TSource}"/> according to specified 
        /// key selector and element selector functions, as well as a comparer.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(this ReadOnlySpan<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer = null)
        {
            var dict = new PooledDictionary<TKey, TValue>(source.Length, comparer);
            foreach (var item in source)
            {
                dict.Add(keySelector(item), valueSelector(item));
            }
            return dict;
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="Span{TSource}"/> according to specified 
        /// key selector and element selector functions, as well as a comparer.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(this Span<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        {
            return ToPooledDictionary((ReadOnlySpan<TSource>)source, keySelector, valueSelector, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="ReadOnlyMemory{TSource}"/> according to specified 
        /// key selector and element selector functions, as well as a comparer.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(this ReadOnlyMemory<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        {
            return ToPooledDictionary(source.Span, keySelector, valueSelector, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="Memory{TSource}"/> according to specified 
        /// key selector and element selector functions, as well as a comparer.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(this Memory<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        {
            return ToPooledDictionary(source.Span, keySelector, valueSelector, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IEnumerable{TSource}"/> according to specified 
        /// key selector and comparer.
        /// </summary>
        public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            var dict = new PooledDictionary<TKey, TSource>((source as ICollection<TSource>)?.Count ?? 0, comparer);
            foreach (var item in source)
            {
                dict.Add(keySelector(item), item);
            }
            return dict;
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="ReadOnlySpan{TSource}"/> according to specified 
        /// key selector and comparer.
        /// </summary>
        public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this ReadOnlySpan<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            var dict = new PooledDictionary<TKey, TSource>(source.Length, comparer);
            foreach (var item in source)
            {
                dict.Add(keySelector(item), item);
            }
            return dict;
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="Span{TSource}"/> according to specified 
        /// key selector and comparer.
        /// </summary>
        public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this Span<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            return ToPooledDictionary((ReadOnlySpan<TSource>)source, keySelector, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="ReadOnlyMemory{TSource}"/> according to specified 
        /// key selector and comparer.
        /// </summary>
        public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this ReadOnlyMemory<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            return ToPooledDictionary(source.Span, keySelector, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="Memory{TSource}"/> according to specified 
        /// key selector and comparer.
        /// </summary>
        public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this Memory<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            return ToPooledDictionary(source.Span, keySelector, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of key/value tuples.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source,
            IEqualityComparer<TKey> comparer = null)
        {
            return new PooledDictionary<TKey, TValue>(source, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of key/value tuples.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this ReadOnlySpan<(TKey, TValue)> source,
            IEqualityComparer<TKey> comparer = null)
        {
            return new PooledDictionary<TKey, TValue>(source, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of key/value tuples.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this Span<(TKey, TValue)> source,
            IEqualityComparer<TKey> comparer = null)
        {
            return new PooledDictionary<TKey, TValue>(source, comparer);
        }
    }
}
