``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                    Method |      N |         Mean |      Error |    StdDev | Ratio | RatioSD |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------- |-------------:|-----------:|----------:|------:|--------:|-------------:|-------------:|-------------:|--------------------:|
|   **ListAddRangeICollection** |   **1000** |     **2.710 ms** |  **0.0429 ms** | **0.0401 ms** |  **1.00** |    **0.00** |   **12851.5625** |            **-** |            **-** |         **39536.68 KB** |
| PooledAddRangeICollection |   1000 |     5.359 ms |  0.0215 ms | 0.0179 ms |  1.98 |    0.03 |      85.9375 |            - |            - |           273.44 KB |
|   ListAddRangeIEnumerable |   1000 |    48.612 ms |  0.1487 ms | 0.1391 ms | 17.94 |    0.27 |   13363.6364 |            - |            - |         41373.17 KB |
| PooledAddRangeIEnumerable |   1000 |    52.826 ms |  0.2267 ms | 0.2120 ms | 19.50 |    0.28 |     100.0000 |            - |            - |           468.81 KB |
|                           |        |              |            |           |       |         |              |              |              |                     |
|   **ListAddRangeICollection** |  **10000** |    **27.574 ms** |  **0.1360 ms** | **0.1272 ms** |  **1.00** |    **0.00** |  **126562.5000** |            **-** |            **-** |         **392106.5 KB** |
| PooledAddRangeICollection |  10000 |    72.212 ms |  0.3444 ms | 0.3053 ms |  2.62 |    0.02 |            - |            - |            - |           274.29 KB |
|   ListAddRangeIEnumerable |  10000 |   496.643 ms |  4.4282 ms | 4.1421 ms | 18.01 |    0.18 |  208000.0000 |            - |            - |         642601.5 KB |
| PooledAddRangeIEnumerable |  10000 |   538.681 ms |  4.3077 ms | 4.0294 ms | 19.54 |    0.14 |            - |            - |            - |              472 KB |
|                           |        |              |            |           |       |         |              |              |              |                     |
|   **ListAddRangeICollection** | **100000** | **2,159.393 ms** |  **5.8275 ms** | **5.4511 ms** |  **1.00** |    **0.00** | **1249000.0000** | **1249000.0000** | **1249000.0000** |       **3916484.38 KB** |
| PooledAddRangeICollection | 100000 |   609.485 ms |  2.1746 ms | 2.0341 ms |  0.28 |    0.00 |            - |            - |            - |              280 KB |
|   ListAddRangeIEnumerable | 100000 | 6,258.956 ms | 10.1069 ms | 8.4397 ms |  2.90 |    0.01 | 1428000.0000 | 1428000.0000 | 1428000.0000 |       5132721.06 KB |
| PooledAddRangeIEnumerable | 100000 | 5,118.166 ms |  6.8588 ms | 6.0801 ms |  2.37 |    0.01 |            - |            - |            - |              472 KB |
