using System;

namespace Collections.Pooled.Benchmarks
{
#if NETCOREAPP3_1
    public static class SpanSplitExtensions
    {
        public static SpanSplitEnumerator<char> Split(this ReadOnlySpan<char> span, char separator)
            => new SpanSplitEnumerator<char>(span, separator);

        public static SpanSplitEnumerator<char> Split(this ReadOnlySpan<char> span, string separator)
            => new SpanSplitEnumerator<char>(span, separator.AsSpan());
    }

    public ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
    {
        private readonly ReadOnlySpan<T> _sequence;
        private readonly T _separator;
        private readonly ReadOnlySpan<T> _separators;
        private readonly int _increment;
        private int _offset;
        private int _index;

        public SpanSplitEnumerator<T> GetEnumerator() => this;

        internal SpanSplitEnumerator(ReadOnlySpan<T> span, T separator)
        {
            _sequence = span;
            _separator = separator;
            _separators = ReadOnlySpan<T>.Empty;
            _increment = 0;
            _index = 0;
            _offset = 0;
        }

        internal SpanSplitEnumerator(ReadOnlySpan<T> span, ReadOnlySpan<T> separators)
        {
            _sequence = span;
            _separator = default!;
            _separators = separators;
            _increment = Math.Max(0, separators.Length - 1);
            _index = 0;
            _offset = 0;
        }

        public Range Current => new Range(_offset, _offset + _index - 1);

        public bool MoveNext()
        {
            if (_sequence.Length - _offset < _index) { return false; }
            var slice = _sequence.Slice(_offset += _index + (_index == 0 ? 0 : _increment));

            var nextIdx = _separators.IsEmpty
                ? slice.IndexOf(_separator)
                : slice.IndexOf(_separators);
            _index = (nextIdx != -1 ? nextIdx : slice.Length) + 1;
            if (_offset + _index > _sequence.Length + 1)
                _index = _sequence.Length + 1 - _offset;
            return true;
        }
    }
#endif
}
