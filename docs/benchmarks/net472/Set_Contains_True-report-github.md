``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                  Method | CountToCheck | InitialSetSize |          Mean |         Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |------------- |--------------- |--------------:|--------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Contains_True** |            **1** |        **8000000** |      **12.57 ns** |     **0.1223 ns** |     **0.1022 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |            1 |        8000000 |      13.05 ns |     0.1718 ns |     0.1523 ns |  1.04 |    0.01 |           - |           - |           - |                   - |
|                         |              |                |               |               |               |       |         |             |             |             |                     |
|   **HashSet_Contains_True** |          **100** |        **8000000** |   **1,692.55 ns** |    **10.6270 ns** |     **9.4205 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |          100 |        8000000 |   1,756.92 ns |    13.8140 ns |    12.9216 ns |  1.04 |    0.01 |           - |           - |           - |                   - |
|                         |              |                |               |               |               |       |         |             |             |             |                     |
|   **HashSet_Contains_True** |        **10000** |        **8000000** | **257,695.25 ns** | **5,852.6729 ns** | **5,188.2421 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |        10000 |        8000000 | 265,679.22 ns | 2,948.6382 ns | 2,758.1580 ns |  1.03 |    0.02 |           - |           - |           - |                   - |
