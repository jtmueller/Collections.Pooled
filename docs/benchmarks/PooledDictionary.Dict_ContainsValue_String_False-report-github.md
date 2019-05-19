``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|          Method |  Job | Runtime |      N |          Mean |         Error |        StdDev |        Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |----- |-------- |------- |--------------:|--------------:|--------------:|--------------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_Contains** |  **Clr** |     **Clr** |   **1000** |      **5.566 ms** |     **0.0974 ms** |     **0.0912 ms** |      **5.526 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **64 B** |
| Pooled_Contains |  Clr |     Clr |   1000 |      5.607 ms |     0.1026 ms |     0.0960 ms |      5.658 ms |  1.01 |    0.03 |     - |     - |     - |      64 B |
|                 |      |         |        |               |               |               |               |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |   1000 |      5.607 ms |     0.1113 ms |     0.1237 ms |      5.592 ms |  1.00 |    0.00 |     - |     - |     - |      32 B |
| Pooled_Contains | Core |    Core |   1000 |      5.054 ms |     0.0614 ms |     0.0574 ms |      5.024 ms |  0.90 |    0.02 |     - |     - |     - |      32 B |
|                 |      |         |        |               |               |               |               |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** |  **10000** |    **564.701 ms** |     **1.8639 ms** |     **1.6523 ms** |    **564.994 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| Pooled_Contains |  Clr |     Clr |  10000 |    564.958 ms |     2.3967 ms |     2.2418 ms |    564.898 ms |  1.00 |    0.01 |     - |     - |     - |    8192 B |
|                 |      |         |        |               |               |               |               |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |  10000 |    491.954 ms |     1.7671 ms |     1.6529 ms |    492.373 ms |  1.00 |    0.00 |     - |     - |     - |      32 B |
| Pooled_Contains | Core |    Core |  10000 |    502.660 ms |     2.2173 ms |     1.8516 ms |    501.965 ms |  1.02 |    0.00 |     - |     - |     - |      32 B |
|                 |      |         |        |               |               |               |               |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** | **100000** | **60,923.542 ms** | **1,209.2130 ms** | **1,293.8443 ms** | **60,656.055 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| Pooled_Contains |  Clr |     Clr | 100000 | 60,307.667 ms | 1,274.1689 ms | 2,659.6649 ms | 59,051.353 ms |  1.01 |    0.05 |     - |     - |     - |    8192 B |
|                 |      |         |        |               |               |               |               |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core | 100000 | 51,908.897 ms |   997.1292 ms | 1,108.3065 ms | 51,625.677 ms |  1.00 |    0.00 |     - |     - |     - |      40 B |
| Pooled_Contains | Core |    Core | 100000 | 52,817.476 ms |   378.8521 ms |   335.8425 ms | 52,864.931 ms |  1.02 |    0.02 |     - |     - |     - |      40 B |
