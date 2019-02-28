``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetKeys** |   **1000** | **64.28 us** | **0.2077 us** | **0.1943 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |   1000 | 75.92 us | 0.2750 us | 0.2572 us |  1.18 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** |  **10000** | **64.26 us** | **0.1730 us** | **0.1618 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |  10000 | 76.03 us | 0.3119 us | 0.2918 us |  1.18 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** | **100000** | **64.24 us** | **0.2101 us** | **0.1863 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys | 100000 | 76.30 us | 0.4919 us | 0.4108 us |  1.19 |           - |           - |           - |                   - |
