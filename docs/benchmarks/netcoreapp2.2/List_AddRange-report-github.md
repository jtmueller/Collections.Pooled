``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |      N |           Mean |         Error |        StdDev | Ratio | RatioSD |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------- |---------------:|--------------:|--------------:|------:|--------:|-------------:|-------------:|-------------:|--------------------:|
|   **ListAddRangeICollection** |   **1000** |     **1,678.0 us** |     **10.172 us** |      **9.515 us** |  **1.00** |    **0.00** |    **6451.1719** |            **-** |            **-** |         **19843.75 KB** |
| PooledAddRangeICollection |   1000 |       713.1 us |      4.300 us |      4.022 us |  0.42 |    0.00 |      63.4766 |            - |            - |           195.31 KB |
|   ListAddRangeIEnumerable |   1000 |    41,629.8 us |    462.084 us |    432.234 us | 24.81 |    0.32 |   13384.6154 |            - |            - |         41367.19 KB |
| PooledAddRangeIEnumerable |   1000 |    42,866.8 us |    205.464 us |    182.138 us | 25.54 |    0.22 |      83.3333 |            - |            - |           390.63 KB |
|                           |        |                |               |               |       |         |              |              |              |                     |
|   **ListAddRangeICollection** |  **10000** |    **16,520.2 us** |    **125.062 us** |    **116.983 us** |  **1.00** |    **0.00** |   **63281.2500** |            **-** |            **-** |           **195625 KB** |
| PooledAddRangeICollection |  10000 |     5,787.3 us |     23.854 us |     21.146 us |  0.35 |    0.00 |      62.5000 |            - |            - |           195.31 KB |
|   ListAddRangeIEnumerable |  10000 |   416,940.4 us |  8,105.629 us |  7,582.011 us | 25.24 |    0.47 |  208000.0000 |            - |            - |        641835.94 KB |
| PooledAddRangeIEnumerable |  10000 |   434,805.2 us |  4,569.596 us |  4,050.828 us | 26.32 |    0.39 |            - |            - |            - |           390.63 KB |
|                           |        |                |               |               |       |         |              |              |              |                     |
|   **ListAddRangeICollection** | **100000** | **1,268,627.4 us** |  **3,379.992 us** |  **3,161.647 us** |  **1.00** |    **0.00** |  **624000.0000** |  **624000.0000** |  **624000.0000** |        **1953437.5 KB** |
| PooledAddRangeICollection | 100000 |    71,258.3 us |  1,135.974 us |  1,062.591 us |  0.06 |    0.00 |            - |            - |            - |           195.31 KB |
|   ListAddRangeIEnumerable | 100000 | 5,584,139.9 us | 27,122.385 us | 24,043.288 us |  4.40 |    0.02 | 1428000.0000 | 1428000.0000 | 1428000.0000 |        5122187.5 KB |
| PooledAddRangeIEnumerable | 100000 | 4,197,653.5 us | 12,994.041 us | 12,154.634 us |  3.31 |    0.01 |            - |            - |            - |           390.63 KB |
