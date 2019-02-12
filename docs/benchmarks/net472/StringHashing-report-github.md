``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                  Method |      N |        Mean |     Error |    StdDev | Ratio |
|------------------------ |------- |------------:|----------:|----------:|------:|
|       **DefaultStringHash** |   **1000** |    **12.37 us** | **0.0432 us** | **0.0404 us** |  **1.00** |
| NonRandomizedStringHash |   1000 |    12.37 us | 0.0321 us | 0.0300 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** |  **10000** |   **123.98 us** | **0.3166 us** | **0.2961 us** |  **1.00** |
| NonRandomizedStringHash |  10000 |   123.57 us | 0.4354 us | 0.3860 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** | **100000** | **1,256.84 us** | **5.4303 us** | **4.5346 us** |  **1.00** |
| NonRandomizedStringHash | 100000 | 1,258.95 us | 3.0927 us | 2.5825 us |  1.00 |
