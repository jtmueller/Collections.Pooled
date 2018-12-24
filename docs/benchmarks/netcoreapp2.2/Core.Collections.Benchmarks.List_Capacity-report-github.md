``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |    **24.98 us** |  **0.4513 us** |  **0.4222 us** |  **1.00** |    **128.0212** |           **-** |           **-** |            **403200 B** |
| PooledSetCapacity |   1000 |    14.96 us |  0.1404 us |  0.1313 us |  0.60 |           - |           - |           - |                   - |
|                   |        |             |            |            |       |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |   **286.63 us** |  **2.4276 us** |  **2.2708 us** |  **1.00** |   **1265.6250** |           **-** |           **-** |           **4003200 B** |
| PooledSetCapacity |  10000 |   119.04 us |  0.1952 us |  0.1826 us |  0.42 |           - |           - |           - |                   - |
|                   |        |             |            |            |       |             |             |             |                     |
|   **ListSetCapacity** | **100000** | **7,820.96 us** | **80.6854 us** | **75.4732 us** |  **1.00** |   **9562.5000** |   **9375.0000** |   **9375.0000** |          **40003200 B** |
| PooledSetCapacity | 100000 | 1,427.96 us |  5.0007 us |  4.4330 us |  0.18 |           - |           - |           - |                   - |
