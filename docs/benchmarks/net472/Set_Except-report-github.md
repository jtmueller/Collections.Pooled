``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|                     Method | CountToIntersect | InitialSetSize |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |----------------- |--------------- |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Except_Hashset** |            **32000** |        **8000000** | **1,363.8 us** |  **28.44 us** |  **77.36 us** | **1,376.7 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Except_PooledSet |            32000 |        8000000 |   991.0 us |  35.15 us |  99.71 us |   957.5 us |  0.73 |    0.09 |           - |           - |           - |                   - |
|        HashSet_Except_Enum |            32000 |        8000000 | 1,499.9 us |  33.78 us |  78.96 us | 1,482.7 us |  1.09 |    0.09 |           - |           - |           - |                   - |
|      PooledSet_Except_Enum |            32000 |        8000000 | 1,089.0 us |  19.39 us |  16.19 us | 1,080.7 us |  0.81 |    0.03 |           - |           - |           - |                   - |
|       HashSet_Except_Array |            32000 |        8000000 | 1,494.0 us |  29.70 us |  56.50 us | 1,489.9 us |  1.09 |    0.07 |           - |           - |           - |                   - |
|     PooledSet_Except_Array |            32000 |        8000000 | 1,008.6 us |  37.74 us | 105.21 us |   960.7 us |  0.74 |    0.08 |           - |           - |           - |                   - |
|                            |                  |                |            |           |           |            |       |         |             |             |             |                     |
|     **HashSet_Except_Hashset** |           **320000** |        **8000000** | **3,607.6 us** |  **71.61 us** | **170.19 us** | **3,565.3 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Except_PooledSet |           320000 |        8000000 | 3,176.5 us | 107.68 us | 301.96 us | 3,074.2 us |  0.88 |    0.09 |           - |           - |           - |                   - |
|        HashSet_Except_Enum |           320000 |        8000000 | 8,536.9 us | 165.58 us | 197.11 us | 8,425.2 us |  2.33 |    0.12 |           - |           - |           - |                   - |
|      PooledSet_Except_Enum |           320000 |        8000000 | 8,769.3 us | 259.97 us | 728.98 us | 8,451.2 us |  2.45 |    0.21 |           - |           - |           - |                   - |
|       HashSet_Except_Array |           320000 |        8000000 | 8,845.6 us | 178.45 us | 447.69 us | 8,720.3 us |  2.47 |    0.16 |           - |           - |           - |                   - |
|     PooledSet_Except_Array |           320000 |        8000000 | 6,548.4 us | 116.32 us | 108.81 us | 6,492.1 us |  1.79 |    0.08 |           - |           - |           - |                   - |
