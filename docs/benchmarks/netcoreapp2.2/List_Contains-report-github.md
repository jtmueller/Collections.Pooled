``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListContains** |   **1000** |   **1.846 ms** | **0.0053 ms** | **0.0050 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |   1000 |   1.843 ms | 0.0043 ms | 0.0041 ms |  1.00 |           - |           - |           - |                   - |
|                |        |            |           |           |       |             |             |             |                     |
|   **ListContains** |  **10000** |  **17.607 ms** | **0.0486 ms** | **0.0455 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |  10000 |  17.603 ms | 0.0730 ms | 0.0610 ms |  1.00 |           - |           - |           - |                   - |
|                |        |            |           |           |       |             |             |             |                     |
|   **ListContains** | **100000** | **175.360 ms** | **0.5791 ms** | **0.5417 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains | 100000 | 175.512 ms | 0.5922 ms | 0.5540 ms |  1.00 |           - |           - |           - |                   - |
