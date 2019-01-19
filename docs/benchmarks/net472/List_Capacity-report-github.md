``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |         Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |     **26.82 us** |  **0.1986 us** |  **0.1858 us** |  **1.00** |    **0.00** |    **128.0212** |           **-** |           **-** |            **403302 B** |
| PooledSetCapacity |   1000 |    106.74 us |  0.5075 us |  0.4499 us |  3.98 |    0.03 |           - |           - |           - |                   - |
|                   |        |              |            |            |       |         |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |    **312.80 us** |  **2.4817 us** |  **2.3214 us** |  **1.00** |    **0.00** |   **1265.6250** |           **-** |           **-** |           **4005600 B** |
| PooledSetCapacity |  10000 |  1,545.31 us | 30.7519 us | 36.6080 us |  4.95 |    0.13 |           - |           - |           - |                   - |
|                   |        |              |            |            |       |         |             |             |             |                     |
|   **ListSetCapacity** | **100000** |  **7,759.67 us** | **64.8500 us** | **60.6608 us** |  **1.00** |    **0.00** |   **7835.9375** |   **7820.3125** |   **7820.3125** |          **40003200 B** |
| PooledSetCapacity | 100000 | 13,386.08 us | 40.8061 us | 38.1700 us |  1.73 |    0.02 |           - |           - |           - |                   - |
