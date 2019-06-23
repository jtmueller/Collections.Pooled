``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|    Method |  Job | Runtime |       Kind |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------- |---------:|----------:|----------:|---------:|------:|--------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |        **Set** | **6.598 ms** | **0.2037 ms** | **0.5908 ms** | **6.452 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |        Set | 6.037 ms | 0.1637 ms | 0.4750 ms | 5.946 ms |  0.92 |    0.11 |     - |     - |     - |         - |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core |        Set | 6.536 ms | 0.2007 ms | 0.5887 ms | 6.425 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |        Set | 6.226 ms | 0.2067 ms | 0.6062 ms | 6.028 ms |  0.96 |    0.12 |     - |     - |     - |         - |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **4.766 ms** | **0.2188 ms** | **0.6416 ms** | **4.504 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **20744 B** |
| PooledSet |  Clr |     Clr | Enumerable | 4.008 ms | 0.1418 ms | 0.4024 ms | 3.861 ms |  0.85 |    0.12 |     - |     - |     - |   20744 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 4.451 ms | 0.1496 ms | 0.4315 ms | 4.380 ms |  1.00 |    0.00 |     - |     - |     - |   12568 B |
| PooledSet | Core |    Core | Enumerable | 3.809 ms | 0.1159 ms | 0.3287 ms | 3.718 ms |  0.86 |    0.11 |     - |     - |     - |   12568 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **4.556 ms** | **0.1393 ms** | **0.3884 ms** | **4.533 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **20744 B** |
| PooledSet |  Clr |     Clr |      Array | 4.070 ms | 0.1671 ms | 0.4821 ms | 3.904 ms |  0.89 |    0.12 |     - |     - |     - |   20744 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core |      Array | 4.351 ms | 0.1636 ms | 0.4721 ms | 4.206 ms |  1.00 |    0.00 |     - |     - |     - |   12560 B |
| PooledSet | Core |    Core |      Array | 3.961 ms | 0.1418 ms | 0.4070 ms | 3.809 ms |  0.92 |    0.14 |     - |     - |     - |   12560 B |
