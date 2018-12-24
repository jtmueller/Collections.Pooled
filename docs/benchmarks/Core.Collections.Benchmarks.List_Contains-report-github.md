``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListContains** |   **1000** |   **1.927 ms** | **0.0336 ms** | **0.0314 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |   1000 |   1.962 ms | 0.0158 ms | 0.0140 ms |  1.02 |    0.02 |           - |           - |           - |                   - |
|                |        |            |           |           |       |         |             |             |             |                     |
|   **ListContains** |  **10000** |  **18.800 ms** | **0.0971 ms** | **0.0909 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |  10000 |  18.132 ms | 0.0645 ms | 0.0504 ms |  0.96 |    0.01 |           - |           - |           - |                   - |
|                |        |            |           |           |       |         |             |             |             |                     |
|   **ListContains** | **100000** | **187.423 ms** | **0.8333 ms** | **0.7795 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains | 100000 | 186.444 ms | 0.4834 ms | 0.4522 ms |  0.99 |    0.00 |           - |           - |           - |                   - |
