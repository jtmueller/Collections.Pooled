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
|      **ListSort_Int** | **100000** |   **7.037 ms** | **0.1406 ms** | **0.1619 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.860 ms | 0.1361 ms | 0.2780 ms |  0.98 |    0.04 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 199.946 ms | 2.3768 ms | 2.2232 ms | 28.38 |    0.69 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 199.036 ms | 2.3860 ms | 2.2319 ms | 28.25 |    0.78 |           - |           - |           - |                   - |
|                   |        |            |           |           |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **14.282 ms** | **0.2755 ms** | **0.2829 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  14.171 ms | 0.2683 ms | 0.2509 ms |  0.99 |    0.03 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 433.140 ms | 3.4946 ms | 3.2689 ms | 30.33 |    0.62 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 432.856 ms | 2.3878 ms | 2.2336 ms | 30.31 |    0.63 |           - |           - |           - |                   - |
