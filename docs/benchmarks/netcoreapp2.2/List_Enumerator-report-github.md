``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|          **ListEnumerate_Int** |   **1000** |   **1,933.9 ns** |     **5.766 ns** |     **5.394 ns** |   **1,932.5 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |   1000 |   2,070.7 ns |     6.810 ns |     6.370 ns |   2,071.3 ns |  1.07 |    0.00 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |   1000 |     534.4 ns |     7.466 ns |     6.984 ns |     536.4 ns |  0.28 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String |   1000 |   3,531.4 ns |     7.340 ns |     6.866 ns |   3,530.7 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String |   1000 |   3,823.7 ns |    12.015 ns |    11.239 ns |   3,820.0 ns |  1.98 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |   1000 |     314.7 ns |     1.507 ns |     1.258 ns |     314.3 ns |  0.16 |    0.00 |           - |           - |           - |                   - |
|                            |        |              |              |              |              |       |         |             |             |             |                     |
|          **ListEnumerate_Int** |  **10000** |  **19,187.9 ns** |    **52.896 ns** |    **49.478 ns** |  **19,181.1 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |  10000 |  24,329.8 ns |   482.261 ns | 1,320.182 ns |  24,761.4 ns |  1.16 |    0.11 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |  10000 |   5,308.0 ns |    13.670 ns |    12.787 ns |   5,305.4 ns |  0.28 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String |  10000 |  35,112.5 ns |   101.937 ns |    90.365 ns |  35,135.3 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String |  10000 |  35,107.0 ns |   199.335 ns |   186.458 ns |  35,055.0 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |  10000 |   3,266.3 ns |    65.029 ns |   157.052 ns |   3,245.5 ns |  0.18 |    0.01 |           - |           - |           - |                   - |
|                            |        |              |              |              |              |       |         |             |             |             |                     |
|          **ListEnumerate_Int** | **100000** | **191,403.6 ns** |   **292.878 ns** |   **259.628 ns** | **191,397.4 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int | 100000 | 204,605.3 ns |   499.988 ns |   443.226 ns | 204,554.9 ns |  1.07 |    0.00 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int | 100000 |  52,973.9 ns |   172.717 ns |   161.559 ns |  52,928.3 ns |  0.28 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String | 100000 | 353,626.8 ns | 7,928.870 ns | 7,416.670 ns | 350,993.7 ns |  1.85 |    0.04 |           - |           - |           - |                   - |
|     PooledEnumerate_String | 100000 | 350,021.4 ns |   796.943 ns |   665.484 ns | 350,173.3 ns |  1.83 |    0.00 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String | 100000 |  53,082.9 ns |   203.341 ns |   190.205 ns |  53,006.5 ns |  0.28 |    0.00 |           - |           - |           - |                   - |
