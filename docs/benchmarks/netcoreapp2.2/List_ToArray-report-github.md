``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |       Mean |      Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |-----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListToArray** |   **1000** |   **2.299 ms** |  **0.0143 ms** | **0.0134 ms** |  **1.00** |    **0.00** |  **12785.1563** |           **-** |           **-** |            **38.38 MB** |
| PooledToArray |   1000 |   2.264 ms |  0.0446 ms | 0.0417 ms |  0.98 |    0.02 |  12785.1563 |           - |           - |            38.38 MB |
|               |        |            |            |           |       |         |             |             |             |                     |
|   **ListToArray** |  **10000** |  **24.646 ms** |  **0.1210 ms** | **0.1132 ms** |  **1.00** |    **0.00** | **126562.5000** |           **-** |           **-** |            **381.7 MB** |
| PooledToArray |  10000 |  24.713 ms |  0.1812 ms | 0.1695 ms |  1.00 |    0.01 | 126562.5000 |           - |           - |            381.7 MB |
|               |        |            |            |           |       |         |             |             |             |                     |
|   **ListToArray** | **100000** | **659.793 ms** | **10.9926 ms** | **9.7446 ms** |  **1.00** |    **0.00** | **977000.0000** | **977000.0000** | **977000.0000** |          **3814.93 MB** |
| PooledToArray | 100000 | 567.208 ms |  7.4997 ms | 7.0152 ms |  0.86 |    0.02 | 975000.0000 | 975000.0000 | 975000.0000 |          3814.93 MB |
