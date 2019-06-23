``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|    Method |  Job | Runtime |       Kind |              Mean |           Error |          StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------- |------------------:|----------------:|----------------:|------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |        **Set** |          **9.216 ns** |       **0.0986 ns** |       **0.0922 ns** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |        Set |          8.389 ns |       0.0493 ns |       0.0461 ns |  0.91 |     - |     - |     - |         - |
|           |      |         |            |                   |                 |                 |       |       |       |       |           |
|   HashSet | Core |    Core |        Set |          8.604 ns |       0.0953 ns |       0.0892 ns |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |        Set |          7.848 ns |       0.0560 ns |       0.0467 ns |  0.91 |     - |     - |     - |         - |
|           |      |         |            |                   |                 |                 |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **12,397,754.129 ns** |  **98,855.2614 ns** |  **87,632.6143 ns** |  **1.00** |     **-** |     **-** |     **-** |   **12168 B** |
| PooledSet |  Clr |     Clr | Enumerable | 12,018,490.000 ns |  95,535.6899 ns |  89,364.1436 ns |  0.97 |     - |     - |     - |   12168 B |
|           |      |         |            |                   |                 |                 |       |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 11,959,406.667 ns |  76,346.8184 ns |  71,414.8613 ns |  1.00 |     - |     - |     - |   12056 B |
| PooledSet | Core |    Core | Enumerable | 12,106,350.208 ns | 154,548.2434 ns | 144,564.5227 ns |  1.01 |     - |     - |     - |   12056 B |
|           |      |         |            |                   |                 |                 |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **12,139,178.683 ns** | **125,697.2588 ns** | **111,427.3459 ns** |  **1.00** |     **-** |     **-** |     **-** |   **12168 B** |
| PooledSet |  Clr |     Clr |      Array | 12,095,923.317 ns | 105,125.3411 ns |  87,784.4209 ns |  1.00 |     - |     - |     - |   12168 B |
|           |      |         |            |                   |                 |                 |       |       |       |       |           |
|   HashSet | Core |    Core |      Array | 11,670,545.647 ns | 112,661.1880 ns |  99,871.2087 ns |  1.00 |     - |     - |     - |   12048 B |
| PooledSet | Core |    Core |      Array | 11,768,128.958 ns |  81,234.0522 ns |  75,986.3828 ns |  1.01 |     - |     - |     - |   12048 B |
