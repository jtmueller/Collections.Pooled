``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |        Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |    **26.17 us** |   **0.4255 us** |   **0.3772 us** |  **1.00** |    **128.0212** |           **-** |           **-** |            **403200 B** |
| PooledSetCapacity |   1000 |    12.51 us |   0.0286 us |   0.0223 us |  0.48 |           - |           - |           - |                   - |
|                   |        |             |             |             |       |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |   **320.60 us** |   **5.4924 us** |   **5.1376 us** |  **1.00** |   **1265.6250** |           **-** |           **-** |           **4003200 B** |
| PooledSetCapacity |  10000 |   110.01 us |   0.6700 us |   0.5595 us |  0.34 |           - |           - |           - |                   - |
|                   |        |             |             |             |       |             |             |             |                     |
|   **ListSetCapacity** | **100000** | **8,062.67 us** | **158.7331 us** | **232.6689 us** |  **1.00** |   **9312.5000** |   **9312.5000** |   **9312.5000** |          **40003200 B** |
| PooledSetCapacity | 100000 | 1,510.16 us |  29.5328 us |  38.4010 us |  0.19 |           - |           - |           - |                   - |
