``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **42.23 us** |  **0.8503 us** |  **1.576 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  44.39 us |  0.8731 us |  1.483 us |  1.06 |    0.05 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 305.57 us |  6.9599 us | 19.744 us |  7.20 |    0.46 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 295.87 us |  5.8781 us | 15.173 us |  7.02 |    0.50 |           - |           - |           - |                   - |
|                      |        |           |            |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** |  **82.75 us** |  **2.1314 us** |  **5.799 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 |  84.68 us |  1.6956 us |  3.425 us |  1.03 |    0.08 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 634.23 us | 12.5978 us | 28.177 us |  7.68 |    0.64 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 585.86 us | 11.4954 us | 15.346 us |  7.24 |    0.38 |           - |           - |           - |                   - |
