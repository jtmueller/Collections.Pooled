``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **6.104 ms** | **0.1624 ms** | **0.4685 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204784 B** |
| PooledAdd |   1000 |  3.459 ms | 0.0706 ms | 0.1969 ms |  0.57 |    0.05 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** |  **9.430 ms** | **0.1879 ms** | **0.2011 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645880 B** |
| PooledAdd |  10000 |  5.236 ms | 0.1045 ms | 0.2484 ms |  0.55 |    0.02 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **10.783 ms** | **0.2105 ms** | **0.3574 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  7.371 ms | 0.1464 ms | 0.4154 ms |  0.69 |    0.05 |           - |           - |           - |                   - |
