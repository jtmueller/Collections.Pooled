``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|      Method |  Job | Runtime |     N |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |----- |-------- |------ |----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_Enum** |  **Clr** |     **Clr** |  **1024** |  **12.66 us** | **0.2393 us** | **0.2239 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Enum |  Clr |     Clr |  1024 |  11.97 us | 0.1782 us | 0.1667 us |  0.95 |    0.02 |     - |     - |     - |         - |
|             |      |         |       |           |           |           |       |         |       |       |       |           |
|   Dict_Enum | Core |    Core |  1024 |  12.38 us | 0.2184 us | 0.2043 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Enum | Core |    Core |  1024 |  12.31 us | 0.2295 us | 0.2254 us |  0.99 |    0.02 |     - |     - |     - |         - |
|             |      |         |       |           |           |           |       |         |       |       |       |           |
|   **Dict_Enum** |  **Clr** |     **Clr** |  **8192** | **100.10 us** | **1.4870 us** | **1.3909 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Enum |  Clr |     Clr |  8192 |  94.81 us | 1.5621 us | 1.4612 us |  0.95 |    0.02 |     - |     - |     - |         - |
|             |      |         |       |           |           |           |       |         |       |       |       |           |
|   Dict_Enum | Core |    Core |  8192 |  96.60 us | 1.0662 us | 0.9974 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Enum | Core |    Core |  8192 |  96.80 us | 0.6707 us | 0.6274 us |  1.00 |    0.01 |     - |     - |     - |         - |
|             |      |         |       |           |           |           |       |         |       |       |       |           |
|   **Dict_Enum** |  **Clr** |     **Clr** | **16384** | **209.25 us** | **1.2082 us** | **1.1301 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Enum |  Clr |     Clr | 16384 | 192.51 us | 3.4157 us | 3.1950 us |  0.92 |    0.02 |     - |     - |     - |         - |
|             |      |         |       |           |           |           |       |         |       |       |       |           |
|   Dict_Enum | Core |    Core | 16384 | 197.92 us | 3.0479 us | 2.8510 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Enum | Core |    Core | 16384 | 199.16 us | 1.9575 us | 1.8311 us |  1.01 |    0.02 |     - |     - |     - |         - |
