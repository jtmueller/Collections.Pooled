``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime |      N |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |----- |-------- |------- |---------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **DictTryGetValue** |  **Clr** |     **Clr** |   **1000** | **81.31 us** | **1.4901 us** | **1.3939 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryGetValue |  Clr |     Clr |   1000 | 69.03 us | 1.3188 us | 1.2952 us |  0.85 |    0.02 |     - |     - |     - |         - |
|                   |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictTryGetValue | Core |    Core |   1000 | 67.53 us | 1.3101 us | 1.2867 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryGetValue | Core |    Core |   1000 | 63.42 us | 1.2083 us | 1.2929 us |  0.94 |    0.02 |     - |     - |     - |         - |
|                   |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictTryGetValue** |  **Clr** |     **Clr** |  **10000** | **81.42 us** | **1.1352 us** | **1.0619 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryGetValue |  Clr |     Clr |  10000 | 69.21 us | 1.2585 us | 1.1772 us |  0.85 |    0.02 |     - |     - |     - |         - |
|                   |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictTryGetValue | Core |    Core |  10000 | 67.61 us | 1.3060 us | 1.3974 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryGetValue | Core |    Core |  10000 | 63.62 us | 1.2508 us | 1.2844 us |  0.94 |    0.03 |     - |     - |     - |         - |
|                   |      |         |        |          |           |           |       |         |       |       |       |           |
|   **DictTryGetValue** |  **Clr** |     **Clr** | **100000** | **81.62 us** | **1.2241 us** | **1.1450 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryGetValue |  Clr |     Clr | 100000 | 69.55 us | 1.1374 us | 1.0639 us |  0.85 |    0.01 |     - |     - |     - |         - |
|                   |      |         |        |          |           |           |       |         |       |       |       |           |
|   DictTryGetValue | Core |    Core | 100000 | 67.65 us | 1.1402 us | 1.0666 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryGetValue | Core |    Core | 100000 | 63.63 us | 0.9990 us | 0.9344 us |  0.94 |    0.02 |     - |     - |     - |         - |
