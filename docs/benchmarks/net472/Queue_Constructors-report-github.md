``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                       Method |      N |   Type |         Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |-------------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|  **QueueICollectionConstructor** |   **1000** |    **Int** |    **11.391 ms** | **0.0244 ms** | **0.0229 ms** |  **1.00** |   **2687.5000** |           **-** |           **-** |          **8283.24 KB** |
| PooledICollectionConstructor |   1000 |    Int |     1.032 ms | 0.0030 ms | 0.0027 ms |  0.09 |     19.5313 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |   1000 |    Int |    10.315 ms | 0.0184 ms | 0.0172 ms |  0.91 |   2687.5000 |           - |           - |          8283.24 KB |
| PooledIEnumerableConstructor |   1000 |    Int |    10.645 ms | 0.0300 ms | 0.0280 ms |  0.93 |     46.8750 |           - |           - |           156.25 KB |
|                              |        |        |              |           |           |       |             |             |             |                     |
|  **QueueICollectionConstructor** |   **1000** | **String** |    **16.695 ms** | **0.0754 ms** | **0.0668 ms** |  **1.00** |   **5281.2500** |           **-** |           **-** |         **16271.43 KB** |
| PooledICollectionConstructor |   1000 | String |     2.752 ms | 0.0252 ms | 0.0236 ms |  0.16 |     19.5313 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |   1000 | String |    14.541 ms | 0.0479 ms | 0.0448 ms |  0.87 |   5281.2500 |           - |           - |         16277.55 KB |
| PooledIEnumerableConstructor |   1000 | String |    19.564 ms | 0.0651 ms | 0.0577 ms |  1.17 |     31.2500 |           - |           - |           164.25 KB |
|                              |        |        |              |           |           |       |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** |    **Int** |   **113.041 ms** | **0.3355 ms** | **0.2974 ms** |  **1.00** |  **41600.0000** |           **-** |           **-** |         **128520.3 KB** |
| PooledICollectionConstructor |  10000 |    Int |    14.515 ms | 0.0318 ms | 0.0282 ms |  0.13 |     15.6250 |           - |           - |             62.5 KB |
|  QueueIEnumerableConstructor |  10000 |    Int |   103.089 ms | 0.3517 ms | 0.3289 ms |  0.91 |  41600.0000 |           - |           - |         128520.3 KB |
| PooledIEnumerableConstructor |  10000 |    Int |   113.616 ms | 0.3100 ms | 0.2900 ms |  1.01 |           - |           - |           - |           156.44 KB |
|                              |        |        |              |           |           |       |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** | **String** |   **228.932 ms** | **0.6845 ms** | **0.6403 ms** |  **1.00** |  **41333.3333** |  **41333.3333** |  **41333.3333** |        **256378.94 KB** |
| PooledICollectionConstructor |  10000 | String |    36.412 ms | 0.1271 ms | 0.1189 ms |  0.16 |           - |           - |           - |            62.86 KB |
|  QueueIEnumerableConstructor |  10000 | String |   208.680 ms | 0.1809 ms | 0.1604 ms |  0.91 |  41333.3333 |  41333.3333 |  41333.3333 |        256378.94 KB |
| PooledIEnumerableConstructor |  10000 | String |   219.521 ms | 0.8929 ms | 0.8352 ms |  0.96 |           - |           - |           - |           165.33 KB |
|                              |        |        |              |           |           |       |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** |    **Int** | **1,238.428 ms** | **3.2802 ms** | **2.7391 ms** |  **1.00** | **209000.0000** | **169000.0000** | **166000.0000** |        **1027748.9 KB** |
| PooledICollectionConstructor | 100000 |    Int |   121.738 ms | 0.3844 ms | 0.3596 ms |  0.10 |           - |           - |           - |               64 KB |
|  QueueIEnumerableConstructor | 100000 |    Int | 1,127.023 ms | 2.2029 ms | 2.0606 ms |  0.91 | 207000.0000 | 168000.0000 | 165000.0000 |       1027708.81 KB |
| PooledIEnumerableConstructor | 100000 |    Int | 1,040.629 ms | 2.0894 ms | 1.9545 ms |  0.84 |           - |           - |           - |              160 KB |
|                              |        |        |              |           |           |       |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** | **String** | **2,078.741 ms** | **2.4459 ms** | **2.2879 ms** |  **1.00** | **336000.0000** | **294000.0000** | **290000.0000** |       **2053203.95 KB** |
| PooledICollectionConstructor | 100000 | String |   314.074 ms | 1.1209 ms | 1.0485 ms |  0.15 |           - |           - |           - |               64 KB |
|  QueueIEnumerableConstructor | 100000 | String | 1,884.354 ms | 3.3267 ms | 2.9490 ms |  0.91 | 339000.0000 | 299000.0000 | 294000.0000 |       2053587.06 KB |
| PooledIEnumerableConstructor | 100000 | String | 2,010.509 ms | 3.0988 ms | 2.7470 ms |  0.97 |           - |           - |           - |           165.83 KB |
