``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                         Method |      N |        Mean |      Error |     StdDev | Ratio | RatioSD |
|------------------------------- |------- |------------:|-----------:|-----------:|------:|--------:|
|   **DictContainsKey_String_False** |   **1000** |    **12.10 us** |  **0.0430 us** |  **0.0359 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False |   1000 |    22.69 us |  0.1136 us |  0.1062 us |  1.87 |    0.01 |
|                                |        |             |            |            |       |         |
|   **DictContainsKey_String_False** |  **10000** |   **141.94 us** |  **0.4294 us** |  **0.3807 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False |  10000 |   224.11 us |  2.2670 us |  2.0097 us |  1.58 |    0.02 |
|                                |        |             |            |            |       |         |
|   **DictContainsKey_String_False** | **100000** | **1,487.74 us** |  **3.6659 us** |  **3.2498 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False | 100000 | 2,234.51 us | 31.9285 us | 28.3038 us |  1.50 |    0.02 |
