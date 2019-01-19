``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                       Method |      N |            Mean |         Error |        StdDev | Ratio |
|----------------------------- |------- |----------------:|--------------:|--------------:|------:|
|   **DictContainsValue_Int_True** |   **1000** |      **1,051.2 us** |     **16.426 us** |     **15.365 us** |  **1.00** |
| PooledContainsValue_Int_True |   1000 |        897.2 us |      2.733 us |      2.282 us |  0.85 |
|                              |        |                 |               |               |       |
|   **DictContainsValue_Int_True** |  **10000** |    **104,331.5 us** |    **201.041 us** |    **188.054 us** |  **1.00** |
| PooledContainsValue_Int_True |  10000 |     89,475.9 us |    294.668 us |    275.633 us |  0.86 |
|                              |        |                 |               |               |       |
|   **DictContainsValue_Int_True** | **100000** | **10,407,499.4 us** | **60,161.116 us** | **56,274.745 us** |  **1.00** |
| PooledContainsValue_Int_True | 100000 |  9,618,614.8 us | 68,303.964 us | 57,036.903 us |  0.92 |
