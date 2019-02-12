``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|           Method |       N |   Type |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------- |------- |------------:|------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueTryDequeue** |   **10000** |    **Int** |   **100.00 us** |   **1.1164 us** |   **0.8716 us** |   **100.39 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 |    Int |    33.93 us |   0.3564 us |   0.2976 us |    34.01 us |  0.34 |    0.00 |           - |           - |           - |                   - |
|                  |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |   **10000** | **String** |    **99.22 us** |   **1.4978 us** |   **1.2507 us** |    **98.87 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 | String |    48.31 us |   0.1839 us |   0.1435 us |    48.28 us |  0.49 |    0.01 |           - |           - |           - |                   - |
|                  |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** |    **Int** |   **969.96 us** |   **2.6339 us** |   **2.3349 us** |   **970.16 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 |    Int |   328.55 us |   0.2566 us |   0.2142 us |   328.61 us |  0.34 |    0.00 |           - |           - |           - |                   - |
|                  |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** | **String** |   **971.89 us** |  **19.3870 us** |  **29.0176 us** |   **987.91 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 | String |   476.74 us |   9.5008 us |  21.4450 us |   483.28 us |  0.49 |    0.02 |           - |           - |           - |                   - |
|                  |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** |    **Int** | **9,463.79 us** | **185.0910 us** | **277.0354 us** | **9,456.75 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 |    Int | 3,225.81 us |  63.9730 us | 175.1251 us | 3,123.79 us |  0.34 |    0.02 |           - |           - |           - |                   - |
|                  |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** | **String** | **9,439.65 us** | **132.0478 us** | **123.5176 us** | **9,434.69 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 | String | 4,614.76 us | 130.1653 us | 121.7567 us | 4,563.27 us |  0.49 |    0.01 |           - |           - |           - |                   - |
