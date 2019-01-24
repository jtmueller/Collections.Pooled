# Collections.Pooled [![NuGet Version](https://img.shields.io/nuget/v/Collections.Pooled.svg?style=flat)](https://www.nuget.org/packages/Collections.Pooled/) [![Build status](https://ci.appveyor.com/api/projects/status/vb6j2jon32ia5qq4/branch/master?svg=true)](https://ci.appveyor.com/project/jtmueller/collections-pooled/branch/master)

This library is based on classes from `System.Collections.Generic` that have been altered 
to take advantage of the new `System.Span<T>` and `System.Buffers.ArrayPool<T>` libraries 
to minimize memory allocations, improve performance, and/or allow greater interoperablity 
with modern API's.

Collections.Pooled supports both .NET Standard 2.0 (.NET Framework 4.6.1+) as well as an 
optimized build for .NET Core 2.1+. An extensive set of unit tests and benchmarks have
been ported from [corefx](https://github.com/dotnet/corefx).

## Installation

Available on NuGet:
```
Install-Package Collections.Pooled
dotnet add package Collections.Pooled
paket add Collections.Pooled
```

## Benchmarks

  * [.NET Core](https://github.com/jtmueller/Collections.Pooled/tree/master/docs/benchmarks/netcoreapp2.2)
  * [.NET Framework](https://github.com/jtmueller/Collections.Pooled/tree/master/docs/benchmarks/net472)

## Collections

### `PooledList<T>`

`PooledList<T>` is based on the corefx source code for `System.Collections.Generic.List<T>`,
modified to use ArrayPool for internal array-storage allocation, and to support `Span<T>`.

There are some API changes worth noting:

  * `Find` and `FindLast` have become `TryFind` and `TryFindLast`, following the standard
    .NET "try" pattern.
  * The new `PooledList<T>.Span` property returns a `Span<T>` over the portion of the internal
    array store that is populated. This span can be further sliced, or passed into other methods
    that can read from or write to a span.
    * Enumerating the Span property can be more than twice as fast as enumerating the list itself,
      but as we are unable to do the normal "collection modified during enumeration" checks, please
      use with caution.
    * The list is unable to increment its version-checking flag when setting values via the Span.
      Therefore, "collection modified during enumeration" checks can be bypassed. Please use with caution.
  * `GetRange` now returns a `Span<T>`. For example, `foo.GetRange(5, 100)` is equivalent to `foo.Span.Slice(5, 100)`.
  * `CopyTo` now takes a `Span<T>` and doesn't offer any start-index or count parameters. Use the `Span` property and slicing instead.
  * `AddRange` and `InsertRange` can now accept a `ReadOnlySpan<T>`.
  * The new `AddSpan` and `InsertSpan` methods ensure the internal storage has the capacity
    to add the requested number of items, and return a `Span<T>` that can be used to write
    directly to that section of the internal storage. Caveats about "collection modified during enumeration"
    checks apply here as well.
  * Delegate types such as `Predicate<T>` and `Converter<T1, T2>` have been replaced with standard `Func<>` equivalents.
  * **PooledList implements IDisposable.** Disposing the list returns the internal array to the ArrayPool.
    If you forget to dispose the list, nothing will break, but memory allocations and GC pauses will be closer to those
    of `List<T>` (you will still benefit from pooling of intermediate arrays as the PooledList is resized).
  * A selection of `ToPooledList()` extension methods are provided.

#### Performance

Please review the benchmark links above for complete details. Performance and memory allocations
both range from "on par with `List<T>`" to "far better than `List<T>`" depending on the operation.

### `PooledDictionary<TKey, TValue>`

`PooledDictionary<TKey, TValue>` is based on the corefx source code for `System.Collections.Generic.Dictionary<TKey, TValue>`,
modified to use ArrayPool for internal storage allocation, and to support `Span<T>`.

There are some API changes worth noting:

  * New methods include: `AddRange`, `GetOrAdd`, `AddOrUpdate`
  * Both constructors and AddRange can take a sequence of `KeyValuePair<TKey, TValue>` objects, or a sequence of 
    `ValueTuple<TKey, TValue>` objects.
  * Significantly reduced memory allocations when adding many items.
  * **PooledDictionary implements IDisposable.** Disposing the dictionary returns the internal arrays to the ArrayPool.
    If you forget to dispose the dictionary, nothing will break, but memory allocations and GC pauses will be closer to those
    of `Dictionary<TKey, TValue>` (you will still benefit from pooling of intermediate arrays as the PooledDictionary is resized).
  * A selection of `ToPooledDictionary()` extension methods are provided.

### `PooledStack<T>`

`PooledStack<T>` is based on the corefx source code for `System.Generic.Collections.Stack<T>`, 
modified to use ArrayPool for internal storage allocation.

  * Other than the ability to pass Spans into the constructor, there are no API changes
    compared to the original Stack.
  * Significantly reduced memory allocations when pushing many items.
  * **PooledStack implements IDisposable.** Disposing the stack returns the internal arrays to the ArrayPool.
    If you forget to dispose the stack, nothing will break, but memory allocations and GC pauses will be closer to those
    of `Stack<T>` (you will still benefit from pooling of intermediate arrays as the PooledStack is resized).
  * A selection of `ToPooledStack()` extension methods are provided.