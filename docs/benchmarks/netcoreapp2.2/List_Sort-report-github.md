``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|            Method |      N |       Mean |     Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListSort_Int** | **100000** |   **6.769 ms** | **0.1352 ms** |  **0.2851 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.868 ms | 0.1357 ms |  0.2740 ms |  1.02 |    0.06 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 193.435 ms | 1.2953 ms |  1.0113 ms | 29.42 |    0.90 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 191.912 ms | 3.8313 ms |  5.4948 ms | 28.40 |    1.21 |           - |           - |           - |                   - |
|                   |        |            |           |            |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **14.611 ms** | **0.3222 ms** |  **0.2856 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  13.755 ms | 0.2430 ms |  0.2029 ms |  0.94 |    0.02 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 442.656 ms | 3.2529 ms |  2.8836 ms | 30.31 |    0.65 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 467.814 ms | 8.7268 ms | 10.7172 ms | 32.11 |    0.98 |           - |           - |           - |                   - |
