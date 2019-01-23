``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                  Method |      N |        Mean |     Error |    StdDev | Ratio |
|------------------------ |------- |------------:|----------:|----------:|------:|
|       **DefaultStringHash** |   **1000** |    **12.38 us** | **0.0382 us** | **0.0357 us** |  **1.00** |
| NonRandomizedStringHash |   1000 |    12.37 us | 0.0383 us | 0.0358 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** |  **10000** |   **123.57 us** | **0.5398 us** | **0.5050 us** |  **1.00** |
| NonRandomizedStringHash |  10000 |   123.36 us | 0.3546 us | 0.3317 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** | **100000** | **1,258.13 us** | **4.1402 us** | **3.8727 us** |  **1.00** |
| NonRandomizedStringHash | 100000 | 1,261.28 us | 4.9357 us | 4.3754 us |  1.00 |
