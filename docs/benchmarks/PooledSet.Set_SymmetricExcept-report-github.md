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
|              Method |  Job | Runtime | CountToIntersect | InitialSetSize |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |----- |-------- |----------------- |--------------- |------------:|-----------:|-----------:|------------:|------:|--------:|------:|------:|------:|----------:|
|     **HashSet_Hashset** |  **Clr** |     **Clr** |            **32000** |        **8000000** |  **1,219.3 us** |  **24.108 us** |  **57.295 us** |  **1,202.5 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet_PooledSet |  Clr |     Clr |            32000 |        8000000 |    853.5 us |  20.113 us |  57.383 us |    817.0 us |  0.70 |    0.06 |     - |     - |     - |         - |
|        HashSet_Enum |  Clr |     Clr |            32000 |        8000000 |  3,277.4 us |  64.453 us | 104.080 us |  3,276.6 us |  2.71 |    0.16 |     - |     - |     - |   33296 B |
|      PooledSet_Enum |  Clr |     Clr |            32000 |        8000000 |  2,570.6 us |  49.329 us |  54.829 us |  2,554.4 us |  2.13 |    0.10 |     - |     - |     - |   33296 B |
|       HashSet_Array |  Clr |     Clr |            32000 |        8000000 |  3,345.0 us |  66.170 us | 120.996 us |  3,346.5 us |  2.76 |    0.15 |     - |     - |     - |   33296 B |
|     PooledSet_Array |  Clr |     Clr |            32000 |        8000000 |  2,428.8 us |  48.827 us |  77.445 us |  2,410.3 us |  2.01 |    0.12 |     - |     - |     - |   25104 B |
|                     |      |         |                  |                |             |            |            |             |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |            32000 |        8000000 |  1,270.0 us |  25.388 us |  68.639 us |  1,255.1 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet_PooledSet | Core |    Core |            32000 |        8000000 |    871.4 us |   7.860 us |   6.137 us |    870.2 us |  0.67 |    0.03 |     - |     - |     - |         - |
|        HashSet_Enum | Core |    Core |            32000 |        8000000 |  3,064.4 us |  81.847 us | 240.044 us |  2,973.4 us |  2.43 |    0.21 |     - |     - |     - |   25096 B |
|      PooledSet_Enum | Core |    Core |            32000 |        8000000 |  2,409.6 us |  53.532 us |  87.955 us |  2,406.7 us |  1.86 |    0.12 |     - |     - |     - |   25096 B |
|       HashSet_Array | Core |    Core |            32000 |        8000000 |  2,846.4 us |  56.050 us | 107.989 us |  2,825.1 us |  2.20 |    0.14 |     - |     - |     - |   25088 B |
|     PooledSet_Array | Core |    Core |            32000 |        8000000 |  2,238.5 us |  43.616 us |  71.663 us |  2,221.5 us |  1.73 |    0.11 |     - |     - |     - |   25056 B |
|                     |      |         |                  |                |             |            |            |             |       |         |       |       |       |           |
|     **HashSet_Hashset** |  **Clr** |     **Clr** |           **320000** |        **8000000** |  **3,205.6 us** |  **66.291 us** | **101.234 us** |  **3,182.8 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet_PooledSet |  Clr |     Clr |           320000 |        8000000 |  2,686.9 us |  52.643 us |  68.450 us |  2,675.9 us |  0.84 |    0.04 |     - |     - |     - |         - |
|        HashSet_Enum |  Clr |     Clr |           320000 |        8000000 | 16,698.0 us | 161.081 us | 142.794 us | 16,650.5 us |  5.23 |    0.20 |     - |     - |     - |   33296 B |
|      PooledSet_Enum |  Clr |     Clr |           320000 |        8000000 | 15,497.9 us | 302.820 us | 297.410 us | 15,396.0 us |  4.84 |    0.17 |     - |     - |     - |   33296 B |
|       HashSet_Array |  Clr |     Clr |           320000 |        8000000 | 17,150.9 us | 202.365 us | 179.391 us | 17,146.1 us |  5.37 |    0.19 |     - |     - |     - |   33296 B |
|     PooledSet_Array |  Clr |     Clr |           320000 |        8000000 | 13,941.9 us | 330.519 us | 292.997 us | 13,928.2 us |  4.37 |    0.19 |     - |     - |     - |   25104 B |
|                     |      |         |                  |                |             |            |            |             |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |           320000 |        8000000 |  3,171.2 us |  62.354 us |  83.240 us |  3,143.4 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet_PooledSet | Core |    Core |           320000 |        8000000 |  2,753.3 us |  54.968 us |  75.240 us |  2,721.2 us |  0.87 |    0.03 |     - |     - |     - |         - |
|        HashSet_Enum | Core |    Core |           320000 |        8000000 | 17,871.1 us | 153.294 us | 135.892 us | 17,863.2 us |  5.70 |    0.13 |     - |     - |     - |   25096 B |
|      PooledSet_Enum | Core |    Core |           320000 |        8000000 | 14,374.2 us | 136.512 us | 113.994 us | 14,384.0 us |  4.58 |    0.09 |     - |     - |     - |   25096 B |
|       HashSet_Array | Core |    Core |           320000 |        8000000 | 15,223.9 us | 258.011 us | 241.343 us | 15,202.3 us |  4.84 |    0.14 |     - |     - |     - |   25088 B |
|     PooledSet_Array | Core |    Core |           320000 |        8000000 | 13,371.5 us | 215.394 us | 179.864 us | 13,368.6 us |  4.26 |    0.09 |     - |     - |     - |   25056 B |
