``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |     N |         Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------ |-------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListBinarySearch_Int** |  **1000** |     **53.85 us** |   **0.3433 us** |   **0.3212 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int |  1000 |     54.76 us |   0.1587 us |   0.1485 us |  1.02 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String |  1000 |  1,100.36 us |   9.9200 us |   9.2791 us | 20.44 |    0.21 |           - |           - |           - |                   - |
| PooledBinarySearch_String |  1000 |  1,078.58 us |   5.5791 us |   5.2187 us | 20.03 |    0.14 |           - |           - |           - |                   - |
|                           |       |              |             |             |       |         |             |             |             |                     |
|      **ListBinarySearch_Int** | **10000** |    **592.12 us** |   **3.0450 us** |   **2.8482 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int | 10000 |    583.85 us |   3.9516 us |   3.5030 us |  0.99 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String | 10000 | 14,951.51 us | 485.4433 us | 498.5145 us | 25.34 |    0.85 |           - |           - |           - |                   - |
| PooledBinarySearch_String | 10000 | 15,544.67 us |  58.0908 us |  51.4960 us | 26.26 |    0.15 |           - |           - |           - |                   - |
