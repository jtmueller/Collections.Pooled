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
|   **HashSet** |  **Clr** |     **Clr** |        **Set** | **1.517 ms** | **0.0531 ms** | **0.1480 ms** | **1.480 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet |  Clr |     Clr |        Set | 1.084 ms | 0.0340 ms | 0.0953 ms | 1.055 ms |  0.72 |    0.09 |     - |     - |     - |    8192 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core |        Set | 1.594 ms | 0.1046 ms | 0.3017 ms | 1.467 ms |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core |        Set | 1.176 ms | 0.0570 ms | 0.1634 ms | 1.124 ms |  0.76 |    0.15 |     - |     - |     - |      40 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **1.719 ms** | **0.0466 ms** | **0.1267 ms** | **1.692 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet |  Clr |     Clr | Enumerable | 1.296 ms | 0.0268 ms | 0.0712 ms | 1.286 ms |  0.76 |    0.07 |     - |     - |     - |    8192 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 1.720 ms | 0.0827 ms | 0.2319 ms | 1.639 ms |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core | Enumerable | 1.307 ms | 0.0512 ms | 0.1418 ms | 1.248 ms |  0.77 |    0.13 |     - |     - |     - |      40 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **1.738 ms** | **0.0574 ms** | **0.1619 ms** | **1.674 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet |  Clr |     Clr |      Array | 1.332 ms | 0.0527 ms | 0.1468 ms | 1.283 ms |  0.77 |    0.11 |     - |     - |     - |    8192 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core |      Array | 1.728 ms | 0.0613 ms | 0.1760 ms | 1.690 ms |  1.00 |    0.00 |     - |     - |     - |      32 B |
| PooledSet | Core |    Core |      Array | 1.374 ms | 0.0680 ms | 0.1940 ms | 1.304 ms |  0.80 |    0.14 |     - |     - |     - |      32 B |
