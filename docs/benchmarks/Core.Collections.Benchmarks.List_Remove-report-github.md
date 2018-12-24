``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|              Method |     N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------ |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListRemove_Int** |  **3000** |   **2.853 ms** | **0.0707 ms** | **0.2016 ms** |   **2.797 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.784 ms | 0.0552 ms | 0.1404 ms |   2.781 ms |  0.98 |    0.08 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  24.155 ms | 0.4387 ms | 0.3889 ms |  24.089 ms |  8.64 |    0.49 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  24.382 ms | 0.4722 ms | 0.6139 ms |  24.531 ms |  8.79 |    0.44 |           - |           - |           - |                   - |
|                     |       |            |           |           |            |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **28.747 ms** | **0.5712 ms** | **0.5866 ms** |  **28.809 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  28.464 ms | 0.5692 ms | 0.7199 ms |  28.468 ms |  0.99 |    0.04 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 263.974 ms | 1.2729 ms | 1.1906 ms | 264.144 ms |  9.19 |    0.20 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 264.982 ms | 2.1946 ms | 2.0528 ms | 264.164 ms |  9.23 |    0.23 |           - |           - |           - |                   - |
