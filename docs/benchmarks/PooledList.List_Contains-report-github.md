``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|         Method |  Job | Runtime |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |----- |-------- |------- |-----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **ListContains** |  **Clr** |     **Clr** |   **1000** |   **5.616 ms** | **0.1116 ms** | **0.1285 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledContains |  Clr |     Clr |   1000 |   1.950 ms | 0.0327 ms | 0.0290 ms |  0.35 |    0.01 |     - |     - |     - |         - |
|                |      |         |        |            |           |           |       |         |       |       |       |           |
|   ListContains | Core |    Core |   1000 |   1.900 ms | 0.0292 ms | 0.0273 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledContains | Core |    Core |   1000 |   1.904 ms | 0.0377 ms | 0.0370 ms |  1.00 |    0.03 |     - |     - |     - |         - |
|                |      |         |        |            |           |           |       |         |       |       |       |           |
|   **ListContains** |  **Clr** |     **Clr** |  **10000** |  **55.713 ms** | **0.6727 ms** | **0.6292 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledContains |  Clr |     Clr |  10000 |  18.714 ms | 0.1559 ms | 0.1458 ms |  0.34 |    0.00 |     - |     - |     - |         - |
|                |      |         |        |            |           |           |       |         |       |       |       |           |
|   ListContains | Core |    Core |  10000 |  18.590 ms | 0.3481 ms | 0.3256 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledContains | Core |    Core |  10000 |  18.541 ms | 0.3603 ms | 0.3371 ms |  1.00 |    0.02 |     - |     - |     - |         - |
|                |      |         |        |            |           |           |       |         |       |       |       |           |
|   **ListContains** |  **Clr** |     **Clr** | **100000** | **555.064 ms** | **8.1408 ms** | **7.6149 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledContains |  Clr |     Clr | 100000 | 185.463 ms | 2.6663 ms | 2.4941 ms |  0.33 |    0.01 |     - |     - |     - |         - |
|                |      |         |        |            |           |           |       |         |       |       |       |           |
|   ListContains | Core |    Core | 100000 | 183.232 ms | 3.4684 ms | 3.2443 ms |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledContains | Core |    Core | 100000 | 184.298 ms | 3.5296 ms | 3.3016 ms |  1.01 |    0.02 |     - |     - |     - |         - |
