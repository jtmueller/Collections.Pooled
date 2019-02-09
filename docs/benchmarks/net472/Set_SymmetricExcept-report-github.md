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
|     **HashSet_SymmetricExcept_Hashset** |            **32000** |        **8000000** |  **1,369.5 us** |  **34.00 us** |  **94.79 us** |  **1,361.8 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |            32000 |        8000000 |    861.9 us |  16.99 us |  37.65 us |    846.3 us |  0.63 |    0.05 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |            32000 |        8000000 |  3,120.4 us |  61.37 us |  81.92 us |  3,116.0 us |  2.27 |    0.16 |           - |           - |           - |             33296 B |
|      PooledSet_SymmetricExcept_Enum |            32000 |        8000000 |  2,587.2 us |  50.94 us |  73.06 us |  2,588.4 us |  1.88 |    0.14 |           - |           - |           - |             33296 B |
|       HashSet_SymmetricExcept_Array |            32000 |        8000000 |  3,042.9 us |  60.44 us | 124.83 us |  3,004.9 us |  2.22 |    0.16 |           - |           - |           - |             33296 B |
|     PooledSet_SymmetricExcept_Array |            32000 |        8000000 |  2,356.2 us |  46.59 us |  66.82 us |  2,354.0 us |  1.71 |    0.11 |           - |           - |           - |             25104 B |
|                                     |                  |                |             |           |           |             |       |         |             |             |             |                     |
|     **HashSet_SymmetricExcept_Hashset** |           **320000** |        **8000000** |  **3,509.8 us** |  **69.86 us** | **157.69 us** |  **3,512.3 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |           320000 |        8000000 |  2,725.2 us |  61.78 us | 106.57 us |  2,698.8 us |  0.79 |    0.05 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |           320000 |        8000000 | 15,727.2 us | 257.28 us | 240.66 us | 15,691.5 us |  4.50 |    0.18 |           - |           - |           - |             33296 B |
|      PooledSet_SymmetricExcept_Enum |           320000 |        8000000 | 15,582.4 us | 339.03 us | 317.13 us | 15,508.4 us |  4.45 |    0.20 |           - |           - |           - |             33296 B |
|       HashSet_SymmetricExcept_Array |           320000 |        8000000 | 16,847.8 us | 266.88 us | 249.64 us | 16,814.4 us |  4.82 |    0.18 |           - |           - |           - |             33296 B |
|     PooledSet_SymmetricExcept_Array |           320000 |        8000000 | 14,684.9 us | 417.86 us | 390.87 us | 14,618.5 us |  4.20 |    0.17 |           - |           - |           - |             25104 B |
