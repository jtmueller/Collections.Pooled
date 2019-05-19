``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      N |       Mean |      Error |    StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |------- |-----------:|-----------:|----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |   **1000** |   **7.000 us** |  **0.1327 us** | **0.1528 us** |   **6.963 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsKey |  Clr |     Clr |   1000 |   5.315 us |  0.0664 us | 0.0621 us |   5.333 us |  0.76 |    0.02 |     - |     - |     - |         - |
|                    |      |         |        |            |            |           |            |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |   1000 |   5.245 us |  0.0748 us | 0.0663 us |   5.234 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsKey | Core |    Core |   1000 |   5.724 us |  0.1193 us | 0.3225 us |   5.578 us |  1.14 |    0.07 |     - |     - |     - |         - |
|                    |      |         |        |            |            |           |            |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |  **10000** |  **68.081 us** |  **1.1620 us** | **1.0870 us** |  **68.046 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsKey |  Clr |     Clr |  10000 |  52.014 us |  0.6558 us | 0.6134 us |  51.848 us |  0.76 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |            |            |           |            |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |  10000 |  51.275 us |  0.5805 us | 0.5146 us |  51.140 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsKey | Core |    Core |  10000 |  55.029 us |  0.7375 us | 0.6898 us |  55.459 us |  1.07 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |            |            |           |            |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** | **100000** | **676.882 us** | **10.0614 us** | **9.4115 us** | **676.075 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsKey |  Clr |     Clr | 100000 | 522.930 us |  3.8215 us | 3.5746 us | 524.018 us |  0.77 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |            |            |           |            |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core | 100000 | 651.207 us |  6.9789 us | 6.5280 us | 653.009 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsKey | Core |    Core | 100000 | 519.223 us |  9.6004 us | 8.9802 us | 521.525 us |  0.80 |    0.02 |     - |     - |     - |         - |
