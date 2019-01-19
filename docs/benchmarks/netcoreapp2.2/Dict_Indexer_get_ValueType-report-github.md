``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     N |       Mean |      Error |      StdDev | Ratio | RatioSD |
|---------------------------- |------ |-----------:|-----------:|------------:|------:|--------:|
|   **DictIndexer_get_ValueType** |  **1024** |   **269.8 us** |  **0.2812 us** |   **0.2348 us** |  **1.00** |    **0.00** |
| PooledIndexer_get_ValueType |  1024 |   267.6 us |  0.8506 us |   0.7540 us |  0.99 |    0.00 |
|                             |       |            |            |             |       |         |
|   **DictIndexer_get_ValueType** |  **8192** | **2,003.5 us** | **15.4847 us** |  **12.0895 us** |  **1.00** |    **0.00** |
| PooledIndexer_get_ValueType |  8192 | 2,146.5 us |  5.0063 us |   4.4379 us |  1.07 |    0.01 |
|                             |       |            |            |             |       |         |
|   **DictIndexer_get_ValueType** | **16384** | **4,080.6 us** | **87.6961 us** | **114.0296 us** |  **1.00** |    **0.00** |
| PooledIndexer_get_ValueType | 16384 | 4,293.9 us |  5.8022 us |   4.8451 us |  1.05 |    0.03 |
