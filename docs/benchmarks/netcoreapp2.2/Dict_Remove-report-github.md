``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|       Method |     N |         Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------- |------ |-------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictRemove** |    **10** |     **71.16 ns** |   **0.5826 ns** |   **0.4865 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove |    10 |     70.71 ns |   0.3120 ns |   0.2766 ns |  0.99 |           - |           - |           - |                   - |
|              |       |              |             |             |       |             |             |             |                     |
|   **DictRemove** |   **100** |    **720.61 ns** |   **7.9679 ns** |   **7.0633 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove |   100 |    717.25 ns |   3.0911 ns |   2.5812 ns |  1.00 |           - |           - |           - |                   - |
|              |       |              |             |             |       |             |             |             |                     |
|   **DictRemove** | **10000** | **70,271.35 ns** | **216.4581 ns** | **202.4750 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove | 10000 | 70,278.44 ns | 387.1205 ns | 362.1128 ns |  1.00 |           - |           - |           - |                   - |
