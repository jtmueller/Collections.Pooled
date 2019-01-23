``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **6.382 ms** | **0.1733 ms** | **0.4859 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204784 B** |
| PooledAdd |   1000 |  3.909 ms | 0.1872 ms | 0.5093 ms |  0.61 |    0.09 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** | **10.714 ms** | **0.2154 ms** | **0.5483 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645880 B** |
| PooledAdd |  10000 |  5.523 ms | 0.2470 ms | 0.7282 ms |  0.54 |    0.07 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **11.229 ms** | **0.2175 ms** | **0.2977 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  6.822 ms | 0.3439 ms | 1.0141 ms |  0.70 |    0.05 |           - |           - |           - |                   - |
