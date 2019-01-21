``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                       Method |      N |   Type |           Mean |        Error |       StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |---------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **StackICollectionConstructor** |   **1000** |    **Int** |       **395.6 us** |     **2.612 us** |     **2.444 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.97 KB** |
| PooledICollectionConstructor |   1000 |    Int |     1,090.6 us |    12.999 us |    12.159 us |  2.76 |    0.04 |     11.7188 |           - |           - |            39.06 KB |
|  StackIEnumerableConstructor |   1000 |    Int |     8,398.1 us |    46.297 us |    43.306 us | 21.23 |    0.22 |   2687.5000 |           - |           - |          8274.68 KB |
| PooledIEnumerableConstructor |   1000 |    Int |     9,527.8 us |    38.408 us |    35.927 us | 24.09 |    0.20 |     15.6250 |           - |           - |            78.13 KB |
|                              |        |        |                |              |              |       |         |             |             |             |                     |
|  **StackICollectionConstructor** |   **1000** | **String** |     **1,055.2 us** |     **7.107 us** |     **6.648 us** |  **1.00** |    **0.00** |   **2556.6406** |           **-** |           **-** |          **7876.89 KB** |
| PooledICollectionConstructor |   1000 | String |     2,951.2 us |    35.584 us |    33.285 us |  2.80 |    0.03 |     11.7188 |           - |           - |            39.06 KB |
|  StackIEnumerableConstructor |   1000 | String |    14,409.5 us |   113.075 us |   105.771 us | 13.66 |    0.14 |   5281.2500 |           - |           - |         16271.19 KB |
| PooledIEnumerableConstructor |   1000 | String |    18,159.4 us |   186.203 us |   174.174 us | 17.21 |    0.22 |           - |           - |           - |               86 KB |
|                              |        |        |                |              |              |       |         |             |             |             |                     |
|  **StackICollectionConstructor** |  **10000** |    **Int** |     **3,725.4 us** |    **34.754 us** |    **29.021 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |         **39210.66 KB** |
| PooledICollectionConstructor |  10000 |    Int |    15,760.4 us |    53.734 us |    50.263 us |  4.23 |    0.04 |           - |           - |           - |            39.25 KB |
|  StackIEnumerableConstructor |  10000 |    Int |    81,163.5 us |   316.660 us |   264.425 us | 21.79 |    0.20 |  41500.0000 |           - |           - |        128520.83 KB |
| PooledIEnumerableConstructor |  10000 |    Int |    96,919.4 us | 1,747.900 us | 1,634.986 us | 25.89 |    0.36 |           - |           - |           - |            78.67 KB |
|                              |        |        |                |              |              |       |         |             |             |             |                     |
|  **StackICollectionConstructor** |  **10000** | **String** |     **9,393.5 us** |    **78.826 us** |    **73.734 us** |  **1.00** |    **0.00** |  **24984.3750** |           **-** |           **-** |         **78371.88 KB** |
| PooledICollectionConstructor |  10000 | String |    39,523.1 us |   157.993 us |   147.787 us |  4.21 |    0.04 |           - |           - |           - |            39.38 KB |
|  StackIEnumerableConstructor |  10000 | String |   206,030.1 us | 2,056.145 us | 1,822.719 us | 21.96 |    0.26 |  41333.3333 |  41333.3333 |  41333.3333 |        256378.94 KB |
| PooledIEnumerableConstructor |  10000 | String |   200,138.1 us |   787.944 us |   737.043 us | 21.31 |    0.18 |           - |           - |           - |               88 KB |
|                              |        |        |                |              |              |       |         |             |             |             |                     |
|  **StackICollectionConstructor** | **100000** |    **Int** |    **68,601.5 us** |   **655.602 us** |   **581.175 us** |  **1.00** |    **0.00** |  **23750.0000** |  **23750.0000** |  **23750.0000** |        **390892.55 KB** |
| PooledICollectionConstructor | 100000 |    Int |   130,868.1 us |   473.294 us |   395.222 us |  1.91 |    0.02 |           - |           - |           - |               40 KB |
|  StackIEnumerableConstructor | 100000 |    Int |   894,255.5 us | 6,738.485 us | 6,303.183 us | 13.04 |    0.16 | 190000.0000 | 151000.0000 | 148000.0000 |       1027458.71 KB |
| PooledIEnumerableConstructor | 100000 |    Int |   913,832.8 us | 5,136.473 us | 4,804.660 us | 13.33 |    0.11 |           - |           - |           - |               80 KB |
|                              |        |        |                |              |              |       |         |             |             |             |                     |
|  **StackICollectionConstructor** | **100000** | **String** |   **518,030.9 us** | **9,736.868 us** | **9,107.873 us** |  **1.00** |    **0.00** |   **6000.0000** |   **6000.0000** |   **6000.0000** |        **781391.77 KB** |
| PooledICollectionConstructor | 100000 | String |   330,863.5 us | 2,365.922 us | 1,975.652 us |  0.64 |    0.01 |           - |           - |           - |               40 KB |
|  StackIEnumerableConstructor | 100000 | String | 1,781,122.4 us | 6,852.579 us | 6,409.907 us |  3.44 |    0.06 | 326000.0000 | 286000.0000 | 283000.0000 |        2053209.6 KB |
| PooledIEnumerableConstructor | 100000 | String | 1,798,668.8 us | 7,251.833 us | 6,055.609 us |  3.48 |    0.07 |           - |           - |           - |               88 KB |
