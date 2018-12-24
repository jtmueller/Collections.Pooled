``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                              Method |      N |         Mean |         Error |         StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|--------------:|---------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|            **ListArrayConstructor_Int** |   **1000** |     **259.8 us** |     **1.1058 us** |      **0.9803 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.75 KB** |
|          PooledArrayConstructor_Int |   1000 |     128.7 us |     1.8644 us |      1.7439 us |  0.50 |    0.01 |     10.0098 |           - |           - |            31.25 KB |
|         ListArrayConstructor_String |   1000 |     634.2 us |     3.2439 us |      2.8756 us |  2.44 |    0.01 |   2556.6406 |           - |           - |             7875 KB |
|       PooledArrayConstructor_String |   1000 |     585.9 us |     1.5798 us |      1.4777 us |  2.26 |    0.01 |      9.7656 |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int |   1000 |     259.8 us |     1.6810 us |      1.4037 us |  1.00 |    0.01 |   1290.0391 |           - |           - |          3968.75 KB |
|    PooledICollectionConstructor_Int |   1000 |     136.4 us |     0.2687 us |      0.2382 us |  0.53 |    0.00 |     10.0098 |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String |   1000 |     618.8 us |     2.7638 us |      2.5853 us |  2.38 |    0.01 |   2556.6406 |           - |           - |             7875 KB |
| PooledICollectionConstructor_String |   1000 |     578.2 us |     1.4391 us |      1.3461 us |  2.23 |    0.01 |      9.7656 |           - |           - |            31.25 KB |
|                                     |        |              |               |                |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** |  **10000** |   **2,585.6 us** |    **12.3521 us** |     **11.5541 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
|          PooledArrayConstructor_Int |  10000 |   1,270.3 us |     1.9302 us |      1.7111 us |  0.49 |    0.00 |      9.7656 |           - |           - |            31.25 KB |
|         ListArrayConstructor_String |  10000 |   6,293.6 us |    24.6263 us |     21.8305 us |  2.43 |    0.02 |  24992.1875 |           - |           - |          78187.5 KB |
|       PooledArrayConstructor_String |  10000 |   8,043.0 us |    41.2115 us |     34.4135 us |  3.11 |    0.02 |           - |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int |  10000 |   2,615.8 us |    15.0580 us |     13.3485 us |  1.01 |    0.01 |  12656.2500 |           - |           - |            39125 KB |
|    PooledICollectionConstructor_Int |  10000 |   1,279.9 us |     2.6783 us |      2.3743 us |  0.49 |    0.00 |      9.7656 |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String |  10000 |   6,088.4 us |    37.7994 us |     29.5113 us |  2.35 |    0.02 |  24992.1875 |           - |           - |          78187.5 KB |
| PooledICollectionConstructor_String |  10000 |   8,137.7 us |    30.6247 us |     27.1480 us |  3.15 |    0.02 |           - |           - |           - |            31.25 KB |
|                                     |        |              |               |                |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** | **100000** | **154,472.3 us** | **2,961.8363 us** |  **3,410.8551 us** |  **1.00** |    **0.00** |  **36750.0000** |  **36750.0000** |  **36750.0000** |        **390945.86 KB** |
|          PooledArrayConstructor_Int | 100000 |  13,633.9 us |    89.7351 us |     83.9382 us |  0.09 |    0.00 |           - |           - |           - |            31.25 KB |
|         ListArrayConstructor_String | 100000 | 379,995.2 us | 7,486.0077 us | 16,588.4759 us |  2.45 |    0.11 |  16000.0000 |  16000.0000 |  16000.0000 |        781399.72 KB |
|       PooledArrayConstructor_String | 100000 |  81,876.8 us |   303.2479 us |    283.6583 us |  0.53 |    0.01 |           - |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int | 100000 | 154,249.9 us | 3,556.8443 us |  3,327.0744 us |  1.00 |    0.03 |  33500.0000 |  33500.0000 |  33500.0000 |        390928.12 KB |
|    PooledICollectionConstructor_Int | 100000 |  13,625.8 us |    65.5412 us |     61.3073 us |  0.09 |    0.00 |           - |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String | 100000 | 378,389.4 us | 7,533.7420 us | 17,757.9163 us |  2.43 |    0.16 |  10000.0000 |  10000.0000 |  10000.0000 |        781366.45 KB |
| PooledICollectionConstructor_String | 100000 |  81,823.8 us |   203.4052 us |    180.3134 us |  0.53 |    0.01 |           - |           - |           - |            31.25 KB |
