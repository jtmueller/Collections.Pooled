``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                              Method |      N |         Mean |         Error |         StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|--------------:|---------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|            **ListArrayConstructor_Int** |   **1000** |     **408.7 us** |     **7.7469 us** |      **8.2891 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.75 KB** |
|          PooledArrayConstructor_Int |   1000 |     136.9 us |     1.0249 us |      0.9587 us |  0.34 |    0.01 |     10.0098 |           - |           - |            31.25 KB |
|         ListArrayConstructor_String |   1000 |     949.5 us |     5.4245 us |      4.8087 us |  2.33 |    0.05 |   2556.6406 |           - |           - |             7875 KB |
|       PooledArrayConstructor_String |   1000 |     640.1 us |     7.8443 us |      6.9538 us |  1.57 |    0.04 |      9.7656 |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int |   1000 |     396.5 us |     6.3496 us |      5.9394 us |  0.97 |    0.02 |   1290.0391 |           - |           - |          3968.75 KB |
|    PooledICollectionConstructor_Int |   1000 |     126.7 us |     0.9254 us |      0.8656 us |  0.31 |    0.01 |     10.0098 |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String |   1000 |     921.3 us |     7.8813 us |      7.3722 us |  2.26 |    0.06 |   2556.6406 |           - |           - |             7875 KB |
| PooledICollectionConstructor_String |   1000 |     643.8 us |     2.1891 us |      1.9406 us |  1.58 |    0.04 |      9.7656 |           - |           - |            31.25 KB |
|                                     |        |              |               |                |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** |  **10000** |   **3,759.2 us** |    **61.9660 us** |     **57.9631 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
|          PooledArrayConstructor_Int |  10000 |   1,069.9 us |     8.9350 us |      8.3578 us |  0.28 |    0.00 |      9.7656 |           - |           - |            31.25 KB |
|         ListArrayConstructor_String |  10000 |   8,382.5 us |    45.0306 us |     37.6026 us |  2.23 |    0.04 |  24984.3750 |           - |           - |          78187.5 KB |
|       PooledArrayConstructor_String |  10000 |   8,749.4 us |    76.2392 us |     71.3142 us |  2.33 |    0.03 |           - |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int |  10000 |   3,809.0 us |    41.7102 us |     39.0158 us |  1.01 |    0.02 |  12656.2500 |           - |           - |            39125 KB |
|    PooledICollectionConstructor_Int |  10000 |   1,105.2 us |     6.6771 us |      5.5757 us |  0.29 |    0.01 |      9.7656 |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String |  10000 |   8,286.2 us |   155.6103 us |    145.5580 us |  2.21 |    0.07 |  24984.3750 |           - |           - |          78187.5 KB |
| PooledICollectionConstructor_String |  10000 |   8,609.2 us |   161.9254 us |    143.5427 us |  2.29 |    0.06 |           - |           - |           - |            31.25 KB |
|                                     |        |              |               |                |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** | **100000** | **170,412.4 us** | **3,326.2177 us** |  **4,662.8926 us** |  **1.00** |    **0.00** |  **37500.0000** |  **37500.0000** |  **37500.0000** |           **390946 KB** |
|          PooledArrayConstructor_Int | 100000 |  14,538.6 us |    84.3630 us |     78.9132 us |  0.09 |    0.00 |           - |           - |           - |            31.25 KB |
|         ListArrayConstructor_String | 100000 | 410,907.5 us | 8,142.9978 us | 13,379.1789 us |  2.41 |    0.12 |  21000.0000 |  21000.0000 |  21000.0000 |        781429.52 KB |
|       PooledArrayConstructor_String | 100000 |  88,563.7 us | 1,662.6129 us |  1,778.9772 us |  0.52 |    0.02 |           - |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int | 100000 | 159,436.6 us | 2,521.5993 us |  2,358.7055 us |  0.94 |    0.03 |  35750.0000 |  35750.0000 |  35750.0000 |        390940.81 KB |
|    PooledICollectionConstructor_Int | 100000 |  14,750.6 us |   291.9172 us |    258.7770 us |  0.09 |    0.00 |           - |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String | 100000 | 397,726.3 us | 9,480.8851 us | 27,354.5212 us |  2.32 |    0.09 |  13000.0000 |  13000.0000 |  13000.0000 |        781390.52 KB |
| PooledICollectionConstructor_String | 100000 |  89,496.6 us |   438.3750 us |    410.0562 us |  0.53 |    0.02 |           - |           - |           - |            31.25 KB |
