``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                     Method |      N |         Mean |       Error |      StdDev | Ratio | RatioSD |
|--------------------------- |------- |-------------:|------------:|------------:|------:|--------:|
|          **ListEnumerate_Int** |   **1000** |   **2,155.4 ns** |    **42.77 ns** |    **73.77 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |   1000 |   2,624.1 ns |    49.50 ns |    50.83 ns |  1.23 |    0.05 |
|    PooledEnumerateSpan_Int |   1000 |     966.9 ns |    15.03 ns |    14.06 ns |  0.45 |    0.01 |
|       ListEnumerate_String |   1000 |   3,912.8 ns |    28.70 ns |    25.45 ns |  1.85 |    0.06 |
|     PooledEnumerate_String |   1000 |   4,042.5 ns |    52.37 ns |    46.42 ns |  1.91 |    0.06 |
| PooledEnumerateSpan_String |   1000 |     989.8 ns |    19.20 ns |    24.29 ns |  0.46 |    0.02 |
|                            |        |              |             |             |       |         |
|          **ListEnumerate_Int** |  **10000** |  **21,806.9 ns** |   **158.07 ns** |   **140.12 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |  10000 |  25,396.1 ns |   496.08 ns |   530.80 ns |  1.16 |    0.02 |
|    PooledEnumerateSpan_Int |  10000 |   9,652.8 ns |    66.47 ns |    62.17 ns |  0.44 |    0.00 |
|       ListEnumerate_String |  10000 |  38,496.5 ns |   504.21 ns |   421.04 ns |  1.76 |    0.02 |
|     PooledEnumerate_String |  10000 |  41,670.0 ns |   610.53 ns |   571.09 ns |  1.91 |    0.02 |
| PooledEnumerateSpan_String |  10000 |   9,664.4 ns |   190.41 ns |   195.53 ns |  0.44 |    0.01 |
|                            |        |              |             |             |       |         |
|          **ListEnumerate_Int** | **100000** | **201,818.5 ns** | **1,337.37 ns** | **1,250.98 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int | 100000 | 256,495.4 ns | 1,263.30 ns | 1,181.69 ns |  1.27 |    0.01 |
|    PooledEnumerateSpan_Int | 100000 |  90,667.4 ns | 1,716.74 ns | 1,908.15 ns |  0.45 |    0.01 |
|       ListEnumerate_String | 100000 | 385,920.0 ns | 2,075.22 ns | 1,941.16 ns |  1.91 |    0.01 |
|     PooledEnumerate_String | 100000 | 393,038.1 ns | 3,470.82 ns | 3,076.79 ns |  1.95 |    0.02 |
| PooledEnumerateSpan_String | 100000 |  96,503.2 ns |   379.06 ns |   316.53 ns |  0.48 |    0.00 |
