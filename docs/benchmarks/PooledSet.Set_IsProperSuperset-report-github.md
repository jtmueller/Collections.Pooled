``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|    Method |  Job | Runtime |       Kind |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------- |------------:|----------:|----------:|------------:|------:|--------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |        **Set** |    **920.2 us** |  **4.122 us** |  **3.654 us** |    **919.5 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **48 B** |
| PooledSet |  Clr |     Clr |        Set |    958.5 us |  8.128 us |  7.603 us |    959.0 us |  1.04 |    0.01 |     - |     - |     - |      48 B |
|           |      |         |            |             |           |           |             |       |         |       |       |       |           |
|   HashSet | Core |    Core |        Set |    948.8 us | 18.900 us | 32.601 us |    931.0 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core |        Set |    945.3 us | 18.452 us | 20.510 us |    954.1 us |  0.98 |    0.02 |     - |     - |     - |      40 B |
|           |      |         |            |             |           |           |             |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **12,181.9 us** | **84.964 us** | **75.318 us** | **12,170.1 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **3584 B** |
| PooledSet |  Clr |     Clr | Enumerable | 11,681.3 us | 64.005 us | 59.871 us | 11,688.5 us |  0.96 |    0.01 |     - |     - |     - |    3584 B |
|           |      |         |            |             |           |           |             |       |         |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 11,811.2 us | 70.757 us | 66.186 us | 11,805.1 us |  1.00 |    0.00 |     - |     - |     - |    3488 B |
| PooledSet | Core |    Core | Enumerable | 11,497.0 us | 72.405 us | 60.462 us | 11,492.9 us |  0.97 |    0.01 |     - |     - |     - |    3488 B |
|           |      |         |            |             |           |           |             |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **12,141.1 us** | **70.829 us** | **66.254 us** | **12,143.6 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **3584 B** |
| PooledSet |  Clr |     Clr |      Array | 11,745.2 us | 79.177 us | 74.062 us | 11,726.4 us |  0.97 |    0.01 |     - |     - |     - |    3584 B |
|           |      |         |            |             |           |           |             |       |         |       |       |       |           |
|   HashSet | Core |    Core |      Array | 11,605.3 us | 51.171 us | 47.866 us | 11,599.7 us |  1.00 |    0.00 |     - |     - |     - |    3480 B |
| PooledSet | Core |    Core |      Array | 11,281.0 us | 74.326 us | 69.524 us | 11,271.1 us |  0.97 |    0.01 |     - |     - |     - |    3480 B |
