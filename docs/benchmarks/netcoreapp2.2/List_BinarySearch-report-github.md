``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |     N |         Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------ |-------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListBinarySearch_Int** |  **1000** |     **47.31 us** |   **0.2458 us** |   **0.2299 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int |  1000 |     48.41 us |   0.1288 us |   0.1204 us |  1.02 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String |  1000 |    838.23 us |   2.4954 us |   2.3342 us | 17.72 |    0.08 |           - |           - |           - |                   - |
| PooledBinarySearch_String |  1000 |    859.74 us |   2.7703 us |   2.3133 us | 18.18 |    0.08 |           - |           - |           - |                   - |
|                           |       |              |             |             |       |         |             |             |             |                     |
|      **ListBinarySearch_Int** | **10000** |    **615.39 us** |   **2.9444 us** |   **2.6101 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int | 10000 |    620.21 us |   2.7871 us |   2.6071 us |  1.01 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String | 10000 | 12,037.91 us | 118.1379 us | 104.7262 us | 19.56 |    0.19 |           - |           - |           - |                   - |
| PooledBinarySearch_String | 10000 | 12,236.85 us |  53.6092 us |  47.5231 us | 19.89 |    0.11 |           - |           - |           - |                   - |
