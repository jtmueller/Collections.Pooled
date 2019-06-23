``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|      Method |  Job | Runtime |     N |       Kind |         Mean |      Error |     StdDev | Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 |    Allocated |
|------------ |----- |-------- |------ |----------- |-------------:|-----------:|-----------:|------:|--------:|-----------:|------:|------:|-------------:|
|       **Stack** |  **Clr** |     **Clr** |  **1000** | **Collection** |     **452.1 us** |   **4.685 us** |   **4.382 us** |  **1.00** |    **0.00** |  **1294.9219** |     **-** |     **-** |    **3984.4 KB** |
| PooledStack |  Clr |     Clr |  1000 | Collection |   1,251.5 us |  24.657 us |  39.817 us |  2.83 |    0.10 |    17.5781 |     - |     - |     54.86 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       Stack | Core |    Core |  1000 | Collection |     478.8 us |   8.675 us |   8.114 us |  1.00 |    0.00 |  1291.9922 |     - |     - |   3960.94 KB |
| PooledStack | Core |    Core |  1000 | Collection |     168.5 us |   2.666 us |   2.363 us |  0.35 |    0.01 |    17.8223 |     - |     - |     54.69 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       **Stack** |  **Clr** |     **Clr** |  **1000** | **Enumerable** |   **9,447.6 us** | **106.916 us** | **100.010 us** |  **1.00** |    **0.00** |  **2687.5000** |     **-** |     **-** |   **8299.77 KB** |
| PooledStack |  Clr |     Clr |  1000 | Enumerable |  10,881.8 us |  91.299 us |  76.239 us |  1.15 |    0.01 |    46.8750 |     - |     - |    148.88 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       Stack | Core |    Core |  1000 | Enumerable |   7,992.7 us | 158.087 us | 221.615 us |  1.00 |    0.00 |  2695.3125 |     - |     - |   8265.63 KB |
| PooledStack | Core |    Core |  1000 | Enumerable |   7,871.7 us | 154.875 us | 195.868 us |  0.99 |    0.04 |    46.8750 |     - |     - |    148.44 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       **Stack** |  **Clr** |     **Clr** | **10000** | **Collection** |   **4,469.3 us** |  **89.208 us** | **203.171 us** |  **1.00** |    **0.00** | **12656.2500** |     **-** |     **-** |  **39210.69 KB** |
| PooledStack |  Clr |     Clr | 10000 | Collection |  17,387.6 us | 211.472 us | 187.464 us |  3.73 |    0.21 |          - |     - |     - |        55 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       Stack | Core |    Core | 10000 | Collection |   4,377.8 us |  79.998 us |  74.830 us |  1.00 |    0.00 | 12656.2500 |     - |     - |  39117.19 KB |
| PooledStack | Core |    Core | 10000 | Collection |   1,373.9 us |  10.013 us |   9.366 us |  0.31 |    0.01 |    17.5781 |     - |     - |     54.69 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       **Stack** |  **Clr** |     **Clr** | **10000** | **Enumerable** |  **92,240.6 us** | **800.420 us** | **748.713 us** |  **1.00** |    **0.00** | **41500.0000** |     **-** |     **-** | **128520.83 KB** |
| PooledStack |  Clr |     Clr | 10000 | Enumerable | 118,867.3 us | 740.891 us | 656.780 us |  1.29 |    0.01 |          - |     - |     - |     150.4 KB |
|             |      |         |       |            |              |            |            |       |         |            |       |       |              |
|       Stack | Core |    Core | 10000 | Enumerable |  77,693.3 us | 805.281 us | 753.260 us |  1.00 |    0.00 | 41571.4286 |     - |     - | 128359.38 KB |
| PooledStack | Core |    Core | 10000 | Enumerable |  76,372.9 us | 361.805 us | 320.731 us |  0.98 |    0.01 |          - |     - |     - |    148.44 KB |
