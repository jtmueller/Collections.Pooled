``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictTryGetValue** |   **1000** | **59.60 us** | **0.1188 us** | **0.1111 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |   1000 | 60.22 us | 0.2606 us | 0.2438 us |  1.01 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** |  **10000** | **59.57 us** | **0.1206 us** | **0.1128 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |  10000 | 60.01 us | 0.1731 us | 0.1534 us |  1.01 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** | **100000** | **59.59 us** | **0.1286 us** | **0.1140 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue | 100000 | 60.17 us | 0.2586 us | 0.2292 us |  1.01 |           - |           - |           - |                   - |
