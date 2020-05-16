using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Collections.Pooled.Benchmarks
{
    public static class SpanSplitExtensions
    {
        public ref struct Enumerator1<T> where T : IEquatable<T>
        {
            public Enumerator1(ReadOnlySpan<T> span, T separator, SpanSplitOptions options)
            {
                Span = span;
                Separator = separator;
                Current = default;
                Options = options;

                if (Span.IsEmpty && Options == SpanSplitOptions.None)
                    TrailingEmptyItem = true;
            }

            private ReadOnlySpan<T> Span { get; set; }
            private T Separator { get; }
            private SpanSplitOptions Options { get; }
            private int SeparatorLength => 1;

            private ReadOnlySpan<T> TrailingEmptyItemSentinel => Unsafe.As<T[]>(nameof(TrailingEmptyItemSentinel)).AsSpan();

            private bool TrailingEmptyItem
            {
                get => Span == TrailingEmptyItemSentinel;
                set => Span = value ? TrailingEmptyItemSentinel : default;
            }

            /// <summary>
            /// Implement IEnumerable.GetEnumerator() to return 'this' as the IEnumerator  
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)] // Only here to make foreach work
            public Enumerator1<T> GetEnumerator() => this;

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public bool MoveNext()
            {
                if (TrailingEmptyItem)
                {
                    TrailingEmptyItem = false;
                    Current = default;
                    return true;
                }

            retry:
                if (Span.IsEmpty)
                {
                    Span = Current = default;
                    return false;
                }

                int idx = Span.IndexOf(Separator);
                if (idx < 0)
                {
                    Current = Span;
                    Span = default;
                }
                else
                {
                    Current = Span.Slice(0, idx);
                    Span = Span.Slice(idx + SeparatorLength);
                    if (Current.IsEmpty && Options == SpanSplitOptions.RemoveEmptyEntries)
                        goto retry;
                    if (Span.IsEmpty && Options == SpanSplitOptions.None)
                        TrailingEmptyItem = true;
                }

                return true;
            }

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public ReadOnlySpan<T> Current { get; private set; }
        }

        public ref struct Enumerator2<T> where T : IEquatable<T>
        {
            public Enumerator2(ReadOnlySpan<T> span, T separator1, T separator2, SpanSplitOptions options)
            {
                Span = span;
                Separator1 = separator1;
                Separator2 = separator2;
                Current = default;
                Options = options;

                if (Span.IsEmpty && Options == SpanSplitOptions.None)
                    TrailingEmptyItem = true;
            }

            private ReadOnlySpan<T> Span { get; set; }
            private T Separator1 { get; }
            private T Separator2 { get; }
            private SpanSplitOptions Options { get; }
            private int SeparatorLength => 1;

            private ReadOnlySpan<T> TrailingEmptyItemSentinel => Unsafe.As<T[]>(nameof(TrailingEmptyItemSentinel)).AsSpan();

            private bool TrailingEmptyItem
            {
                get => Span == TrailingEmptyItemSentinel;
                set => Span = value ? TrailingEmptyItemSentinel : default;
            }

            /// <summary>
            /// Implement IEnumerable.GetEnumerator() to return 'this' as the IEnumerator  
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)] // Only here to make foreach work
            public Enumerator2<T> GetEnumerator() => this;

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public bool MoveNext()
            {
                if (TrailingEmptyItem)
                {
                    TrailingEmptyItem = false;
                    Current = default;
                    return true;
                }

            retry:
                if (Span.IsEmpty)
                {
                    Span = Current = default;
                    return false;
                }

                int idx = Span.IndexOfAny(Separator1, Separator2);
                if (idx < 0)
                {
                    Current = Span;
                    Span = default;
                }
                else
                {
                    Current = Span.Slice(0, idx);
                    Span = Span.Slice(idx + SeparatorLength);
                    if (Current.IsEmpty && Options == SpanSplitOptions.RemoveEmptyEntries)
                        goto retry;
                    if (Span.IsEmpty && Options == SpanSplitOptions.None)
                        TrailingEmptyItem = true;
                }

                return true;
            }

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public ReadOnlySpan<T> Current { get; private set; }
        }

        public ref struct Enumerator3<T> where T : IEquatable<T>
        {
            public Enumerator3(ReadOnlySpan<T> span, T separator1, T separator2, T separator3, SpanSplitOptions options)
            {
                Span = span;
                Separator1 = separator1;
                Separator2 = separator2;
                Separator3 = separator3;
                Current = default;
                Options = options;

                if (Span.IsEmpty && Options == SpanSplitOptions.None)
                    TrailingEmptyItem = true;
            }

            private ReadOnlySpan<T> Span { get; set; }
            private T Separator1 { get; }
            private T Separator2 { get; }
            private T Separator3 { get; }
            private SpanSplitOptions Options { get; }
            private int SeparatorLength => 1;

            private ReadOnlySpan<T> TrailingEmptyItemSentinel => Unsafe.As<T[]>(nameof(TrailingEmptyItemSentinel)).AsSpan();

            private bool TrailingEmptyItem
            {
                get => Span == TrailingEmptyItemSentinel;
                set => Span = value ? TrailingEmptyItemSentinel : default;
            }

            /// <summary>
            /// Implement IEnumerable.GetEnumerator() to return 'this' as the IEnumerator  
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)] // Only here to make foreach work
            public Enumerator3<T> GetEnumerator() => this;

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public bool MoveNext()
            {
                if (TrailingEmptyItem)
                {
                    TrailingEmptyItem = false;
                    Current = default;
                    return true;
                }

            retry:
                if (Span.IsEmpty)
                {
                    Span = Current = default;
                    return false;
                }

                int idx = Span.IndexOfAny(Separator1, Separator2, Separator3);
                if (idx < 0)
                {
                    Current = Span;
                    Span = default;
                }
                else
                {
                    Current = Span.Slice(0, idx);
                    Span = Span.Slice(idx + SeparatorLength);
                    if (Current.IsEmpty && Options == SpanSplitOptions.RemoveEmptyEntries)
                        goto retry;
                    if (Span.IsEmpty && Options == SpanSplitOptions.None)
                        TrailingEmptyItem = true;
                }

                return true;
            }

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public ReadOnlySpan<T> Current { get; private set; }
        }

        public ref struct EnumeratorN<T> where T : IEquatable<T>
        {
            public EnumeratorN(ReadOnlySpan<T> span, ReadOnlySpan<T> separators, SpanSplitOptions options)
            {
                Span = span;
                Separators = separators;
                Current = default;
                Options = options;

                if (Span.IsEmpty && Options == SpanSplitOptions.None)
                    TrailingEmptyItem = true;
            }

            private ReadOnlySpan<T> Span { get; set; }
            private ReadOnlySpan<T> Separators { get; }
            private SpanSplitOptions Options { get; }
            private int SeparatorLength => 1;

            private ReadOnlySpan<T> TrailingEmptyItemSentinel => Unsafe.As<T[]>(nameof(TrailingEmptyItemSentinel)).AsSpan();

            private bool TrailingEmptyItem
            {
                get => Span == TrailingEmptyItemSentinel;
                set => Span = value ? TrailingEmptyItemSentinel : default;
            }

            /// <summary>
            /// Implement IEnumerable.GetEnumerator() to return 'this' as the IEnumerator  
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)] // Only here to make foreach work
            public EnumeratorN<T> GetEnumerator() => this;

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public bool MoveNext()
            {
                if (TrailingEmptyItem)
                {
                    TrailingEmptyItem = false;
                    Current = default;
                    return true;
                }

            retry:
                if (Span.IsEmpty)
                {
                    Span = Current = default;
                    return false;
                }

                int idx = Span.IndexOfAny(Separators);
                if (idx < 0)
                {
                    Current = Span;
                    Span = default;
                }
                else
                {
                    Current = Span.Slice(0, idx);
                    Span = Span.Slice(idx + SeparatorLength);
                    if (Current.IsEmpty && Options == SpanSplitOptions.RemoveEmptyEntries)
                        goto retry;
                    if (Span.IsEmpty && Options == SpanSplitOptions.None)
                        TrailingEmptyItem = true;
                }

                return true;
            }

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public ReadOnlySpan<T> Current { get; private set; }
        }

        public ref struct EnumeratorAll<T> where T : IEquatable<T>
        {
            public EnumeratorAll(ReadOnlySpan<T> span, ReadOnlySpan<T> separator, SpanSplitOptions options)
            {
                Span = span;
                Separator = separator;
                Current = default;
                Options = options;

                if (Span.IsEmpty && Options == SpanSplitOptions.None)
                    TrailingEmptyItem = true;
            }

            private ReadOnlySpan<T> Span { get; set; }
            private ReadOnlySpan<T> Separator { get; }
            private SpanSplitOptions Options { get; }
            private int SeparatorLength => Separator.Length;

            private ReadOnlySpan<T> TrailingEmptyItemSentinel => Unsafe.As<T[]>(nameof(TrailingEmptyItemSentinel)).AsSpan();

            private bool TrailingEmptyItem
            {
                get => Span == TrailingEmptyItemSentinel;
                set => Span = value ? TrailingEmptyItemSentinel : default;
            }

            /// <summary>
            /// Implement IEnumerable.GetEnumerator() to return 'this' as the IEnumerator  
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)] // Only here to make foreach work
            public EnumeratorAll<T> GetEnumerator() => this;

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public bool MoveNext()
            {
                if (TrailingEmptyItem)
                {
                    TrailingEmptyItem = false;
                    Current = default;
                    return true;
                }

            retry:
                if (Span.IsEmpty)
                {
                    Span = Current = default;
                    return false;
                }

                int idx = Span.IndexOf(Separator);
                if (idx < 0)
                {
                    Current = Span;
                    Span = default;
                }
                else
                {
                    Current = Span.Slice(0, idx);
                    Span = Span.Slice(idx + SeparatorLength);
                    if (Current.IsEmpty && Options == SpanSplitOptions.RemoveEmptyEntries)
                        goto retry;
                    if (Span.IsEmpty && Options == SpanSplitOptions.None)
                        TrailingEmptyItem = true;
                }

                return true;
            }

            /// <summary>
            /// Implements the IEnumerator pattern.
            /// </summary>
            public ReadOnlySpan<T> Current { get; private set; }
        }

        /// <summary>
        /// Splits on any occurrence of the <paramref name="separator"/> value.
        /// </summary>
        /// <param name="span">The span to split.</param>
        /// <param name="splitValues">The value to split on.</param>
        /// <param name="options">The <see cref="SpanSplitOptions"/> for this call.</param>
        [Pure]
        public static Enumerator1<T> Split<T>(this ReadOnlySpan<T> span, T separator, SpanSplitOptions options = SpanSplitOptions.None)
            where T : IEquatable<T> => new Enumerator1<T>(span, separator, options);

        /// <summary>
        /// Splits on any of the given values.
        /// </summary>
        /// <param name="span">The span to split.</param>
        /// <param name="separator1"/>
        /// <param name="separator2"/>
        /// <param name="options">The <see cref="SpanSplitOptions"/> for this call.</param>
        [Pure]
        public static Enumerator2<T> Split<T>(this ReadOnlySpan<T> span, T separator1, T separator2, SpanSplitOptions options = SpanSplitOptions.None)
            where T : IEquatable<T> => new Enumerator2<T>(span, separator1, separator2, options);

        /// <summary>
        /// Splits on any of the given values.
        /// </summary>
        /// <param name="span">The span to split.</param>
        /// <param name="separator1"/>
        /// <param name="separator2"/>
        /// <param name="separator3"/>
        /// <param name="options">The <see cref="SpanSplitOptions"/> for this call.</param>
        [Pure]
        public static Enumerator3<T> Split<T>(this ReadOnlySpan<T> span, T separator1, T separator2, T separator3, SpanSplitOptions options = SpanSplitOptions.None)
            where T : IEquatable<T> => new Enumerator3<T>(span, separator1, separator2, separator3, options);

        /// <summary>
        /// Splits on any of the <paramref name="splitValues"/> values given.
        /// </summary>
        /// <param name="span">The span to split.</param>
        /// <param name="splitValues">A span containing values, any one of which will trigger a split.</param>
        /// <param name="options">The <see cref="SpanSplitOptions"/> for this call.</param>
        [Pure]
        public static EnumeratorN<T> Split<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> splitValues, SpanSplitOptions options = SpanSplitOptions.None)
            where T : IEquatable<T> => new EnumeratorN<T>(span, splitValues, options);

        /// <summary>
        /// Splits on any occurrence of all of the values in <paramref name="splitAll"/>, in sequence.
        /// </summary>
        /// <param name="span">The span to split.</param>
        /// <param name="splitValues">A span containing values, any one of which will trigger a split.</param>
        /// <param name="options">The <see cref="SpanSplitOptions"/> for this call.</param>
        [Pure]
        public static EnumeratorAll<T> SplitAll<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> splitAll, SpanSplitOptions options = SpanSplitOptions.None)
            where T : IEquatable<T> => new EnumeratorAll<T>(span, splitAll, options);
    }

    /// <summary>
    /// Specifies whether <see cref="ReadOnlySpan{T}"/> split methods can return empty spans.
    /// </summary>
    [Flags]
    public enum SpanSplitOptions : byte
    {
        /// <summary>
        /// The return value may include empty spans.
        /// </summary>
        None = 0,
        /// <summary>
        /// The return value does not include empty spans.
        /// </summary>
        RemoveEmptyEntries = 1
    }
}
