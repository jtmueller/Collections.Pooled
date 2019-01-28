``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|               Method | CountToUnion | InitialSetSize |        Mean |      Error |       StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------------- |--------------- |------------:|-----------:|-------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Union_NoOp** |        **32000** |          **32000** |    **974.2 us** |  **23.685 us** |    **67.190 us** |    **960.1 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |        32000 |          32000 |    674.3 us |  13.445 us |    32.470 us |    662.1 us |  0.69 |    0.05 |           - |           - |           - |                   - |
|                      |              |                |             |            |              |             |       |         |             |             |             |                     |
|   **HashSet_Union_NoOp** |        **32000** |         **320000** |    **924.5 us** |   **7.854 us** |     **7.347 us** |    **925.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |        32000 |         320000 |    622.2 us |   4.534 us |     4.241 us |    622.1 us |  0.67 |    0.01 |           - |           - |           - |                   - |
|                      |              |                |             |            |              |             |       |         |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |          **32000** | **13,062.2 us** | **388.579 us** | **1,139.634 us** | **12,870.1 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |       320000 |          32000 |  8,758.7 us |  87.405 us |    81.758 us |  8,742.0 us |  0.70 |    0.04 |           - |           - |           - |                   - |
|                      |              |                |             |            |              |             |       |         |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |         **320000** |  **9,092.2 us** | **116.446 us** |   **108.924 us** |  **9,093.3 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |       320000 |         320000 |  6,018.5 us |  51.833 us |    48.484 us |  6,003.8 us |  0.66 |    0.01 |           - |           - |           - |                   - |
