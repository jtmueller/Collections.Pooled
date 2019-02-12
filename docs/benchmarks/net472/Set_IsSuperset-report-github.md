``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                         Method | InitialSetSize |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |--------------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsSuperset_Hashset** |          **32000** |   **522.6 us** |  **6.024 us** |  **5.635 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsSuperset_PooledSet |          32000 |   530.4 us |  7.238 us |  6.771 us |  1.02 |    0.02 |           - |           - |           - |                48 B |
|        HashSet_IsSuperset_Enum |          32000 |   878.3 us |  6.974 us |  6.523 us |  1.68 |    0.03 |           - |           - |           - |                48 B |
|      PooledSet_IsSuperset_Enum |          32000 |   876.0 us |  7.725 us |  7.226 us |  1.68 |    0.02 |           - |           - |           - |                48 B |
|       HashSet_IsSuperset_Array |          32000 |   849.8 us |  7.855 us |  7.347 us |  1.63 |    0.03 |           - |           - |           - |                40 B |
|     PooledSet_IsSuperset_Array |          32000 |   657.9 us |  4.409 us |  4.124 us |  1.26 |    0.02 |           - |           - |           - |                   - |
|                                |                |            |           |           |       |         |             |             |             |                     |
|     **HashSet_IsSuperset_Hashset** |         **320000** |   **784.5 us** |  **5.196 us** |  **4.860 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsSuperset_PooledSet |         320000 |   783.7 us |  5.839 us |  5.462 us |  1.00 |    0.01 |           - |           - |           - |                48 B |
|        HashSet_IsSuperset_Enum |         320000 | 8,699.2 us | 64.732 us | 60.550 us | 11.09 |    0.13 |           - |           - |           - |                   - |
|      PooledSet_IsSuperset_Enum |         320000 | 8,915.8 us | 80.266 us | 75.081 us | 11.37 |    0.14 |           - |           - |           - |                   - |
|       HashSet_IsSuperset_Array |         320000 | 8,508.8 us | 65.288 us | 61.071 us | 10.85 |    0.08 |           - |           - |           - |                   - |
|     PooledSet_IsSuperset_Array |         320000 | 6,772.7 us | 57.040 us | 53.355 us |  8.63 |    0.09 |           - |           - |           - |                   - |
