``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                    Method |      N |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------------------ |------- |------------:|----------:|----------:|------:|--------:|
|   **DictContainsKey_String_False_IgnoreCase** |   **1000** |    **27.21 us** | **0.1243 us** | **0.1102 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False_IgnoreCase |   1000 |    25.35 us | 0.0845 us | 0.0749 us |  0.93 |    0.00 |
|                                           |        |             |           |           |       |         |
|   **DictContainsKey_String_False_IgnoreCase** |  **10000** |   **292.45 us** | **4.9918 us** | **4.6694 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False_IgnoreCase |  10000 |   274.67 us | 1.0159 us | 0.9006 us |  0.94 |    0.02 |
|                                           |        |             |           |           |       |         |
|   **DictContainsKey_String_False_IgnoreCase** | **100000** | **2,932.90 us** | **6.1308 us** | **5.7348 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False_IgnoreCase | 100000 | 2,557.54 us | 7.9452 us | 7.4320 us |  0.87 |    0.00 |
