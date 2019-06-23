``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|     Method |  Job | Runtime |     N |   Type |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|----------- |----- |-------- |------ |------- |-----------:|----------:|-----------:|-----------:|------:|--------:|--------:|--------:|--------:|----------:|
|  **StackPush** |  **Clr** |     **Clr** |  **1000** |    **Int** |   **4.723 us** | **0.0846 us** |  **0.0661 us** |   **4.732 us** |  **1.00** |    **0.00** |  **2.6855** |       **-** |       **-** |    **8459 B** |
| PooledPush |  Clr |     Clr |  1000 |    Int |   5.233 us | 0.0558 us |  0.0522 us |   5.246 us |  1.11 |    0.02 |  0.0153 |       - |       - |      56 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  StackPush | Core |    Core |  1000 |    Int |   3.485 us | 0.0350 us |  0.0328 us |   3.486 us |  1.00 |    0.00 |  2.6817 |       - |       - |    8424 B |
| PooledPush | Core |    Core |  1000 |    Int |   2.908 us | 0.0298 us |  0.0279 us |   2.903 us |  0.83 |    0.01 |  0.0153 |       - |       - |      56 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  **StackPush** |  **Clr** |     **Clr** |  **1000** | **String** |   **7.566 us** | **0.0924 us** |  **0.0864 us** |   **7.553 us** |  **1.00** |    **0.00** |  **5.2872** |       **-** |       **-** |   **16662 B** |
| PooledPush |  Clr |     Clr |  1000 | String |  10.917 us | 0.2140 us |  0.3967 us |  10.841 us |  1.43 |    0.05 |  0.0153 |       - |       - |      56 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  StackPush | Core |    Core |  1000 | String |   7.448 us | 0.1470 us |  0.1510 us |   7.475 us |  1.00 |    0.00 |  5.2872 |       - |       - |   16600 B |
| PooledPush | Core |    Core |  1000 | String |   6.731 us | 0.1360 us |  0.2985 us |   6.670 us |  0.91 |    0.04 |  0.0153 |       - |       - |      56 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  **StackPush** |  **Clr** |     **Clr** | **10000** |    **Int** |  **50.229 us** | **1.0527 us** |  **2.1740 us** |  **49.608 us** |  **1.00** |    **0.00** | **41.6260** |       **-** |       **-** |  **131606 B** |
| PooledPush |  Clr |     Clr | 10000 |    Int |  61.679 us | 1.2239 us |  1.5478 us |  61.496 us |  1.21 |    0.06 |       - |       - |       - |      56 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  StackPush | Core |    Core | 10000 |    Int |  38.564 us | 0.7656 us |  2.1595 us |  38.152 us |  1.00 |    0.00 | 41.6260 |       - |       - |  131400 B |
| PooledPush | Core |    Core | 10000 |    Int |  27.527 us | 0.6063 us |  1.7297 us |  27.094 us |  0.72 |    0.06 |       - |       - |       - |      56 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  **StackPush** |  **Clr** |     **Clr** | **10000** | **String** | **159.609 us** | **4.3082 us** | **12.3610 us** | **156.659 us** |  **1.00** |    **0.00** | **41.5039** | **41.5039** | **41.5039** |  **262700 B** |
| PooledPush |  Clr |     Clr | 10000 | String | 139.581 us | 3.7011 us | 10.8548 us | 137.533 us |  0.87 |    0.08 |       - |       - |       - |      58 B |
|            |      |         |       |        |            |           |            |            |       |         |         |         |         |           |
|  StackPush | Core |    Core | 10000 | String | 151.637 us | 3.0269 us |  7.8135 us | 147.682 us |  1.00 |    0.00 | 41.5039 | 41.5039 | 41.5039 |  262456 B |
| PooledPush | Core |    Core | 10000 | String |  66.974 us | 0.4042 us |  0.3781 us |  66.910 us |  0.42 |    0.02 |       - |       - |       - |      56 B |
