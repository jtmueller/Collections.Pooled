``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                          Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------------- |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_True** |   **1000** |      **3.412 ms** |  **0.0090 ms** |  **0.0084 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |   1000 |      3.430 ms |  0.0303 ms |  0.0284 ms |  1.01 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_True** |  **10000** |    **339.919 ms** |  **2.1650 ms** |  **1.9192 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |  10000 |    337.529 ms |  0.8546 ms |  0.7576 ms |  0.99 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_True** | **100000** | **34,156.346 ms** | **47.7508 ms** | **39.8740 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True | 100000 | 34,210.739 ms | 52.2546 ms | 43.6350 ms |  1.00 |           - |           - |           - |                   - |
