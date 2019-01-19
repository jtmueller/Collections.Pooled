``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                              Method |      N |         Mean |        Error |        StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|-------------:|--------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|            **ListArrayConstructor_Int** |   **1000** |     **283.2 us** |     **1.670 us** |      **1.481 us** |     **283.3 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.75 KB** |
|          PooledArrayConstructor_Int |   1000 |     134.8 us |     2.582 us |      2.870 us |     135.7 us |  0.48 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|         ListArrayConstructor_String |   1000 |     633.5 us |     3.588 us |      3.180 us |     632.9 us |  2.24 |    0.01 |   2556.6406 |           - |           - |             7875 KB |
|       PooledArrayConstructor_String |   1000 |     652.8 us |     8.252 us |      7.719 us |     653.9 us |  2.30 |    0.03 |     12.6953 |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int |   1000 |     271.1 us |     1.720 us |      1.436 us |     270.9 us |  0.96 |    0.01 |   1290.0391 |           - |           - |          3968.75 KB |
|    PooledICollectionConstructor_Int |   1000 |     129.3 us |     2.527 us |      3.624 us |     130.7 us |  0.45 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String |   1000 |     681.3 us |     1.869 us |      1.657 us |     681.6 us |  2.41 |    0.01 |   2556.6406 |           - |           - |             7875 KB |
| PooledICollectionConstructor_String |   1000 |     585.2 us |     2.179 us |      1.932 us |     584.4 us |  2.07 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|                                     |        |              |              |               |              |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** |  **10000** |   **2,694.6 us** |    **15.140 us** |     **13.421 us** |   **2,692.3 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
|          PooledArrayConstructor_Int |  10000 |   1,078.7 us |    21.380 us |     27.800 us |   1,094.0 us |  0.40 |    0.01 |     11.7188 |           - |           - |            39.06 KB |
|         ListArrayConstructor_String |  10000 |   6,358.6 us |    34.606 us |     28.897 us |   6,355.9 us |  2.36 |    0.02 |  24992.1875 |           - |           - |          78187.5 KB |
|       PooledArrayConstructor_String |  10000 |   8,919.9 us |    38.036 us |     33.718 us |   8,910.0 us |  3.31 |    0.02 |           - |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int |  10000 |   2,700.7 us |    47.942 us |     37.430 us |   2,699.2 us |  1.00 |    0.01 |  12656.2500 |           - |           - |            39125 KB |
|    PooledICollectionConstructor_Int |  10000 |   1,071.6 us |    21.253 us |     33.089 us |   1,074.2 us |  0.40 |    0.01 |     11.7188 |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String |  10000 |   6,276.9 us |   115.003 us |    112.949 us |   6,297.8 us |  2.33 |    0.04 |  24992.1875 |           - |           - |          78187.5 KB |
| PooledICollectionConstructor_String |  10000 |   8,670.1 us |   169.969 us |    288.621 us |   8,804.9 us |  3.22 |    0.11 |           - |           - |           - |            39.06 KB |
|                                     |        |              |              |               |              |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** | **100000** | **167,005.1 us** | **3,864.058 us** |  **3,425.386 us** | **166,383.7 us** |  **1.00** |    **0.00** |  **34000.0000** |  **34000.0000** |  **34000.0000** |        **390916.83 KB** |
|          PooledArrayConstructor_Int | 100000 |  14,571.4 us |    13.368 us |     11.163 us |  14,570.8 us |  0.09 |    0.00 |           - |           - |           - |            39.06 KB |
|         ListArrayConstructor_String | 100000 | 392,432.3 us | 7,805.978 us | 15,768.461 us | 393,311.7 us |  2.35 |    0.10 |  17000.0000 |  17000.0000 |  17000.0000 |        781399.17 KB |
|       PooledArrayConstructor_String | 100000 |  89,273.5 us |   600.535 us |    468.858 us |  89,072.5 us |  0.54 |    0.01 |           - |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int | 100000 | 158,683.1 us | 3,055.453 us |  3,864.169 us | 159,065.0 us |  0.96 |    0.03 |  34000.0000 |  34000.0000 |  34000.0000 |        390916.23 KB |
|    PooledICollectionConstructor_Int | 100000 |  14,502.7 us |   264.447 us |    206.463 us |  14,567.6 us |  0.09 |    0.00 |           - |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String | 100000 | 389,796.9 us | 7,780.949 us | 19,377.264 us | 394,257.6 us |  2.34 |    0.11 |  16000.0000 |  16000.0000 |  16000.0000 |        781398.98 KB |
| PooledICollectionConstructor_String | 100000 |  89,082.1 us |   318.387 us |    297.819 us |  88,964.5 us |  0.53 |    0.01 |           - |           - |           - |            39.06 KB |
