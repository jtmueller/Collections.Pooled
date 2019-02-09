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
|     **HashSet_Intersect_Hashset** |            **32000** |        **8000000** |  **5.894 ms** | **0.1578 ms** | **0.4553 ms** |  **5.771 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |            32000 |        8000000 |  5.249 ms | 0.1200 ms | 0.1429 ms |  5.234 ms |  0.89 |    0.07 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |            32000 |        8000000 |  4.418 ms | 0.1154 ms | 0.3293 ms |  4.314 ms |  0.75 |    0.08 |           - |           - |           - |             20744 B |
|      PooledSet_Intersect_Enum |            32000 |        8000000 |  3.591 ms | 0.0759 ms | 0.2065 ms |  3.550 ms |  0.61 |    0.05 |           - |           - |           - |             20744 B |
|       HashSet_Intersect_Array |            32000 |        8000000 |  4.093 ms | 0.0964 ms | 0.2798 ms |  3.988 ms |  0.70 |    0.07 |           - |           - |           - |             20744 B |
|     PooledSet_Intersect_Array |            32000 |        8000000 |  3.567 ms | 0.0734 ms | 0.2095 ms |  3.513 ms |  0.61 |    0.05 |           - |           - |           - |             12552 B |
|                               |                  |                |           |           |           |           |       |         |             |             |             |                     |
|     **HashSet_Intersect_Hashset** |           **320000** |        **8000000** |  **2.548 ms** | **0.0472 ms** | **0.0864 ms** |  **2.523 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |           320000 |        8000000 |  2.309 ms | 0.0452 ms | 0.0815 ms |  2.294 ms |  0.91 |    0.04 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |           320000 |        8000000 | 13.243 ms | 0.2646 ms | 0.4904 ms | 13.156 ms |  5.20 |    0.24 |           - |           - |           - |             20744 B |
|      PooledSet_Intersect_Enum |           320000 |        8000000 | 12.120 ms | 0.2386 ms | 0.4423 ms | 12.046 ms |  4.76 |    0.25 |           - |           - |           - |             20744 B |
|       HashSet_Intersect_Array |           320000 |        8000000 | 12.790 ms | 0.2546 ms | 0.3484 ms | 12.725 ms |  4.99 |    0.24 |           - |           - |           - |             20744 B |
|     PooledSet_Intersect_Array |           320000 |        8000000 | 11.680 ms | 0.3627 ms | 0.9868 ms | 11.534 ms |  4.64 |    0.55 |           - |           - |           - |             12552 B |
