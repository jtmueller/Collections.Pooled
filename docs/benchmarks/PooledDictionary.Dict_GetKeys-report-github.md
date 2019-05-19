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
|   **DictGetKeys** |  **Clr** |     **Clr** |   **1000** | **62.00 us** | **0.9378 us** | **0.8772 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledGetKeys |  Clr |     Clr |   1000 | 80.19 us | 1.5389 us | 1.4395 us |  1.29 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictGetKeys | Core |    Core |   1000 | 61.83 us | 0.7999 us | 0.7482 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledGetKeys | Core |    Core |   1000 | 73.86 us | 1.4299 us | 1.3376 us |  1.19 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictGetKeys** |  **Clr** |     **Clr** |  **10000** | **61.52 us** | **1.1497 us** | **1.0755 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledGetKeys |  Clr |     Clr |  10000 | 79.09 us | 1.2944 us | 1.2108 us |  1.29 |    0.03 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictGetKeys | Core |    Core |  10000 | 61.74 us | 0.9596 us | 0.8976 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledGetKeys | Core |    Core |  10000 | 74.26 us | 0.9041 us | 0.8457 us |  1.20 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictGetKeys** |  **Clr** |     **Clr** | **100000** | **62.23 us** | **0.8676 us** | **0.8116 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledGetKeys |  Clr |     Clr | 100000 | 81.51 us | 1.5407 us | 1.5821 us |  1.31 |    0.03 |     - |     - |     - |         - |
|               |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictGetKeys | Core |    Core | 100000 | 92.86 us | 0.9894 us | 0.9255 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledGetKeys | Core |    Core | 100000 | 80.68 us | 0.8836 us | 0.8265 us |  0.87 |    0.01 |     - |     - |     - |         - |
