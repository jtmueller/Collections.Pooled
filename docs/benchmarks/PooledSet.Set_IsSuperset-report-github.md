``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|    Method |  Job | Runtime |       Kind |        Mean |      Error |     StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------- |------------:|-----------:|-----------:|------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |        **Set** |    **929.0 us** |   **8.374 us** |   **7.833 us** |  **1.00** |     **-** |     **-** |     **-** |      **48 B** |
| PooledSet |  Clr |     Clr |        Set |    955.4 us |   6.793 us |   6.022 us |  1.03 |     - |     - |     - |      48 B |
|           |      |         |            |             |            |            |       |       |       |       |           |
|   HashSet | Core |    Core |        Set |    931.3 us |   6.456 us |   6.039 us |  1.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core |        Set |    965.8 us |   8.771 us |   8.204 us |  1.04 |     - |     - |     - |      40 B |
|           |      |         |            |             |            |            |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **10,608.8 us** |  **84.619 us** |  **79.153 us** |  **1.00** |     **-** |     **-** |     **-** |     **128 B** |
| PooledSet |  Clr |     Clr | Enumerable | 10,844.6 us | 102.552 us |  95.927 us |  1.02 |     - |     - |     - |     128 B |
|           |      |         |            |             |            |            |       |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 10,508.4 us |  52.658 us |  49.256 us |  1.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core | Enumerable | 10,598.0 us |  60.290 us |  56.395 us |  1.01 |     - |     - |     - |      40 B |
|           |      |         |            |             |            |            |       |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **10,194.9 us** |  **90.928 us** |  **85.054 us** |  **1.00** |     **-** |     **-** |     **-** |     **128 B** |
| PooledSet |  Clr |     Clr |      Array | 10,645.6 us |  95.085 us |  74.236 us |  1.04 |     - |     - |     - |     128 B |
|           |      |         |            |             |            |            |       |       |       |       |           |
|   HashSet | Core |    Core |      Array | 10,350.7 us | 131.561 us | 123.062 us |  1.00 |     - |     - |     - |      32 B |
| PooledSet | Core |    Core |      Array | 10,541.6 us |  58.071 us |  51.478 us |  1.02 |     - |     - |     - |      32 B |
