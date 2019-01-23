``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                    Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------ |-------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListBinarySearch_Int** |  **1000** |     **56.17 us** |  **0.1047 us** |  **0.0980 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int |  1000 |     55.57 us |  0.1550 us |  0.1374 us |  0.99 |    0.00 |           - |           - |           - |                   - |
|   ListBinarySearch_String |  1000 |    855.24 us |  1.8835 us |  1.6697 us | 15.23 |    0.04 |           - |           - |           - |                   - |
| PooledBinarySearch_String |  1000 |    913.15 us |  1.4311 us |  1.2686 us | 16.26 |    0.03 |           - |           - |           - |                   - |
|                           |       |              |            |            |       |         |             |             |             |                     |
|      **ListBinarySearch_Int** | **10000** |    **670.70 us** |  **2.1588 us** |  **2.0193 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int | 10000 |    669.89 us |  1.6863 us |  1.5773 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|   ListBinarySearch_String | 10000 | 11,744.00 us | 21.2015 us | 18.7946 us | 17.51 |    0.06 |           - |           - |           - |                   - |
| PooledBinarySearch_String | 10000 | 11,746.06 us | 40.9369 us | 38.2924 us | 17.51 |    0.06 |           - |           - |           - |                   - |
