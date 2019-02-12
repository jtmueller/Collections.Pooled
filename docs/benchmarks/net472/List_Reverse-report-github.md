``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |     StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **42.78 us** |  **0.8591 us** |  **1.1759 us** |  **42.38 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  44.42 us |  0.6380 us |  0.4981 us |  44.33 us |  1.03 |    0.03 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 289.27 us |  5.7665 us | 15.6882 us | 285.25 us |  6.81 |    0.44 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 298.22 us |  5.9725 us | 14.6507 us | 304.11 us |  7.02 |    0.36 |           - |           - |           - |                   - |
|                      |        |           |            |            |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** |  **81.06 us** |  **1.6245 us** |  **2.8452 us** |  **81.82 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 |  84.62 us |  1.6197 us |  1.8003 us |  83.65 us |  1.05 |    0.04 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 569.94 us | 12.2125 us | 34.4455 us | 565.64 us |  7.05 |    0.58 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 576.20 us | 11.4098 us | 26.8942 us | 574.13 us |  7.09 |    0.47 |           - |           - |           - |                   - |
