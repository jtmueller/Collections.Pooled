``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|           Method | CountToRemove | InitialSetSize |         Mean |        Error |      StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------------- |--------------- |-------------:|-------------:|------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Remove** |             **1** |        **8000000** |     **684.5 ns** |     **61.56 ns** |    **156.7 ns** |     **645.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |             1 |        8000000 |     607.4 ns |     46.06 ns |    118.1 ns |     610.0 ns |  0.93 |    0.27 |           - |           - |           - |                   - |
|                  |               |                |              |              |             |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |           **100** |        **8000000** |   **8,561.3 ns** |    **682.07 ns** |  **1,967.9 ns** |   **8,880.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |           100 |        8000000 |  32,497.4 ns |    717.43 ns |    736.7 ns |  32,315.0 ns |  3.01 |    0.52 |           - |           - |           - |                   - |
|                  |               |                |              |              |             |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |         **10000** |        **8000000** | **405,767.4 ns** | **21,577.27 ns** | **63,282.4 ns** | **438,430.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |         10000 |        8000000 | 523,977.5 ns | 10,654.43 ns | 26,135.5 ns | 514,600.0 ns |  1.31 |    0.20 |           - |           - |           - |                   - |
