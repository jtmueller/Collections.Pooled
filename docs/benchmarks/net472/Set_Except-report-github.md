``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|                     Method | CountToIntersect | InitialSetSize |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |----------------- |--------------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Except_Hashset** |            **32000** |        **8000000** | **1,355.8 us** |  **26.92 us** |  **45.71 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Except_PooledSet |            32000 |        8000000 |   956.5 us |  18.37 us |  23.24 us |  0.70 |    0.02 |           - |           - |           - |                   - |
|        HashSet_Except_Enum |            32000 |        8000000 | 1,500.5 us |  30.74 us |  73.65 us |  1.10 |    0.07 |           - |           - |           - |                   - |
|      PooledSet_Except_Enum |            32000 |        8000000 | 1,123.8 us |  21.48 us |  16.77 us |  0.82 |    0.02 |           - |           - |           - |                   - |
|       HashSet_Except_Array |            32000 |        8000000 | 1,498.6 us |  29.14 us |  58.19 us |  1.11 |    0.06 |           - |           - |           - |                   - |
|     PooledSet_Except_Array |            32000 |        8000000 |   961.1 us |  22.13 us |  50.41 us |  0.71 |    0.04 |           - |           - |           - |                   - |
|                            |                  |                |            |           |           |       |         |             |             |             |                     |
|     **HashSet_Except_Hashset** |           **320000** |        **8000000** | **3,552.8 us** |  **81.33 us** | **238.52 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Except_PooledSet |           320000 |        8000000 | 3,184.0 us |  85.85 us | 251.79 us |  0.90 |    0.09 |           - |           - |           - |                   - |
|        HashSet_Except_Enum |           320000 |        8000000 | 8,931.1 us | 176.03 us | 268.82 us |  2.52 |    0.14 |           - |           - |           - |                   - |
|      PooledSet_Except_Enum |           320000 |        8000000 | 8,698.0 us | 171.16 us | 329.76 us |  2.45 |    0.18 |           - |           - |           - |                   - |
|       HashSet_Except_Array |           320000 |        8000000 | 8,970.6 us | 176.49 us | 269.52 us |  2.54 |    0.18 |           - |           - |           - |                   - |
|     PooledSet_Except_Array |           320000 |        8000000 | 6,851.2 us | 134.94 us | 284.62 us |  1.94 |    0.13 |           - |           - |           - |                   - |
