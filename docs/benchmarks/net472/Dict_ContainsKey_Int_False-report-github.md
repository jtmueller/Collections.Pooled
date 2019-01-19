``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                      Method |      N |       Mean |      Error |    StdDev | Ratio | RatioSD |
|---------------------------- |------- |-----------:|-----------:|----------:|------:|--------:|
|   **DictContainsKey_Int_False** |   **1000** |   **6.396 us** |  **0.0122 us** | **0.0114 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_False |   1000 |   5.102 us |  0.0936 us | 0.0829 us |  0.80 |    0.01 |
|                             |        |            |            |           |       |         |
|   **DictContainsKey_Int_False** |  **10000** |  **64.214 us** |  **0.3980 us** | **0.3723 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_False |  10000 |  50.408 us |  0.9730 us | 1.4261 us |  0.79 |    0.03 |
|                             |        |            |            |           |       |         |
|   **DictContainsKey_Int_False** | **100000** | **639.357 us** | **10.2232 us** | **9.5628 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_False | 100000 | 511.395 us |  0.6167 us | 0.5467 us |  0.80 |    0.01 |
