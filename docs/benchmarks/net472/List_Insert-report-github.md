``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|              Method |      N |            Mean |         Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------- |----------------:|--------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListInsert_Int** |   **1000** |        **467.1 us** |      **1.409 us** |      **1.318 us** |  **1.00** |    **0.00** |     **10.2539** |           **-** |           **-** |             **33120 B** |
|    PooledInsert_Int |   1000 |        470.0 us |      1.326 us |      1.176 us |  1.01 |    0.00 |           - |           - |           - |                60 B |
|   ListInsert_String |   1000 |        860.1 us |      1.496 us |      1.400 us |  1.84 |    0.00 |     20.5078 |           - |           - |             65848 B |
| PooledInsert_String |   1000 |      2,507.6 us |      9.539 us |      8.922 us |  5.37 |    0.03 |           - |           - |           - |                64 B |
|                     |        |                 |               |               |       |         |             |             |             |                     |
|      **ListInsert_Int** |  **10000** |     **47,503.0 us** |    **118.099 us** |    **110.470 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |            **263105 B** |
|    PooledInsert_Int |  10000 |     47,647.0 us |    192.668 us |    180.222 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|   ListInsert_String |  10000 |    239,251.5 us |    288.062 us |    224.900 us |  5.03 |    0.01 |           - |           - |           - |            527259 B |
| PooledInsert_String |  10000 |    251,338.0 us |    930.717 us |    870.593 us |  5.29 |    0.02 |           - |           - |           - |                   - |
|                     |        |                 |               |               |       |         |             |             |             |                     |
|      **ListInsert_Int** | **100000** |  **5,504,460.7 us** |  **3,186.803 us** |  **2,980.938 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |           **4202808 B** |
|    PooledInsert_Int | 100000 |  5,505,968.5 us |  6,170.671 us |  5,772.050 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|   ListInsert_String | 100000 | 26,626,276.6 us | 22,082.903 us | 19,575.918 us |  4.84 |    0.00 |   1000.0000 |   1000.0000 |   1000.0000 |           8397136 B |
| PooledInsert_String | 100000 | 26,626,382.7 us | 28,870.385 us | 24,108.079 us |  4.84 |    0.00 |           - |           - |           - |                   - |
