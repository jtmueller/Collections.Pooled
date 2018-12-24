``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|               Method |     N |        Mean |      Error |     StdDev | Ratio | RatioSD |
|--------------------- |------ |------------:|-----------:|-----------:|------:|--------:|
|      **ListReverse_Int** |  **1000** |    **832.8 ns** |  **15.732 ns** |  **16.834 ns** |  **1.00** |    **0.00** |
|    PooledReverse_Int |  1000 |    579.1 ns |   7.485 ns |   5.844 ns |  0.70 |    0.02 |
|   ListReverse_String |  1000 |  3,500.8 ns |  53.968 ns |  50.481 ns |  4.21 |    0.09 |
| PooledReverse_String |  1000 |  3,236.8 ns |  18.477 ns |  17.284 ns |  3.90 |    0.09 |
|                      |       |             |            |            |       |         |
|      **ListReverse_Int** | **10000** |  **8,417.8 ns** | **168.215 ns** | **165.210 ns** |  **1.00** |    **0.00** |
|    PooledReverse_Int | 10000 |  6,004.3 ns |  65.172 ns |  60.962 ns |  0.71 |    0.01 |
|   ListReverse_String | 10000 | 34,139.2 ns | 198.842 ns | 185.997 ns |  4.06 |    0.08 |
| PooledReverse_String | 10000 | 34,142.1 ns | 280.581 ns | 262.456 ns |  4.06 |    0.08 |
