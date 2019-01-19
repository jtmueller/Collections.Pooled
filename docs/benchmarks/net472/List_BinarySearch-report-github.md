``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                    Method |     N |         Mean |       Error |      StdDev |       Median | Ratio | RatioSD |
|-------------------------- |------ |-------------:|------------:|------------:|-------------:|------:|--------:|
|      **ListBinarySearch_Int** |  **1000** |     **59.07 us** |   **1.1686 us** |   **2.2234 us** |     **58.15 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int |  1000 |     57.01 us |   0.1630 us |   0.1525 us |     56.96 us |  0.97 |    0.04 |
|   ListBinarySearch_String |  1000 |    928.18 us |  18.4182 us |  17.2284 us |    933.89 us | 15.75 |    0.62 |
| PooledBinarySearch_String |  1000 |    956.55 us |   5.4886 us |   5.1340 us |    957.45 us | 16.23 |    0.63 |
|                           |       |              |             |             |              |       |         |
|      **ListBinarySearch_Int** | **10000** |    **724.18 us** |  **14.2919 us** |  **24.2688 us** |    **736.39 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int | 10000 |    734.86 us |  14.5978 us |  14.9908 us |    741.19 us |  1.02 |    0.05 |
|   ListBinarySearch_String | 10000 | 12,655.54 us | 247.7274 us | 407.0233 us | 12,874.59 us | 17.52 |    0.80 |
| PooledBinarySearch_String | 10000 | 12,943.16 us |  49.3195 us |  46.1335 us | 12,940.82 us | 17.94 |    0.69 |
