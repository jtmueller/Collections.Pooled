``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|          Method |  Job | Runtime |      N |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |----- |-------- |------- |------------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_Contains** |  **Clr** |     **Clr** |   **1000** |    **23.45 us** |  **0.3766 us** |  **0.2940 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr |   1000 |    34.65 us |  0.4391 us |  0.4107 us |  1.48 |    0.02 |     - |     - |     - |         - |
|                 |      |         |        |             |            |            |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |   1000 |    19.01 us |  0.3361 us |  0.3144 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core |   1000 |    21.09 us |  0.5110 us |  0.5680 us |  1.11 |    0.04 |     - |     - |     - |         - |
|                 |      |         |        |             |            |            |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** |  **10000** |   **270.97 us** |  **1.1743 us** |  **1.0985 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr |  10000 |   350.89 us |  3.2423 us |  2.8743 us |  1.30 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |             |            |            |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |  10000 |   233.05 us |  2.2123 us |  2.0693 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core |  10000 |   213.98 us |  0.4965 us |  0.4146 us |  0.92 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |             |            |            |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** | **100000** | **2,632.73 us** |  **7.3538 us** |  **6.8788 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr | 100000 | 4,077.37 us | 13.4980 us | 12.6260 us |  1.55 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |             |            |            |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core | 100000 | 2,899.92 us | 54.9086 us | 48.6751 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core | 100000 | 2,861.76 us | 12.9943 us | 12.1549 us |  0.99 |    0.02 |     - |     - |     - |         - |
