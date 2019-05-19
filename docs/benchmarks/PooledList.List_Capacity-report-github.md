``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime |      N |         Mean |       Error |      StdDev | Ratio | RatioSD |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|------------------ |----- |-------- |------- |-------------:|------------:|------------:|------:|--------:|----------:|----------:|----------:|-----------:|
|   **ListSetCapacity** |  **Clr** |     **Clr** |   **1000** |     **26.94 us** |   **0.3940 us** |   **0.3685 us** |  **1.00** |    **0.00** |  **128.5095** |         **-** |         **-** |   **404856 B** |
| PooledSetCapacity |  Clr |     Clr |   1000 |    101.39 us |   1.6720 us |   1.5640 us |  3.76 |    0.06 |         - |         - |         - |          - |
|                   |      |         |        |              |             |             |       |         |           |           |           |            |
|   ListSetCapacity | Core |    Core |   1000 |     26.74 us |   0.2260 us |   0.2114 us |  1.00 |    0.00 |  128.5095 |         - |         - |   403200 B |
| PooledSetCapacity | Core |    Core |   1000 |     12.21 us |   0.1291 us |   0.1208 us |  0.46 |    0.01 |         - |         - |         - |          - |
|                   |      |         |        |              |             |             |       |         |           |           |           |            |
|   **ListSetCapacity** |  **Clr** |     **Clr** |  **10000** |    **309.37 us** |   **3.3239 us** |   **2.9465 us** |  **1.00** |    **0.00** | **1265.6250** |         **-** |         **-** |  **4005600 B** |
| PooledSetCapacity |  Clr |     Clr |  10000 |  1,524.75 us |  11.2652 us |  10.5375 us |  4.93 |    0.06 |         - |         - |         - |          - |
|                   |      |         |        |              |             |             |       |         |           |           |           |            |
|   ListSetCapacity | Core |    Core |  10000 |    309.94 us |   4.2924 us |   4.0151 us |  1.00 |    0.00 | 1265.6250 |         - |         - |  4003200 B |
| PooledSetCapacity | Core |    Core |  10000 |    108.40 us |   1.8764 us |   1.6634 us |  0.35 |    0.01 |         - |         - |         - |          - |
|                   |      |         |        |              |             |             |       |         |           |           |           |            |
|   **ListSetCapacity** |  **Clr** |     **Clr** | **100000** |  **7,285.50 us** | **144.5850 us** | **142.0018 us** |  **1.00** |    **0.00** | **7789.0625** | **7789.0625** | **7789.0625** | **40003200 B** |
| PooledSetCapacity |  Clr |     Clr | 100000 | 12,822.77 us | 213.4884 us | 199.6971 us |  1.76 |    0.05 |         - |         - |         - |          - |
|                   |      |         |        |              |             |             |       |         |           |           |           |            |
|   ListSetCapacity | Core |    Core | 100000 |  7,349.97 us | 140.8365 us | 150.6935 us |  1.00 |    0.00 | 8406.2500 | 8406.2500 | 8406.2500 | 40003200 B |
| PooledSetCapacity | Core |    Core | 100000 |  1,497.09 us |  29.8759 us |  33.2070 us |  0.20 |    0.01 |         - |         - |         - |          - |
