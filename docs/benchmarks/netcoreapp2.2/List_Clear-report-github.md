``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |     Mean |    Error |     StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |---------:|---------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** | **50.64 us** | **1.017 us** |  **0.9511 us** | **50.715 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 | 90.51 us | 2.097 us |  5.7050 us | 92.705 us |  1.76 |    0.13 |           - |           - |           - |                   - |
|             |        |          |          |            |           |       |         |             |             |             |                     |
|   **ClearList** | **100000** | **10.08 us** | **1.329 us** |  **3.8352 us** |  **8.193 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 | 21.38 us | 4.373 us | 12.6871 us | 26.405 us |  2.14 |    1.07 |           - |           - |           - |                   - |
