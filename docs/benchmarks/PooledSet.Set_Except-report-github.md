``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|              Method |  Job | Runtime | CountToIntersect | InitialSetSize |       Mean |      Error |     StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |----- |-------- |----------------- |--------------- |-----------:|-----------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|     **HashSet_Hashset** |  **Clr** |     **Clr** |            **32000** |        **8000000** | **1,311.7 us** |  **25.908 us** |  **63.064 us** | **1,301.0 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet_PooledSet |  Clr |     Clr |            32000 |        8000000 |   969.7 us |  21.553 us |  61.142 us |   952.0 us |  0.74 |    0.05 |     - |     - |     - |    8192 B |
|        HashSet_Enum |  Clr |     Clr |            32000 |        8000000 | 1,590.2 us |  31.573 us |  56.121 us | 1,582.4 us |  1.22 |    0.07 |     - |     - |     - |    8192 B |
|      PooledSet_Enum |  Clr |     Clr |            32000 |        8000000 | 1,153.8 us |  25.057 us |  40.463 us | 1,151.0 us |  0.88 |    0.05 |     - |     - |     - |    8192 B |
|       HashSet_Array |  Clr |     Clr |            32000 |        8000000 | 1,617.3 us |  37.593 us | 108.465 us | 1,586.1 us |  1.23 |    0.10 |     - |     - |     - |    8192 B |
|     PooledSet_Array |  Clr |     Clr |            32000 |        8000000 | 1,024.2 us |  20.312 us |  41.031 us | 1,019.2 us |  0.78 |    0.05 |     - |     - |     - |         - |
|                     |      |         |                  |                |            |            |            |            |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |            32000 |        8000000 | 1,353.7 us |  45.639 us | 129.470 us | 1,305.6 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |            32000 |        8000000 |   965.9 us |  19.114 us |  31.936 us |   952.8 us |  0.68 |    0.08 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |            32000 |        8000000 | 1,497.6 us |  29.708 us |  68.259 us | 1,482.4 us |  1.09 |    0.10 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |            32000 |        8000000 | 1,169.8 us |  23.349 us |  47.165 us | 1,155.0 us |  0.85 |    0.09 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |            32000 |        8000000 | 1,486.0 us |  30.175 us |  66.235 us | 1,468.0 us |  1.08 |    0.11 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |            32000 |        8000000 |   959.4 us |   9.342 us |   8.282 us |   959.6 us |  0.60 |    0.02 |     - |     - |     - |         - |
|                     |      |         |                  |                |            |            |            |            |       |         |       |       |       |           |
|     **HashSet_Hashset** |  **Clr** |     **Clr** |           **320000** |        **8000000** | **3,365.0 us** |  **66.331 us** |  **76.387 us** | **3,366.7 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet_PooledSet |  Clr |     Clr |           320000 |        8000000 | 3,037.5 us |  60.648 us | 110.898 us | 3,015.9 us |  0.91 |    0.04 |     - |     - |     - |    8192 B |
|        HashSet_Enum |  Clr |     Clr |           320000 |        8000000 | 8,799.7 us | 147.300 us | 151.267 us | 8,821.9 us |  2.61 |    0.07 |     - |     - |     - |    8192 B |
|      PooledSet_Enum |  Clr |     Clr |           320000 |        8000000 | 8,466.3 us | 167.013 us | 228.609 us | 8,421.9 us |  2.52 |    0.07 |     - |     - |     - |    8192 B |
|       HashSet_Array |  Clr |     Clr |           320000 |        8000000 | 9,007.2 us | 104.056 us |  86.892 us | 9,016.0 us |  2.66 |    0.06 |     - |     - |     - |    8192 B |
|     PooledSet_Array |  Clr |     Clr |           320000 |        8000000 | 6,786.8 us | 115.024 us |  96.050 us | 6,805.2 us |  2.01 |    0.04 |     - |     - |     - |         - |
|                     |      |         |                  |                |            |            |            |            |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |           320000 |        8000000 | 3,516.1 us | 108.554 us | 316.658 us | 3,371.0 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |           320000 |        8000000 | 3,065.1 us |  59.888 us |  91.455 us | 3,069.6 us |  0.81 |    0.07 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |           320000 |        8000000 | 9,433.6 us | 178.455 us | 149.018 us | 9,366.2 us |  2.42 |    0.08 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |           320000 |        8000000 | 8,626.5 us | 163.823 us | 153.240 us | 8,603.9 us |  2.21 |    0.08 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |           320000 |        8000000 | 8,689.2 us | 165.944 us | 147.105 us | 8,654.2 us |  2.23 |    0.09 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |           320000 |        8000000 | 6,700.8 us | 131.624 us | 166.462 us | 6,645.2 us |  1.73 |    0.10 |     - |     - |     - |         - |
