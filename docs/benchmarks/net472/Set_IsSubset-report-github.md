``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                       Method | InitialSetSize |               Mean |             Error |            StdDev |        Ratio |   RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |--------------- |-------------------:|------------------:|------------------:|-------------:|----------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsSubset_Hashset** |         **320000** |           **7.039 ns** |         **0.0706 ns** |         **0.0660 ns** |         **1.00** |      **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsSubset_PooledSet |         320000 |           5.888 ns |         0.0501 ns |         0.0418 ns |         0.84 |      0.01 |           - |           - |           - |                   - |
|        HashSet_IsSubset_Enum |         320000 |  10,670,207.135 ns |    51,410.5828 ns |    48,089.4910 ns | 1,515,979.18 | 15,871.69 |           - |           - |           - |             12168 B |
|      PooledSet_IsSubset_Enum |         320000 |  10,361,818.255 ns |    78,305.1215 ns |    73,246.6592 ns | 1,472,124.19 | 13,136.05 |           - |           - |           - |             12168 B |
|       HashSet_IsSubset_Array |         320000 |  10,564,750.346 ns |    67,506.5232 ns |    59,842.7745 ns | 1,501,576.36 | 17,648.90 |           - |           - |           - |             12168 B |
|     PooledSet_IsSubset_Array |         320000 |   9,039,191.261 ns |    70,062.4726 ns |    62,108.5570 ns | 1,284,763.72 | 17,262.28 |           - |           - |           - |             12040 B |
|                              |                |                    |                   |                   |              |           |             |             |             |                     |
|     **HashSet_IsSubset_Hashset** |        **8000000** |   **3,298,025.728 ns** |    **64,645.8257 ns** |    **92,713.1114 ns** |         **1.00** |      **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsSubset_PooledSet |        8000000 |   3,138,741.797 ns |    22,985.5228 ns |    17,945.5843 ns |         0.94 |      0.03 |           - |           - |           - |                   - |
|        HashSet_IsSubset_Enum |        8000000 | 303,695,973.333 ns | 2,413,549.0571 ns | 2,257,635.2855 ns |        91.02 |      2.76 |           - |           - |           - |             16648 B |
|      PooledSet_IsSubset_Enum |        8000000 | 304,727,923.077 ns | 5,886,620.0042 ns | 4,915,594.3099 ns |        90.88 |      2.96 |           - |           - |           - |             16648 B |
|       HashSet_IsSubset_Array |        8000000 | 306,452,886.667 ns | 2,902,395.3535 ns | 2,714,902.4144 ns |        91.86 |      3.19 |           - |           - |           - |             20744 B |
|     PooledSet_IsSubset_Array |        8000000 | 260,394,008.333 ns | 2,925,640.1831 ns | 2,284,147.4145 ns |        77.58 |      2.45 |           - |           - |           - |             12552 B |
