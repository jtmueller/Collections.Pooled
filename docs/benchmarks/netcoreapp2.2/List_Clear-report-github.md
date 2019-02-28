``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** | **52.394 us** | **1.0518 us** | **1.5417 us** | **52.635 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 | 96.032 us | 1.1633 us | 0.9714 us | 96.155 us |  1.85 |    0.06 |           - |           - |           - |                   - |
|             |        |           |           |           |           |       |         |             |             |             |                     |
|   **ClearList** | **100000** |  **8.016 us** | **0.1994 us** | **0.5184 us** |  **7.950 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 | 27.577 us | 2.6455 us | 6.9693 us | 31.140 us |  3.44 |    0.90 |           - |           - |           - |                   - |
