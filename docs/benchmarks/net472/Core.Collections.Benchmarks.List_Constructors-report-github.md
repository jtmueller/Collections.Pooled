``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                              Method |      N |         Mean |        Error |       StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|            **ListArrayConstructor_Int** |   **1000** |     **262.6 us** |     **1.720 us** |     **1.609 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.97 KB** |
|          PooledArrayConstructor_Int |   1000 |   1,068.0 us |     2.218 us |     1.966 us |  4.07 |    0.03 |      9.7656 |           - |           - |            31.25 KB |
|         ListArrayConstructor_String |   1000 |     750.9 us |     4.328 us |     3.614 us |  2.86 |    0.02 |   2556.6406 |           - |           - |          7876.88 KB |
|       PooledArrayConstructor_String |   1000 |   4,260.5 us |    12.860 us |    12.030 us | 16.23 |    0.09 |      7.8125 |           - |           - |            31.25 KB |
|      ListICollectionConstructor_Int |   1000 |     278.3 us |     1.831 us |     1.623 us |  1.06 |    0.01 |   1290.0391 |           - |           - |          3968.97 KB |
|    PooledICollectionConstructor_Int |   1000 |   1,074.5 us |     3.447 us |     3.224 us |  4.09 |    0.03 |      9.7656 |           - |           - |            31.25 KB |
|   ListICollectionConstructor_String |   1000 |     735.3 us |     3.688 us |     3.269 us |  2.80 |    0.02 |   2556.6406 |           - |           - |          7876.88 KB |
| PooledICollectionConstructor_String |   1000 |   2,735.2 us |     7.931 us |     7.030 us | 10.42 |    0.07 |      7.8125 |           - |           - |            31.25 KB |
|                                     |        |              |              |              |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** |  **10000** |   **2,619.1 us** |    **14.378 us** |    **13.449 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |         **39210.66 KB** |
|          PooledArrayConstructor_Int |  10000 |  15,222.9 us |    28.322 us |    26.493 us |  5.81 |    0.03 |           - |           - |           - |            31.38 KB |
|         ListArrayConstructor_String |  10000 |   7,267.3 us |    25.495 us |    23.848 us |  2.77 |    0.02 |  24992.1875 |           - |           - |         78371.88 KB |
|       PooledArrayConstructor_String |  10000 |  50,331.4 us |   192.347 us |   179.922 us | 19.22 |    0.11 |           - |           - |           - |               32 KB |
|      ListICollectionConstructor_Int |  10000 |   2,603.5 us |    22.512 us |    21.057 us |  0.99 |    0.01 |  12656.2500 |           - |           - |         39210.66 KB |
|    PooledICollectionConstructor_Int |  10000 |  15,276.9 us |    32.429 us |    28.747 us |  5.83 |    0.03 |           - |           - |           - |            31.38 KB |
|   ListICollectionConstructor_String |  10000 |   7,181.1 us |    22.846 us |    21.371 us |  2.74 |    0.02 |  24992.1875 |           - |           - |         78371.88 KB |
| PooledICollectionConstructor_String |  10000 |  36,539.7 us |   143.395 us |   134.132 us | 13.95 |    0.09 |           - |           - |           - |            31.43 KB |
|                                     |        |              |              |              |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** | **100000** |  **62,971.1 us** |   **915.340 us** |   **856.210 us** |  **1.00** |    **0.00** |  **22250.0000** |  **22000.0000** |  **22000.0000** |        **390872.01 KB** |
|          PooledArrayConstructor_Int | 100000 | 138,787.3 us |   951.732 us |   890.250 us |  2.20 |    0.04 |           - |           - |           - |               32 KB |
|         ListArrayConstructor_String | 100000 | 490,329.2 us | 9,080.163 us | 8,049.328 us |  7.78 |    0.15 |  29000.0000 |  29000.0000 |  29000.0000 |         781563.5 KB |
|       PooledArrayConstructor_String | 100000 | 447,665.6 us | 2,565.758 us | 2,400.012 us |  7.11 |    0.11 |           - |           - |           - |               32 KB |
|      ListICollectionConstructor_Int | 100000 |  63,323.1 us | 1,048.839 us |   929.769 us |  1.00 |    0.02 |  23333.3333 |  23000.0000 |  23000.0000 |        390885.44 KB |
|    PooledICollectionConstructor_Int | 100000 | 138,329.3 us |   639.320 us |   566.741 us |  2.19 |    0.03 |           - |           - |           - |               32 KB |
|   ListICollectionConstructor_String | 100000 | 481,372.9 us | 2,347.632 us | 2,081.115 us |  7.64 |    0.11 |  27000.0000 |  27000.0000 |  27000.0000 |        781554.33 KB |
| PooledICollectionConstructor_String | 100000 | 313,611.9 us |   966.461 us |   856.743 us |  4.98 |    0.06 |           - |           - |           - |               32 KB |
