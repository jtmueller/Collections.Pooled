``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      N |          Mean |       Error |      StdDev |        Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |------- |--------------:|------------:|------------:|--------------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_ContainsVal** |  **Clr** |     **Clr** |   **1000** |      **3.651 ms** |   **0.1017 ms** |   **0.2999 ms** |      **3.537 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsVal |  Clr |     Clr |   1000 |      3.439 ms |   0.0665 ms |   0.0653 ms |      3.419 ms |  0.86 |    0.03 |     - |     - |     - |         - |
|                    |      |         |        |               |             |             |               |       |         |       |       |       |           |
|   Dict_ContainsVal | Core |    Core |   1000 |      3.584 ms |   0.0580 ms |   0.0542 ms |      3.582 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsVal | Core |    Core |   1000 |      4.054 ms |   0.0810 ms |   0.0757 ms |      4.039 ms |  1.13 |    0.03 |     - |     - |     - |         - |
|                    |      |         |        |               |             |             |               |       |         |       |       |       |           |
|   **Dict_ContainsVal** |  **Clr** |     **Clr** |  **10000** |    **347.070 ms** |   **3.2488 ms** |   **3.0389 ms** |    **347.089 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsVal |  Clr |     Clr |  10000 |    336.994 ms |   6.6092 ms |   7.0718 ms |    334.278 ms |  0.97 |    0.02 |     - |     - |     - |         - |
|                    |      |         |        |               |             |             |               |       |         |       |       |       |           |
|   Dict_ContainsVal | Core |    Core |  10000 |    395.703 ms |   4.9541 ms |   4.6341 ms |    393.091 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsVal | Core |    Core |  10000 |    378.763 ms |   4.9371 ms |   4.6182 ms |    377.804 ms |  0.96 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |               |             |             |               |       |         |       |       |       |           |
|   **Dict_ContainsVal** |  **Clr** |     **Clr** | **100000** | **34,834.310 ms** | **304.4786 ms** | **284.8094 ms** | **34,817.288 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsVal |  Clr |     Clr | 100000 | 34,908.730 ms | 343.2186 ms | 321.0469 ms | 34,988.725 ms |  1.00 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |               |             |             |               |       |         |       |       |       |           |
|   Dict_ContainsVal | Core |    Core | 100000 | 37,349.103 ms | 384.7658 ms | 359.9101 ms | 37,424.012 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsVal | Core |    Core | 100000 | 40,555.459 ms | 271.0092 ms | 253.5022 ms | 40,604.130 ms |  1.09 |    0.02 |     - |     - |     - |         - |
