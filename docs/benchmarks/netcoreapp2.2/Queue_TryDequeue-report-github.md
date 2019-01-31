``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|           Method |       N |   Type |       Mean |      Error |    StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------- |------- |-----------:|-----------:|----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueTryDequeue** |   **10000** |    **Int** |   **189.3 us** |   **8.498 us** |  **23.69 us** |   **178.07 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 |    Int |   200.6 us |   8.166 us |  23.30 us |   198.89 us |  1.07 |    0.17 |           - |           - |           - |                   - |
|                  |         |        |            |            |           |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |   **10000** | **String** |   **100.5 us** |   **5.910 us** |  **17.05 us** |    **88.58 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |   10000 | String |   216.6 us |   5.341 us |  14.80 us |   211.64 us |  2.20 |    0.34 |           - |           - |           - |                   - |
|                  |         |        |            |            |           |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** |    **Int** | **1,804.6 us** |  **35.492 us** |  **68.38 us** | **1,794.92 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 |    Int | 1,821.4 us |  35.917 us |  70.05 us | 1,827.61 us |  1.01 |    0.05 |           - |           - |           - |                   - |
|                  |         |        |            |            |           |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** |  **100000** | **String** |   **700.8 us** |  **68.412 us** | **201.72 us** |   **624.91 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue |  100000 | String |   573.7 us |  31.332 us |  83.63 us |   563.16 us |  0.83 |    0.21 |           - |           - |           - |                   - |
|                  |         |        |            |            |           |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** |    **Int** | **3,707.8 us** |  **73.925 us** | **163.81 us** | **3,643.36 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 |    Int | 3,444.3 us | 161.752 us | 461.49 us | 3,553.94 us |  1.00 |    0.07 |           - |           - |           - |                   - |
|                  |         |        |            |            |           |             |       |         |             |             |             |                     |
|  **QueueTryDequeue** | **1000000** | **String** | **5,352.6 us** | **102.385 us** | **109.55 us** | **5,289.90 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryDequeue | 1000000 | String | 5,391.2 us | 106.449 us | 122.59 us | 5,330.99 us |  1.01 |    0.03 |           - |           - |           - |                   - |
