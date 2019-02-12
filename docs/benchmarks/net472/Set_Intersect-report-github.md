``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|                        Method | CountToIntersect | InitialSetSize |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |----------------- |--------------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Intersect_Hashset** |            **32000** |        **8000000** |  **5.182 ms** | **0.1032 ms** | **0.1447 ms** |  **5.133 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |            32000 |        8000000 |  4.887 ms | 0.0559 ms | 0.0495 ms |  4.891 ms |  0.94 |    0.03 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |            32000 |        8000000 |  3.800 ms | 0.0557 ms | 0.0465 ms |  3.794 ms |  0.74 |    0.03 |           - |           - |           - |             20744 B |
|      PooledSet_Intersect_Enum |            32000 |        8000000 |  3.327 ms | 0.0279 ms | 0.0218 ms |  3.319 ms |  0.64 |    0.02 |           - |           - |           - |             20744 B |
|       HashSet_Intersect_Array |            32000 |        8000000 |  3.802 ms | 0.0748 ms | 0.0800 ms |  3.759 ms |  0.74 |    0.03 |           - |           - |           - |             20744 B |
|     PooledSet_Intersect_Array |            32000 |        8000000 |  3.285 ms | 0.0640 ms | 0.0997 ms |  3.257 ms |  0.64 |    0.03 |           - |           - |           - |             12552 B |
|                               |                  |                |           |           |           |           |       |         |             |             |             |                     |
|     **HashSet_Intersect_Hashset** |           **320000** |        **8000000** |  **2.562 ms** | **0.1045 ms** | **0.2948 ms** |  **2.472 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |           320000 |        8000000 |  2.374 ms | 0.0461 ms | 0.0661 ms |  2.349 ms |  0.93 |    0.09 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |           320000 |        8000000 | 12.136 ms | 0.2419 ms | 0.2484 ms | 12.118 ms |  4.69 |    0.54 |           - |           - |           - |             20744 B |
|      PooledSet_Intersect_Enum |           320000 |        8000000 | 11.397 ms | 0.2102 ms | 0.1966 ms | 11.328 ms |  4.48 |    0.45 |           - |           - |           - |             20744 B |
|       HashSet_Intersect_Array |           320000 |        8000000 | 12.049 ms | 0.2371 ms | 0.3549 ms | 11.982 ms |  4.76 |    0.50 |           - |           - |           - |             20744 B |
|     PooledSet_Intersect_Array |           320000 |        8000000 | 10.004 ms | 0.1544 ms | 0.1369 ms | 10.025 ms |  3.93 |    0.38 |           - |           - |           - |             12552 B |
