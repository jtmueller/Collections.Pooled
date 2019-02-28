``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetItem** |   **1000** | **531.6 us** | **3.392 us** | **2.832 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |   1000 | 510.8 us | 2.304 us | 1.924 us |  0.96 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** |  **10000** | **663.4 us** | **2.525 us** | **2.238 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |  10000 | 510.7 us | 2.499 us | 2.338 us |  0.77 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** | **100000** | **529.6 us** | **1.179 us** | **1.103 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem | 100000 | 510.3 us | 3.261 us | 3.051 us |  0.96 |           - |           - |           - |                   - |
