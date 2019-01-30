``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|           Method |       N |   Type |         Mean |       Error |     StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------- |------- |-------------:|------------:|-----------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueTryDequeue** |   **10000** |    **Int** |    **110.04 us** |   **5.4948 us** |  **15.766 us** |    **100.42 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 |    Int |     32.37 us |   0.7876 us |   1.555 us |     31.70 us |  0.30 |    0.04 |           - |           - |           - |                   - |
|                  |         |        |              |             |            |              |       |         |             |             |             |                     |
|  **QueueTryDequeue** |   **10000** | **String** |     **99.52 us** |   **2.7166 us** |   **2.408 us** |     **98.89 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 | String |     50.18 us |   1.2079 us |   1.438 us |     49.68 us |  0.50 |    0.02 |           - |           - |           - |                   - |
|                  |         |        |              |             |            |              |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** |    **Int** |    **998.86 us** |  **19.7952 us** |  **53.518 us** |    **972.92 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 |    Int |    315.59 us |  13.5509 us |  23.733 us |    306.19 us |  0.31 |    0.03 |           - |           - |           - |                   - |
|                  |         |        |              |             |            |              |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** | **String** |  **1,106.82 us** |  **49.4381 us** | **145.769 us** |  **1,052.73 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 | String |    501.45 us |  10.0173 us |  23.415 us |    498.49 us |  0.45 |    0.06 |           - |           - |           - |                   - |
|                  |         |        |              |             |            |              |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** |    **Int** | **10,742.63 us** | **281.8646 us** | **831.084 us** | **10,711.69 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 |    Int |  3,188.68 us |  92.1820 us | 265.966 us |  3,135.87 us |  0.30 |    0.03 |           - |           - |           - |                   - |
|                  |         |        |              |             |            |              |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** | **String** |  **9,653.71 us** | **264.7919 us** | **271.922 us** |  **9,531.76 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 | String |  5,114.71 us | 171.9669 us | 493.405 us |  4,939.14 us |  0.51 |    0.03 |           - |           - |           - |                   - |
