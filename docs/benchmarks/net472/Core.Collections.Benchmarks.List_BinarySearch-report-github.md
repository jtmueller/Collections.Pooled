``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                    Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD |
|-------------------------- |------ |-------------:|-----------:|-----------:|------:|--------:|
|      **ListBinarySearch_Int** |  **1000** |     **55.94 us** |  **0.2234 us** |  **0.2090 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int |  1000 |     56.20 us |  0.1485 us |  0.1316 us |  1.01 |    0.01 |
|   ListBinarySearch_String |  1000 |    840.37 us |  1.5742 us |  1.3955 us | 15.03 |    0.05 |
| PooledBinarySearch_String |  1000 |    856.99 us |  1.1371 us |  1.0080 us | 15.33 |    0.05 |
|                           |       |              |            |            |       |         |
|      **ListBinarySearch_Int** | **10000** |    **670.74 us** |  **1.9810 us** |  **1.8531 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int | 10000 |    669.65 us |  1.6976 us |  1.4175 us |  1.00 |    0.00 |
|   ListBinarySearch_String | 10000 | 11,682.38 us | 31.6867 us | 29.6398 us | 17.42 |    0.05 |
| PooledBinarySearch_String | 10000 | 11,595.43 us | 28.2952 us | 26.4673 us | 17.29 |    0.06 |
