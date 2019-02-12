``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                    Method | CountToUnion | InitialSetSize |        Mean |     Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------------- |--------------- |------------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Union_Hashset** |        **32000** |         **320000** |  **1,177.2 us** |  **41.98 us** | **119.099 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |        32000 |         320000 |    946.0 us |  18.22 us |  20.982 us |  0.80 |    0.07 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |        32000 |         320000 |  1,292.3 us |  35.25 us | 100.562 us |  1.11 |    0.14 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |        32000 |         320000 |  1,100.5 us |  77.05 us |  68.303 us |  0.92 |    0.10 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |        32000 |         320000 |  1,222.9 us |  32.11 us |  89.505 us |  1.05 |    0.12 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |        32000 |         320000 |    932.2 us |  10.26 us |   8.569 us |  0.78 |    0.08 |           - |           - |           - |                   - |
|                           |              |                |             |           |            |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |        **32000** |        **8000000** |  **1,365.2 us** |  **26.91 us** |  **35.920 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |        32000 |        8000000 |    978.5 us |  19.06 us |  27.340 us |  0.72 |    0.02 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |        32000 |        8000000 |  1,419.8 us |  24.17 us |  18.872 us |  1.05 |    0.02 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |        32000 |        8000000 |  1,074.4 us |  11.65 us |   9.726 us |  0.80 |    0.02 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |        32000 |        8000000 |  1,421.6 us |  18.66 us |  15.583 us |  1.05 |    0.02 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |        32000 |        8000000 |    835.5 us |  21.60 us |  42.136 us |  0.63 |    0.04 |           - |           - |           - |                   - |
|                           |              |                |             |           |            |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |       **320000** |         **320000** |  **3,497.8 us** |  **66.80 us** |  **71.479 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |       320000 |         320000 |  3,177.3 us |  75.90 us | 113.601 us |  0.91 |    0.03 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |       320000 |         320000 | 11,014.5 us | 183.31 us | 162.499 us |  3.15 |    0.09 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |       320000 |         320000 | 10,285.6 us | 159.64 us | 149.327 us |  2.94 |    0.06 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |       320000 |         320000 | 10,556.3 us | 164.10 us | 137.031 us |  3.02 |    0.09 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |       320000 |         320000 |  9,302.1 us | 180.98 us | 160.437 us |  2.66 |    0.07 |           - |           - |           - |                   - |
|                           |              |                |             |           |            |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |       **320000** |        **8000000** |  **3,688.0 us** | **187.93 us** | **184.568 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |       320000 |        8000000 |  3,342.8 us |  66.29 us | 146.904 us |  0.90 |    0.06 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |       320000 |        8000000 | 10,772.0 us | 114.60 us | 101.593 us |  2.92 |    0.14 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |       320000 |        8000000 | 10,386.7 us | 199.21 us | 195.653 us |  2.82 |    0.14 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |       320000 |        8000000 | 10,806.1 us | 212.17 us | 283.242 us |  2.96 |    0.14 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |       320000 |        8000000 |  7,774.2 us | 152.59 us | 181.645 us |  2.11 |    0.11 |           - |           - |           - |                   - |
