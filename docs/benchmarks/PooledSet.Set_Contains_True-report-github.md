``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|    Method |  Job | Runtime |     N |       Mean |     Error |    StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |------ |-----------:|----------:|----------:|------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |   **100** |   **1.994 us** | **0.0170 us** | **0.0159 us** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |   100 |   1.965 us | 0.0145 us | 0.0136 us |  0.99 |     - |     - |     - |         - |
|           |      |         |       |            |           |           |       |       |       |       |           |
|   HashSet | Core |    Core |   100 |   2.023 us | 0.0105 us | 0.0099 us |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |   100 |   2.025 us | 0.0158 us | 0.0148 us |  1.00 |     - |     - |     - |         - |
|           |      |         |       |            |           |           |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |  **1000** |  **25.604 us** | **0.1483 us** | **0.1315 us** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |  1000 |  26.127 us | 0.1812 us | 0.1695 us |  1.02 |     - |     - |     - |         - |
|           |      |         |       |            |           |           |       |       |       |       |           |
|   HashSet | Core |    Core |  1000 |  27.391 us | 0.1838 us | 0.1719 us |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |  1000 |  26.079 us | 0.1783 us | 0.1668 us |  0.95 |     - |     - |     - |         - |
|           |      |         |       |            |           |           |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **10000** | **292.173 us** | **2.0299 us** | **1.8987 us** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr | 10000 | 298.492 us | 4.3441 us | 4.0635 us |  1.02 |     - |     - |     - |         - |
|           |      |         |       |            |           |           |       |       |       |       |           |
|   HashSet | Core |    Core | 10000 | 308.915 us | 2.5316 us | 2.3680 us |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core | 10000 | 297.864 us | 2.3446 us | 2.1931 us |  0.96 |     - |     - |     - |         - |
