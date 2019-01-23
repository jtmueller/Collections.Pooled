``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|       Method |     N |         Mean |         Error |        StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------- |------ |-------------:|--------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictRemove** |    **10** |     **74.07 ns** |     **0.2183 ns** |     **0.2042 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove |    10 |     72.01 ns |     0.3155 ns |     0.2797 ns |  0.97 |    0.00 |           - |           - |           - |                   - |
|              |       |              |               |               |       |         |             |             |             |                     |
|   **DictRemove** |   **100** |    **723.20 ns** |     **4.7246 ns** |     **4.4194 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove |   100 |    732.52 ns |    13.7543 ns |    12.1928 ns |  1.01 |    0.01 |           - |           - |           - |                   - |
|              |       |              |               |               |       |         |             |             |             |                     |
|   **DictRemove** | **10000** | **70,899.64 ns** |   **402.3524 ns** |   **356.6749 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove | 10000 | 72,058.25 ns | 1,272.0188 ns | 1,127.6116 ns |  1.02 |    0.02 |           - |           - |           - |                   - |
