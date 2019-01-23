``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **42.51 us** |  **0.8465 us** |  **1.548 us** |  **41.94 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  44.47 us |  1.0002 us |  1.951 us |  43.91 us |  1.05 |    0.06 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 300.12 us |  6.4644 us | 17.030 us | 304.18 us |  7.05 |    0.45 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 298.68 us |  5.9693 us | 13.351 us | 304.39 us |  6.99 |    0.41 |           - |           - |           - |                   - |
|                      |        |           |            |           |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** |  **82.93 us** |  **1.6456 us** |  **3.248 us** |  **82.40 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 |  85.76 us |  1.5402 us |  1.286 us |  86.27 us |  1.04 |    0.04 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 643.50 us | 31.8713 us | 90.931 us | 611.76 us |  7.80 |    1.19 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 602.35 us | 12.3270 us | 30.469 us | 595.41 us |  7.29 |    0.48 |           - |           - |           - |                   - |
