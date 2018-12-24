``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |        Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |    **35.84 us** |   **0.2967 us** |   **0.2630 us** |  **1.00** |    **127.9907** |           **-** |           **-** |            **403200 B** |
| PooledSetCapacity |   1000 |    13.48 us |   0.1690 us |   0.1581 us |  0.38 |           - |           - |           - |                   - |
|                   |        |             |             |             |       |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |   **382.22 us** |   **2.1537 us** |   **2.0145 us** |  **1.00** |   **1265.6250** |           **-** |           **-** |           **4003200 B** |
| PooledSetCapacity |  10000 |   117.04 us |   0.3647 us |   0.3411 us |  0.31 |           - |           - |           - |                   - |
|                   |        |             |             |             |       |             |             |             |                     |
|   **ListSetCapacity** | **100000** | **8,421.33 us** | **166.8735 us** | **309.3108 us** |  **1.00** |  **10156.2500** |  **10156.2500** |  **10156.2500** |          **40003200 B** |
| PooledSetCapacity | 100000 | 1,542.01 us |  23.1746 us |  21.6775 us |  0.19 |           - |           - |           - |                   - |
