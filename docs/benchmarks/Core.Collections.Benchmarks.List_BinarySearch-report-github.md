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
|      **ListBinarySearch_Int** |  **1000** |     **55.27 us** |   **0.8362 us** |   **0.6983 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int |  1000 |     57.66 us |   0.4875 us |   0.4560 us |  1.04 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String |  1000 |    986.31 us |   8.5515 us |   7.5807 us | 17.84 |    0.26 |           - |           - |           - |                   - |
| PooledBinarySearch_String |  1000 |    906.31 us |   4.6324 us |   4.1065 us | 16.40 |    0.23 |           - |           - |           - |                   - |
|                           |       |              |             |             |       |         |             |             |             |                     |
|      **ListBinarySearch_Int** | **10000** |    **681.89 us** |   **4.2100 us** |   **3.9380 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int | 10000 |    720.83 us |   8.6645 us |   8.1047 us |  1.06 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String | 10000 | 13,342.94 us |  50.6953 us |  44.9400 us | 19.57 |    0.14 |           - |           - |           - |                   - |
| PooledBinarySearch_String | 10000 | 13,169.78 us | 237.9698 us | 222.5971 us | 19.31 |    0.33 |           - |           - |           - |                   - |
