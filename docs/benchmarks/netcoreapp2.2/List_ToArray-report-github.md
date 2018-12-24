``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |       Mean |     Error |    StdDev | Ratio |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |-----------:|----------:|----------:|------:|-------------:|-------------:|-------------:|--------------------:|
|   **ListToArray** |   **1000** |   **2.330 ms** | **0.0192 ms** | **0.0180 ms** |  **1.00** |   **12785.1563** |            **-** |            **-** |            **38.38 MB** |
| PooledToArray |   1000 |   2.359 ms | 0.0174 ms | 0.0162 ms |  1.01 |   12785.1563 |            - |            - |            38.38 MB |
|               |        |            |           |           |       |              |              |              |                     |
|   **ListToArray** |  **10000** |  **24.931 ms** | **0.1373 ms** | **0.1217 ms** |  **1.00** |  **126562.5000** |            **-** |            **-** |            **381.7 MB** |
| PooledToArray |  10000 |  24.537 ms | 0.1416 ms | 0.1255 ms |  0.98 |  126562.5000 |            - |            - |            381.7 MB |
|               |        |            |           |           |       |              |              |              |                     |
|   **ListToArray** | **100000** | **666.777 ms** | **7.5667 ms** | **6.7077 ms** |  **1.00** | **1075000.0000** | **1018000.0000** | **1018000.0000** |          **3814.93 MB** |
| PooledToArray | 100000 | 569.543 ms | 6.3031 ms | 5.8959 ms |  0.85 | 1023000.0000 | 1023000.0000 | 1023000.0000 |          3814.93 MB |
