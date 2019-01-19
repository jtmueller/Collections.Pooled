``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                          Method |      N |          Mean |       Error |      StdDev |        Median | Ratio | RatioSD |
|-------------------------------- |------- |--------------:|------------:|------------:|--------------:|------:|--------:|
|   **DictContainsValue_String_True** |   **1000** |      **3.465 ms** |   **0.0178 ms** |   **0.0166 ms** |      **3.460 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_True |   1000 |      3.564 ms |   0.0710 ms |   0.1298 ms |      3.482 ms |  1.03 |    0.04 |
|                                 |        |               |             |             |               |       |         |
|   **DictContainsValue_String_True** |  **10000** |    **368.818 ms** |   **0.8403 ms** |   **0.7860 ms** |    **368.856 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_True |  10000 |    341.948 ms |   1.1487 ms |   0.9592 ms |    341.898 ms |  0.93 |    0.00 |
|                                 |        |               |             |             |               |       |         |
|   **DictContainsValue_String_True** | **100000** | **34,939.396 ms** | **599.9545 ms** | **561.1978 ms** | **34,663.294 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_True | 100000 | 37,223.680 ms |  72.3787 ms |  64.1618 ms | 37,226.484 ms |  1.07 |    0.02 |
