``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |     StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **62.71 us** |  **7.3159 us** | **21.5711 us** |  **50.66 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  50.33 us |  0.9946 us |  0.9769 us |  50.01 us |  0.83 |    0.24 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 248.69 us |  8.4150 us | 23.4575 us | 236.17 us |  4.32 |    1.16 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 249.53 us |  8.8031 us | 24.3934 us | 236.11 us |  4.33 |    1.17 |           - |           - |           - |                   - |
|                      |        |           |            |            |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** | **111.84 us** |  **7.9364 us** | **23.0250 us** |  **97.56 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 | 121.47 us | 10.2024 us | 29.5991 us | 101.19 us |  1.12 |    0.33 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 502.02 us | 13.0987 us | 36.7302 us | 487.19 us |  4.65 |    0.90 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 520.94 us | 21.7691 us | 61.0429 us | 497.46 us |  4.83 |    1.06 |           - |           - |           - |                   - |
