``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                  Method |      N |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD |
|------------------------ |------- |------------:|-----------:|-----------:|------------:|------:|--------:|
|       **DefaultStringHash** |   **1000** |    **12.49 us** |  **0.1762 us** |  **0.1471 us** |    **12.42 us** |  **1.00** |    **0.00** |
| NonRandomizedStringHash |   1000 |    13.18 us |  0.2603 us |  0.2893 us |    13.16 us |  1.06 |    0.02 |
|                         |        |             |            |            |             |       |         |
|       **DefaultStringHash** |  **10000** |   **129.37 us** |  **2.5851 us** |  **5.1627 us** |   **125.64 us** |  **1.00** |    **0.00** |
| NonRandomizedStringHash |  10000 |   125.86 us |  1.7874 us |  1.4926 us |   125.53 us |  0.98 |    0.04 |
|                         |        |             |            |            |             |       |         |
|       **DefaultStringHash** | **100000** | **1,376.59 us** |  **2.2229 us** |  **1.9705 us** | **1,376.33 us** |  **1.00** |    **0.00** |
| NonRandomizedStringHash | 100000 | 1,309.45 us | 25.4597 us | 34.8495 us | 1,298.08 us |  0.95 |    0.02 |
