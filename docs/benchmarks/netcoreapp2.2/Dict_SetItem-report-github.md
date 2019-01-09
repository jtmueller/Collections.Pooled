``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetItem** |   **1000** | **755.9 us** | **3.511 us** | **3.284 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |   1000 | 807.8 us | 6.744 us | 6.309 us |  1.07 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** |  **10000** | **756.9 us** | **3.968 us** | **3.711 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |  10000 | 809.1 us | 6.695 us | 6.262 us |  1.07 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** | **100000** | **756.0 us** | **3.772 us** | **3.528 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem | 100000 | 826.1 us | 6.974 us | 6.523 us |  1.09 |           - |           - |           - |                   - |
