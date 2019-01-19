``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|            Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListSort_Int** | **100000** |   **7.185 ms** | **0.1436 ms** | **0.2399 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.733 ms | 0.1341 ms | 0.2126 ms |  0.94 |    0.04 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 199.117 ms | 3.6626 ms | 3.2468 ms | 27.91 |    0.81 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 214.588 ms | 1.1771 ms | 1.1011 ms | 30.06 |    1.08 |           - |           - |           - |                   - |
|                   |        |            |           |           |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **14.990 ms** | **0.1405 ms** | **0.1173 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  14.947 ms | 0.1568 ms | 0.1390 ms |  1.00 |    0.01 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 473.242 ms | 3.2159 ms | 2.6854 ms | 31.57 |    0.28 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 470.821 ms | 2.3894 ms | 2.2351 ms | 31.41 |    0.30 |           - |           - |           - |                   - |
