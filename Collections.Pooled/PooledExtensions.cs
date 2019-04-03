using System;
using System.Collections.Generic;

namespace Collections.Pooled
{
    /// <summary>
    /// Extension methods for creating pooled collections.
    /// </summary>
    public static class PooledExtensions
    {
        #region PooledList

        public static PooledList<T> ToPooledList<T>(this IEnumerable<T> items)
            => new PooledList<T>(items);

        public static PooledList<T> ToPooledList<T>(this T[] array)
            => new PooledList<T>(array.AsSpan());

        public static PooledList<T> ToPooledList<T>(this ReadOnlySpan<T> span)
            => new PooledList<T>(span);

        public static PooledList<T> ToPooledList<T>(this Span<T> span)
            => new PooledList<T>(span);

        public static PooledList<T> ToPooledList<T>(this ReadOnlyMemory<T> memory)
            => new PooledList<T>(memory.Span);

        public static PooledList<T> ToPooledList<T>(this Memory<T> memory)
            => new PooledList<T>(memory.Span);

        #endregion

        #region PooledDictionary

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IEnumerable{TSource}"/> according to specified 
        /// key selector and element selector functions, as well as a comparer.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
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
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
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
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of KeyValuePair values.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
            IEqualityComparer<TKey> comparer = null)
        {
            return new PooledDictionary<TKey, TValue>(source, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of key/value tuples.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> source,
            IEqualityComparer<TKey> comparer = null)
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            var dict = new PooledDictionary<TKey, TValue>((source as ICollection<Tuple<TKey, TValue>>)?.Count ?? 0, comparer);
            foreach (var pair in source)
            {
                dict.Add(pair.Item1, pair.Item2);
            }
            return dict;
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a span of key/value tuples.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this ReadOnlySpan<(TKey, TValue)> source,
            IEqualityComparer<TKey> comparer = null)
        {
            return new PooledDictionary<TKey, TValue>(source, comparer);
        }

        /// <summary>
        /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a span of key/value tuples.
        /// </summary>
        public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this Span<(TKey, TValue)> source,
            IEqualityComparer<TKey> comparer = null)
        {
            return new PooledDictionary<TKey, TValue>(source, comparer);
        }

        #endregion

        #region PooledSet

        public static PooledSet<T> ToPooledSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
            => new PooledSet<T>(source, comparer);

        public static PooledSet<T> ToPooledSet<T>(this Span<T> source, IEqualityComparer<T> comparer = null)
            => new PooledSet<T>(source, comparer);

        public static PooledSet<T> ToPooledSet<T>(this ReadOnlySpan<T> source, IEqualityComparer<T> comparer = null)
            => new PooledSet<T>(source, comparer);

        public static PooledSet<T> ToPooledSet<T>(this Memory<T> source, IEqualityComparer<T> comparer = null)
            => new PooledSet<T>(source.Span, comparer);

        public static PooledSet<T> ToPooledSet<T>(this ReadOnlyMemory<T> source, IEqualityComparer<T> comparer = null)
            => new PooledSet<T>(source.Span, comparer);

        #endregion

        #region PooledStack

        /// <summary>
        /// Creates an instance of PooledStack from the given items.
        /// </summary>
        public static PooledStack<T> ToPooledStack<T>(this IEnumerable<T> items)
            => new PooledStack<T>(items);

        /// <summary>
        /// Creates an instance of PooledStack from the given items.
        /// </summary>
        public static PooledStack<T> ToPooledStack<T>(this T[] array)
            => new PooledStack<T>(array.AsSpan());

        /// <summary>
        /// Creates an instance of PooledStack from the given items.
        /// </summary>
        public static PooledStack<T> ToPooledStack<T>(this ReadOnlySpan<T> span)
            => new PooledStack<T>(span);

        /// <summary>
        /// Creates an instance of PooledStack from the given items.
        /// </summary>
        public static PooledStack<T> ToPooledStack<T>(this Span<T> span)
            => new PooledStack<T>(span);

        /// <summary>
        /// Creates an instance of PooledStack from the given items.
        /// </summary>
        public static PooledStack<T> ToPooledStack<T>(this ReadOnlyMemory<T> memory)
            => new PooledStack<T>(memory.Span);

        /// <summary>
        /// Creates an instance of PooledStack from the given items.
        /// </summary>
        public static PooledStack<T> ToPooledStack<T>(this Memory<T> memory)
            => new PooledStack<T>(memory.Span);

        #endregion

        #region PooledQueue

        /// <summary>
        /// Creates an instance of PooledQueue from the given items.
        /// </summary>
        public static PooledQueue<T> ToPooledQueue<T>(this IEnumerable<T> items)
            => new PooledQueue<T>(items);

        /// <summary>
        /// Creates an instance of PooledQueue from the given items.
        /// </summary>
        public static PooledQueue<T> ToPooledQueue<T>(this ReadOnlySpan<T> span)
            => new PooledQueue<T>(span);

        /// <summary>
        /// Creates an instance of PooledQueue from the given items.
        /// </summary>
        public static PooledQueue<T> ToPooledQueue<T>(this Span<T> span)
            => new PooledQueue<T>(span);

        /// <summary>
        /// Creates an instance of PooledQueue from the given items.
        /// </summary>
        public static PooledQueue<T> ToPooledQueue<T>(this ReadOnlyMemory<T> memory)
            => new PooledQueue<T>(memory.Span);

        /// <summary>
        /// Creates an instance of PooledQueue from the given items.
        /// </summary>
        public static PooledQueue<T> ToPooledQueue<T>(this Memory<T> memory)
            => new PooledQueue<T>(memory.Span);

        /// <summary>
        /// Creates an instance of PooledQueue from the given items.
        /// </summary>
        public static PooledQueue<T> ToPooledQueue<T>(this T[] array)
            => new PooledQueue<T>(array.AsSpan());


        #endregion
    }
}
