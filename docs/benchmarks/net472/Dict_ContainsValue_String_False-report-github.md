``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                           Method |      N |          Mean |         Error |        StdDev | Ratio | RatioSD |
|--------------------------------- |------- |--------------:|--------------:|--------------:|------:|--------:|
|   **DictContainsValue_String_False** |   **1000** |      **5.646 ms** |     **0.0350 ms** |     **0.0273 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_False |   1000 |      5.988 ms |     0.0584 ms |     0.0488 ms |  1.06 |    0.01 |
|                                  |        |               |               |               |       |         |
|   **DictContainsValue_String_False** |  **10000** |    **553.427 ms** |     **5.0806 ms** |     **4.2425 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_False |  10000 |    560.013 ms |     2.6989 ms |     2.5245 ms |  1.01 |    0.01 |
|                                  |        |               |               |               |       |         |
|   **DictContainsValue_String_False** | **100000** | **61,545.536 ms** | **1,185.3961 ms** | **1,317.5647 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_False | 100000 | 59,555.538 ms | 1,271.4272 ms | 1,697.3187 ms |  0.97 |    0.02 |
