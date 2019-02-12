``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                   Method | CountToCheck | InitialSetSize |          Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------- |------------- |--------------- |--------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Contains_False** |            **1** |        **8000000** |      **8.516 ns** |   **0.0279 ns** |   **0.0261 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |            1 |        8000000 |      9.669 ns |   0.0652 ns |   0.0610 ns |  1.14 |           - |           - |           - |                   - |
|                          |              |                |               |             |             |       |             |             |             |                     |
|   **HashSet_Contains_False** |          **100** |        **8000000** |    **750.307 ns** |   **1.9856 ns** |   **1.8574 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |          100 |        8000000 |    769.488 ns |   3.2694 ns |   3.0582 ns |  1.03 |           - |           - |           - |                   - |
|                          |              |                |               |             |             |       |             |             |             |                     |
|   **HashSet_Contains_False** |        **10000** |        **8000000** | **74,120.077 ns** | **296.8082 ns** | **277.6346 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |        10000 |        8000000 | 75,974.495 ns | 259.4335 ns | 242.6743 ns |  1.03 |           - |           - |           - |                   - |
