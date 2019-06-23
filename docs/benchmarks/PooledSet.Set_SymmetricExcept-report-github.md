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
|    Method |  Job | Runtime |       Kind |     Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------- |---------:|----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |        **Set** | **1.414 ms** | **0.0483 ms** | **0.1353 ms** | **1.3844 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |        Set | 1.049 ms | 0.0540 ms | 0.1513 ms | 0.9958 ms |  0.75 |    0.12 |     - |     - |     - |         - |
|           |      |         |            |          |           |           |           |       |         |       |       |       |           |
|   HashSet | Core |    Core |        Set | 1.452 ms | 0.0652 ms | 0.1839 ms | 1.4095 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |        Set | 1.118 ms | 0.0762 ms | 0.2174 ms | 1.0284 ms |  0.78 |    0.17 |     - |     - |     - |         - |
|           |      |         |            |          |           |           |           |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **3.770 ms** | **0.1151 ms** | **0.3320 ms** | **3.6440 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **33296 B** |
| PooledSet |  Clr |     Clr | Enumerable | 2.978 ms | 0.1261 ms | 0.3597 ms | 2.8102 ms |  0.80 |    0.13 |     - |     - |     - |   33296 B |
|           |      |         |            |          |           |           |           |       |         |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 3.492 ms | 0.1355 ms | 0.3932 ms | 3.3649 ms |  1.00 |    0.00 |     - |     - |     - |   25096 B |
| PooledSet | Core |    Core | Enumerable | 2.799 ms | 0.0942 ms | 0.2641 ms | 2.7264 ms |  0.81 |    0.11 |     - |     - |     - |   25096 B |
|           |      |         |            |          |           |           |           |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **3.794 ms** | **0.1056 ms** | **0.2926 ms** | **3.7377 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **33296 B** |
| PooledSet |  Clr |     Clr |      Array | 2.985 ms | 0.1030 ms | 0.2956 ms | 2.8755 ms |  0.79 |    0.10 |     - |     - |     - |   33296 B |
|           |      |         |            |          |           |           |           |       |         |       |       |       |           |
|   HashSet | Core |    Core |      Array | 3.178 ms | 0.0792 ms | 0.2196 ms | 3.1201 ms |  1.00 |    0.00 |     - |     - |     - |   25088 B |
| PooledSet | Core |    Core |      Array | 2.718 ms | 0.0733 ms | 0.2080 ms | 2.6538 ms |  0.86 |    0.09 |     - |     - |     - |   25088 B |
