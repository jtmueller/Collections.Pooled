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
|    Method |  Job | Runtime |      N |      Mean |     Error |    StdDev | Ratio | RatioSD |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|---------- |----- |-------- |------- |----------:|----------:|----------:|------:|--------:|----------:|----------:|----------:|-----------:|
|   **DictAdd** |  **Clr** |     **Clr** |   **1000** |  **6.023 ms** | **0.1273 ms** | **0.2571 ms** |  **1.00** |    **0.00** | **1000.0000** | **1000.0000** | **1000.0000** |  **7204928 B** |
| PooledAdd |  Clr |     Clr |   1000 |  6.670 ms | 0.1326 ms | 0.3047 ms |  1.11 |    0.07 |         - |         - |         - |          - |
|           |      |         |        |           |           |           |       |         |           |           |           |            |
|   DictAdd | Core |    Core |   1000 |  5.619 ms | 0.1119 ms | 0.2658 ms |  1.00 |    0.00 | 1000.0000 | 1000.0000 | 1000.0000 |  7204784 B |
| PooledAdd | Core |    Core |   1000 |  3.737 ms | 0.0741 ms | 0.1428 ms |  0.67 |    0.04 |         - |         - |         - |          - |
|           |      |         |        |           |           |           |       |         |           |           |           |            |
|   **DictAdd** |  **Clr** |     **Clr** |  **10000** |  **9.992 ms** | **0.1950 ms** | **0.2466 ms** |  **1.00** |    **0.00** |         **-** |         **-** |         **-** | **14645904 B** |
| PooledAdd |  Clr |     Clr |  10000 |  6.747 ms | 0.1345 ms | 0.2807 ms |  0.67 |    0.03 |         - |         - |         - |          - |
|           |      |         |        |           |           |           |       |         |           |           |           |            |
|   DictAdd | Core |    Core |  10000 |  9.400 ms | 0.1691 ms | 0.1412 ms |  1.00 |    0.00 |         - |         - |         - | 14645880 B |
| PooledAdd | Core |    Core |  10000 |  4.138 ms | 0.1049 ms | 0.3042 ms |  0.44 |    0.03 |         - |         - |         - |          - |
|           |      |         |        |           |           |           |       |         |           |           |           |            |
|   **DictAdd** |  **Clr** |     **Clr** | **100000** | **11.398 ms** | **0.2229 ms** | **0.2819 ms** |  **1.00** |    **0.00** |         **-** |         **-** |         **-** | **13850984 B** |
| PooledAdd |  Clr |     Clr | 100000 |  6.596 ms | 0.2004 ms | 0.5749 ms |  0.58 |    0.03 |         - |         - |         - |          - |
|           |      |         |        |           |           |           |       |         |           |           |           |            |
|   DictAdd | Core |    Core | 100000 | 11.359 ms | 0.2298 ms | 0.6631 ms |  1.00 |    0.00 |         - |         - |         - | 13850984 B |
| PooledAdd | Core |    Core | 100000 |  5.104 ms | 0.1640 ms | 0.4732 ms |  0.45 |    0.05 |         - |         - |         - |          - |
