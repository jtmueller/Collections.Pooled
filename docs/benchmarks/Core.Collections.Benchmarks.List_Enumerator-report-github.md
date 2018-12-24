``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |         Mean |        Error |       StdDev | Ratio | RatioSD |
|--------------------------- |------- |-------------:|-------------:|-------------:|------:|--------:|
|          **ListEnumerate_Int** |   **1000** |   **2,108.8 ns** |    **18.256 ns** |    **16.183 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |   1000 |   2,251.5 ns |    18.121 ns |    16.064 ns |  1.07 |    0.01 |
|    PooledEnumerateSpan_Int |   1000 |     578.4 ns |     3.977 ns |     3.720 ns |  0.27 |    0.00 |
|       ListEnumerate_String |   1000 |   3,831.4 ns |    28.896 ns |    27.030 ns |  1.82 |    0.02 |
|     PooledEnumerate_String |   1000 |   4,707.2 ns |    28.158 ns |    24.962 ns |  2.23 |    0.03 |
| PooledEnumerateSpan_String |   1000 |     602.9 ns |     8.345 ns |     7.806 ns |  0.29 |    0.00 |
|                            |        |              |              |              |       |         |
|          **ListEnumerate_Int** |  **10000** |  **21,018.2 ns** |   **115.134 ns** |    **96.142 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |  10000 |  22,347.2 ns |   302.126 ns |   267.827 ns |  1.06 |    0.01 |
|    PooledEnumerateSpan_Int |  10000 |   5,698.1 ns |    86.666 ns |    81.067 ns |  0.27 |    0.00 |
|       ListEnumerate_String |  10000 |  38,399.9 ns |   269.185 ns |   251.795 ns |  1.83 |    0.02 |
|     PooledEnumerate_String |  10000 |  45,347.9 ns |   269.969 ns |   225.437 ns |  2.16 |    0.01 |
| PooledEnumerateSpan_String |  10000 |   5,835.8 ns |    25.456 ns |    23.812 ns |  0.28 |    0.00 |
|                            |        |              |              |              |       |         |
|          **ListEnumerate_Int** | **100000** | **208,949.8 ns** | **1,021.065 ns** |   **852.636 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int | 100000 | 221,710.4 ns | 1,557.245 ns | 1,456.648 ns |  1.06 |    0.01 |
|    PooledEnumerateSpan_Int | 100000 |  57,830.0 ns |   100.470 ns |    83.897 ns |  0.28 |    0.00 |
|       ListEnumerate_String | 100000 | 381,964.8 ns | 2,833.131 ns | 2,650.113 ns |  1.83 |    0.01 |
|     PooledEnumerate_String | 100000 | 445,038.6 ns | 2,746.160 ns | 2,144.021 ns |  2.13 |    0.01 |
| PooledEnumerateSpan_String | 100000 |  58,183.4 ns |   848.401 ns |   793.594 ns |  0.28 |    0.00 |
