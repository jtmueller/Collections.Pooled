``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                  Method |    N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **8.169 us** | **0.0288 us** | **0.0269 us** |  **1.00** |    **0.00** |     **10.5133** |           **-** |           **-** |             **33122 B** |
|     PooledAdd_ValueType |  256 |  11.849 us | 0.0196 us | 0.0183 us |  1.45 |    0.01 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType |  256 |  14.357 us | 0.0631 us | 0.0591 us |  1.76 |    0.01 |     20.8282 |           - |           - |             65851 B |
| PooledAdd_ReferenceType |  256 |  26.876 us | 0.0658 us | 0.0616 us |  3.29 |    0.01 |           - |           - |           - |                40 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **16.082 us** | **0.0606 us** | **0.0566 us** |  **1.00** |    **0.00** |     **20.8130** |           **-** |           **-** |             **65851 B** |
|     PooledAdd_ValueType |  512 |  22.849 us | 0.1106 us | 0.0923 us |  1.42 |    0.01 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType |  512 |  28.293 us | 0.1046 us | 0.0978 us |  1.76 |    0.01 |     41.6565 |           - |           - |            131435 B |
| PooledAdd_ReferenceType |  512 |  52.717 us | 0.1491 us | 0.1395 us |  3.28 |    0.02 |           - |           - |           - |                40 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** | **110.465 us** | **0.2195 us** | **0.2053 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262531 B** |
|     PooledAdd_ValueType | 2048 |  88.820 us | 0.2409 us | 0.2136 us |  0.80 |    0.00 |           - |           - |           - |                41 B |
|   ListAdd_ReferenceType | 2048 | 269.830 us | 0.8836 us | 0.8265 us |  2.44 |    0.01 |    124.5117 |    124.5117 |    124.5117 |            525552 B |
| PooledAdd_ReferenceType | 2048 | 208.235 us | 0.5837 us | 0.5460 us |  1.89 |    0.01 |           - |           - |           - |                42 B |
