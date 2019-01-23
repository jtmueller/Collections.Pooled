``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |         Mean |        Error |       StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|          **ListEnumerate_Int** |   **1000** |   **1,950.8 ns** |     **8.365 ns** |     **7.416 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |   1000 |   2,392.0 ns |    12.076 ns |    10.084 ns |  1.23 |    0.01 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |   1000 |     537.8 ns |     6.352 ns |     5.941 ns |  0.28 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String |   1000 |   3,573.6 ns |    10.000 ns |     9.354 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String |   1000 |   3,303.7 ns |    13.800 ns |    12.908 ns |  1.69 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |   1000 |     317.4 ns |     2.182 ns |     1.822 ns |  0.16 |    0.00 |           - |           - |           - |                   - |
|                            |        |              |              |              |       |         |             |             |             |                     |
|          **ListEnumerate_Int** |  **10000** |  **19,539.3 ns** |   **166.626 ns** |   **155.862 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |  10000 |  23,664.3 ns |   134.757 ns |   119.459 ns |  1.21 |    0.01 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |  10000 |   5,344.8 ns |    24.389 ns |    22.814 ns |  0.27 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String |  10000 |  35,437.9 ns |   105.633 ns |    98.809 ns |  1.81 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String |  10000 |  33,259.6 ns |   241.286 ns |   225.699 ns |  1.70 |    0.02 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |  10000 |   3,276.6 ns |    63.475 ns |   100.678 ns |  0.17 |    0.01 |           - |           - |           - |                   - |
|                            |        |              |              |              |       |         |             |             |             |                     |
|          **ListEnumerate_Int** | **100000** | **194,378.5 ns** |   **970.093 ns** |   **907.425 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int | 100000 | 236,534.3 ns |   513.313 ns |   480.153 ns |  1.22 |    0.01 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int | 100000 |  53,248.4 ns |   352.294 ns |   312.300 ns |  0.27 |    0.00 |           - |           - |           - |                   - |
|       ListEnumerate_String | 100000 | 354,993.3 ns | 1,584.851 ns | 1,404.929 ns |  1.83 |    0.01 |           - |           - |           - |                   - |
|     PooledEnumerate_String | 100000 | 333,119.6 ns | 2,322.867 ns | 2,172.812 ns |  1.71 |    0.01 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String | 100000 |  53,433.2 ns |   120.097 ns |   112.338 ns |  0.27 |    0.00 |           - |           - |           - |                   - |
