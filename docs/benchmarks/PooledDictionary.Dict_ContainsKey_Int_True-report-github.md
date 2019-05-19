``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      N |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |------- |-----------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |   **1000** |   **9.424 us** |  **0.1490 us** |  **0.1394 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsKey |  Clr |     Clr |   1000 |   7.440 us |  0.0489 us |  0.0457 us |  0.79 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |            |            |            |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |   1000 |   7.143 us |  0.1424 us |  0.2379 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsKey | Core |    Core |   1000 |   6.279 us |  0.1227 us |  0.1759 us |  0.87 |    0.04 |     - |     - |     - |         - |
|                    |      |         |        |            |            |            |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** |  **10000** |  **90.414 us** |  **1.7045 us** |  **1.5944 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsKey |  Clr |     Clr |  10000 |  71.492 us |  1.3930 us |  1.6583 us |  0.79 |    0.03 |     - |     - |     - |         - |
|                    |      |         |        |            |            |            |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core |  10000 |  63.967 us |  0.7934 us |  0.7421 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsKey | Core |    Core |  10000 |  61.796 us |  1.7779 us |  1.5761 us |  0.97 |    0.03 |     - |     - |     - |         - |
|                    |      |         |        |            |            |            |       |         |       |       |       |           |
|   **Dict_ContainsKey** |  **Clr** |     **Clr** | **100000** | **911.141 us** |  **7.1964 us** |  **6.3794 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_ContainsKey |  Clr |     Clr | 100000 | 734.064 us | 12.9171 us | 12.0827 us |  0.81 |    0.01 |     - |     - |     - |         - |
|                    |      |         |        |            |            |            |       |         |       |       |       |           |
|   Dict_ContainsKey | Core |    Core | 100000 | 661.875 us | 10.6443 us |  9.9567 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_ContainsKey | Core |    Core | 100000 | 654.034 us | 12.9410 us | 16.3662 us |  0.98 |    0.03 |     - |     - |     - |         - |
