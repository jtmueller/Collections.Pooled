``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListToArray** |   **1000** |   **2.201 ms** | **0.0154 ms** | **0.0136 ms** |  **1.00** |  **12785.1563** |           **-** |           **-** |            **38.38 MB** |
| PooledToArray |   1000 |   2.323 ms | 0.0086 ms | 0.0081 ms |  1.06 |  12785.1563 |           - |           - |            38.38 MB |
|               |        |            |           |           |       |             |             |             |                     |
|   **ListToArray** |  **10000** |  **25.246 ms** | **0.0625 ms** | **0.0584 ms** |  **1.00** | **126562.5000** |           **-** |           **-** |            **381.7 MB** |
| PooledToArray |  10000 |  24.841 ms | 0.3306 ms | 0.3093 ms |  0.98 | 126562.5000 |           - |           - |            381.7 MB |
|               |        |            |           |           |       |             |             |             |                     |
|   **ListToArray** | **100000** | **644.997 ms** | **4.1800 ms** | **3.7055 ms** |  **1.00** | **975000.0000** | **975000.0000** | **975000.0000** |          **3814.93 MB** |
| PooledToArray | 100000 | 573.899 ms | 3.9911 ms | 3.5380 ms |  0.89 | 976000.0000 | 976000.0000 | 976000.0000 |          3814.93 MB |
