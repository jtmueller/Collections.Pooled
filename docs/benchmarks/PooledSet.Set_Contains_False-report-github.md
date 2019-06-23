``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|    Method |  Job | Runtime |     N |        Mean |      Error |     StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |------ |------------:|-----------:|-----------:|------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |   **100** |    **952.8 ns** |   **3.852 ns** |   **3.603 ns** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |   100 |    899.4 ns |   4.996 ns |   4.673 ns |  0.94 |     - |     - |     - |         - |
|           |      |         |       |             |            |            |       |       |       |       |           |
|   HashSet | Core |    Core |   100 |    910.6 ns |   4.719 ns |   4.183 ns |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |   100 |  1,016.5 ns |   3.965 ns |   3.515 ns |  1.12 |     - |     - |     - |         - |
|           |      |         |       |             |            |            |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |  **1000** |  **9,357.1 ns** |  **50.792 ns** |  **47.511 ns** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |  1000 |  8,901.6 ns |  70.105 ns |  65.576 ns |  0.95 |     - |     - |     - |         - |
|           |      |         |       |             |            |            |       |       |       |       |           |
|   HashSet | Core |    Core |  1000 |  9,008.1 ns |  81.657 ns |  76.382 ns |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |  1000 |  8,849.9 ns |  48.825 ns |  45.671 ns |  0.98 |     - |     - |     - |         - |
|           |      |         |       |             |            |            |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **10000** | **90,866.6 ns** | **447.535 ns** | **418.624 ns** |  **1.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr | 10000 | 88,410.7 ns | 537.986 ns | 503.232 ns |  0.97 |     - |     - |     - |         - |
|           |      |         |       |             |            |            |       |       |       |       |           |
|   HashSet | Core |    Core | 10000 | 90,224.5 ns | 554.238 ns | 518.434 ns |  1.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core | 10000 | 95,908.0 ns | 602.085 ns | 563.190 ns |  1.06 |     - |     - |     - |         - |
