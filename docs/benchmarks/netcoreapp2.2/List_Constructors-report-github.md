``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                              Method |      N |         Mean |        Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|-------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|            **ListArrayConstructor_Int** |   **1000** |   **1,141.9 us** |    **13.262 us** |     **12.405 us** |  **1.00** |    **0.00** |   **1289.0625** |           **-** |           **-** |          **3968.75 KB** |
|          PooledArrayConstructor_Int |   1000 |     166.4 us |     1.251 us |      1.170 us |  0.15 |    0.00 |     12.6953 |           - |           - |            39.06 KB |
|         ListArrayConstructor_String |   1000 |   2,526.6 us |    22.215 us |     20.780 us |  2.21 |    0.03 |   2554.6875 |           - |           - |             7875 KB |
|       PooledArrayConstructor_String |   1000 |     721.5 us |     5.541 us |      4.912 us |  0.63 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int |   1000 |   1,115.5 us |     8.089 us |      7.567 us |  0.98 |    0.01 |   1289.0625 |           - |           - |          3968.75 KB |
|    PooledICollectionConstructor_Int |   1000 |     163.0 us |     1.910 us |      1.787 us |  0.14 |    0.00 |     12.6953 |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String |   1000 |   2,522.8 us |    21.387 us |     20.005 us |  2.21 |    0.03 |   2554.6875 |           - |           - |             7875 KB |
| PooledICollectionConstructor_String |   1000 |     697.4 us |     4.600 us |      4.303 us |  0.61 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|                                     |        |              |              |               |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** |  **10000** |   **9,855.1 us** |   **106.504 us** |     **83.152 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
|          PooledArrayConstructor_Int |  10000 |   1,221.2 us |     9.113 us |      8.525 us |  0.12 |    0.00 |     11.7188 |           - |           - |            39.06 KB |
|         ListArrayConstructor_String |  10000 |  19,458.2 us |   137.238 us |    121.658 us |  1.98 |    0.02 |  24968.7500 |           - |           - |          78187.5 KB |
|       PooledArrayConstructor_String |  10000 |   9,563.4 us |   185.452 us |    173.472 us |  0.97 |    0.02 |           - |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int |  10000 |   9,827.6 us |    92.233 us |     86.275 us |  1.00 |    0.01 |  12656.2500 |           - |           - |            39125 KB |
|    PooledICollectionConstructor_Int |  10000 |   1,134.3 us |     9.369 us |      8.763 us |  0.12 |    0.00 |     11.7188 |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String |  10000 |  19,239.1 us |   151.684 us |    141.885 us |  1.95 |    0.02 |  24968.7500 |           - |           - |          78187.5 KB |
| PooledICollectionConstructor_String |  10000 |   9,478.2 us |    66.951 us |     62.626 us |  0.96 |    0.01 |           - |           - |           - |            39.06 KB |
|                                     |        |              |              |               |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** | **100000** | **184,671.5 us** | **1,924.389 us** |  **1,606.952 us** |  **1.00** |    **0.00** |  **46666.6667** |  **46666.6667** |  **46666.6667** |        **391044.46 KB** |
|          PooledArrayConstructor_Int | 100000 |  15,763.3 us |   147.852 us |    138.301 us |  0.09 |    0.00 |           - |           - |           - |            39.06 KB |
|         ListArrayConstructor_String | 100000 | 494,008.6 us | 9,751.201 us | 13,017.573 us |  2.66 |    0.08 |  20000.0000 |  20000.0000 |  20000.0000 |        781424.14 KB |
|       PooledArrayConstructor_String | 100000 |  95,182.8 us |   611.956 us |    542.483 us |  0.52 |    0.01 |           - |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int | 100000 | 185,511.4 us | 1,557.566 us |  1,456.948 us |  1.01 |    0.01 |  46000.0000 |  46000.0000 |  46000.0000 |        391039.22 KB |
|    PooledICollectionConstructor_Int | 100000 |  16,490.8 us |   173.816 us |    162.588 us |  0.09 |    0.00 |           - |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String | 100000 | 500,435.4 us | 9,832.581 us |  9,656.909 us |  2.72 |    0.06 |  16000.0000 |  16000.0000 |  16000.0000 |        781399.23 KB |
| PooledICollectionConstructor_String | 100000 |  94,852.5 us |   616.586 us |    546.588 us |  0.51 |    0.01 |           - |           - |           - |            39.06 KB |
