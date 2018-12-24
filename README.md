# Core.Collections

This library is based on classes from `System.Collections.Generic` that have been altered 
to take advantage of the new `System.Span<T>` and `System.Buffers.ArrayPool<T>` libraries 
to minimize memory allocations, improve performance, and/or allow greater interoperablity 
with modern API's.

Core.Collections supports both .NET Standard 2.0 (.NET Framework 4.6.1+) as well as an 
optimized build for .NET Core 2.2+. An extensive set of unit tests and benchmarks have
been ported from [corefx](https://github.com/dotnet/corefx).

## Benchmarks

  * [.NET Core](https://github.com/jtmueller/Core.Collections/tree/master/docs/benchmarks/netcoreapp2.2)
  * [.NET Framework](https://github.com/jtmueller/Core.Collections/tree/master/docs/benchmarks/net472)

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

#### Performance

Please review the benchmark links above for complete details. Performance and memory allocations
both range from "on par with `List<T>`" to "far better than `List<T>`" depending on the operation.

For example, AddRange is a particular strength for PooledList:

|         Method |       N |            Mean | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |-------- |----------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListAddRange** |    **1000** |      **1,290.5 us** |  **1.00** |   **6451.1719** |           **-** |           **-** |         **19843.75 KB** |
| PooledAddRange |    1000 |        677.6 us |  0.53 |     50.7813 |           - |           - |           156.25 KB |
|                |         |                 |       |             |             |             |                     |
|   **ListAddRange** |   **10000** |     **13,279.8 us** |  **1.00** |  **63281.2500** |           **-** |           **-** |           **195625 KB** |
| PooledAddRange |   10000 |      4,899.8 us |  0.37 |     46.8750 |           - |           - |           156.25 KB |
|                |         |                 |       |             |             |             |                     |
|   **ListAddRange** |  **100000** |    **342,688.0 us** |  **1.00** | **519000.0000** | **510000.0000** | **510000.0000** |       **1956398.97 KB** |
| PooledAddRange |  100000 |     72,609.7 us |  0.21 |           - |           - |           - |           156.25 KB |
|                |         |                 |       |             |             |             |                     |
|   **ListAddRange** | **1000000** | **11,534,038.8 us** |  **1.00** | **769000.0000** | **768000.0000** | **768000.0000** |       **19531562.5 KB** |
| PooledAddRange | 1000000 |  1,357,305.1 us |  0.12 |           - |           - |           - |           156.25 KB |

That's right, the extreme case of adding a million integers 5000 times allocates 19.5 GB with `List<T>` and 156 KB 
with `PooledList<T>`, while `PooledList<T>` gets it done in 12% of the time.
