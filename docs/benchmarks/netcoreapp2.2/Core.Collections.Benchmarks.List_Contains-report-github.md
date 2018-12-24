``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio |
|--------------- |------- |-----------:|----------:|----------:|------:|
|   **ListContains** |   **1000** |   **1.848 ms** | **0.0024 ms** | **0.0022 ms** |  **1.00** |
| PooledContains |   1000 |   1.848 ms | 0.0054 ms | 0.0042 ms |  1.00 |
|                |        |            |           |           |       |
|   **ListContains** |  **10000** |  **17.747 ms** | **0.0823 ms** | **0.0729 ms** |  **1.00** |
| PooledContains |  10000 |  17.663 ms | 0.0258 ms | 0.0215 ms |  1.00 |
|                |        |            |           |           |       |
|   **ListContains** | **100000** | **176.127 ms** | **0.7680 ms** | **0.7184 ms** |  **1.00** |
| PooledContains | 100000 | 175.371 ms | 0.8191 ms | 0.6840 ms |  1.00 |
