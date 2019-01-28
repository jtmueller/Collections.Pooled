``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|                              Method | CountToIntersect | InitialSetSize |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |----------------- |--------------- |------------:|----------:|----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_SymmetricExcept_Hashset** |            **32000** |        **8000000** |  **1,254.5 us** |  **24.66 us** |  **30.29 us** |  **1,262.4 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |            32000 |        8000000 |    837.6 us |  19.53 us |  44.87 us |    809.7 us |  0.68 |    0.05 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |            32000 |        8000000 |  2,894.7 us |  54.50 us |  42.55 us |  2,872.0 us |  2.33 |    0.08 |           - |           - |           - |             33296 B |
|      PooledSet_SymmetricExcept_Enum |            32000 |        8000000 |  2,643.5 us |  66.47 us | 193.89 us |  2,544.0 us |  2.10 |    0.20 |           - |           - |           - |             33296 B |
|       HashSet_SymmetricExcept_Array |            32000 |        8000000 |  3,130.3 us |  88.68 us | 260.07 us |  2,965.6 us |  2.49 |    0.25 |           - |           - |           - |             33296 B |
|     PooledSet_SymmetricExcept_Array |            32000 |        8000000 |  2,371.6 us |  28.12 us |  21.96 us |  2,360.4 us |  1.91 |    0.05 |           - |           - |           - |             25104 B |
|                                     |                  |                |             |           |           |             |       |         |             |             |             |                     |
|     **HashSet_SymmetricExcept_Hashset** |           **320000** |        **8000000** |  **3,222.2 us** |  **78.44 us** | **230.05 us** |  **3,206.7 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |           320000 |        8000000 |  2,920.8 us |  69.31 us | 201.07 us |  2,808.8 us |  0.91 |    0.09 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |           320000 |        8000000 | 15,776.5 us | 346.43 us | 425.45 us | 15,664.7 us |  4.89 |    0.35 |           - |           - |           - |             33296 B |
|      PooledSet_SymmetricExcept_Enum |           320000 |        8000000 | 15,348.9 us | 263.69 us | 246.66 us | 15,228.0 us |  4.70 |    0.32 |           - |           - |           - |             33296 B |
|       HashSet_SymmetricExcept_Array |           320000 |        8000000 | 16,591.8 us | 381.40 us | 423.93 us | 16,551.1 us |  5.13 |    0.27 |           - |           - |           - |             33296 B |
|     PooledSet_SymmetricExcept_Array |           320000 |        8000000 | 14,450.9 us | 285.68 us | 409.71 us | 14,333.7 us |  4.50 |    0.32 |           - |           - |           - |             25104 B |
