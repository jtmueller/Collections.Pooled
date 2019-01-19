``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |     StdDev |   Median | Ratio | RatioSD |
|-------------- |------- |---------:|----------:|-----------:|---------:|------:|--------:|
|   **DictGetItem** |   **1000** | **539.6 us** |  **1.046 us** |  **0.8736 us** | **539.6 us** |  **1.00** |    **0.00** |
| PooledGetItem |   1000 | 701.2 us | 13.819 us | 20.6839 us | 713.1 us |  1.29 |    0.04 |
|               |        |          |           |            |          |       |         |
|   **DictGetItem** |  **10000** | **580.9 us** |  **1.375 us** |  **1.2865 us** | **580.5 us** |  **1.00** |    **0.00** |
| PooledGetItem |  10000 | 715.4 us |  1.217 us |  1.1381 us | 715.7 us |  1.23 |    0.00 |
|               |        |          |           |            |          |       |         |
|   **DictGetItem** | **100000** | **560.6 us** | **11.078 us** | **19.6910 us** | **559.4 us** |  **1.00** |    **0.00** |
| PooledGetItem | 100000 | 748.7 us |  1.906 us |  1.7829 us | 748.5 us |  1.31 |    0.04 |
