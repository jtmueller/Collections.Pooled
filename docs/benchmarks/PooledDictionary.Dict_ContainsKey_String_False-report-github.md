``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      N |        Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |------- |------------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |   **1000** |    **12.88 us** | **0.3011 us** | **0.3916 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **40 B** |
| Pooled_ContainsKey |  Clr |     Clr |   1000 |    25.21 us | 0.4474 us | 0.4185 us |  1.95 |    0.08 |     - |     - |     - |      40 B |
|                    |      |         |        |             |           |           |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |   1000 |    10.59 us | 0.1518 us | 0.1345 us |  1.00 |    0.00 |     - |     - |     - |      32 B |
| Pooled_ContainsKey | Core |    Core |   1000 |    11.05 us | 0.1561 us | 0.1460 us |  1.04 |    0.02 |     - |     - |     - |      32 B |
|                    |      |         |        |             |           |           |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |  **10000** |   **145.69 us** | **2.9818 us** | **4.1801 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **42 B** |
| Pooled_ContainsKey |  Clr |     Clr |  10000 |   213.69 us | 1.3414 us | 1.2548 us |  1.45 |    0.05 |     - |     - |     - |      42 B |
|                    |      |         |        |             |           |           |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |  10000 |   107.99 us | 0.9544 us | 0.8928 us |  1.00 |    0.00 |     - |     - |     - |      32 B |
| Pooled_ContainsKey | Core |    Core |  10000 |   108.53 us | 0.4077 us | 0.3813 us |  1.01 |    0.01 |     - |     - |     - |      32 B |
|                    |      |         |        |             |           |           |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** | **100000** | **1,574.03 us** | **8.6724 us** | **8.1121 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **48 B** |
| Pooled_ContainsKey |  Clr |     Clr | 100000 | 2,134.24 us | 5.6764 us | 5.3097 us |  1.36 |    0.01 |     - |     - |     - |      64 B |
|                    |      |         |        |             |           |           |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core | 100000 | 1,079.28 us | 6.8137 us | 5.3197 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| Pooled_ContainsKey | Core |    Core | 100000 | 1,146.78 us | 8.5043 us | 7.1015 us |  1.06 |    0.01 |     - |     - |     - |      40 B |
