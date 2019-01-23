``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|              Method |      N |           Mean |         Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------- |---------------:|--------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListInsert_Int** |   **1000** |       **252.2 us** |      **2.799 us** |      **2.338 us** |  **1.00** |    **0.00** |     **10.2539** |           **-** |           **-** |             **33056 B** |
|    PooledInsert_Int |   1000 |       252.4 us |      2.132 us |      1.665 us |  1.00 |    0.01 |           - |           - |           - |                40 B |
|   ListInsert_String |   1000 |       505.4 us |      7.149 us |      5.582 us |  2.00 |    0.03 |     20.5078 |           - |           - |             65808 B |
| PooledInsert_String |   1000 |       511.9 us |      2.780 us |      2.601 us |  2.03 |    0.02 |           - |           - |           - |                40 B |
|                     |        |                |               |               |       |         |             |             |             |                     |
|      **ListInsert_Int** |  **10000** |    **29,341.2 us** |    **106.497 us** |     **99.618 us** |  **1.00** |    **0.00** |     **31.2500** |     **31.2500** |     **31.2500** |            **262504 B** |
|    PooledInsert_Int |  10000 |    29,247.5 us |    239.446 us |    199.948 us |  1.00 |    0.01 |           - |           - |           - |                40 B |
|   ListInsert_String |  10000 |    60,820.4 us |    267.768 us |    237.369 us |  2.07 |    0.01 |    111.1111 |    111.1111 |    111.1111 |            524632 B |
| PooledInsert_String |  10000 |    60,727.2 us |    266.093 us |    248.904 us |  2.07 |    0.01 |           - |           - |           - |                40 B |
|                     |        |                |               |               |       |         |             |             |             |                     |
|      **ListInsert_Int** | **100000** | **3,584,215.5 us** | **14,064.936 us** | **13,156.349 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |           **4194760 B** |
|    PooledInsert_Int | 100000 | 3,595,707.7 us |  8,786.730 us |  7,789.208 us |  1.00 |    0.00 |           - |           - |           - |                40 B |
|   ListInsert_String | 100000 | 7,296,772.2 us | 12,416.065 us | 10,367.977 us |  2.03 |    0.00 |   1000.0000 |   1000.0000 |   1000.0000 |           8389048 B |
| PooledInsert_String | 100000 | 7,288,395.3 us | 16,589.298 us | 15,517.639 us |  2.03 |    0.01 |           - |           - |           - |                40 B |
