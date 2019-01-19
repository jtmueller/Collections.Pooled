``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                  Method |    N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **8.531 us** | **0.1706 us** | **0.3033 us** |   **8.346 us** |  **1.00** |    **0.00** |     **10.5133** |           **-** |           **-** |             **33122 B** |
|     PooledAdd_ValueType |  256 |  12.658 us | 0.2447 us | 0.3181 us |  12.830 us |  1.50 |    0.06 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType |  256 |  15.612 us | 0.2314 us | 0.2051 us |  15.639 us |  1.85 |    0.06 |     20.8130 |           - |           - |             65851 B |
| PooledAdd_ReferenceType |  256 |  27.592 us | 0.5416 us | 0.9197 us |  27.067 us |  3.24 |    0.15 |           - |           - |           - |                40 B |
|                         |      |            |           |           |            |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **17.576 us** | **0.3358 us** | **0.3733 us** |  **17.705 us** |  **1.00** |    **0.00** |     **20.8130** |           **-** |           **-** |             **65851 B** |
|     PooledAdd_ValueType |  512 |  24.976 us | 0.1080 us | 0.0902 us |  24.985 us |  1.42 |    0.03 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType |  512 |  31.021 us | 0.1241 us | 0.1160 us |  31.020 us |  1.76 |    0.04 |     41.6260 |           - |           - |            131435 B |
| PooledAdd_ReferenceType |  512 |  53.667 us | 1.3841 us | 1.3594 us |  53.102 us |  3.05 |    0.11 |           - |           - |           - |                40 B |
|                         |      |            |           |           |            |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** | **117.993 us** | **2.2595 us** | **2.6021 us** | **119.384 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262531 B** |
|     PooledAdd_ValueType | 2048 |  92.462 us | 1.8042 us | 3.2990 us |  90.558 us |  0.78 |    0.03 |           - |           - |           - |                41 B |
|   ListAdd_ReferenceType | 2048 | 287.051 us | 0.6773 us | 0.6004 us | 287.118 us |  2.42 |    0.04 |    124.5117 |    124.5117 |    124.5117 |            525552 B |
| PooledAdd_ReferenceType | 2048 | 209.379 us | 1.2023 us | 1.0040 us | 209.142 us |  1.76 |    0.04 |           - |           - |           - |                42 B |
