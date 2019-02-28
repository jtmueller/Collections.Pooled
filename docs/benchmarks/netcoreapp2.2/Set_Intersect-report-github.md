``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                        Method | CountToIntersect | InitialSetSize |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |----------------- |--------------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Intersect_Hashset** |            **32000** |        **8000000** |  **5.426 ms** | **0.1079 ms** | **0.2229 ms** |  **5.462 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |            32000 |        8000000 |  5.417 ms | 0.1050 ms | 0.1078 ms |  5.368 ms |  0.96 |    0.02 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |            32000 |        8000000 |  3.954 ms | 0.0824 ms | 0.1527 ms |  3.951 ms |  0.73 |    0.03 |           - |           - |           - |             12608 B |
|      PooledSet_Intersect_Enum |            32000 |        8000000 |  3.464 ms | 0.0646 ms | 0.0572 ms |  3.457 ms |  0.61 |    0.02 |           - |           - |           - |             12568 B |
|       HashSet_Intersect_Array |            32000 |        8000000 |  3.985 ms | 0.0186 ms | 0.0145 ms |  3.986 ms |  0.71 |    0.02 |           - |           - |           - |             12600 B |
|     PooledSet_Intersect_Array |            32000 |        8000000 |  3.400 ms | 0.0849 ms | 0.0908 ms |  3.364 ms |  0.60 |    0.02 |           - |           - |           - |             12528 B |
|                               |                  |                |           |           |           |           |       |         |             |             |             |                     |
|     **HashSet_Intersect_Hashset** |           **320000** |        **8000000** |  **2.624 ms** | **0.0836 ms** | **0.2452 ms** |  **2.468 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |           320000 |        8000000 |  2.877 ms | 0.0545 ms | 0.0560 ms |  2.840 ms |  0.95 |    0.03 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |           320000 |        8000000 | 12.453 ms | 0.1073 ms | 0.1004 ms | 12.462 ms |  4.11 |    0.09 |           - |           - |           - |             12608 B |
|      PooledSet_Intersect_Enum |           320000 |        8000000 | 11.445 ms | 0.2185 ms | 0.2145 ms | 11.417 ms |  3.77 |    0.14 |           - |           - |           - |             12568 B |
|       HashSet_Intersect_Array |           320000 |        8000000 | 12.262 ms | 0.2726 ms | 0.2550 ms | 12.182 ms |  4.05 |    0.15 |           - |           - |           - |             12600 B |
|     PooledSet_Intersect_Array |           320000 |        8000000 | 10.609 ms | 0.2200 ms | 0.2161 ms | 10.614 ms |  3.49 |    0.12 |           - |           - |           - |             12528 B |
