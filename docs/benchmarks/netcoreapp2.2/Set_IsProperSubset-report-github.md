``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                             Method | InitialSetSize |              Mean |             Error |            StdDev |         Ratio |    RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------- |--------------- |------------------:|------------------:|------------------:|--------------:|-----------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsProperSubset_Hashset** |         **320000** |          **12.57 ns** |         **0.2221 ns** |         **0.1855 ns** |          **1.00** |       **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsProperSubset_PooledSet |         320000 |          10.39 ns |         0.0885 ns |         0.0828 ns |          0.83 |       0.01 |           - |           - |           - |                   - |
|        HashSet_IsProperSubset_Enum |         320000 |  11,849,240.78 ns |    71,552.6795 ns |    66,930.4208 ns |    941,480.91 |  16,325.06 |           - |           - |           - |             12096 B |
|      PooledSet_IsProperSubset_Enum |         320000 |  11,367,242.79 ns |    69,088.0691 ns |    57,691.6667 ns |    904,167.56 |  15,583.98 |           - |           - |           - |             12056 B |
|       HashSet_IsProperSubset_Array |         320000 |  11,473,259.53 ns |   177,272.1757 ns |   165,820.5031 ns |    913,973.76 |  14,862.29 |           - |           - |           - |             12088 B |
|     PooledSet_IsProperSubset_Array |         320000 |   9,421,017.29 ns |   106,219.4840 ns |    99,357.7713 ns |    750,642.50 |  10,442.79 |           - |           - |           - |             12016 B |
|                                    |                |                   |                   |                   |               |            |             |             |             |                     |
|     **HashSet_IsProperSubset_Hashset** |        **8000000** |          **11.87 ns** |         **0.1507 ns** |         **0.1410 ns** |          **1.00** |       **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsProperSubset_PooledSet |        8000000 |          12.03 ns |         0.0719 ns |         0.0673 ns |          1.01 |       0.01 |           - |           - |           - |                   - |
|        HashSet_IsProperSubset_Enum |        8000000 | 341,399,666.67 ns | 3,748,762.2356 ns | 3,506,594.5211 ns | 28,769,876.70 | 433,572.14 |           - |           - |           - |             12608 B |
|      PooledSet_IsProperSubset_Enum |        8000000 | 327,170,392.31 ns | 1,373,228.6802 ns | 1,146,708.1418 ns | 27,618,313.84 | 343,950.96 |           - |           - |           - |             12568 B |
|       HashSet_IsProperSubset_Array |        8000000 | 339,132,233.33 ns | 3,376,787.6755 ns | 3,158,649.2867 ns | 28,579,149.21 | 437,452.84 |           - |           - |           - |             12600 B |
|     PooledSet_IsProperSubset_Array |        8000000 | 300,462,191.67 ns | 3,193,699.6432 ns | 2,493,430.6088 ns | 25,411,899.67 | 199,564.02 |           - |           - |           - |             12528 B |
