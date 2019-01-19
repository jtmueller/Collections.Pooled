``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                        Method |      N |          Mean |      Error |     StdDev |        Median | Ratio | RatioSD |
|------------------------------ |------- |--------------:|-----------:|-----------:|--------------:|------:|--------:|
|   **DictContainsValue_Int_False** |   **1000** |      **1.978 ms** |  **0.0392 ms** |  **0.0482 ms** |      **1.964 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_Int_False |   1000 |      1.905 ms |  0.0290 ms |  0.0271 ms |      1.911 ms |  0.96 |    0.03 |
|                               |        |               |            |            |               |       |         |
|   **DictContainsValue_Int_False** |  **10000** |    **192.781 ms** |  **0.5264 ms** |  **0.4924 ms** |    **192.609 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_Int_False |  10000 |    187.638 ms |  3.7465 ms |  5.9423 ms |    191.024 ms |  0.97 |    0.03 |
|                               |        |               |            |            |               |       |         |
|   **DictContainsValue_Int_False** | **100000** | **20,656.780 ms** | **64.2509 ms** | **60.1003 ms** | **20,633.790 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_Int_False | 100000 | 19,120.838 ms | 46.8978 ms | 39.1618 ms | 19,126.517 ms |  0.93 |    0.00 |
