``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                   Method | CountToCheck | InitialSetSize |          Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------- |------------- |--------------- |--------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Contains_False** |            **1** |        **8000000** |      **8.879 ns** |   **0.1407 ns** |   **0.1098 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |            1 |        8000000 |      8.647 ns |   0.0540 ns |   0.0479 ns |  0.97 |    0.02 |           - |           - |           - |                   - |
|                          |              |                |               |             |             |       |         |             |             |             |                     |
|   **HashSet_Contains_False** |          **100** |        **8000000** |    **796.865 ns** |  **22.2733 ns** |  **25.6499 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |          100 |        8000000 |    803.101 ns |   8.4008 ns |   7.8581 ns |  1.00 |    0.04 |           - |           - |           - |                   - |
|                          |              |                |               |             |             |       |         |             |             |             |                     |
|   **HashSet_Contains_False** |        **10000** |        **8000000** | **77,508.303 ns** | **785.3837 ns** | **734.6484 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |        10000 |        8000000 | 79,485.993 ns | 634.1623 ns | 593.1958 ns |  1.03 |    0.01 |           - |           - |           - |                   - |
