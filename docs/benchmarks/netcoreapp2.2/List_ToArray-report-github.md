``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |-----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListToArray** |   **1000** |   **2.582 ms** |  **0.0219 ms** |  **0.0194 ms** |  **1.00** |    **0.00** |  **12785.1563** |           **-** |           **-** |            **38.38 MB** |
| PooledToArray |   1000 |   2.567 ms |  0.0507 ms |  0.0676 ms |  0.99 |    0.03 |  12785.1563 |           - |           - |            38.38 MB |
|               |        |            |            |            |       |         |             |             |             |                     |
|   **ListToArray** |  **10000** |  **27.545 ms** |  **0.1381 ms** |  **0.1225 ms** |  **1.00** |    **0.00** | **126562.5000** |           **-** |           **-** |            **381.7 MB** |
| PooledToArray |  10000 |  26.972 ms |  0.5021 ms |  0.5156 ms |  0.98 |    0.02 | 126562.5000 |           - |           - |            381.7 MB |
|               |        |            |            |            |       |         |             |             |             |                     |
|   **ListToArray** | **100000** | **724.518 ms** | **11.0931 ms** | **10.3765 ms** |  **1.00** |    **0.00** | **985000.0000** | **985000.0000** | **985000.0000** |          **3814.93 MB** |
| PooledToArray | 100000 | 645.732 ms | 11.1261 ms |  9.8630 ms |  0.89 |    0.02 | 999000.0000 | 999000.0000 | 999000.0000 |          3814.93 MB |
