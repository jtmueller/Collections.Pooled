``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                              Method |      N |         Mean |        Error |        StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------ |------- |-------------:|-------------:|--------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|           **StackArrayConstructor_Int** |   **1000** |   **1,153.8 us** |    **10.044 us** |      **8.904 us** |   **1,156.3 us** |  **1.00** |    **0.00** |   **1289.0625** |           **-** |           **-** |          **3968.75 KB** |
|          PooledArrayConstructor_Int |   1000 |     164.1 us |     3.167 us |      3.647 us |     163.9 us |  0.14 |    0.00 |     12.6953 |           - |           - |            39.06 KB |
|        StackArrayConstructor_String |   1000 |   3,375.7 us |   259.926 us |    766.398 us |   3,115.5 us |  3.75 |    0.25 |   2554.6875 |           - |           - |             7875 KB |
|       PooledArrayConstructor_String |   1000 |     713.6 us |     4.961 us |      4.640 us |     712.4 us |  0.62 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|     StackICollectionConstructor_Int |   1000 |   1,123.0 us |    11.342 us |     10.610 us |   1,123.7 us |  0.97 |    0.01 |   1289.0625 |           - |           - |          3968.75 KB |
|    PooledICollectionConstructor_Int |   1000 |     181.8 us |     3.079 us |      2.730 us |     181.1 us |  0.16 |    0.00 |     12.6953 |           - |           - |            39.06 KB |
|  StackICollectionConstructor_String |   1000 |   2,553.3 us |    22.723 us |     20.144 us |   2,548.9 us |  2.21 |    0.02 |   2554.6875 |           - |           - |             7875 KB |
| PooledICollectionConstructor_String |   1000 |     698.1 us |     6.691 us |      5.588 us |     696.6 us |  0.61 |    0.01 |     12.6953 |           - |           - |            39.06 KB |
|                                     |        |              |              |               |              |       |         |             |             |             |                     |
|           **StackArrayConstructor_Int** |  **10000** |   **9,794.4 us** |    **71.151 us** |     **66.555 us** |   **9,816.2 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
|          PooledArrayConstructor_Int |  10000 |   1,470.1 us |     8.808 us |      8.239 us |   1,472.5 us |  0.15 |    0.00 |     11.7188 |           - |           - |            39.06 KB |
|        StackArrayConstructor_String |  10000 |  19,431.0 us |    89.194 us |     74.481 us |  19,446.7 us |  1.98 |    0.02 |  24968.7500 |           - |           - |          78187.5 KB |
|       PooledArrayConstructor_String |  10000 |   9,562.3 us |   189.679 us |    186.290 us |   9,544.7 us |  0.98 |    0.02 |           - |           - |           - |            39.06 KB |
|     StackICollectionConstructor_Int |  10000 |   9,771.5 us |    89.574 us |     83.787 us |   9,762.5 us |  1.00 |    0.01 |  12656.2500 |           - |           - |            39125 KB |
|    PooledICollectionConstructor_Int |  10000 |   1,303.5 us |     8.439 us |      7.894 us |   1,301.5 us |  0.13 |    0.00 |     11.7188 |           - |           - |            39.06 KB |
|  StackICollectionConstructor_String |  10000 |  19,266.0 us |   167.335 us |    139.732 us |  19,282.2 us |  1.97 |    0.02 |  24968.7500 |           - |           - |          78187.5 KB |
| PooledICollectionConstructor_String |  10000 |  10,015.3 us |   147.923 us |    123.522 us |  10,009.1 us |  1.02 |    0.02 |           - |           - |           - |            39.06 KB |
|                                     |        |              |              |               |              |       |         |             |             |             |                     |
|           **StackArrayConstructor_Int** | **100000** | **206,638.3 us** | **3,459.237 us** |  **3,235.773 us** | **206,603.0 us** |  **1.00** |    **0.00** |  **43000.0000** |  **43000.0000** |  **43000.0000** |        **391009.09 KB** |
|          PooledArrayConstructor_Int | 100000 |  16,127.9 us |   246.158 us |    230.256 us |  16,084.3 us |  0.08 |    0.00 |           - |           - |           - |            39.06 KB |
|        StackArrayConstructor_String | 100000 | 496,583.7 us | 9,842.066 us | 17,494.263 us | 494,670.3 us |  2.48 |    0.09 |  24000.0000 |  24000.0000 |  24000.0000 |        781448.16 KB |
|       PooledArrayConstructor_String | 100000 |  93,601.9 us |   803.725 us |    751.805 us |  93,509.0 us |  0.45 |    0.01 |           - |           - |           - |            39.06 KB |
|     StackICollectionConstructor_Int | 100000 | 184,245.2 us | 1,003.450 us |    938.627 us | 184,073.1 us |  0.89 |    0.02 |  47333.3333 |  47333.3333 |  47333.3333 |        391049.75 KB |
|    PooledICollectionConstructor_Int | 100000 |  16,090.6 us |   120.040 us |    112.286 us |  16,074.4 us |  0.08 |    0.00 |           - |           - |           - |            39.06 KB |
|  StackICollectionConstructor_String | 100000 | 486,239.9 us | 9,704.580 us |  9,531.195 us | 486,817.6 us |  2.35 |    0.07 |  16000.0000 |  16000.0000 |  16000.0000 |        781399.55 KB |
| PooledICollectionConstructor_String | 100000 |  93,901.1 us |   423.104 us |    395.772 us |  93,771.3 us |  0.45 |    0.01 |           - |           - |           - |            39.06 KB |
