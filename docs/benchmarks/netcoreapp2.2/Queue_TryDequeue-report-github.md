``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|           Method |       N |   Type |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------- |------- |------------:|-----------:|-----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueTryDequeue** |   **10000** |    **Int** |   **181.84 us** |   **7.503 us** |  **21.529 us** |   **166.47 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 |    Int |   192.07 us |   7.706 us |  21.480 us |   188.90 us |  1.07 |    0.16 |           - |           - |           - |                   - |
|                  |         |        |             |            |            |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |   **10000** | **String** |    **95.59 us** |   **6.438 us** |   **8.594 us** |    **91.96 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 | String |   196.99 us |   6.031 us |  16.912 us |   184.75 us |  2.03 |    0.21 |           - |           - |           - |                   - |
|                  |         |        |             |            |            |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** |    **Int** | **1,390.01 us** | **221.461 us** | **652.983 us** | **1,696.21 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 |    Int | 1,703.89 us |  33.935 us |  50.792 us | 1,718.41 us |  0.95 |    0.09 |           - |           - |           - |                   - |
|                  |         |        |             |            |            |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** | **String** |   **923.29 us** |  **17.850 us** |  **14.906 us** |   **916.46 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 | String |   609.84 us |  37.149 us |  99.800 us |   584.55 us |  0.67 |    0.08 |           - |           - |           - |                   - |
|                  |         |        |             |            |            |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** |    **Int** | **3,514.13 us** | **120.080 us** | **334.735 us** | **3,577.20 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 |    Int | 3,844.77 us | 130.830 us | 375.376 us | 3,844.63 us |  1.11 |    0.14 |           - |           - |           - |                   - |
|                  |         |        |             |            |            |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** | **String** | **5,394.94 us** | **104.278 us** | **115.904 us** | **5,409.49 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 | String | 5,605.38 us | 105.925 us | 104.032 us | 5,574.58 us |  1.04 |    0.04 |           - |           - |           - |                   - |
