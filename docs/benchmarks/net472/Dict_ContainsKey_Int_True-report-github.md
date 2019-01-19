``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                     Method |      N |       Mean |      Error |     StdDev | Ratio | RatioSD |
|--------------------------- |------- |-----------:|-----------:|-----------:|------:|--------:|
|   **DictContainsKey_Int_True** |   **1000** |   **8.971 us** |  **0.0312 us** |  **0.0291 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_True |   1000 |   7.449 us |  0.0874 us |  0.0818 us |  0.83 |    0.01 |
|                            |        |            |            |            |       |         |
|   **DictContainsKey_Int_True** |  **10000** |  **88.435 us** |  **1.7286 us** |  **2.4233 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_True |  10000 |  78.926 us |  1.5525 us |  2.3237 us |  0.89 |    0.03 |
|                            |        |            |            |            |       |         |
|   **DictContainsKey_Int_True** | **100000** | **895.199 us** |  **4.1109 us** |  **3.4328 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_True | 100000 | 708.650 us | 13.8688 us | 20.7582 us |  0.79 |    0.03 |
