using System;

namespace Collections.Pooled.Benchmarks
{
#if NETCOREAPP3_1
    public static class SpanSplitExtensions
    {
        // Borrowed from here until it's merged into dotnet
        // https://github.com/dotnet/runtime/blob/2da605832d09f8bd046517b463652cc52ec6553e/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.Split.cs

        public static SpanSplitEnumerator<char> Split(this ReadOnlySpan<char> span)
            => new SpanSplitEnumerator<char>(span, ' ');

        public static SpanSplitEnumerator<char> Split(this ReadOnlySpan<char> span, char separator)
            => new SpanSplitEnumerator<char>(span, separator);

        public static SpanSplitSequenceEnumerator<char> Split(this ReadOnlySpan<char> span, string separator)
            => new SpanSplitSequenceEnumerator<char>(span, separator);
    }

    public ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
    {
        private readonly ReadOnlySpan<T> _sequence;
        private readonly T _separator;
        private int _offset;
        private int _index;

        public SpanSplitEnumerator<T> GetEnumerator() => this;

        internal SpanSplitEnumerator(ReadOnlySpan<T> span, T separator)
        {
            _sequence = span;
            _separator = separator;
            _index = 0;
            _offset = 0;
        }

        public Range Current => new Range(_offset, _offset + _index - 1);

        public bool MoveNext()
        {
            if (_sequence.Length - _offset < _index) { return false; }
            var slice = _sequence.Slice(_offset += _index);

            var nextIdx = slice.IndexOf(_separator);
            _index = (nextIdx != -1 ? nextIdx : slice.Length) + 1;
            return true;
        }
    }

    public ref struct SpanSplitSequenceEnumerator<T> where T : IEquatable<T>
    {
        private readonly ReadOnlySpan<T> _sequence;
        private readonly ReadOnlySpan<T> _separator;
        private int _offset;
        private int _index;

        public SpanSplitSequenceEnumerator<T> GetEnumerator() => this;

        internal SpanSplitSequenceEnumerator(ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
        {
            _sequence = span;
            _separator = separator;
            _index = 0;
            _offset = 0;
        }

        public Range Current => new Range(_offset, _offset + _index - 1);

        public bool MoveNext()
        {
            if (_sequence.Length - _offset < _index) { return false; }
            var slice = _sequence.Slice(_offset += _index);

            var nextIdx = slice.IndexOf(_separator);
            _index = (nextIdx != -1 ? nextIdx : slice.Length) + 1;
            return true;
        }
    }
#endif
}
