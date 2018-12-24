``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|            Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListSort_Int** | **100000** |   **6.719 ms** | **0.1334 ms** | **0.2663 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.747 ms | 0.1349 ms | 0.2904 ms |  1.01 |    0.05 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 193.503 ms | 1.3430 ms | 1.2563 ms | 28.65 |    0.91 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 197.590 ms | 0.9574 ms | 0.7995 ms | 29.22 |    0.99 |           - |           - |           - |                   - |
|                   |        |            |           |           |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **13.855 ms** | **0.3261 ms** | **0.3349 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  13.650 ms | 0.2346 ms | 0.2194 ms |  0.99 |    0.03 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 427.099 ms | 1.4006 ms | 1.2416 ms | 30.79 |    0.82 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 422.249 ms | 2.0801 ms | 1.9457 ms | 30.49 |    0.82 |           - |           - |           - |                   - |
