``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | RatioSD |
|-------------- |------- |---------:|----------:|----------:|------:|--------:|
|   **DictGetKeys** |   **1000** | **95.97 us** | **0.2163 us** | **0.1806 us** |  **1.00** |    **0.00** |
| PooledGetKeys |   1000 | 83.47 us | 0.6451 us | 0.5719 us |  0.87 |    0.00 |
|               |        |          |           |           |       |         |
|   **DictGetKeys** |  **10000** | **95.96 us** | **0.2524 us** | **0.2237 us** |  **1.00** |    **0.00** |
| PooledGetKeys |  10000 | 78.81 us | 1.6411 us | 2.3006 us |  0.83 |    0.03 |
|               |        |          |           |           |       |         |
|   **DictGetKeys** | **100000** | **63.99 us** | **0.1591 us** | **0.1411 us** |  **1.00** |    **0.00** |
| PooledGetKeys | 100000 | 71.79 us | 2.2181 us | 2.1785 us |  1.12 |    0.04 |
