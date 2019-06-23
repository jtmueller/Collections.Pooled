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
|   **HashSet** |  **Clr** |     **Clr** |        **Set** | **1.553 ms** | **0.0550 ms** | **0.1523 ms** | **1.517 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet |  Clr |     Clr |        Set | 1.106 ms | 0.0453 ms | 0.1248 ms | 1.077 ms |  0.72 |    0.10 |     - |     - |     - |    8192 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core |        Set | 1.529 ms | 0.0477 ms | 0.1330 ms | 1.499 ms |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core |        Set | 1.151 ms | 0.0620 ms | 0.1768 ms | 1.086 ms |  0.76 |    0.13 |     - |     - |     - |      40 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **1.731 ms** | **0.0508 ms** | **0.1458 ms** | **1.698 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet |  Clr |     Clr | Enumerable | 1.229 ms | 0.0509 ms | 0.1444 ms | 1.165 ms |  0.71 |    0.10 |     - |     - |     - |    8192 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 1.680 ms | 0.0616 ms | 0.1768 ms | 1.641 ms |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet | Core |    Core | Enumerable | 1.318 ms | 0.0755 ms | 0.2191 ms | 1.223 ms |  0.79 |    0.13 |     - |     - |     - |      40 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **1.648 ms** | **0.0654 ms** | **0.1767 ms** | **1.601 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet |  Clr |     Clr |      Array | 1.257 ms | 0.0652 ms | 0.1859 ms | 1.185 ms |  0.77 |    0.15 |     - |     - |     - |    8192 B |
|           |      |         |            |          |           |           |          |       |         |       |       |       |           |
|   HashSet | Core |    Core |      Array | 1.597 ms | 0.0609 ms | 0.1707 ms | 1.541 ms |  1.00 |    0.00 |     - |     - |     - |      32 B |
| PooledSet | Core |    Core |      Array | 1.240 ms | 0.0590 ms | 0.1634 ms | 1.188 ms |  0.79 |    0.14 |     - |     - |     - |      32 B |
