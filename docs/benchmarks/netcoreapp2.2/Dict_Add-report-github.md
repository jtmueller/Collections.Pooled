``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **5.974 ms** | **0.1914 ms** | **0.5584 ms** |  **5.843 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204784 B** |
| PooledAdd |   1000 |  3.677 ms | 0.2076 ms | 0.5542 ms |  3.628 ms |  0.62 |    0.11 |           - |           - |           - |                   - |
|           |        |           |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** |  **9.974 ms** | **0.2365 ms** | **0.6937 ms** |  **9.823 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645880 B** |
| PooledAdd |  10000 |  4.911 ms | 0.2604 ms | 0.7301 ms |  4.662 ms |  0.49 |    0.08 |           - |           - |           - |                   - |
|           |        |           |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **11.531 ms** | **0.3218 ms** | **0.9489 ms** | **11.314 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  7.378 ms | 0.4344 ms | 1.2808 ms |  7.297 ms |  0.64 |    0.11 |           - |           - |           - |                   - |
