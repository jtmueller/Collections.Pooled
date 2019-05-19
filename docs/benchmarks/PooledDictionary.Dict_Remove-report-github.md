``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|       Method |  Job | Runtime |     N |         Mean |        Error |        StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------- |----- |-------- |------ |-------------:|-------------:|--------------:|------:|--------:|------:|------:|------:|----------:|
|   **DictRemove** |  **Clr** |     **Clr** |    **10** |     **79.20 ns** |     **1.518 ns** |     **1.5585 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledRemove |  Clr |     Clr |    10 |     72.86 ns |     1.493 ns |     1.3968 ns |  0.92 |    0.02 |     - |     - |     - |         - |
|              |      |         |       |              |              |               |       |         |       |       |       |           |
|   DictRemove | Core |    Core |    10 |     71.93 ns |     1.092 ns |     0.9676 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledRemove | Core |    Core |    10 |     72.19 ns |     1.360 ns |     1.2723 ns |  1.00 |    0.02 |     - |     - |     - |         - |
|              |      |         |       |              |              |               |       |         |       |       |       |           |
|   **DictRemove** |  **Clr** |     **Clr** |   **100** |    **823.81 ns** |    **13.080 ns** |    **12.2353 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledRemove |  Clr |     Clr |   100 |    752.93 ns |     8.305 ns |     7.7690 ns |  0.91 |    0.02 |     - |     - |     - |         - |
|              |      |         |       |              |              |               |       |         |       |       |       |           |
|   DictRemove | Core |    Core |   100 |    731.74 ns |    13.130 ns |    12.2820 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledRemove | Core |    Core |   100 |    763.37 ns |     7.523 ns |     7.0372 ns |  1.04 |    0.02 |     - |     - |     - |         - |
|              |      |         |       |              |              |               |       |         |       |       |       |           |
|   **DictRemove** |  **Clr** |     **Clr** | **10000** | **80,518.33 ns** | **1,469.254 ns** | **1,374.3413 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledRemove |  Clr |     Clr | 10000 | 73,528.51 ns | 1,436.287 ns | 1,709.7977 ns |  0.91 |    0.02 |     - |     - |     - |         - |
|              |      |         |       |              |              |               |       |         |       |       |       |           |
|   DictRemove | Core |    Core | 10000 | 69,799.36 ns | 1,366.442 ns | 1,573.5964 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledRemove | Core |    Core | 10000 | 70,689.20 ns | 1,332.905 ns | 1,246.8005 ns |  1.01 |    0.03 |     - |     - |     - |         - |
