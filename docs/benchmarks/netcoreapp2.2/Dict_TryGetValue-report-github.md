``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictTryGetValue** |   **1000** | **60.51 us** | **0.2591 us** | **0.2424 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |   1000 | 60.93 us | 0.3376 us | 0.3158 us |  1.01 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** |  **10000** | **60.60 us** | **0.3074 us** | **0.2875 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |  10000 | 60.94 us | 0.3129 us | 0.2926 us |  1.01 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** | **100000** | **60.45 us** | **0.2842 us** | **0.2519 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue | 100000 | 65.59 us | 0.3901 us | 0.3458 us |  1.09 |           - |           - |           - |                   - |
