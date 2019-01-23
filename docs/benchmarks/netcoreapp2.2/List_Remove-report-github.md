``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|              Method |     N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------ |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListRemove_Int** |  **3000** |   **2.831 ms** | **0.0588 ms** | **0.1697 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.853 ms | 0.0729 ms | 0.2079 ms |  1.01 |    0.08 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  23.222 ms | 0.4523 ms | 0.6487 ms |  8.08 |    0.58 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  23.737 ms | 0.4695 ms | 0.8585 ms |  8.27 |    0.59 |           - |           - |           - |                   - |
|                     |       |            |           |           |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **25.586 ms** | **0.3591 ms** | **0.2998 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  26.053 ms | 0.5064 ms | 0.5418 ms |  1.02 |    0.02 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 250.975 ms | 3.0312 ms | 2.8354 ms |  9.82 |    0.12 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 249.741 ms | 2.2638 ms | 2.1176 ms |  9.75 |    0.15 |           - |           - |           - |                   - |
