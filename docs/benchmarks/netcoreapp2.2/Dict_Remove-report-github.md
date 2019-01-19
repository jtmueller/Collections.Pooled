``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|       Method |     N |         Mean |        Error |       StdDev | Ratio | RatioSD |
|------------- |------ |-------------:|-------------:|-------------:|------:|--------:|
|   **DictRemove** |    **10** |     **72.11 ns** |     **2.110 ns** |     **1.762 ns** |  **1.00** |    **0.00** |
| PooledRemove |    10 |     75.27 ns |     1.532 ns |     3.427 ns |  1.04 |    0.04 |
|              |       |              |              |              |       |         |
|   **DictRemove** |   **100** |    **778.57 ns** |     **1.652 ns** |     **1.546 ns** |  **1.00** |    **0.00** |
| PooledRemove |   100 |    746.02 ns |    14.609 ns |    17.391 ns |  0.96 |    0.02 |
|              |       |              |              |              |       |         |
|   **DictRemove** | **10000** | **76,689.87 ns** | **1,233.633 ns** | **1,153.941 ns** |  **1.00** |    **0.00** |
| PooledRemove | 10000 | 85,085.50 ns | 1,073.916 ns |   951.999 ns |  1.11 |    0.02 |
