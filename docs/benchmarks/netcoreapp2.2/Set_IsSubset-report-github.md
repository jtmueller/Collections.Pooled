``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method | InitialSetSize |               Mean |             Error |            StdDev |        Ratio |   RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |--------------- |-------------------:|------------------:|------------------:|-------------:|----------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsSubset_Hashset** |         **320000** |           **7.100 ns** |         **0.1830 ns** |         **0.3346 ns** |         **1.00** |      **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsSubset_PooledSet |         320000 |           6.795 ns |         0.3029 ns |         0.3831 ns |         0.93 |      0.06 |           - |           - |           - |                   - |
|        HashSet_IsSubset_Enum |         320000 |  11,692,194.375 ns |   161,909.9330 ns |   151,450.6518 ns | 1,567,777.41 | 66,341.71 |           - |           - |           - |             12096 B |
|      PooledSet_IsSubset_Enum |         320000 |  10,932,550.599 ns |    79,854.7207 ns |    74,696.1553 ns | 1,465,655.52 | 52,927.27 |           - |           - |           - |             12056 B |
|       HashSet_IsSubset_Array |         320000 |  11,635,130.208 ns |    62,916.3900 ns |    58,852.0303 ns | 1,559,803.27 | 54,557.75 |           - |           - |           - |             12088 B |
|     PooledSet_IsSubset_Array |         320000 |   9,336,271.823 ns |    81,778.2453 ns |    76,495.4214 ns | 1,251,647.07 | 45,271.41 |           - |           - |           - |             12016 B |
|                              |                |                    |                   |                   |              |           |             |             |             |                     |
|     **HashSet_IsSubset_Hashset** |        **8000000** |   **3,501,736.270 ns** |    **16,825.0976 ns** |    **15,738.2067 ns** |         **1.00** |      **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsSubset_PooledSet |        8000000 |   3,425,180.354 ns |    24,919.4372 ns |    22,090.4320 ns |         0.98 |      0.01 |           - |           - |           - |                   - |
|        HashSet_IsSubset_Enum |        8000000 | 330,686,680.000 ns | 2,970,560.4433 ns | 2,778,664.0817 ns |        94.44 |      0.88 |           - |           - |           - |             12608 B |
|      PooledSet_IsSubset_Enum |        8000000 | 327,314,513.333 ns | 2,155,067.5621 ns | 2,015,851.5347 ns |        93.47 |      0.75 |           - |           - |           - |             12568 B |
|       HashSet_IsSubset_Array |        8000000 | 341,220,260.000 ns | 2,860,325.2190 ns | 2,675,549.9845 ns |        97.45 |      0.97 |           - |           - |           - |             12600 B |
|     PooledSet_IsSubset_Array |        8000000 | 302,103,553.333 ns | 2,434,749.6991 ns | 2,277,466.3792 ns |        86.27 |      0.72 |           - |           - |           - |             12528 B |
