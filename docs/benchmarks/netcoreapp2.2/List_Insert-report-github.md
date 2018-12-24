``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|              Method |      N |           Mean |          Error |         StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------- |---------------:|---------------:|---------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListInsert_Int** |   **1000** |       **251.8 us** |      **0.5343 us** |      **0.4998 us** |  **1.00** |    **0.00** |     **10.2539** |           **-** |           **-** |             **33056 B** |
|    PooledInsert_Int |   1000 |       265.7 us |      5.1550 us |      7.2267 us |  1.06 |    0.03 |           - |           - |           - |                32 B |
|   ListInsert_String |   1000 |       499.5 us |      1.2042 us |      1.1264 us |  1.98 |    0.01 |     20.5078 |           - |           - |             65808 B |
| PooledInsert_String |   1000 |       509.4 us |      3.7396 us |      3.4980 us |  2.02 |    0.01 |           - |           - |           - |                32 B |
|                     |        |                |                |                |       |         |             |             |             |                     |
|      **ListInsert_Int** |  **10000** |    **29,311.8 us** |     **80.1440 us** |     **71.0456 us** |  **1.00** |    **0.00** |     **31.2500** |     **31.2500** |     **31.2500** |            **262504 B** |
|    PooledInsert_Int |  10000 |    29,206.1 us |     74.2900 us |     58.0007 us |  1.00 |    0.00 |           - |           - |           - |                32 B |
|   ListInsert_String |  10000 |    60,993.1 us |    331.4486 us |    310.0372 us |  2.08 |    0.01 |    111.1111 |    111.1111 |    111.1111 |            524632 B |
| PooledInsert_String |  10000 |    60,688.8 us |    250.3903 us |    209.0872 us |  2.07 |    0.01 |           - |           - |           - |                32 B |
|                     |        |                |                |                |       |         |             |             |             |                     |
|      **ListInsert_Int** | **100000** | **3,588,924.0 us** |  **4,882.7648 us** |  **4,328.4438 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |           **4194760 B** |
|    PooledInsert_Int | 100000 | 3,584,928.6 us |  7,024.9446 us |  6,571.1375 us |  1.00 |    0.00 |           - |           - |           - |                32 B |
|   ListInsert_String | 100000 | 7,321,473.8 us | 28,584.3089 us | 25,339.2453 us |  2.04 |    0.01 |   1000.0000 |   1000.0000 |   1000.0000 |           8389048 B |
| PooledInsert_String | 100000 | 7,338,277.1 us | 10,896.9421 us |  9,659.8554 us |  2.04 |    0.00 |           - |           - |           - |                32 B |
