``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|              Method |      N |           Mean |         Error |          StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------- |---------------:|--------------:|----------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListInsert_Int** |   **1000** |       **275.3 us** |      **3.124 us** |       **2.9221 us** |  **1.00** |    **0.00** |     **10.2539** |           **-** |           **-** |             **33056 B** |
|    PooledInsert_Int |   1000 |       281.3 us |      1.254 us |       0.9788 us |  1.02 |    0.01 |           - |           - |           - |                40 B |
|   ListInsert_String |   1000 |       539.6 us |      4.600 us |       4.0780 us |  1.96 |    0.03 |     20.5078 |           - |           - |             65808 B |
| PooledInsert_String |   1000 |       502.0 us |      9.428 us |       7.8731 us |  1.82 |    0.04 |           - |           - |           - |                40 B |
|                     |        |                |               |                 |       |         |             |             |             |                     |
|      **ListInsert_Int** |  **10000** |    **31,609.2 us** |    **612.978 us** |   **1,024.1488 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |            **262504 B** |
|    PooledInsert_Int |  10000 |    25,664.9 us |    633.188 us |     621.8756 us |  0.82 |    0.05 |           - |           - |           - |                40 B |
|   ListInsert_String |  10000 |    65,868.7 us |     83.526 us |      74.0433 us |  2.11 |    0.13 |           - |           - |           - |            524632 B |
| PooledInsert_String |  10000 |    59,488.2 us |  2,068.509 us |   6,099.0460 us |  1.85 |    0.20 |           - |           - |           - |                40 B |
|                     |        |                |               |                 |       |         |             |             |             |                     |
|      **ListInsert_Int** | **100000** | **3,848,576.9 us** | **76,843.427 us** | **110,206.5465 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |           **4194760 B** |
|    PooledInsert_Int | 100000 | 3,432,114.4 us | 68,509.559 us | 133,622.6329 us |  0.90 |    0.05 |           - |           - |           - |                40 B |
|   ListInsert_String | 100000 | 7,914,812.4 us | 86,905.199 us |  81,291.1771 us |  2.05 |    0.06 |   1000.0000 |   1000.0000 |   1000.0000 |           8389048 B |
| PooledInsert_String | 100000 | 7,948,510.1 us | 78,308.191 us |  65,390.8862 us |  2.06 |    0.06 |           - |           - |           - |                40 B |
