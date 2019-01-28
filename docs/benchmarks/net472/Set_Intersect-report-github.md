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
|     **HashSet_Intersect_Hashset** |            **32000** |        **8000000** |  **5.332 ms** | **0.1052 ms** | **0.2619 ms** |  **5.327 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |            32000 |        8000000 |  5.124 ms | 0.1018 ms | 0.2646 ms |  5.124 ms |  0.96 |    0.07 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |            32000 |        8000000 |  4.138 ms | 0.1090 ms | 0.3196 ms |  4.097 ms |  0.78 |    0.07 |           - |           - |           - |             20744 B |
|      PooledSet_Intersect_Enum |            32000 |        8000000 |  3.389 ms | 0.0359 ms | 0.0280 ms |  3.386 ms |  0.63 |    0.03 |           - |           - |           - |             20744 B |
|       HashSet_Intersect_Array |            32000 |        8000000 |  4.052 ms | 0.1186 ms | 0.3404 ms |  3.902 ms |  0.76 |    0.08 |           - |           - |           - |             20744 B |
|     PooledSet_Intersect_Array |            32000 |        8000000 |  3.459 ms | 0.0695 ms | 0.2017 ms |  3.416 ms |  0.65 |    0.04 |           - |           - |           - |             12552 B |
|                               |                  |                |           |           |           |           |       |         |             |             |             |                     |
|     **HashSet_Intersect_Hashset** |           **320000** |        **8000000** |  **2.587 ms** | **0.0633 ms** | **0.1795 ms** |  **2.512 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Intersect_PooledSet |           320000 |        8000000 |  2.361 ms | 0.0699 ms | 0.1960 ms |  2.253 ms |  0.92 |    0.10 |           - |           - |           - |                   - |
|        HashSet_Intersect_Enum |           320000 |        8000000 | 12.349 ms | 0.3054 ms | 0.2708 ms | 12.238 ms |  5.01 |    0.33 |           - |           - |           - |             20744 B |
|      PooledSet_Intersect_Enum |           320000 |        8000000 | 12.192 ms | 0.2316 ms | 0.2053 ms | 12.202 ms |  4.95 |    0.31 |           - |           - |           - |             20744 B |
|       HashSet_Intersect_Array |           320000 |        8000000 | 12.400 ms | 0.2416 ms | 0.2782 ms | 12.322 ms |  4.96 |    0.39 |           - |           - |           - |             20744 B |
|     PooledSet_Intersect_Array |           320000 |        8000000 | 11.091 ms | 0.2190 ms | 0.4943 ms | 10.957 ms |  4.31 |    0.37 |           - |           - |           - |             12552 B |
