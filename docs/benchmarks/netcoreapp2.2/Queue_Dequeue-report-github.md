``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|        Method |       N |   Type |       Mean |      Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |-------- |------- |-----------:|-----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueDequeue** |   **10000** |    **Int** |   **234.5 us** |  **13.709 us** |  **39.77 us** |   **226.6 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledDequeue |   10000 |    Int |   226.9 us |   9.811 us |  28.15 us |   223.6 us |  0.99 |    0.19 |           - |           - |           - |                   - |
|               |         |        |            |            |           |            |       |         |             |             |             |                     |
|  **QueueDequeue** |   **10000** | **String** |   **101.5 us** |   **7.852 us** |  **22.27 us** |   **103.8 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledDequeue |   10000 | String |   250.5 us |  12.447 us |  34.90 us |   244.9 us |  2.61 |    0.71 |           - |           - |           - |                   - |
|               |         |        |            |            |           |            |       |         |             |             |             |                     |
|  **QueueDequeue** |  **100000** |    **Int** | **1,619.5 us** | **281.805 us** | **830.91 us** | **2,046.2 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledDequeue |  100000 |    Int | 1,637.3 us | 270.577 us | 797.80 us | 2,030.5 us |  1.06 |    0.31 |           - |           - |           - |                   - |
|               |         |        |            |            |           |            |       |         |             |             |             |                     |
|  **QueueDequeue** |  **100000** | **String** |   **378.2 us** |  **23.632 us** |  **62.26 us** |   **368.6 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledDequeue |  100000 | String |   395.7 us |  28.186 us |  75.72 us |   379.1 us |  1.07 |    0.22 |           - |           - |           - |                   - |
|               |         |        |            |            |           |            |       |         |             |             |             |                     |
|  **QueueDequeue** | **1000000** |    **Int** | **3,566.4 us** | **179.124 us** | **522.51 us** | **3,526.0 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledDequeue | 1000000 |    Int | 3,511.5 us | 179.853 us | 530.30 us | 3,579.7 us |  1.00 |    0.14 |           - |           - |           - |                   - |
|               |         |        |            |            |           |            |       |         |             |             |             |                     |
|  **QueueDequeue** | **1000000** | **String** | **3,093.3 us** | **115.326 us** | **340.04 us** | **2,876.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledDequeue | 1000000 | String | 3,457.6 us |  58.420 us |  48.78 us | 3,429.6 us |  0.98 |    0.04 |           - |           - |           - |                   - |
