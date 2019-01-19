``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |
|-------------- |------- |---------:|----------:|----------:|---------:|------:|--------:|
|   **DictGetItem** |   **1000** | **914.9 us** | **18.134 us** | **17.810 us** | **923.3 us** |  **1.00** |    **0.00** |
| PooledGetItem |   1000 | 782.5 us | 15.075 us | 19.602 us | 792.1 us |  0.86 |    0.03 |
|               |        |          |           |           |          |       |         |
|   **DictGetItem** |  **10000** | **857.2 us** |  **8.956 us** |  **7.939 us** | **854.2 us** |  **1.00** |    **0.00** |
| PooledGetItem |  10000 | 746.4 us | 14.926 us | 24.937 us | 732.8 us |  0.88 |    0.03 |
|               |        |          |           |           |          |       |         |
|   **DictGetItem** | **100000** | **895.2 us** | **17.840 us** | **27.774 us** | **898.4 us** |  **1.00** |    **0.00** |
| PooledGetItem | 100000 | 802.4 us | 15.430 us | 17.770 us | 809.7 us |  0.89 |    0.04 |
