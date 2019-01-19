``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |     N |         Mean |       Error |      StdDev |       Median | Ratio | RatioSD |
|-------------------------- |------ |-------------:|------------:|------------:|-------------:|------:|--------:|
|      **ListBinarySearch_Int** |  **1000** |     **53.29 us** |   **0.7309 us** |   **0.6104 us** |     **53.20 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int |  1000 |     54.77 us |   0.1714 us |   0.1431 us |     54.75 us |  1.03 |    0.01 |
|   ListBinarySearch_String |  1000 |    892.35 us |  17.7877 us |  27.6934 us |    878.90 us | 16.84 |    0.65 |
| PooledBinarySearch_String |  1000 |    916.77 us |   1.3567 us |   1.2691 us |    916.91 us | 17.21 |    0.20 |
|                           |       |              |             |             |              |       |         |
|      **ListBinarySearch_Int** | **10000** |    **620.97 us** |   **2.1778 us** |   **1.8186 us** |    **621.25 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int | 10000 |    689.53 us |  12.8093 us |  16.6558 us |    695.24 us |  1.11 |    0.02 |
|   ListBinarySearch_String | 10000 | 11,947.72 us | 317.0239 us | 296.5443 us | 11,802.07 us | 19.19 |    0.44 |
| PooledBinarySearch_String | 10000 | 13,677.31 us |  21.0027 us |  18.6184 us | 13,678.24 us | 22.03 |    0.08 |
