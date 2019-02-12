``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method | InitialSetSize |               Mean |             Error |            StdDev |        Ratio |   RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |--------------- |-------------------:|------------------:|------------------:|-------------:|----------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsSubset_Hashset** |         **320000** |           **6.054 ns** |         **0.0339 ns** |         **0.0300 ns** |         **1.00** |      **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsSubset_PooledSet |         320000 |           6.147 ns |         0.0339 ns |         0.0300 ns |         1.02 |      0.01 |           - |           - |           - |                   - |
|        HashSet_IsSubset_Enum |         320000 |  10,290,325.962 ns |    38,990.3164 ns |    32,558.6801 ns | 1,699,850.60 |  8,855.53 |           - |           - |           - |             12096 B |
|      PooledSet_IsSubset_Enum |         320000 |   9,914,816.797 ns |    35,411.2449 ns |    33,123.7004 ns | 1,636,920.41 |  8,336.35 |           - |           - |           - |             12056 B |
|       HashSet_IsSubset_Array |         320000 |  10,038,873.854 ns |    43,199.0049 ns |    40,408.3760 ns | 1,658,428.58 | 11,039.79 |           - |           - |           - |             12088 B |
|     PooledSet_IsSubset_Array |         320000 |   8,124,141.994 ns |    32,501.5961 ns |    28,811.8184 ns | 1,342,027.40 |  6,334.15 |           - |           - |           - |             12016 B |
|                              |                |                    |                   |                   |              |           |             |             |             |                     |
|     **HashSet_IsSubset_Hashset** |        **8000000** |   **3,001,006.897 ns** |    **12,604.5000 ns** |    **11,790.2571 ns** |         **1.00** |      **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsSubset_PooledSet |        8000000 |   2,984,392.604 ns |    11,779.3125 ns |    11,018.3762 ns |         0.99 |      0.01 |           - |           - |           - |                   - |
|        HashSet_IsSubset_Enum |        8000000 | 303,377,600.000 ns | 1,608,010.3379 ns | 1,342,761.4593 ns |       101.12 |      0.60 |           - |           - |           - |             12608 B |
|      PooledSet_IsSubset_Enum |        8000000 | 291,363,076.923 ns | 1,037,748.3003 ns |   866,566.8306 ns |        97.11 |      0.52 |           - |           - |           - |             12568 B |
|       HashSet_IsSubset_Array |        8000000 | 301,595,300.000 ns | 1,967,541.3984 ns | 1,840,439.4448 ns |       100.50 |      0.77 |           - |           - |           - |             12600 B |
|     PooledSet_IsSubset_Array |        8000000 | 260,690,238.462 ns |   887,733.4421 ns |   741,297.6298 ns |        86.89 |      0.38 |           - |           - |           - |             12528 B |
