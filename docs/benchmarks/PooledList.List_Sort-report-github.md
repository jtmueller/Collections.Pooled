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
|        Method |  Job | Runtime |      N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |------- |-----------:|----------:|----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|      **List_Int** |  **Clr** |     **Clr** | **100000** |   **7.012 ms** | **0.1381 ms** | **0.1696 ms** |   **6.969 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|    Pooled_Int |  Clr |     Clr | 100000 |   7.007 ms | 0.1389 ms | 0.2677 ms |   6.982 ms |  0.99 |    0.04 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr | 100000 | 178.796 ms | 3.7193 ms | 3.2970 ms | 177.216 ms | 25.55 |    0.77 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr | 100000 | 174.003 ms | 4.2594 ms | 3.9842 ms | 172.076 ms | 24.83 |    1.05 |     - |     - |     - |         - |
|               |      |         |        |            |           |           |            |       |         |       |       |       |           |
|      List_Int | Core |    Core | 100000 |   6.932 ms | 0.1326 ms | 0.1578 ms |   6.965 ms |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core | 100000 |   6.837 ms | 0.1355 ms | 0.2611 ms |   6.778 ms |  1.01 |    0.03 |     - |     - |     - |         - |
|   List_String | Core |    Core | 100000 | 137.275 ms | 2.6264 ms | 2.1932 ms | 137.144 ms | 19.93 |    0.54 |     - |     - |     - |         - |
| Pooled_String | Core |    Core | 100000 | 139.028 ms | 2.4692 ms | 2.0619 ms | 138.358 ms | 20.19 |    0.60 |     - |     - |     - |         - |
|               |      |         |        |            |           |           |            |       |         |       |       |       |           |
|      **List_Int** |  **Clr** |     **Clr** | **200000** |  **14.512 ms** | **0.2786 ms** | **0.2470 ms** |  **14.395 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|    Pooled_Int |  Clr |     Clr | 200000 |  14.641 ms | 0.2906 ms | 0.2984 ms |  14.566 ms |  1.01 |    0.03 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr | 200000 | 377.466 ms | 7.4210 ms | 7.2884 ms | 374.201 ms | 26.06 |    0.68 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr | 200000 | 376.874 ms | 7.4716 ms | 8.3046 ms | 371.240 ms | 25.98 |    0.89 |     - |     - |     - |         - |
|               |      |         |        |            |           |           |            |       |         |       |       |       |           |
|      List_Int | Core |    Core | 200000 |  14.223 ms | 0.2743 ms | 0.2817 ms |  14.260 ms |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core | 200000 |  14.124 ms | 0.2779 ms | 0.3515 ms |  14.044 ms |  1.00 |    0.03 |     - |     - |     - |         - |
|   List_String | Core |    Core | 200000 | 323.450 ms | 6.2174 ms | 6.9107 ms | 320.858 ms | 22.74 |    0.63 |     - |     - |     - |         - |
| Pooled_String | Core |    Core | 200000 | 309.010 ms | 5.9388 ms | 7.0697 ms | 304.529 ms | 21.75 |    0.76 |     - |     - |     - |         - |
