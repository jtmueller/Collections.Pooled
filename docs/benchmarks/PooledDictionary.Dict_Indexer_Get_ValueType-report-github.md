``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|     Method |  Job | Runtime |     N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------- |----- |-------- |------ |-----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_Int** |  **Clr** |     **Clr** |  **1024** |   **266.5 us** |  **4.233 us** |  **3.960 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Int |  Clr |     Clr |  1024 |   252.8 us |  4.679 us |  4.377 us |  0.95 |    0.02 |     - |     - |     - |         - |
|            |      |         |       |            |           |           |       |         |       |       |       |           |
|   Dict_Int | Core |    Core |  1024 |   256.8 us |  5.067 us |  4.740 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Int | Core |    Core |  1024 |   257.0 us |  4.988 us |  4.899 us |  1.00 |    0.02 |     - |     - |     - |         - |
|            |      |         |       |            |           |           |       |         |       |       |       |           |
|   **Dict_Int** |  **Clr** |     **Clr** |  **8192** | **2,131.0 us** | **22.419 us** | **20.970 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Int |  Clr |     Clr |  8192 | 2,062.0 us | 23.006 us | 21.519 us |  0.97 |    0.01 |     - |     - |     - |         - |
|            |      |         |       |            |           |           |       |         |       |       |       |           |
|   Dict_Int | Core |    Core |  8192 | 2,083.9 us | 19.761 us | 18.485 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Int | Core |    Core |  8192 | 2,045.1 us | 35.162 us | 32.890 us |  0.98 |    0.02 |     - |     - |     - |         - |
|            |      |         |       |            |           |           |       |         |       |       |       |           |
|   **Dict_Int** |  **Clr** |     **Clr** | **16384** | **4,203.0 us** | **82.780 us** | **85.008 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Int |  Clr |     Clr | 16384 | 4,065.4 us | 62.546 us | 48.832 us |  0.97 |    0.02 |     - |     - |     - |         - |
|            |      |         |       |            |           |           |       |         |       |       |       |           |
|   Dict_Int | Core |    Core | 16384 | 4,100.8 us | 81.439 us | 83.632 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Int | Core |    Core | 16384 | 4,130.7 us | 80.099 us | 82.256 us |  1.01 |    0.02 |     - |     - |     - |         - |
