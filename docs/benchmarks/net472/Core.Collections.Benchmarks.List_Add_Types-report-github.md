``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                  Method |    N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **8.192 us** | **0.0368 us** | **0.0345 us** |  **1.00** |    **0.00** |     **10.5133** |           **-** |           **-** |             **33122 B** |
|     PooledAdd_ValueType |  256 |  11.923 us | 0.0334 us | 0.0313 us |  1.46 |    0.01 |           - |           - |           - |                32 B |
|   ListAdd_ReferenceType |  256 |  14.545 us | 0.0573 us | 0.0536 us |  1.78 |    0.01 |     20.8282 |           - |           - |             65851 B |
| PooledAdd_ReferenceType |  256 |  27.309 us | 0.0655 us | 0.0581 us |  3.33 |    0.01 |           - |           - |           - |                32 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **16.100 us** | **0.0871 us** | **0.0815 us** |  **1.00** |    **0.00** |     **20.8130** |           **-** |           **-** |             **65851 B** |
|     PooledAdd_ValueType |  512 |  22.900 us | 0.0831 us | 0.0778 us |  1.42 |    0.01 |           - |           - |           - |                32 B |
|   ListAdd_ReferenceType |  512 |  28.427 us | 0.0830 us | 0.0736 us |  1.76 |    0.01 |     41.6565 |           - |           - |            131435 B |
| PooledAdd_ReferenceType |  512 |  53.491 us | 0.1275 us | 0.1192 us |  3.32 |    0.02 |           - |           - |           - |                32 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** | **110.376 us** | **0.4683 us** | **0.4380 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262531 B** |
|     PooledAdd_ValueType | 2048 |  88.906 us | 0.3279 us | 0.2907 us |  0.81 |    0.00 |           - |           - |           - |                33 B |
|   ListAdd_ReferenceType | 2048 | 289.444 us | 2.1689 us | 1.9227 us |  2.62 |    0.02 |    124.5117 |    124.5117 |    124.5117 |            525552 B |
| PooledAdd_ReferenceType | 2048 | 214.283 us | 4.1240 us | 4.9093 us |  1.95 |    0.05 |           - |           - |           - |                34 B |
