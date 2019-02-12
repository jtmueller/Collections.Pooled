``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |         Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |     **23.67 us** |  **0.0815 us** |  **0.0680 us** |  **1.00** |    **0.00** |    **128.0212** |           **-** |           **-** |            **403302 B** |
| PooledSetCapacity |   1000 |     99.64 us |  0.3176 us |  0.2480 us |  4.21 |    0.02 |           - |           - |           - |                   - |
|                   |        |              |            |            |       |         |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |    **286.60 us** |  **1.4800 us** |  **1.3844 us** |  **1.00** |    **0.00** |   **1265.6250** |           **-** |           **-** |           **4005600 B** |
| PooledSetCapacity |  10000 |  1,448.12 us |  3.7227 us |  3.4822 us |  5.05 |    0.02 |           - |           - |           - |                   - |
|                   |        |              |            |            |       |         |             |             |             |                     |
|   **ListSetCapacity** | **100000** |  **7,142.29 us** | **56.7342 us** | **53.0692 us** |  **1.00** |    **0.00** |   **7968.7500** |   **7960.9375** |   **7960.9375** |          **40003200 B** |
| PooledSetCapacity | 100000 | 12,212.43 us | 26.1052 us | 24.4188 us |  1.71 |    0.01 |           - |           - |           - |                   - |
