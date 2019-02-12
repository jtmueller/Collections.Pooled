``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                  Method |    N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **8.160 us** | **0.0395 us** | **0.0370 us** |  **1.00** |    **0.00** |     **10.5133** |           **-** |           **-** |             **33122 B** |
|     PooledAdd_ValueType |  256 |  11.842 us | 0.0268 us | 0.0237 us |  1.45 |    0.01 |      0.0153 |           - |           - |                56 B |
|   ListAdd_ReferenceType |  256 |  14.390 us | 0.0773 us | 0.0723 us |  1.76 |    0.01 |     20.8130 |           - |           - |             65851 B |
| PooledAdd_ReferenceType |  256 |  26.589 us | 0.0662 us | 0.0587 us |  3.26 |    0.02 |           - |           - |           - |                56 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **16.134 us** | **0.0640 us** | **0.0598 us** |  **1.00** |    **0.00** |     **20.8130** |           **-** |           **-** |             **65851 B** |
|     PooledAdd_ValueType |  512 |  22.886 us | 0.0291 us | 0.0258 us |  1.42 |    0.00 |           - |           - |           - |                56 B |
|   ListAdd_ReferenceType |  512 |  28.433 us | 0.0783 us | 0.0733 us |  1.76 |    0.01 |     41.6565 |           - |           - |            131435 B |
| PooledAdd_ReferenceType |  512 |  52.399 us | 0.1718 us | 0.1434 us |  3.25 |    0.02 |           - |           - |           - |                56 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** | **108.933 us** | **0.3092 us** | **0.2741 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262531 B** |
|     PooledAdd_ValueType | 2048 |  88.542 us | 0.2873 us | 0.2546 us |  0.81 |    0.00 |           - |           - |           - |                57 B |
|   ListAdd_ReferenceType | 2048 | 264.739 us | 1.1079 us | 1.0363 us |  2.43 |    0.01 |    124.5117 |    124.5117 |    124.5117 |            525552 B |
| PooledAdd_ReferenceType | 2048 | 206.273 us | 0.3682 us | 0.3264 us |  1.89 |    0.00 |           - |           - |           - |                58 B |
