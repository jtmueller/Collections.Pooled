``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|            Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListSort_Int** | **100000** |   **6.843 ms** | **0.1354 ms** | **0.3000 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.813 ms | 0.1340 ms | 0.3051 ms |  1.00 |    0.06 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 197.622 ms | 1.6190 ms | 1.5145 ms | 28.84 |    1.52 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 196.802 ms | 1.6149 ms | 1.5106 ms | 28.72 |    1.37 |           - |           - |           - |                   - |
|                   |        |            |           |           |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **13.978 ms** | **0.2661 ms** | **0.2733 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  13.882 ms | 0.1909 ms | 0.1693 ms |  0.99 |    0.03 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 426.847 ms | 1.2842 ms | 1.1385 ms | 30.52 |    0.65 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 432.240 ms | 2.4275 ms | 2.2707 ms | 30.90 |    0.61 |           - |           - |           - |                   - |
