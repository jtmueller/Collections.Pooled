``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |       Mean |     Error |     StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |-----------:|----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** |  **88.305 us** | **2.3667 us** |  **6.6365 us** | **88.495 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 | 100.936 us | 5.0092 us | 14.2102 us | 96.387 us |  1.15 |    0.18 |           - |           - |           - |                   - |
|             |        |            |           |            |           |       |         |             |             |             |                     |
|   **ClearList** | **100000** |   **7.929 us** | **0.3336 us** |  **0.9018 us** |  **7.775 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 |  19.674 us | 3.0438 us |  8.9747 us | 26.835 us |  2.53 |    1.18 |           - |           - |           - |                   - |
