``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                     Method |      N |         Mean |        Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-------------:|-------------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|          **ListEnumerate_Int** |   **1000** |   **1,924.6 ns** |     **7.798 ns** |   **7.294 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |   1000 |   2,060.2 ns |     6.656 ns |   6.226 ns |  1.07 |    0.01 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |   1000 |     882.0 ns |     1.624 ns |   1.439 ns |  0.46 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String |   1000 |   3,526.2 ns |     7.611 ns |   6.747 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String |   1000 |   3,529.0 ns |     6.640 ns |   5.545 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |   1000 |     903.8 ns |     4.722 ns |   4.186 ns |  0.47 |    0.00 |           - |           - |           - |                   - |
|                            |        |              |              |            |       |         |             |             |             |                     |
|          **ListEnumerate_Int** |  **10000** |  **19,217.0 ns** |    **59.688 ns** |  **52.912 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |  10000 |  20,612.6 ns |   368.141 ns | 326.347 ns |  1.07 |    0.02 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |  10000 |   8,777.5 ns |    31.927 ns |  26.661 ns |  0.46 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String |  10000 |  35,091.3 ns |   160.538 ns | 142.312 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String |  10000 |  35,037.9 ns |   108.261 ns |  90.403 ns |  1.82 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |  10000 |   7,420.2 ns |    67.913 ns |  56.711 ns |  0.39 |    0.00 |           - |           - |           - |                   - |
|                            |        |              |              |            |       |         |             |             |             |                     |
|          **ListEnumerate_Int** | **100000** | **191,937.6 ns** |   **793.904 ns** | **742.618 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int | 100000 | 204,557.5 ns |   715.108 ns | 668.912 ns |  1.07 |    0.01 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int | 100000 |  87,800.0 ns |   334.699 ns | 313.078 ns |  0.46 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String | 100000 | 350,026.9 ns |   875.009 ns | 775.673 ns |  1.82 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String | 100000 | 350,351.3 ns | 1,034.970 ns | 968.112 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String | 100000 |  73,385.8 ns |   381.084 ns | 337.821 ns |  0.38 |    0.00 |           - |           - |           - |                   - |
