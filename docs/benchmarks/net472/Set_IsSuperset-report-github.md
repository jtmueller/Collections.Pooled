``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                         Method | InitialSetSize |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |--------------- |-----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsSuperset_Hashset** |          **32000** |   **590.0 us** |  **19.024 us** |  **16.865 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsSuperset_PooledSet |          32000 |   541.1 us |   6.196 us |   5.492 us |  0.92 |    0.03 |           - |           - |           - |                48 B |
|        HashSet_IsSuperset_Enum |          32000 |   913.3 us |  17.904 us |  16.747 us |  1.55 |    0.06 |           - |           - |           - |                48 B |
|      PooledSet_IsSuperset_Enum |          32000 |   917.1 us |   5.653 us |   4.414 us |  1.55 |    0.04 |           - |           - |           - |                48 B |
|       HashSet_IsSuperset_Array |          32000 |   900.1 us |  18.377 us |  19.664 us |  1.53 |    0.04 |           - |           - |           - |                40 B |
|     PooledSet_IsSuperset_Array |          32000 |   676.5 us |   2.923 us |   2.591 us |  1.15 |    0.03 |           - |           - |           - |                   - |
|                                |                |            |            |            |       |         |             |             |             |                     |
|     **HashSet_IsSuperset_Hashset** |         **320000** |   **811.8 us** |   **3.691 us** |   **3.272 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsSuperset_PooledSet |         320000 |   822.9 us |   4.328 us |   4.049 us |  1.01 |    0.01 |           - |           - |           - |                48 B |
|        HashSet_IsSuperset_Enum |         320000 | 9,064.1 us | 116.753 us | 109.211 us | 11.17 |    0.12 |           - |           - |           - |                   - |
|      PooledSet_IsSuperset_Enum |         320000 | 9,041.8 us |  37.729 us |  33.446 us | 11.14 |    0.07 |           - |           - |           - |                   - |
|       HashSet_IsSuperset_Array |         320000 | 8,785.6 us |  33.639 us |  29.820 us | 10.82 |    0.06 |           - |           - |           - |                   - |
|     PooledSet_IsSuperset_Array |         320000 | 6,959.1 us |  25.755 us |  24.091 us |  8.57 |    0.05 |           - |           - |           - |                   - |
