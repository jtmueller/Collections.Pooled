``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                              Method |      N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|            **ListArrayConstructor_Int** |   **1000** |     **288.2 us** |     **1.293 us** |     **1.080 us** |     **288.2 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.97 KB** |
|          PooledArrayConstructor_Int |   1000 |   1,124.0 us |    22.372 us |    46.203 us |   1,094.5 us |  3.99 |    0.19 |     11.7188 |           - |           - |            39.06 KB |
|         ListArrayConstructor_String |   1000 |     787.5 us |     6.278 us |     5.243 us |     786.0 us |  2.73 |    0.02 |   2556.6406 |           - |           - |          7876.88 KB |
|       PooledArrayConstructor_String |   1000 |   4,818.3 us |     6.852 us |     6.409 us |   4,820.2 us | 16.71 |    0.07 |      7.8125 |           - |           - |            39.06 KB |
|      ListICollectionConstructor_Int |   1000 |     283.9 us |     3.515 us |     2.935 us |     282.7 us |  0.98 |    0.01 |   1290.0391 |           - |           - |          3968.97 KB |
|    PooledICollectionConstructor_Int |   1000 |   1,180.3 us |     2.483 us |     2.322 us |   1,179.9 us |  4.09 |    0.01 |     11.7188 |           - |           - |            39.06 KB |
|   ListICollectionConstructor_String |   1000 |     807.6 us |    15.640 us |    19.779 us |     815.7 us |  2.80 |    0.08 |   2556.6406 |           - |           - |          7876.88 KB |
| PooledICollectionConstructor_String |   1000 |   2,734.8 us |     7.018 us |     5.860 us |   2,733.3 us |  9.49 |    0.04 |     11.7188 |           - |           - |            39.06 KB |
|                                     |        |              |              |              |              |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** |  **10000** |   **2,918.8 us** |     **9.660 us** |     **9.036 us** |   **2,920.4 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |         **39210.66 KB** |
|          PooledArrayConstructor_Int |  10000 |  15,534.1 us |    40.193 us |    33.563 us |  15,533.7 us |  5.32 |    0.03 |           - |           - |           - |            39.13 KB |
|         ListArrayConstructor_String |  10000 |   7,374.0 us |    29.901 us |    26.506 us |   7,379.0 us |  2.53 |    0.01 |  24992.1875 |           - |           - |         78371.88 KB |
|       PooledArrayConstructor_String |  10000 |  56,600.7 us |   336.771 us |   315.016 us |  56,685.8 us | 19.39 |    0.12 |           - |           - |           - |            39.11 KB |
|      ListICollectionConstructor_Int |  10000 |   2,769.9 us |    54.601 us |    99.842 us |   2,720.9 us |  0.95 |    0.04 |  12656.2500 |           - |           - |         39210.66 KB |
|    PooledICollectionConstructor_Int |  10000 |  16,906.3 us |   129.726 us |   101.281 us |  16,866.2 us |  5.79 |    0.03 |           - |           - |           - |            39.25 KB |
|   ListICollectionConstructor_String |  10000 |   7,421.8 us |    44.226 us |    39.205 us |   7,408.7 us |  2.54 |    0.02 |  24992.1875 |           - |           - |         78371.88 KB |
| PooledICollectionConstructor_String |  10000 |  40,165.1 us |   192.751 us |   180.300 us |  40,186.8 us | 13.76 |    0.08 |           - |           - |           - |            39.38 KB |
|                                     |        |              |              |              |              |       |         |             |             |             |                     |
|            **ListArrayConstructor_Int** | **100000** |  **63,285.3 us** |   **816.791 us** |   **724.064 us** |  **63,043.2 us** |  **1.00** |    **0.00** |  **22500.0000** |  **22375.0000** |  **22375.0000** |        **390864.85 KB** |
|          PooledArrayConstructor_Int | 100000 | 140,357.4 us |   492.531 us |   436.616 us | 140,354.7 us |  2.22 |    0.02 |           - |           - |           - |               40 KB |
|         ListArrayConstructor_String | 100000 | 517,473.5 us | 8,037.517 us | 7,125.049 us | 519,256.7 us |  8.18 |    0.12 |  28000.0000 |  28000.0000 |  28000.0000 |        781555.77 KB |
|       PooledArrayConstructor_String | 100000 | 465,258.3 us | 2,681.828 us | 2,239.448 us | 464,661.8 us |  7.35 |    0.09 |           - |           - |           - |               40 KB |
|      ListICollectionConstructor_Int | 100000 |  62,672.7 us | 1,202.182 us | 1,124.522 us |  62,277.9 us |  0.99 |    0.02 |  22000.0000 |  21777.7778 |  21777.7778 |        390864.95 KB |
|    PooledICollectionConstructor_Int | 100000 | 140,304.7 us |   182.869 us |   162.109 us | 140,265.7 us |  2.22 |    0.02 |           - |           - |           - |               40 KB |
|   ListICollectionConstructor_String | 100000 | 526,487.4 us | 3,474.316 us | 3,249.878 us | 526,554.1 us |  8.32 |    0.12 |  27000.0000 |  27000.0000 |  27000.0000 |        781554.56 KB |
| PooledICollectionConstructor_String | 100000 | 345,842.6 us |   493.312 us |   437.308 us | 345,798.0 us |  5.47 |    0.06 |           - |           - |           - |               40 KB |
