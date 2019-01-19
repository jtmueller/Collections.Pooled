``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                        Method |      N |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD |
|------------------------------ |------- |------------:|-----------:|-----------:|------------:|------:|--------:|
|   **DictContainsKey_String_True** |   **1000** |    **24.68 us** |  **0.4851 us** |  **0.9111 us** |    **25.31 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_True |   1000 |    31.36 us |  0.0518 us |  0.0433 us |    31.37 us |  1.27 |    0.05 |
|                               |        |             |            |            |             |       |         |
|   **DictContainsKey_String_True** |  **10000** |   **277.42 us** |  **0.5247 us** |  **0.4651 us** |   **277.36 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_True |  10000 |   353.25 us |  1.2291 us |  1.1497 us |   352.84 us |  1.27 |    0.00 |
|                               |        |             |            |            |             |       |         |
|   **DictContainsKey_String_True** | **100000** | **2,801.69 us** | **10.5416 us** |  **9.8606 us** | **2,799.46 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_True | 100000 | 3,883.43 us | 19.3301 us | 17.1356 us | 3,885.67 us |  1.39 |    0.01 |
