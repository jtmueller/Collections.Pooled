``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                       Method |      N |   Type |         Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |-------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|  **QueueICollectionConstructor** |   **1000** |    **Int** |    **11.766 ms** |  **0.0411 ms** |  **0.0343 ms** |  **1.00** |   **2687.5000** |           **-** |           **-** |          **8283.24 KB** |
| PooledICollectionConstructor |   1000 |    Int |     1.088 ms |  0.0064 ms |  0.0056 ms |  0.09 |     13.6719 |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor |   1000 |    Int |    10.702 ms |  0.0480 ms |  0.0449 ms |  0.91 |   2687.5000 |           - |           - |          8283.24 KB |
| PooledIEnumerableConstructor |   1000 |    Int |    10.142 ms |  0.0583 ms |  0.0517 ms |  0.86 |     31.2500 |           - |           - |              125 KB |
|                              |        |        |              |            |            |       |             |             |             |                     |
|  **QueueICollectionConstructor** |   **1000** | **String** |    **17.457 ms** |  **0.1016 ms** |  **0.0951 ms** |  **1.00** |   **5281.2500** |           **-** |           **-** |         **16271.43 KB** |
| PooledICollectionConstructor |   1000 | String |     2.811 ms |  0.0215 ms |  0.0191 ms |  0.16 |     11.7188 |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor |   1000 | String |    15.238 ms |  0.0972 ms |  0.0909 ms |  0.87 |   5281.2500 |           - |           - |         16277.55 KB |
| PooledIEnumerableConstructor |   1000 | String |    19.718 ms |  0.1103 ms |  0.0978 ms |  1.13 |     31.2500 |           - |           - |              133 KB |
|                              |        |        |              |            |            |       |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** |    **Int** |   **117.254 ms** |  **0.7671 ms** |  **0.6801 ms** |  **1.00** |  **41600.0000** |           **-** |           **-** |         **128520.3 KB** |
| PooledICollectionConstructor |  10000 |    Int |    14.749 ms |  0.0632 ms |  0.0592 ms |  0.13 |           - |           - |           - |               47 KB |
|  QueueIEnumerableConstructor |  10000 |    Int |   107.577 ms |  0.7614 ms |  0.6749 ms |  0.92 |  41600.0000 |           - |           - |         128520.3 KB |
| PooledIEnumerableConstructor |  10000 |    Int |   107.013 ms |  0.4483 ms |  0.4193 ms |  0.91 |           - |           - |           - |            126.4 KB |
|                              |        |        |              |            |            |       |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** | **String** |   **252.116 ms** |  **2.3299 ms** |  **2.0654 ms** |  **1.00** |  **41500.0000** |  **41500.0000** |  **41500.0000** |        **256378.94 KB** |
| PooledICollectionConstructor |  10000 | String |    37.089 ms |  0.1585 ms |  0.1405 ms |  0.15 |           - |           - |           - |            47.43 KB |
|  QueueIEnumerableConstructor |  10000 | String |   230.505 ms |  1.6925 ms |  1.5004 ms |  0.91 |  41333.3333 |  41333.3333 |  41333.3333 |        256378.94 KB |
| PooledIEnumerableConstructor |  10000 | String |   218.684 ms |  0.5767 ms |  0.4503 ms |  0.87 |           - |           - |           - |           133.33 KB |
|                              |        |        |              |            |            |       |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** |    **Int** | **1,316.771 ms** |  **6.4342 ms** |  **5.7037 ms** |  **1.00** | **211000.0000** | **169000.0000** | **166000.0000** |       **1027504.86 KB** |
| PooledICollectionConstructor | 100000 |    Int |   126.526 ms |  5.7559 ms |  5.1024 ms |  0.10 |           - |           - |           - |               48 KB |
|  QueueIEnumerableConstructor | 100000 |    Int | 1,204.817 ms |  4.4635 ms |  3.7273 ms |  0.91 | 207000.0000 | 167000.0000 | 164000.0000 |       1027472.11 KB |
| PooledIEnumerableConstructor | 100000 |    Int |   978.338 ms |  2.6196 ms |  2.4504 ms |  0.74 |           - |           - |           - |              128 KB |
|                              |        |        |              |            |            |       |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** | **String** | **2,230.824 ms** | **18.5342 ms** | **17.3369 ms** |  **1.00** | **367000.0000** | **302000.0000** | **300000.0000** |       **2053670.26 KB** |
| PooledICollectionConstructor | 100000 | String |   318.834 ms |  1.6053 ms |  1.5016 ms |  0.14 |           - |           - |           - |               48 KB |
|  QueueIEnumerableConstructor | 100000 | String | 2,030.491 ms |  5.4027 ms |  5.0537 ms |  0.91 | 345000.0000 | 296000.0000 | 294000.0000 |       2053772.52 KB |
| PooledIEnumerableConstructor | 100000 | String | 2,011.165 ms |  8.5119 ms |  7.1078 ms |  0.90 |           - |           - |           - |              136 KB |
