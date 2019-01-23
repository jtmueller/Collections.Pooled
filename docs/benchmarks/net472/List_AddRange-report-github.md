``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                    Method |      N |         Mean |      Error |     StdDev | Ratio | RatioSD |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------- |-------------:|-----------:|-----------:|------:|--------:|-------------:|-------------:|-------------:|--------------------:|
|   **ListAddRangeICollection** |   **1000** |     **2.776 ms** |  **0.0460 ms** |  **0.0408 ms** |  **1.00** |    **0.00** |   **12851.5625** |            **-** |            **-** |         **39536.68 KB** |
| PooledAddRangeICollection |   1000 |     5.122 ms |  0.0226 ms |  0.0212 ms |  1.85 |    0.03 |      62.5000 |            - |            - |           195.32 KB |
|   ListAddRangeIEnumerable |   1000 |    48.552 ms |  0.1257 ms |  0.1176 ms | 17.49 |    0.25 |   13363.6364 |            - |            - |         41372.92 KB |
| PooledAddRangeIEnumerable |   1000 |    54.754 ms |  0.1901 ms |  0.1778 ms | 19.73 |    0.29 |     111.1111 |            - |            - |           391.12 KB |
|                           |        |              |            |            |       |         |              |              |              |                     |
|   **ListAddRangeICollection** |  **10000** |    **27.637 ms** |  **0.1219 ms** |  **0.1081 ms** |  **1.00** |    **0.00** |  **126562.5000** |            **-** |            **-** |         **392106.5 KB** |
| PooledAddRangeICollection |  10000 |    72.154 ms |  0.3130 ms |  0.2928 ms |  2.61 |    0.02 |            - |            - |            - |           195.43 KB |
|   ListAddRangeIEnumerable |  10000 |   489.118 ms |  3.4898 ms |  3.2644 ms | 17.71 |    0.16 |  208000.0000 |            - |            - |         642601.5 KB |
| PooledAddRangeIEnumerable |  10000 |   568.107 ms |  4.5736 ms |  4.0544 ms | 20.56 |    0.18 |            - |            - |            - |              392 KB |
|                           |        |              |            |            |       |         |              |              |              |                     |
|   **ListAddRangeICollection** | **100000** | **2,152.914 ms** |  **5.4579 ms** |  **5.1054 ms** |  **1.00** |    **0.00** | **1249000.0000** | **1249000.0000** | **1249000.0000** |       **3916484.38 KB** |
| PooledAddRangeICollection | 100000 |   609.740 ms |  1.7779 ms |  1.6631 ms |  0.28 |    0.00 |            - |            - |            - |              200 KB |
|   ListAddRangeIEnumerable | 100000 | 6,092.838 ms | 34.1754 ms | 30.2956 ms |  2.83 |    0.01 | 1428000.0000 | 1428000.0000 | 1428000.0000 |       5132721.06 KB |
| PooledAddRangeIEnumerable | 100000 | 5,307.591 ms | 22.3227 ms | 19.7885 ms |  2.47 |    0.01 |            - |            - |            - |              392 KB |
