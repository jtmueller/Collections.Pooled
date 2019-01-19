``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |---------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictTryGetValue** |   **1000** | **68.66 us** | **0.1466 us** | **0.1371 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |   1000 | 65.67 us | 0.6620 us | 0.6193 us |  0.96 |    0.01 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |         |             |             |             |                     |
|   **DictTryGetValue** |  **10000** | **60.54 us** | **0.2633 us** | **0.2463 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |  10000 | 65.74 us | 0.1541 us | 0.1366 us |  1.09 |    0.01 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |         |             |             |             |                     |
|   **DictTryGetValue** | **100000** | **62.97 us** | **1.2523 us** | **1.7555 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue | 100000 | 64.86 us | 1.2688 us | 1.6046 us |  1.03 |    0.04 |           - |           - |           - |                   - |
