``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                     Method |      N |         Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|          **ListEnumerate_Int** |   **1000** |   **1,928.7 ns** |   **6.270 ns** |   **5.558 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |   1000 |   2,065.6 ns |   5.938 ns |   5.554 ns |  1.07 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |   1000 |     884.4 ns |   2.393 ns |   2.238 ns |  0.46 |           - |           - |           - |                   - |
|       ListEnumerate_String |   1000 |   3,532.2 ns |  11.061 ns |  10.347 ns |  1.83 |           - |           - |           - |                   - |
|     PooledEnumerate_String |   1000 |   3,341.8 ns |   6.802 ns |   6.363 ns |  1.73 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |   1000 |     909.1 ns |   1.900 ns |   1.778 ns |  0.47 |           - |           - |           - |                   - |
|                            |        |              |            |            |       |             |             |             |                     |
|          **ListEnumerate_Int** |  **10000** |  **19,224.1 ns** |  **41.344 ns** |  **36.650 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int |  10000 |  20,452.5 ns |  53.151 ns |  47.117 ns |  1.06 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int |  10000 |   8,784.6 ns |  35.742 ns |  33.433 ns |  0.46 |           - |           - |           - |                   - |
|       ListEnumerate_String |  10000 |  35,085.6 ns | 142.362 ns | 133.166 ns |  1.83 |           - |           - |           - |                   - |
|     PooledEnumerate_String |  10000 |  33,619.3 ns | 164.470 ns | 145.798 ns |  1.75 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String |  10000 |   7,386.0 ns |  21.684 ns |  20.283 ns |  0.38 |           - |           - |           - |                   - |
|                            |        |              |            |            |       |             |             |             |                     |
|          **ListEnumerate_Int** | **100000** | **191,543.0 ns** | **303.848 ns** | **269.353 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|        PooledEnumerate_Int | 100000 | 204,359.4 ns | 430.565 ns | 402.751 ns |  1.07 |           - |           - |           - |                   - |
|    PooledEnumerateSpan_Int | 100000 |  87,493.4 ns | 163.263 ns | 144.729 ns |  0.46 |           - |           - |           - |                   - |
|       ListEnumerate_String | 100000 | 350,271.7 ns | 906.598 ns | 848.033 ns |  1.83 |           - |           - |           - |                   - |
|     PooledEnumerate_String | 100000 | 340,601.3 ns | 926.448 ns | 866.600 ns |  1.78 |           - |           - |           - |                   - |
| PooledEnumerateSpan_String | 100000 |  73,905.3 ns | 888.474 ns | 787.609 ns |  0.39 |           - |           - |           - |                   - |
