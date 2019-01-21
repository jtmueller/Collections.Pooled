``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |   Type |           Mean |         Error |        StdDev |         Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |---------------:|--------------:|--------------:|---------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListICollectionConstructor** |   **1000** |    **Int** |       **391.1 us** |      **8.392 us** |     **24.345 us** |       **383.6 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.75 KB** |
| PooledICollectionConstructor |   1000 |    Int |       144.7 us |      2.515 us |      2.353 us |       145.7 us |  0.36 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |   1000 |    Int |     8,068.3 us |     39.763 us |     31.045 us |     8,076.9 us | 19.88 |    0.63 |   2687.5000 |           - |           - |          8273.44 KB |
| PooledIEnumerableConstructor |   1000 |    Int |     7,300.8 us |     72.697 us |     68.001 us |     7,311.8 us | 17.97 |    0.62 |     23.4375 |           - |           - |            78.13 KB |
|                              |        |        |                |               |               |                |       |         |             |             |             |                     |
|   **ListICollectionConstructor** |   **1000** | **String** |       **908.0 us** |      **9.783 us** |      **8.673 us** |       **910.4 us** |  **1.00** |    **0.00** |   **2556.6406** |           **-** |           **-** |             **7875 KB** |
| PooledICollectionConstructor |   1000 | String |       638.0 us |      2.734 us |      2.557 us |       637.1 us |  0.70 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |   1000 | String |    14,418.6 us |    157.464 us |    147.292 us |    14,442.1 us | 15.87 |    0.24 |   5281.2500 |           - |           - |         16265.63 KB |
| PooledIEnumerableConstructor |   1000 | String |    13,354.3 us |    169.146 us |    149.943 us |    13,347.1 us | 14.71 |    0.19 |     15.6250 |           - |           - |            85.94 KB |
|                              |        |        |                |               |               |                |       |         |             |             |             |                     |
|   **ListICollectionConstructor** |  **10000** |    **Int** |     **3,699.9 us** |     **16.555 us** |     **15.485 us** |     **3,702.6 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
| PooledICollectionConstructor |  10000 |    Int |     1,189.4 us |     16.863 us |     14.949 us |     1,189.4 us |  0.32 |    0.00 |     11.7188 |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |  10000 |    Int |    80,398.4 us |  1,221.243 us |  1,019.793 us |    79,945.4 us | 21.74 |    0.29 |  41571.4286 |           - |           - |        128367.19 KB |
| PooledIEnumerableConstructor |  10000 |    Int |    70,655.4 us |  1,277.167 us |  1,132.176 us |    70,608.8 us | 19.11 |    0.30 |           - |           - |           - |            78.13 KB |
|                              |        |        |                |               |               |                |       |         |             |             |             |                     |
|   **ListICollectionConstructor** |  **10000** | **String** |     **8,353.7 us** |     **81.059 us** |     **71.857 us** |     **8,351.0 us** |  **1.00** |    **0.00** |  **24984.3750** |           **-** |           **-** |          **78187.5 KB** |
| PooledICollectionConstructor |  10000 | String |     8,633.4 us |     61.351 us |     47.899 us |     8,633.1 us |  1.03 |    0.01 |           - |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |  10000 | String |   200,572.3 us |  3,871.430 us |  3,621.338 us |   201,645.1 us | 23.95 |    0.37 |  41333.3333 |  41333.3333 |  41333.3333 |        256359.38 KB |
| PooledIEnumerableConstructor |  10000 | String |   131,352.8 us |    515.264 us |    456.768 us |   131,435.1 us | 15.72 |    0.13 |           - |           - |           - |            85.94 KB |
|                              |        |        |                |               |               |                |       |         |             |             |             |                     |
|   **ListICollectionConstructor** | **100000** |    **Int** |   **167,928.3 us** |  **3,094.122 us** |  **5,886.889 us** |   **168,984.0 us** |  **1.00** |    **0.00** |  **33333.3333** |  **33333.3333** |  **33333.3333** |         **390911.1 KB** |
| PooledICollectionConstructor | 100000 |    Int |    14,938.7 us |     38.913 us |     34.496 us |    14,943.0 us |  0.09 |    0.00 |           - |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor | 100000 |    Int | 1,060,729.5 us | 11,459.723 us | 10,158.746 us | 1,059,940.8 us |  6.28 |    0.23 | 217000.0000 | 177000.0000 | 175000.0000 |       1025166.01 KB |
| PooledIEnumerableConstructor | 100000 |    Int |   685,837.9 us |  2,631.400 us |  2,461.414 us |   685,701.8 us |  4.06 |    0.14 |           - |           - |           - |            78.13 KB |
|                              |        |        |                |               |               |                |       |         |             |             |             |                     |
|   **ListICollectionConstructor** | **100000** | **String** |   **402,210.5 us** |  **7,970.753 us** | **12,172.181 us** |   **404,803.6 us** |  **1.00** |    **0.00** |  **10000.0000** |  **10000.0000** |  **10000.0000** |        **781366.21 KB** |
| PooledICollectionConstructor | 100000 | String |    85,478.2 us |    977.193 us |    816.000 us |    85,332.5 us |  0.21 |    0.01 |           - |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor | 100000 | String | 1,946,020.3 us | 32,471.227 us | 30,373.606 us | 1,945,977.7 us |  4.82 |    0.15 | 374000.0000 | 335000.0000 | 332000.0000 |       2048995.01 KB |
| PooledIEnumerableConstructor | 100000 | String | 1,341,813.6 us | 10,884.975 us |  9,089.447 us | 1,344,441.0 us |  3.33 |    0.08 |           - |           - |           - |            85.94 KB |
