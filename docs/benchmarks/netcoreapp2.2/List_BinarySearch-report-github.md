``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------ |-------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListBinarySearch_Int** |  **1000** |     **49.15 us** |  **0.2459 us** |  **0.2300 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int |  1000 |     49.63 us |  0.2842 us |  0.2659 us |  1.01 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String |  1000 |    892.49 us |  5.1087 us |  4.5287 us | 18.17 |    0.13 |           - |           - |           - |                   - |
| PooledBinarySearch_String |  1000 |    876.72 us |  3.6402 us |  3.2270 us | 17.85 |    0.09 |           - |           - |           - |                   - |
|                           |       |              |            |            |       |         |             |             |             |                     |
|      **ListBinarySearch_Int** | **10000** |    **622.38 us** |  **4.3485 us** |  **3.8548 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledBinarySearch_Int | 10000 |    630.21 us |  2.0217 us |  1.7922 us |  1.01 |    0.01 |           - |           - |           - |                   - |
|   ListBinarySearch_String | 10000 | 11,971.21 us | 51.2390 us | 47.9290 us | 19.23 |    0.16 |           - |           - |           - |                   - |
| PooledBinarySearch_String | 10000 | 11,864.90 us | 41.9751 us | 39.2635 us | 19.06 |    0.13 |           - |           - |           - |                   - |
