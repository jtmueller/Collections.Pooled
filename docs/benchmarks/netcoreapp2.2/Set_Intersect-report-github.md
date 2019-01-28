``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                        Method | CountToIntersect | InitialSetSize |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |----------------- |--------------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Intersect_Hashset** |            **32000** |        **8000000** |  **6.195 ms** | **0.1475 ms** | **0.4350 ms** |  **6.207 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |            32000 |        8000000 |  6.047 ms | 0.1857 ms | 0.5387 ms |  6.006 ms |  0.98 |    0.10 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |            32000 |        8000000 |  4.952 ms | 0.1590 ms | 0.4664 ms |  4.941 ms |  0.80 |    0.09 |           - |           - |           - |             12608 B |
|      PooledSet_Intersect_Enum |            32000 |        8000000 |  4.285 ms | 0.1620 ms | 0.4701 ms |  4.238 ms |  0.69 |    0.09 |           - |           - |           - |             12568 B |
|       HashSet_Intersect_Array |            32000 |        8000000 |  4.762 ms | 0.1258 ms | 0.3669 ms |  4.725 ms |  0.77 |    0.07 |           - |           - |           - |             12600 B |
|     PooledSet_Intersect_Array |            32000 |        8000000 |  4.210 ms | 0.1920 ms | 0.5602 ms |  4.093 ms |  0.68 |    0.10 |           - |           - |           - |             12528 B |
|                               |                  |                |           |           |           |           |       |         |             |             |             |                     |
|     **HashSet_Intersect_Hashset** |           **320000** |        **8000000** |  **3.441 ms** | **0.2315 ms** | **0.6789 ms** |  **3.258 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |           320000 |        8000000 |  3.148 ms | 0.1580 ms | 0.4558 ms |  3.132 ms |  0.95 |    0.24 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |           320000 |        8000000 | 14.301 ms | 0.2832 ms | 0.3971 ms | 14.307 ms |  3.53 |    0.46 |           - |           - |           - |             12608 B |
|      PooledSet_Intersect_Enum |           320000 |        8000000 | 13.636 ms | 0.2657 ms | 0.3810 ms | 13.609 ms |  3.39 |    0.47 |           - |           - |           - |             12568 B |
|       HashSet_Intersect_Array |           320000 |        8000000 | 14.069 ms | 0.2813 ms | 0.6407 ms | 14.114 ms |  3.93 |    0.78 |           - |           - |           - |             12600 B |
|     PooledSet_Intersect_Array |           320000 |        8000000 | 11.817 ms | 0.2433 ms | 0.7059 ms | 11.598 ms |  3.54 |    0.61 |           - |           - |           - |             12528 B |
