``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |      N |           Mean |        Error |       StdDev | Ratio | RatioSD |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------- |---------------:|-------------:|-------------:|------:|--------:|-------------:|-------------:|-------------:|--------------------:|
|   **ListAddRangeICollection** |   **1000** |     **1,333.2 us** |     **9.481 us** |     **8.868 us** |  **1.00** |    **0.00** |    **6451.1719** |            **-** |            **-** |         **19843.75 KB** |
| PooledAddRangeICollection |   1000 |       713.3 us |     2.475 us |     2.315 us |  0.54 |    0.00 |      88.8672 |            - |            - |           273.44 KB |
|   ListAddRangeIEnumerable |   1000 |    39,898.3 us |   214.306 us |   200.462 us | 29.93 |    0.24 |   13384.6154 |            - |            - |         41367.19 KB |
| PooledAddRangeIEnumerable |   1000 |    45,911.2 us |   116.986 us |   109.428 us | 34.44 |    0.23 |      90.9091 |            - |            - |           468.75 KB |
|                           |        |                |              |              |       |         |              |              |              |                     |
|   **ListAddRangeICollection** |  **10000** |    **13,133.9 us** |   **119.178 us** |   **111.479 us** |  **1.00** |    **0.00** |   **63281.2500** |            **-** |            **-** |           **195625 KB** |
| PooledAddRangeICollection |  10000 |     5,675.8 us |    14.616 us |    13.672 us |  0.43 |    0.00 |      85.9375 |            - |            - |           273.44 KB |
|   ListAddRangeIEnumerable |  10000 |   390,966.8 us |   869.243 us |   725.858 us | 29.73 |    0.26 |  208000.0000 |            - |            - |        641835.94 KB |
| PooledAddRangeIEnumerable |  10000 |   438,033.1 us | 1,833.599 us | 1,625.438 us | 33.34 |    0.32 |            - |            - |            - |           468.75 KB |
|                           |        |                |              |              |       |         |              |              |              |                     |
|   **ListAddRangeICollection** | **100000** | **1,076,759.7 us** | **4,636.316 us** | **3,871.534 us** |  **1.00** |    **0.00** |  **624000.0000** |  **624000.0000** |  **624000.0000** |        **1953437.5 KB** |
| PooledAddRangeICollection | 100000 |    70,037.6 us |   262.061 us |   245.132 us |  0.07 |    0.00 |            - |            - |            - |           273.44 KB |
|   ListAddRangeIEnumerable | 100000 | 5,130,867.5 us | 5,197.014 us | 4,607.018 us |  4.76 |    0.02 | 1428000.0000 | 1428000.0000 | 1428000.0000 |        5122187.5 KB |
| PooledAddRangeIEnumerable | 100000 | 4,300,716.6 us | 5,190.893 us | 4,855.564 us |  3.99 |    0.02 |            - |            - |            - |           468.75 KB |
