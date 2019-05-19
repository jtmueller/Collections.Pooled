``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|                  Method |  Job | Runtime |      N |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----- |-------- |------- |------------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|       **DefaultStringHash** |  **Clr** |     **Clr** |   **1000** |    **13.03 us** |  **0.2430 us** |  **0.2273 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| NonRandomizedStringHash |  Clr |     Clr |   1000 |    13.05 us |  0.2501 us |  0.2340 us |  1.00 |    0.03 |     - |     - |     - |         - |
|                         |      |         |        |             |            |            |       |         |       |       |       |           |
|       DefaultStringHash | Core |    Core |   1000 |    14.17 us |  0.2795 us |  0.2615 us |  1.00 |    0.00 |     - |     - |     - |         - |
| NonRandomizedStringHash | Core |    Core |   1000 |    14.29 us |  0.1666 us |  0.1476 us |  1.01 |    0.02 |     - |     - |     - |         - |
|                         |      |         |        |             |            |            |       |         |       |       |       |           |
|       **DefaultStringHash** |  **Clr** |     **Clr** |  **10000** |   **133.24 us** |  **2.5134 us** |  **2.4685 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| NonRandomizedStringHash |  Clr |     Clr |  10000 |   132.42 us |  2.5280 us |  2.7050 us |  0.99 |    0.02 |     - |     - |     - |         - |
|                         |      |         |        |             |            |            |       |         |       |       |       |           |
|       DefaultStringHash | Core |    Core |  10000 |   143.96 us |  1.9485 us |  1.8227 us |  1.00 |    0.00 |     - |     - |     - |         - |
| NonRandomizedStringHash | Core |    Core |  10000 |   142.04 us |  2.4168 us |  2.2607 us |  0.99 |    0.02 |     - |     - |     - |         - |
|                         |      |         |        |             |            |            |       |         |       |       |       |           |
|       **DefaultStringHash** |  **Clr** |     **Clr** | **100000** | **1,344.74 us** | **14.2882 us** | **12.6661 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| NonRandomizedStringHash |  Clr |     Clr | 100000 | 1,353.88 us | 17.7274 us | 16.5822 us |  1.01 |    0.01 |     - |     - |     - |         - |
|                         |      |         |        |             |            |            |       |         |       |       |       |           |
|       DefaultStringHash | Core |    Core | 100000 | 1,443.31 us | 10.4293 us |  9.7556 us |  1.00 |    0.00 |     - |     - |     - |         - |
| NonRandomizedStringHash | Core |    Core | 100000 | 1,442.73 us |  9.5008 us |  8.8871 us |  1.00 |    0.01 |     - |     - |     - |         - |
