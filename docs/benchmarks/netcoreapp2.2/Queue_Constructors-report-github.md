``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |   Type |           Mean |         Error |         StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |---------------:|--------------:|---------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueICollectionConstructor** |   **1000** |    **Int** |       **249.6 us** |     **1.7052 us** |      **1.5116 us** |  **1.00** |    **0.00** |   **1293.4570** |           **-** |           **-** |          **3976.56 KB** |
| PooledICollectionConstructor |   1000 |    Int |       144.4 us |     0.5489 us |      0.5134 us |  0.58 |    0.00 |     20.2637 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |   1000 |    Int |     7,983.6 us |    39.6498 us |     37.0884 us | 31.98 |    0.19 |   2687.5000 |           - |           - |          8281.25 KB |
| PooledIEnumerableConstructor |   1000 |    Int |     7,924.7 us |    32.3013 us |     28.6342 us | 31.75 |    0.20 |     46.8750 |           - |           - |           156.25 KB |
|                              |        |        |                |               |                |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** |   **1000** | **String** |       **657.8 us** |     **1.8736 us** |      **1.6609 us** |  **1.00** |    **0.00** |   **2563.4766** |           **-** |           **-** |          **7882.81 KB** |
| PooledICollectionConstructor |   1000 | String |       601.9 us |     2.1862 us |      2.0450 us |  0.92 |    0.00 |     19.5313 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |   1000 | String |    11,918.0 us |    56.9602 us |     53.2806 us | 18.12 |    0.09 |   5281.2500 |           - |           - |         16273.44 KB |
| PooledIEnumerableConstructor |   1000 | String |    13,095.8 us |    33.8843 us |     31.6954 us | 19.90 |    0.06 |     46.8750 |           - |           - |           164.06 KB |
|                              |        |        |                |               |                |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** |    **Int** |     **2,715.1 us** |    **20.0141 us** |     **18.7212 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |         **39132.81 KB** |
| PooledICollectionConstructor |  10000 |    Int |     1,136.1 us |     2.4336 us |      2.1573 us |  0.42 |    0.00 |     19.5313 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |  10000 |    Int |    72,636.9 us |   163.2328 us |    144.7017 us | 26.75 |    0.19 |  41571.4286 |           - |           - |           128375 KB |
| PooledIEnumerableConstructor |  10000 |    Int |    72,221.7 us |   355.5399 us |    332.5723 us | 26.60 |    0.21 |           - |           - |           - |           156.25 KB |
|                              |        |        |                |               |                |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** | **String** |     **6,235.9 us** |    **33.3302 us** |     **29.5464 us** |  **1.00** |    **0.00** |  **24992.1875** |           **-** |           **-** |         **78195.31 KB** |
| PooledICollectionConstructor |  10000 | String |     7,961.4 us |    22.4970 us |     18.7860 us |  1.28 |    0.01 |     15.6250 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |  10000 | String |   173,318.8 us |   936.4091 us |    875.9177 us | 27.80 |    0.21 |  41333.3333 |  41333.3333 |  41333.3333 |        256367.19 KB |
| PooledIEnumerableConstructor |  10000 | String |   139,590.6 us |   459.2453 us |    429.5783 us | 22.38 |    0.12 |           - |           - |           - |           164.06 KB |
|                              |        |        |                |               |                |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** |    **Int** |   **160,402.4 us** | **2,953.7178 us** |  **2,762.9095 us** |  **1.00** |    **0.00** |  **34500.0000** |  **34500.0000** |  **34500.0000** |        **390931.65 KB** |
| PooledICollectionConstructor | 100000 |    Int |    14,342.2 us |   105.6134 us |     98.7908 us |  0.09 |    0.00 |     15.6250 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor | 100000 |    Int |   966,884.5 us | 2,739.0718 us |  2,428.1158 us |  6.03 |    0.11 | 215000.0000 | 175000.0000 | 173000.0000 |       1025241.51 KB |
| PooledIEnumerableConstructor | 100000 |    Int |   718,201.6 us | 3,404.9187 us |  3,184.9631 us |  4.48 |    0.08 |           - |           - |           - |           156.25 KB |
|                              |        |        |                |               |                |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** | **String** |   **370,400.8 us** | **7,405.1536 us** | **19,246.9788 us** |  **1.00** |    **0.00** |  **10000.0000** |  **10000.0000** |  **10000.0000** |        **781380.29 KB** |
| PooledICollectionConstructor | 100000 | String |    81,417.3 us |   238.1136 us |    222.7316 us |  0.22 |    0.01 |           - |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor | 100000 | String | 1,795,009.6 us | 8,277.6445 us |  7,742.9138 us |  4.85 |    0.24 | 370000.0000 | 330000.0000 | 327000.0000 |        2049704.3 KB |
| PooledIEnumerableConstructor | 100000 | String | 1,306,838.8 us | 3,483.7806 us |  3,258.7305 us |  3.53 |    0.18 |           - |           - |           - |           164.06 KB |
