``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|      Method |  Job | Runtime |      N |           Mean |         Error |        StdDev |         Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |----- |-------- |------- |---------------:|--------------:|--------------:|---------------:|------:|--------:|------:|------:|------:|----------:|
|   **ClearList** |  **Clr** |     **Clr** |  **10000** |  **44,420.018 us** |   **888.9363 us** |   **912.8721 us** |  **44,223.600 us** | **1.000** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| ClearPooled |  Clr |     Clr |  10000 |     139.358 us |     3.5397 us |     9.5698 us |     135.700 us | 0.003 |    0.00 |     - |     - |     - |         - |
|             |      |         |        |                |               |               |                |       |         |       |       |       |           |
|   ClearList | Core |    Core |  10000 |      53.528 us |     1.0558 us |     1.5476 us |      53.200 us |  1.00 |    0.00 |     - |     - |     - |         - |
| ClearPooled | Core |    Core |  10000 |      92.401 us |     1.8449 us |     4.6624 us |      92.300 us |  1.71 |    0.14 |     - |     - |     - |         - |
|             |      |         |        |                |               |               |                |       |         |       |       |       |           |
|   **ClearList** |  **Clr** |     **Clr** | **100000** | **430,639.793 us** | **7,896.4544 us** | **7,386.3484 us** | **427,014.100 us** | **1.000** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| ClearPooled |  Clr |     Clr | 100000 |     155.943 us |     3.0784 us |     2.7290 us |     155.900 us | 0.000 |    0.00 |     - |     - |     - |         - |
|             |      |         |        |                |               |               |                |       |         |       |       |       |           |
|   ClearList | Core |    Core | 100000 |       6.410 us |     0.1298 us |     0.2307 us |       6.450 us |  1.00 |    0.00 |     - |     - |     - |         - |
| ClearPooled | Core |    Core | 100000 |      30.953 us |     0.2966 us |     0.2774 us |      30.900 us |  4.78 |    0.19 |     - |     - |     - |         - |
