``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|                              Method | CountToIntersect | InitialSetSize |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |----------------- |--------------- |------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_SymmetricExcept_Hashset** |            **32000** |        **8000000** |  **1,172.2 us** |  **46.432 us** |  **43.433 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |            32000 |        8000000 |    826.0 us |   1.137 us |   1.008 us |  0.70 |    0.03 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |            32000 |        8000000 |  2,871.7 us |  55.673 us |  61.881 us |  2.45 |    0.10 |           - |           - |           - |             33296 B |
|      PooledSet_SymmetricExcept_Enum |            32000 |        8000000 |  2,476.8 us |  70.986 us |  81.747 us |  2.12 |    0.12 |           - |           - |           - |             33296 B |
|       HashSet_SymmetricExcept_Array |            32000 |        8000000 |  2,890.1 us |  50.304 us |  44.593 us |  2.47 |    0.10 |           - |           - |           - |             33296 B |
|     PooledSet_SymmetricExcept_Array |            32000 |        8000000 |  2,307.7 us |  25.642 us |  21.412 us |  1.96 |    0.08 |           - |           - |           - |             25104 B |
|                                     |                  |                |             |            |            |       |         |             |             |             |                     |
|     **HashSet_SymmetricExcept_Hashset** |           **320000** |        **8000000** |  **3,094.6 us** |  **61.729 us** | **132.878 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |           320000 |        8000000 |  2,799.3 us |  51.415 us |  45.578 us |  0.90 |    0.04 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |           320000 |        8000000 | 15,023.0 us | 142.461 us | 118.962 us |  4.83 |    0.18 |           - |           - |           - |             33296 B |
|      PooledSet_SymmetricExcept_Enum |           320000 |        8000000 | 15,091.1 us | 151.192 us | 126.252 us |  4.86 |    0.18 |           - |           - |           - |             33296 B |
|       HashSet_SymmetricExcept_Array |           320000 |        8000000 | 15,465.5 us | 170.118 us | 150.805 us |  4.99 |    0.21 |           - |           - |           - |             33296 B |
|     PooledSet_SymmetricExcept_Array |           320000 |        8000000 | 13,915.0 us | 223.650 us | 186.758 us |  4.48 |    0.16 |           - |           - |           - |             25104 B |
