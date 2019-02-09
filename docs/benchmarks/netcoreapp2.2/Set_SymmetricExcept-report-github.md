``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                              Method | CountToIntersect | InitialSetSize |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |----------------- |--------------- |------------:|----------:|----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_SymmetricExcept_Hashset** |            **32000** |        **8000000** |  **1,353.6 us** |  **27.62 us** |  **76.99 us** |  **1,344.3 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |            32000 |        8000000 |    931.9 us |  25.30 us |  69.69 us |    916.9 us |  0.69 |    0.06 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |            32000 |        8000000 |  3,660.1 us |  74.69 us | 205.71 us |  3,610.3 us |  2.71 |    0.21 |           - |           - |           - |             25176 B |
|      PooledSet_SymmetricExcept_Enum |            32000 |        8000000 |  2,509.6 us |  49.99 us | 130.81 us |  2,482.1 us |  1.85 |    0.14 |           - |           - |           - |             25096 B |
|       HashSet_SymmetricExcept_Array |            32000 |        8000000 |  3,618.2 us |  82.67 us | 234.53 us |  3,542.5 us |  2.67 |    0.20 |           - |           - |           - |             25168 B |
|     PooledSet_SymmetricExcept_Array |            32000 |        8000000 |  2,545.7 us |  57.26 us | 164.30 us |  2,525.2 us |  1.88 |    0.16 |           - |           - |           - |             25056 B |
|                                     |                  |                |             |           |           |             |       |         |             |             |             |                     |
|     **HashSet_SymmetricExcept_Hashset** |           **320000** |        **8000000** |  **3,395.9 us** |  **76.29 us** | **220.12 us** |  **3,315.6 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_SymmetricExcept_PooledSet |           320000 |        8000000 |  3,161.4 us | 112.58 us | 330.18 us |  3,010.1 us |  0.94 |    0.10 |           - |           - |           - |                   - |
|        HashSet_SymmetricExcept_Enum |           320000 |        8000000 | 18,229.9 us | 363.65 us | 597.48 us | 18,139.5 us |  5.19 |    0.30 |           - |           - |           - |             25176 B |
|      PooledSet_SymmetricExcept_Enum |           320000 |        8000000 | 16,932.5 us | 336.17 us | 843.38 us | 16,799.0 us |  4.98 |    0.32 |           - |           - |           - |             25096 B |
|       HashSet_SymmetricExcept_Array |           320000 |        8000000 | 18,465.8 us | 340.77 us | 318.76 us | 18,425.7 us |  5.18 |    0.28 |           - |           - |           - |             25168 B |
|     PooledSet_SymmetricExcept_Array |           320000 |        8000000 | 15,157.3 us | 302.54 us | 707.18 us | 15,033.0 us |  4.44 |    0.29 |           - |           - |           - |             25056 B |
