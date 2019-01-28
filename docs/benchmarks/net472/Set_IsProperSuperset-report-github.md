``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                               Method | InitialSetSize |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------- |--------------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsProperSuperset_Hashset** |          **32000** |   **547.0 us** |  **2.814 us** |  **2.495 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsProperSuperset_PooledSet |          32000 |   545.0 us |  3.654 us |  3.239 us |  1.00 |    0.01 |           - |           - |           - |                48 B |
|        HashSet_IsProperSuperset_Enum |          32000 |   910.0 us |  4.572 us |  4.053 us |  1.66 |    0.01 |           - |           - |           - |                48 B |
|      PooledSet_IsProperSuperset_Enum |          32000 |   949.0 us | 41.079 us | 48.901 us |  1.75 |    0.11 |           - |           - |           - |                48 B |
|       HashSet_IsProperSuperset_Array |          32000 |   929.7 us |  7.578 us |  7.089 us |  1.70 |    0.01 |           - |           - |           - |                40 B |
|     PooledSet_IsProperSuperset_Array |          32000 |   677.5 us |  7.409 us |  6.186 us |  1.24 |    0.01 |           - |           - |           - |                   - |
|                                      |                |            |           |           |       |         |             |             |             |                     |
|     **HashSet_IsProperSuperset_Hashset** |         **320000** |   **819.7 us** |  **5.745 us** |  **5.093 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsProperSuperset_PooledSet |         320000 |   826.7 us |  4.304 us |  3.815 us |  1.01 |    0.01 |           - |           - |           - |                48 B |
|        HashSet_IsProperSuperset_Enum |         320000 | 9,085.0 us | 69.065 us | 64.604 us | 11.08 |    0.11 |           - |           - |           - |                   - |
|      PooledSet_IsProperSuperset_Enum |         320000 | 9,088.0 us | 41.141 us | 36.470 us | 11.09 |    0.09 |           - |           - |           - |                   - |
|       HashSet_IsProperSuperset_Array |         320000 | 8,905.1 us | 61.202 us | 51.106 us | 10.86 |    0.08 |           - |           - |           - |                   - |
|     PooledSet_IsProperSuperset_Array |         320000 | 7,007.1 us | 28.900 us | 22.563 us |  8.54 |    0.06 |           - |           - |           - |                   - |
