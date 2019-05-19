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
|   **DictGetItem** |  **Clr** |     **Clr** |   **1000** | **809.1 us** |  **7.306 us** |  **6.834 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledGetItem |  Clr |     Clr |   1000 | 660.2 us |  7.951 us |  7.438 us |  0.82 |    0.01 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictGetItem | Core |    Core |   1000 | 580.1 us |  7.564 us |  7.075 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledGetItem | Core |    Core |   1000 | 536.1 us | 10.706 us | 11.456 us |  0.92 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictGetItem** |  **Clr** |     **Clr** |  **10000** | **799.8 us** | **12.976 us** | **12.137 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledGetItem |  Clr |     Clr |  10000 | 655.0 us | 11.549 us | 10.803 us |  0.82 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictGetItem | Core |    Core |  10000 | 607.8 us |  8.263 us |  7.729 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledGetItem | Core |    Core |  10000 | 541.7 us |  8.624 us |  8.067 us |  0.89 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictGetItem** |  **Clr** |     **Clr** | **100000** | **803.8 us** | **11.964 us** | **11.192 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledGetItem |  Clr |     Clr | 100000 | 652.0 us | 11.496 us | 10.754 us |  0.81 |    0.01 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictGetItem | Core |    Core | 100000 | 578.7 us |  9.368 us |  8.763 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledGetItem | Core |    Core | 100000 | 534.6 us | 10.162 us | 11.295 us |  0.92 |    0.02 |     - |     - |     - |         - |
