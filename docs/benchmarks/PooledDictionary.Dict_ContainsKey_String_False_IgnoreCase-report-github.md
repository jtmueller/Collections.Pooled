``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      N |        Mean |      Error |      StdDev |      Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |------- |------------:|-----------:|------------:|------------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |   **1000** |    **27.84 us** |  **0.4091 us** |   **0.3827 us** |    **28.04 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **40 B** |
| Pooled_ContainsKey |  Clr |     Clr |   1000 |    26.56 us |  0.5251 us |   0.5157 us |    26.59 us |  0.95 |    0.03 |     - |     - |     - |      40 B |
|                    |      |         |        |             |            |             |             |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |   1000 |    15.83 us |  0.2403 us |   0.2247 us |    15.92 us |  1.00 |    0.00 |     - |     - |     - |      32 B |
| Pooled_ContainsKey | Core |    Core |   1000 |    16.13 us |  0.2319 us |   0.2169 us |    16.17 us |  1.02 |    0.02 |     - |     - |     - |      32 B |
|                    |      |         |        |             |            |             |             |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |  **10000** |   **282.00 us** |  **1.0478 us** |   **0.9801 us** |   **282.05 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **44 B** |
| Pooled_ContainsKey |  Clr |     Clr |  10000 |   273.89 us |  1.5677 us |   1.3091 us |   273.43 us |  0.97 |    0.01 |     - |     - |     - |      44 B |
|                    |      |         |        |             |            |             |             |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |  10000 |   171.71 us |  0.5441 us |   0.5089 us |   171.71 us |  1.00 |    0.00 |     - |     - |     - |      32 B |
| Pooled_ContainsKey | Core |    Core |  10000 |   209.45 us |  4.1163 us |   5.3524 us |   211.24 us |  1.21 |    0.03 |     - |     - |     - |      32 B |
|                    |      |         |        |             |            |             |             |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** | **100000** | **2,983.40 us** | **39.0932 us** |  **36.5678 us** | **2,995.09 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **62 B** |
| Pooled_ContainsKey |  Clr |     Clr | 100000 | 2,872.42 us | 44.4022 us |  41.5339 us | 2,884.51 us |  0.96 |    0.02 |     - |     - |     - |      64 B |
|                    |      |         |        |             |            |             |             |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core | 100000 | 2,213.54 us | 26.8260 us |  25.0931 us | 2,222.19 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| Pooled_ContainsKey | Core |    Core | 100000 | 2,265.00 us | 44.9656 us | 106.8655 us | 2,229.03 us |  1.05 |    0.06 |     - |     - |     - |      40 B |
