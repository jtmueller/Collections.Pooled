``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|              Method |     N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------ |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListRemove_Int** |  **3000** |   **2.698 ms** | **0.0552 ms** | **0.1583 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.718 ms | 0.0598 ms | 0.1744 ms |  1.01 |    0.09 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  23.550 ms | 0.4535 ms | 0.4658 ms |  8.72 |    0.46 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  24.444 ms | 0.4834 ms | 0.9428 ms |  9.03 |    0.54 |           - |           - |           - |                   - |
|                     |       |            |           |           |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **26.227 ms** | **0.5542 ms** | **0.5184 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  26.179 ms | 0.4849 ms | 0.4762 ms |  1.00 |    0.03 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 257.623 ms | 2.9788 ms | 2.7864 ms |  9.83 |    0.18 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 259.416 ms | 4.6016 ms | 4.0792 ms |  9.92 |    0.24 |           - |           - |           - |                   - |
