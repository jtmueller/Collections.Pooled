``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |         Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |     **24.98 us** |   **0.0697 us** |   **0.0617 us** |  **1.00** |    **0.00** |    **128.0212** |           **-** |           **-** |            **403302 B** |
| PooledSetCapacity |   1000 |     97.99 us |   0.5283 us |   0.4412 us |  3.92 |    0.02 |           - |           - |           - |                   - |
|                   |        |              |             |             |       |         |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |    **287.82 us** |   **5.6539 us** |   **5.5529 us** |  **1.00** |    **0.00** |   **1265.6250** |           **-** |           **-** |           **4005600 B** |
| PooledSetCapacity |  10000 |  1,448.06 us |   7.2680 us |   6.0691 us |  5.02 |    0.10 |           - |           - |           - |                   - |
|                   |        |              |             |             |       |         |             |             |             |                     |
|   **ListSetCapacity** | **100000** |  **7,299.12 us** | **138.5980 us** | **129.6446 us** |  **1.00** |    **0.00** |   **7992.1875** |   **7968.7500** |   **7968.7500** |          **40003200 B** |
| PooledSetCapacity | 100000 | 12,281.61 us |  27.9196 us |  26.1160 us |  1.68 |    0.03 |           - |           - |           - |                   - |
