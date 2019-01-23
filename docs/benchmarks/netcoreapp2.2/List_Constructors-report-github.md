``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |   Type |           Mean |         Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |------- |---------------:|--------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListICollectionConstructor** |   **1000** |    **Int** |       **314.7 us** |     **2.8038 us** |     **2.4855 us** |  **1.00** |    **0.00** |   **1290.0391** |           **-** |           **-** |          **3968.75 KB** |
| PooledICollectionConstructor |   1000 |    Int |       138.6 us |     0.6428 us |     0.5698 us |  0.44 |    0.00 |     12.6953 |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |   1000 |    Int |     8,019.8 us |    52.2032 us |    48.8309 us | 25.49 |    0.25 |   2687.5000 |           - |           - |          8273.44 KB |
| PooledIEnumerableConstructor |   1000 |    Int |     7,923.8 us |    39.9847 us |    35.4454 us | 25.18 |    0.22 |     15.6250 |           - |           - |            78.13 KB |
|                              |        |        |                |               |               |       |         |             |             |             |                     |
|   **ListICollectionConstructor** |   **1000** | **String** |       **746.0 us** |    **14.4624 us** |    **14.2040 us** |  **1.00** |    **0.00** |   **2556.6406** |           **-** |           **-** |             **7875 KB** |
| PooledICollectionConstructor |   1000 | String |       587.3 us |     3.7003 us |     3.4612 us |  0.79 |    0.02 |     12.6953 |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |   1000 | String |    13,127.3 us |    74.0515 us |    69.2678 us | 17.61 |    0.38 |   5281.2500 |           - |           - |         16265.63 KB |
| PooledIEnumerableConstructor |   1000 | String |    11,840.0 us |    72.2095 us |    64.0118 us | 15.86 |    0.32 |     15.6250 |           - |           - |            85.94 KB |
|                              |        |        |                |               |               |       |         |             |             |             |                     |
|   **ListICollectionConstructor** |  **10000** |    **Int** |     **3,283.2 us** |    **17.8233 us** |    **16.6719 us** |  **1.00** |    **0.00** |  **12656.2500** |           **-** |           **-** |            **39125 KB** |
| PooledICollectionConstructor |  10000 |    Int |     1,136.8 us |     3.9015 us |     3.4586 us |  0.35 |    0.00 |     11.7188 |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |  10000 |    Int |    79,381.9 us |   593.6112 us |   495.6922 us | 24.16 |    0.17 |  41571.4286 |           - |           - |        128367.19 KB |
| PooledIEnumerableConstructor |  10000 |    Int |    70,616.7 us |   264.3000 us |   247.2264 us | 21.51 |    0.14 |           - |           - |           - |            78.13 KB |
|                              |        |        |                |               |               |       |         |             |             |             |                     |
|   **ListICollectionConstructor** |  **10000** | **String** |     **7,599.5 us** |    **81.1423 us** |    **75.9006 us** |  **1.00** |    **0.00** |  **24984.3750** |           **-** |           **-** |          **78187.5 KB** |
| PooledICollectionConstructor |  10000 | String |     8,253.1 us |    32.6712 us |    28.9622 us |  1.09 |    0.01 |           - |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor |  10000 | String |   202,666.9 us | 1,296.4790 us | 1,149.2949 us | 26.66 |    0.28 |  41333.3333 |  41333.3333 |  41333.3333 |        256359.38 KB |
| PooledIEnumerableConstructor |  10000 | String |   128,767.2 us |   542.4546 us |   480.8719 us | 16.94 |    0.19 |           - |           - |           - |            85.94 KB |
|                              |        |        |                |               |               |       |         |             |             |             |                     |
|   **ListICollectionConstructor** | **100000** |    **Int** |   **175,626.8 us** |   **894.4694 us** |   **836.6872 us** |  **1.00** |    **0.00** |  **28666.6667** |  **28666.6667** |  **28666.6667** |        **390900.86 KB** |
| PooledICollectionConstructor | 100000 |    Int |    14,506.4 us |    80.1430 us |    74.9658 us |  0.08 |    0.00 |           - |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor | 100000 |    Int | 1,071,364.6 us | 6,071.6854 us | 5,679.4583 us |  6.10 |    0.05 | 220000.0000 | 181000.0000 | 178000.0000 |       1025120.55 KB |
| PooledIEnumerableConstructor | 100000 |    Int |   680,656.1 us | 3,366.9135 us | 3,149.4130 us |  3.88 |    0.03 |           - |           - |           - |            78.13 KB |
|                              |        |        |                |               |               |       |         |             |             |             |                     |
|   **ListICollectionConstructor** | **100000** | **String** |   **424,012.4 us** | **9,791.5408 us** | **9,159.0133 us** |  **1.00** |    **0.00** |  **16000.0000** |  **16000.0000** |  **16000.0000** |        **781406.25 KB** |
| PooledICollectionConstructor | 100000 | String |    82,432.1 us |   262.2249 us |   218.9697 us |  0.19 |    0.00 |           - |           - |           - |            39.06 KB |
|   ListIEnumerableConstructor | 100000 | String | 1,972,049.4 us | 6,083.7785 us | 5,393.1112 us |  4.64 |    0.10 | 373000.0000 | 334000.0000 | 331000.0000 |        2049194.8 KB |
| PooledIEnumerableConstructor | 100000 | String | 1,243,681.1 us | 4,863.6209 us | 4,061.3437 us |  2.94 |    0.06 |           - |           - |           - |            85.94 KB |
