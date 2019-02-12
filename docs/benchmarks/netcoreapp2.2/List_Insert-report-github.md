``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|              Method |      N |           Mean |          Error |         StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------- |---------------:|---------------:|---------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListInsert_Int** |   **1000** |       **250.2 us** |      **0.7706 us** |      **0.6016 us** |  **1.00** |    **0.00** |     **10.2539** |           **-** |           **-** |             **33056 B** |
|    PooledInsert_Int |   1000 |       250.7 us |      3.4154 us |      2.8520 us |  1.00 |    0.01 |           - |           - |           - |                56 B |
|   ListInsert_String |   1000 |       498.5 us |      6.5450 us |      6.1222 us |  1.99 |    0.02 |     20.5078 |           - |           - |             65808 B |
| PooledInsert_String |   1000 |       506.5 us |      1.7357 us |      1.6236 us |  2.02 |    0.01 |           - |           - |           - |                56 B |
|                     |        |                |                |                |       |         |             |             |             |                     |
|      **ListInsert_Int** |  **10000** |    **29,142.0 us** |     **50.2922 us** |     **47.0434 us** |  **1.00** |    **0.00** |     **31.2500** |     **31.2500** |     **31.2500** |            **262504 B** |
|    PooledInsert_Int |  10000 |    29,044.2 us |     50.5482 us |     47.2828 us |  1.00 |    0.00 |           - |           - |           - |                56 B |
|   ListInsert_String |  10000 |    60,506.1 us |    226.7769 us |    212.1273 us |  2.08 |    0.01 |    111.1111 |    111.1111 |    111.1111 |            524632 B |
| PooledInsert_String |  10000 |    60,278.8 us |    199.3618 us |    186.4832 us |  2.07 |    0.01 |           - |           - |           - |                56 B |
|                     |        |                |                |                |       |         |             |             |             |                     |
|      **ListInsert_Int** | **100000** | **3,570,353.0 us** |  **4,036.7513 us** |  **3,775.9796 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |           **4194760 B** |
|    PooledInsert_Int | 100000 | 3,575,622.5 us |  5,351.4480 us |  4,743.9193 us |  1.00 |    0.00 |           - |           - |           - |                56 B |
|   ListInsert_String | 100000 | 7,288,969.0 us |  9,221.6375 us |  8,625.9254 us |  2.04 |    0.00 |   1000.0000 |   1000.0000 |   1000.0000 |           8389048 B |
| PooledInsert_String | 100000 | 7,286,278.2 us | 13,179.7229 us | 12,328.3209 us |  2.04 |    0.00 |           - |           - |           - |                56 B |
