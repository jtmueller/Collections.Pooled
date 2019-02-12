``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                             Method | InitialSetSize |               Mean |             Error |            StdDev |         Ratio |    RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------- |--------------- |-------------------:|------------------:|------------------:|--------------:|-----------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsProperSubset_Hashset** |         **320000** |           **9.953 ns** |         **0.1133 ns** |         **0.1059 ns** |          **1.00** |       **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsProperSubset_PooledSet |         320000 |          10.246 ns |         0.1288 ns |         0.1205 ns |          1.03 |       0.01 |           - |           - |           - |                   - |
|        HashSet_IsProperSubset_Enum |         320000 |  10,168,064.349 ns |   103,129.7714 ns |    96,467.6522 ns |  1,021,663.58 |  13,820.91 |           - |           - |           - |             12168 B |
|      PooledSet_IsProperSubset_Enum |         320000 |   9,877,788.333 ns |    84,864.2333 ns |    79,382.0564 ns |    992,512.25 |  13,717.36 |           - |           - |           - |             12168 B |
|       HashSet_IsProperSubset_Array |         320000 |  10,104,045.208 ns |   110,412.4106 ns |   103,279.8374 ns |  1,015,255.68 |  16,008.07 |           - |           - |           - |             12168 B |
|     PooledSet_IsProperSubset_Array |         320000 |   8,749,514.740 ns |    99,185.5296 ns |    92,778.2059 ns |    879,156.73 |  14,384.29 |           - |           - |           - |             12040 B |
|                                    |                |                    |                   |                   |               |            |             |             |             |                     |
|     **HashSet_IsProperSubset_Hashset** |        **8000000** |           **9.610 ns** |         **0.2052 ns** |         **0.1602 ns** |          **1.00** |       **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsProperSubset_PooledSet |        8000000 |          11.061 ns |         0.1538 ns |         0.1364 ns |          1.15 |       0.03 |           - |           - |           - |                   - |
|        HashSet_IsProperSubset_Enum |        8000000 | 294,024,453.333 ns | 3,233,406.5151 ns | 3,024,530.4603 ns | 30,596,123.92 | 664,906.10 |           - |           - |           - |             16648 B |
|      PooledSet_IsProperSubset_Enum |        8000000 | 294,432,843.333 ns | 4,151,879.8800 ns | 3,883,671.0157 ns | 30,626,775.82 | 752,399.15 |           - |           - |           - |             16648 B |
|       HashSet_IsProperSubset_Array |        8000000 | 293,804,610.000 ns | 3,516,772.4146 ns | 3,289,591.0986 ns | 30,587,324.84 | 461,499.90 |           - |           - |           - |             16648 B |
|     PooledSet_IsProperSubset_Array |        8000000 | 250,493,071.429 ns | 2,638,117.8360 ns | 2,338,622.7466 ns | 26,097,870.73 | 444,125.51 |           - |           - |           - |             12552 B |
