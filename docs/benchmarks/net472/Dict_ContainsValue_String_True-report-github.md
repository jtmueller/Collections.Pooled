``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                          Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------------- |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_True** |   **1000** |      **3.421 ms** |  **0.0093 ms** |  **0.0082 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |   1000 |      3.414 ms |  0.0091 ms |  0.0081 ms |  1.00 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_True** |  **10000** |    **337.730 ms** |  **1.3899 ms** |  **1.2321 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |  10000 |    336.783 ms |  0.9592 ms |  0.8503 ms |  1.00 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_True** | **100000** | **34,216.828 ms** | **39.8495 ms** | **33.2761 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True | 100000 | 34,218.350 ms | 27.3172 ms | 22.8111 ms |  1.00 |           - |           - |           - |                   - |
