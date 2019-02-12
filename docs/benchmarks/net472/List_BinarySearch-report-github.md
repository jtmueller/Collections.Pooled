``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                    Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------ |-------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListBinarySearch_Int** |  **1000** |     **56.20 us** |  **0.1858 us** |  **0.1738 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int |  1000 |     55.76 us |  0.1917 us |  0.1793 us |  0.99 |    0.00 |           - |           - |           - |                   - |
|   ListBinarySearch_String |  1000 |    865.97 us |  2.8651 us |  2.5399 us | 15.40 |    0.05 |           - |           - |           - |                   - |
| PooledBinarySearch_String |  1000 |    881.94 us |  1.9548 us |  1.8285 us | 15.69 |    0.05 |           - |           - |           - |                   - |
|                           |       |              |            |            |       |         |             |             |             |                     |
|      **ListBinarySearch_Int** | **10000** |    **672.70 us** |  **1.8468 us** |  **1.6371 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int | 10000 |    673.29 us |  2.1339 us |  1.9961 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|   ListBinarySearch_String | 10000 | 12,262.75 us | 47.7396 us | 44.6557 us | 18.22 |    0.07 |           - |           - |           - |                   - |
| PooledBinarySearch_String | 10000 | 11,849.12 us | 35.8404 us | 33.5251 us | 17.61 |    0.06 |           - |           - |           - |                   - |
