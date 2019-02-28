``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                  Method | CountToCheck | InitialSetSize |          Mean |         Error |         StdDev |        Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |------------- |--------------- |--------------:|--------------:|---------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Contains_True** |            **1** |        **8000000** |      **12.82 ns** |     **0.2905 ns** |      **0.6127 ns** |      **12.52 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |            1 |        8000000 |      12.76 ns |     0.4960 ns |      0.4142 ns |      12.67 ns |  0.96 |    0.06 |           - |           - |           - |                   - |
|                         |              |                |               |               |                |               |       |         |             |             |             |                     |
|   **HashSet_Contains_True** |          **100** |        **8000000** |   **1,951.45 ns** |    **42.6095 ns** |    **113.7333 ns** |   **1,896.08 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |          100 |        8000000 |   1,780.69 ns |    35.6900 ns |     99.4896 ns |   1,726.07 ns |  0.92 |    0.08 |           - |           - |           - |                   - |
|                         |              |                |               |               |                |               |       |         |             |             |             |                     |
|   **HashSet_Contains_True** |        **10000** |        **8000000** | **261,834.03 ns** | **5,506.4182 ns** | **15,440.5871 ns** | **255,417.15 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |        10000 |        8000000 | 262,574.37 ns | 5,032.5683 ns |  6,180.4459 ns | 263,861.43 ns |  0.98 |    0.07 |           - |           - |           - |                   - |
