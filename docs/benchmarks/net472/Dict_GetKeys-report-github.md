``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | RatioSD |
|-------------- |------- |---------:|----------:|----------:|------:|--------:|
|   **DictGetKeys** |   **1000** | **94.70 us** | **1.8835 us** | **1.7618 us** |  **1.00** |    **0.00** |
| PooledGetKeys |   1000 | 82.99 us | 0.4890 us | 0.3818 us |  0.88 |    0.02 |
|               |        |          |           |           |       |         |
|   **DictGetKeys** |  **10000** | **94.61 us** | **1.8306 us** | **1.7124 us** |  **1.00** |    **0.00** |
| PooledGetKeys |  10000 | 76.27 us | 0.2039 us | 0.1702 us |  0.81 |    0.02 |
|               |        |          |           |           |       |         |
|   **DictGetKeys** | **100000** | **94.92 us** | **1.8239 us** | **1.7913 us** |  **1.00** |    **0.00** |
| PooledGetKeys | 100000 | 82.44 us | 1.4905 us | 1.3213 us |  0.87 |    0.02 |
