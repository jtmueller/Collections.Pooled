``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |         Mean |        Error |       StdDev | Ratio | RatioSD |
|--------------------------- |------- |-------------:|-------------:|-------------:|------:|--------:|
|          **ListEnumerate_Int** |   **1000** |   **1,952.8 ns** |     **6.607 ns** |     **6.180 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |   1000 |   2,075.2 ns |     4.381 ns |     4.098 ns |  1.06 |    0.00 |
|    PooledEnumerateSpan_Int |   1000 |     536.6 ns |     2.382 ns |     2.229 ns |  0.27 |    0.00 |
|       ListEnumerate_String |   1000 |   3,581.2 ns |    19.487 ns |    18.228 ns |  1.83 |    0.01 |
|     PooledEnumerate_String |   1000 |   3,556.8 ns |     6.612 ns |     5.862 ns |  1.82 |    0.01 |
| PooledEnumerateSpan_String |   1000 |     314.7 ns |     1.220 ns |     1.141 ns |  0.16 |    0.00 |
|                            |        |              |              |              |       |         |
|          **ListEnumerate_Int** |  **10000** |  **19,335.9 ns** |    **36.279 ns** |    **32.161 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |  10000 |  24,935.7 ns |    90.701 ns |    84.841 ns |  1.29 |    0.00 |
|    PooledEnumerateSpan_Int |  10000 |   5,337.7 ns |    12.072 ns |    11.292 ns |  0.28 |    0.00 |
|       ListEnumerate_String |  10000 |  37,879.2 ns |   112.372 ns |   105.113 ns |  1.96 |    0.01 |
|     PooledEnumerate_String |  10000 |  35,240.0 ns |   113.171 ns |   105.860 ns |  1.82 |    0.01 |
| PooledEnumerateSpan_String |  10000 |   3,199.8 ns |    37.940 ns |    29.621 ns |  0.17 |    0.00 |
|                            |        |              |              |              |       |         |
|          **ListEnumerate_Int** | **100000** | **192,900.4 ns** |   **358.351 ns** |   **317.669 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int | 100000 | 205,581.2 ns |   525.150 ns |   465.532 ns |  1.07 |    0.00 |
|    PooledEnumerateSpan_Int | 100000 |  53,389.9 ns |    85.394 ns |    79.878 ns |  0.28 |    0.00 |
|       ListEnumerate_String | 100000 | 373,715.1 ns | 3,567.600 ns | 3,337.135 ns |  1.94 |    0.02 |
|     PooledEnumerate_String | 100000 | 352,012.9 ns | 1,022.581 ns |   956.523 ns |  1.83 |    0.01 |
| PooledEnumerateSpan_String | 100000 |  53,422.3 ns |    69.809 ns |    61.884 ns |  0.28 |    0.00 |
