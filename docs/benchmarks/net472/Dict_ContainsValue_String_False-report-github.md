``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                           Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------------- |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_False** |   **1000** |      **5.491 ms** |  **0.0473 ms** |  **0.0442 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_False |   1000 |      5.630 ms |  0.0629 ms |  0.0589 ms |  1.03 |           - |           - |           - |                   - |
|                                  |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_False** |  **10000** |    **543.382 ms** |  **4.2080 ms** |  **3.9362 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_False |  10000 |    544.265 ms |  6.4653 ms |  5.7313 ms |  1.00 |           - |           - |           - |                   - |
|                                  |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_False** | **100000** | **54,209.747 ms** | **92.8040 ms** | **86.8089 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_False | 100000 | 54,175.560 ms | 86.2791 ms | 80.7055 ms |  1.00 |           - |           - |           - |                   - |
