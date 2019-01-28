``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|           Method | CountToRemove | InitialSetSize |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------------- |--------------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Remove** |             **1** |        **8000000** |   **1,895.0 ns** |    **666.17 ns** |   **1,922.1 ns** |     **920.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |             1 |        8000000 |     673.9 ns |     46.01 ns |     117.1 ns |     670.0 ns |  0.66 |    0.39 |           - |           - |           - |                   - |
|                  |               |                |              |              |              |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |           **100** |        **8000000** |  **10,744.8 ns** |  **1,195.82 ns** |   **3,293.6 ns** |  **10,080.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |           100 |        8000000 |  19,993.5 ns |  4,093.38 ns |  12,069.4 ns |  12,526.5 ns |  2.03 |    1.38 |           - |           - |           - |                   - |
|                  |               |                |              |              |              |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |         **10000** |        **8000000** | **468,284.2 ns** | **29,455.48 ns** |  **85,455.7 ns** | **466,245.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |         10000 |        8000000 | 788,584.0 ns | 61,891.02 ns | 182,487.1 ns | 849,160.0 ns |  1.76 |    0.51 |           - |           - |           - |                   - |
