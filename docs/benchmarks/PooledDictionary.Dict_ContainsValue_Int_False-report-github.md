``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|          Method |  Job | Runtime |      N |          Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |----- |-------- |------- |--------------:|------------:|------------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_Contains** |  **Clr** |     **Clr** |   **1000** |      **2.051 ms** |   **0.0057 ms** |   **0.0053 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr |   1000 |      1.878 ms |   0.0188 ms |   0.0176 ms |  0.92 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |               |             |             |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |   1000 |      1.116 ms |   0.0187 ms |   0.0166 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core |   1000 |      1.098 ms |   0.0133 ms |   0.0118 ms |  0.98 |    0.02 |     - |     - |     - |         - |
|                 |      |         |        |               |             |             |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** |  **10000** |    **204.969 ms** |   **3.9155 ms** |   **3.6626 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr |  10000 |    189.309 ms |   2.5650 ms |   2.3993 ms |  0.92 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |               |             |             |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |  10000 |    111.506 ms |   2.1442 ms |   2.6333 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core |  10000 |    109.932 ms |   0.4731 ms |   0.3694 ms |  0.99 |    0.03 |     - |     - |     - |         - |
|                 |      |         |        |               |             |             |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** | **100000** | **20,366.875 ms** |  **90.1556 ms** |  **79.9206 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr | 100000 | 18,698.928 ms | 632.3037 ms | 621.0068 ms |  0.92 |    0.03 |     - |     - |     - |         - |
|                 |      |         |        |               |             |             |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core | 100000 | 11,223.661 ms | 268.6000 ms | 402.0279 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core | 100000 | 11,229.984 ms | 223.8566 ms | 291.0769 ms |  1.00 |    0.04 |     - |     - |     - |         - |
