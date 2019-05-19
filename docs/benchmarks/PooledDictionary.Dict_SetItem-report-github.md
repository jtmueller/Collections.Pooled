``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|        Method |  Job | Runtime |      N |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |------- |---------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **DictSetItem** |  **Clr** |     **Clr** |   **1000** | **974.8 us** | **14.437 us** | **13.504 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSetItem |  Clr |     Clr |   1000 | 818.7 us | 14.790 us | 13.834 us |  0.84 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictSetItem | Core |    Core |   1000 | 679.9 us | 12.012 us | 11.236 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSetItem | Core |    Core |   1000 | 713.3 us |  8.274 us |  7.739 us |  1.05 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictSetItem** |  **Clr** |     **Clr** |  **10000** | **979.8 us** |  **9.941 us** |  **9.299 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSetItem |  Clr |     Clr |  10000 | 834.5 us |  5.472 us |  4.851 us |  0.85 |    0.01 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictSetItem | Core |    Core |  10000 | 814.0 us |  6.376 us |  5.964 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSetItem | Core |    Core |  10000 | 692.0 us |  7.564 us |  6.316 us |  0.85 |    0.01 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictSetItem** |  **Clr** |     **Clr** | **100000** | **964.7 us** | **18.526 us** | **19.025 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSetItem |  Clr |     Clr | 100000 | 823.3 us | 11.988 us | 11.214 us |  0.85 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictSetItem | Core |    Core | 100000 | 685.4 us | 10.181 us |  9.523 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSetItem | Core |    Core | 100000 | 712.4 us |  8.169 us |  7.641 us |  1.04 |    0.02 |     - |     - |     - |         - |
