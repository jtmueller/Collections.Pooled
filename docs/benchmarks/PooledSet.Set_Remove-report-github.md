``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|    Method |  Job | Runtime | CountToRemove |         Mean |       Error |      StdDev |       Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |-------------- |-------------:|------------:|------------:|-------------:|------:|--------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |             **1** |   **1,042.3 ns** |    **70.15 ns** |    **203.5 ns** |   **1,000.0 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |             1 |     710.4 ns |    45.67 ns |    131.8 ns |     700.0 ns |  0.71 |    0.18 |     - |     - |     - |         - |
|           |      |         |               |              |             |             |              |       |         |       |       |       |           |
|   HashSet | Core |    Core |             1 |     670.1 ns |    81.31 ns |    208.4 ns |     600.0 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |             1 |     769.0 ns |   134.43 ns |    368.0 ns |     600.0 ns |  1.28 |    0.76 |     - |     - |     - |         - |
|           |      |         |               |              |             |             |              |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |           **100** |  **10,185.3 ns** |   **709.17 ns** |  **2,034.7 ns** |  **10,400.0 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |           100 |  10,538.9 ns |   310.35 ns |    865.1 ns |  10,400.0 ns |  1.07 |    0.23 |     - |     - |     - |         - |
|           |      |         |               |              |             |             |              |       |         |       |       |       |           |
|   HashSet | Core |    Core |           100 |   9,707.8 ns |   525.67 ns |  1,465.3 ns |   9,900.0 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |           100 |  11,148.1 ns |   445.44 ns |  1,173.5 ns |  10,800.0 ns |  1.17 |    0.21 |     - |     - |     - |         - |
|           |      |         |               |              |             |             |              |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |          **1000** |  **66,127.8 ns** | **6,421.45 ns** | **18,629.8 ns** |  **64,700.0 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |          1000 |  72,994.4 ns | 3,099.43 ns |  8,640.0 ns |  68,850.0 ns |  1.20 |    0.39 |     - |     - |     - |         - |
|           |      |         |               |              |             |             |              |       |         |       |       |       |           |
|   HashSet | Core |    Core |          1000 |  65,325.5 ns | 6,852.83 ns | 19,551.5 ns |  63,850.0 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |          1000 | 104,113.1 ns | 8,711.78 ns | 25,550.1 ns | 103,700.0 ns |  1.73 |    0.69 |     - |     - |     - |         - |
