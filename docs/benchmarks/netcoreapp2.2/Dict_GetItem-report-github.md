``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetItem** |   **1000** | **538.8 us** | **2.533 us** | **2.370 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |   1000 | 517.1 us | 2.136 us | 1.894 us |  0.96 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** |  **10000** | **538.2 us** | **2.017 us** | **1.887 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |  10000 | 516.2 us | 3.105 us | 2.593 us |  0.96 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** | **100000** | **539.1 us** | **3.122 us** | **2.920 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem | 100000 | 516.2 us | 2.789 us | 2.472 us |  0.96 |           - |           - |           - |                   - |
