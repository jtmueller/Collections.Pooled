``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                     Method |      N |         Mean |        Error |     StdDev | Ratio | RatioSD |
|--------------------------- |------- |-------------:|-------------:|-----------:|------:|--------:|
|          **ListEnumerate_Int** |   **1000** |   **1,928.3 ns** |     **9.353 ns** |   **8.748 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |   1000 |   2,353.8 ns |     9.764 ns |   9.133 ns |  1.22 |    0.01 |
|    PooledEnumerateSpan_Int |   1000 |     880.2 ns |     1.709 ns |   1.515 ns |  0.46 |    0.00 |
|       ListEnumerate_String |   1000 |   3,811.0 ns |     9.179 ns |   8.137 ns |  1.98 |    0.01 |
|     PooledEnumerate_String |   1000 |   4,107.5 ns |    15.920 ns |  14.891 ns |  2.13 |    0.01 |
| PooledEnumerateSpan_String |   1000 |     908.1 ns |     2.294 ns |   2.145 ns |  0.47 |    0.00 |
|                            |        |              |              |            |       |         |
|          **ListEnumerate_Int** |  **10000** |  **19,238.8 ns** |   **160.678 ns** | **142.437 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |  10000 |  23,283.4 ns |    35.441 ns |  29.594 ns |  1.21 |    0.01 |
|    PooledEnumerateSpan_Int |  10000 |   8,763.8 ns |    33.396 ns |  31.238 ns |  0.46 |    0.00 |
|       ListEnumerate_String |  10000 |  37,873.6 ns |    97.941 ns |  91.614 ns |  1.97 |    0.02 |
|     PooledEnumerate_String |  10000 |  40,784.7 ns |   101.836 ns |  95.258 ns |  2.12 |    0.01 |
| PooledEnumerateSpan_String |  10000 |   8,831.2 ns |    47.198 ns |  44.149 ns |  0.46 |    0.00 |
|                            |        |              |              |            |       |         |
|          **ListEnumerate_Int** | **100000** | **191,324.8 ns** |   **290.984 ns** | **272.186 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int | 100000 | 232,769.7 ns |   404.098 ns | 377.993 ns |  1.22 |    0.00 |
|    PooledEnumerateSpan_Int | 100000 |  87,420.1 ns |   162.405 ns | 143.968 ns |  0.46 |    0.00 |
|       ListEnumerate_String | 100000 | 379,068.7 ns | 1,020.996 ns | 955.040 ns |  1.98 |    0.00 |
|     PooledEnumerate_String | 100000 | 407,117.4 ns |   818.344 ns | 765.480 ns |  2.13 |    0.01 |
| PooledEnumerateSpan_String | 100000 |  87,498.6 ns |   198.179 ns | 185.377 ns |  0.46 |    0.00 |
