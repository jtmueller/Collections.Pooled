``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method | CountToCheck | InitialSetSize |          Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |------------- |--------------- |--------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Contains_True** |            **1** |        **8000000** |      **13.41 ns** |     **0.0518 ns** |     **0.0459 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |            1 |        8000000 |      13.28 ns |     0.0428 ns |     0.0400 ns |  0.99 |           - |           - |           - |                   - |
|                         |              |                |               |               |               |       |             |             |             |                     |
|   **HashSet_Contains_True** |          **100** |        **8000000** |   **1,879.53 ns** |    **22.5750 ns** |    **21.1166 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |          100 |        8000000 |   1,803.85 ns |    25.6223 ns |    23.9671 ns |  0.96 |           - |           - |           - |                   - |
|                         |              |                |               |               |               |       |             |             |             |                     |
|   **HashSet_Contains_True** |        **10000** |        **8000000** | **260,845.05 ns** | **1,273.9858 ns** | **1,191.6871 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Contains_True |        10000 |        8000000 | 250,534.62 ns |   765.2608 ns |   639.0275 ns |  0.96 |           - |           - |           - |                   - |
