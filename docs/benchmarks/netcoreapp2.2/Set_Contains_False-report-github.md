``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                   Method | CountToCheck | InitialSetSize |          Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------- |------------- |--------------- |--------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Contains_False** |            **1** |        **8000000** |      **9.542 ns** |   **0.2264 ns** |   **0.2224 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |            1 |        8000000 |      9.205 ns |   0.0819 ns |   0.0726 ns |  0.97 |    0.02 |           - |           - |           - |                   - |
|                          |              |                |               |             |             |       |         |             |             |             |                     |
|   **HashSet_Contains_False** |          **100** |        **8000000** |    **893.709 ns** |   **2.7219 ns** |   **2.5460 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |          100 |        8000000 |    772.655 ns |   1.2797 ns |   1.1344 ns |  0.86 |    0.00 |           - |           - |           - |                   - |
|                          |              |                |               |             |             |       |         |             |             |             |                     |
|   **HashSet_Contains_False** |        **10000** |        **8000000** | **88,067.926 ns** | **160.7565 ns** | **134.2389 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_False |        10000 |        8000000 | 76,069.789 ns | 248.3741 ns | 232.3293 ns |  0.86 |    0.00 |           - |           - |           - |                   - |
