``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |   Type |           Mean |        Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |---------------:|-------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueueICollectionConstructor** |   **1000** |    **Int** |       **285.3 us** |     **7.615 us** |     **22.092 us** |  **1.00** |    **0.00** |   **1293.4570** |           **-** |           **-** |          **3976.56 KB** |
| PooledICollectionConstructor |   1000 |    Int |       138.7 us |     1.131 us |      1.002 us |  0.44 |    0.03 |     15.1367 |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor |   1000 |    Int |     8,027.5 us |    17.897 us |     16.741 us | 25.79 |    1.65 |   2687.5000 |           - |           - |          8281.25 KB |
| PooledIEnumerableConstructor |   1000 |    Int |     7,865.6 us |    35.098 us |     32.830 us | 25.27 |    1.60 |     31.2500 |           - |           - |              125 KB |
|                              |        |        |                |              |               |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** |   **1000** | **String** |       **666.0 us** |     **5.782 us** |      **5.126 us** |  **1.00** |    **0.00** |   **2563.4766** |           **-** |           **-** |          **7882.81 KB** |
| PooledICollectionConstructor |   1000 | String |       604.8 us |     1.377 us |      1.221 us |  0.91 |    0.01 |     14.6484 |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor |   1000 | String |    12,344.0 us |    29.457 us |     27.554 us | 18.53 |    0.14 |   5281.2500 |           - |           - |         16273.44 KB |
| PooledIEnumerableConstructor |   1000 | String |    13,640.0 us |    49.413 us |     43.803 us | 20.48 |    0.13 |     31.2500 |           - |           - |           132.81 KB |
|                              |        |        |                |              |               |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** |    **Int** |     **2,646.0 us** |    **19.261 us** |     **18.017 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |         **39132.81 KB** |
| PooledICollectionConstructor |  10000 |    Int |     1,144.8 us |     4.032 us |      3.574 us |  0.43 |    0.00 |     13.6719 |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor |  10000 |    Int |    73,742.3 us |   343.689 us |    321.487 us | 27.87 |    0.20 |  41571.4286 |           - |           - |           128375 KB |
| PooledIEnumerableConstructor |  10000 |    Int |    70,373.6 us |   451.377 us |    422.219 us | 26.60 |    0.23 |           - |           - |           - |              125 KB |
|                              |        |        |                |              |               |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** |  **10000** | **String** |     **6,317.8 us** |    **93.604 us** |     **87.557 us** |  **1.00** |    **0.00** |  **24992.1875** |           **-** |           **-** |         **78195.31 KB** |
| PooledICollectionConstructor |  10000 | String |     8,063.2 us |    40.070 us |     35.521 us |  1.28 |    0.02 |           - |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor |  10000 | String |   177,053.0 us |   835.623 us |    740.758 us | 28.03 |    0.42 |  41333.3333 |  41333.3333 |  41333.3333 |        256367.19 KB |
| PooledIEnumerableConstructor |  10000 | String |   139,338.1 us | 1,386.927 us |  1,229.475 us | 22.06 |    0.36 |           - |           - |           - |           132.81 KB |
|                              |        |        |                |              |               |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** |    **Int** |   **163,694.5 us** | **3,238.811 us** |  **3,180.945 us** |  **1.00** |    **0.00** |  **37000.0000** |  **37000.0000** |  **37000.0000** |        **390945.67 KB** |
| PooledICollectionConstructor | 100000 |    Int |    14,353.6 us |   163.933 us |    153.343 us |  0.09 |    0.00 |           - |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor | 100000 |    Int | 1,010,813.2 us | 7,149.570 us |  6,337.907 us |  6.17 |    0.14 | 216000.0000 | 176000.0000 | 174000.0000 |       1025213.43 KB |
| PooledIEnumerableConstructor | 100000 |    Int |   715,287.7 us | 6,483.407 us |  5,413.938 us |  4.38 |    0.10 |           - |           - |           - |              125 KB |
|                              |        |        |                |              |               |       |         |             |             |             |                     |
|  **QueueICollectionConstructor** | **100000** | **String** |   **379,050.4 us** | **7,476.814 us** | **14,758.501 us** |  **1.00** |    **0.00** |  **16000.0000** |  **16000.0000** |  **16000.0000** |        **781404.85 KB** |
| PooledICollectionConstructor | 100000 | String |    81,106.7 us |   308.118 us |    273.139 us |  0.21 |    0.01 |           - |           - |           - |            46.88 KB |
|  QueueIEnumerableConstructor | 100000 | String | 1,830,788.2 us | 3,819.223 us |  3,385.642 us |  4.83 |    0.26 | 375000.0000 | 336000.0000 | 332000.0000 |       2049594.12 KB |
| PooledIEnumerableConstructor | 100000 | String | 1,347,422.9 us | 5,346.316 us |  4,739.370 us |  3.56 |    0.20 |           - |           - |           - |           132.81 KB |
