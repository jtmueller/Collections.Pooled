``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                           Method |      N |          Mean |       Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------------- |------- |--------------:|------------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_False** |   **1000** |      **5.484 ms** |   **0.0567 ms** |  **0.0531 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_False |   1000 |      5.472 ms |   0.0625 ms |  0.0554 ms |  1.00 |    0.02 |           - |           - |           - |                   - |
|                                  |        |               |             |            |       |         |             |             |             |                     |
|   **DictContainsValue_String_False** |  **10000** |    **570.583 ms** |   **2.7689 ms** |  **2.5900 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_False |  10000 |    545.853 ms |   8.5505 ms |  7.9981 ms |  0.96 |    0.01 |           - |           - |           - |                   - |
|                                  |        |               |             |            |       |         |             |             |             |                     |
|   **DictContainsValue_String_False** | **100000** | **54,320.479 ms** | **108.5546 ms** | **96.2309 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_False | 100000 | 54,325.972 ms |  46.6305 ms | 38.9386 ms |  1.00 |    0.00 |           - |           - |           - |                   - |
