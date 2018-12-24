``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|            Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListSort_Int** | **100000** |   **6.659 ms** | **0.1299 ms** | **0.2060 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.677 ms | 0.1321 ms | 0.2449 ms |  1.00 |    0.05 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 196.894 ms | 1.7441 ms | 1.6314 ms | 29.48 |    1.01 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 197.188 ms | 2.1916 ms | 2.0500 ms | 29.53 |    1.11 |           - |           - |           - |                   - |
|                   |        |            |           |           |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **13.982 ms** | **0.2310 ms** | **0.2048 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  13.889 ms | 0.1689 ms | 0.1498 ms |  0.99 |    0.02 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 427.156 ms | 2.4870 ms | 2.3263 ms | 30.55 |    0.50 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 429.732 ms | 3.5586 ms | 3.3287 ms | 30.74 |    0.52 |           - |           - |           - |                   - |
