``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | RatioSD |
|-------------- |------- |---------:|----------:|----------:|------:|--------:|
|   **DictGetItem** |   **1000** | **619.1 us** | **12.180 us** | **25.424 us** |  **1.00** |    **0.00** |
| PooledGetItem |   1000 | 873.7 us | 18.519 us | 36.985 us |  1.41 |    0.07 |
|               |        |          |           |           |       |         |
|   **DictGetItem** |  **10000** | **580.2 us** |  **4.025 us** |  **3.765 us** |  **1.00** |    **0.00** |
| PooledGetItem |  10000 | 854.0 us |  4.965 us |  4.645 us |  1.47 |    0.01 |
|               |        |          |           |           |       |         |
|   **DictGetItem** | **100000** | **578.6 us** |  **4.365 us** |  **3.870 us** |  **1.00** |    **0.00** |
| PooledGetItem | 100000 | 875.5 us |  5.520 us |  5.163 us |  1.51 |    0.01 |
