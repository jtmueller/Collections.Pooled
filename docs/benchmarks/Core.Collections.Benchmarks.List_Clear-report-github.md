``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |      Mean |     Error |     StdDev |    Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |----------:|----------:|-----------:|----------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** | **80.152 us** | **3.9955 us** | **11.0047 us** | **85.105 us** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 | 90.371 us | 4.0598 us | 10.6951 us | 94.120 us |           - |           - |           - |                   - |
|   **ClearList** | **100000** |  **7.339 us** | **0.2634 us** |  **0.6893 us** |  **7.187 us** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 | 13.862 us | 2.7319 us |  8.0549 us |  8.360 us |           - |           - |           - |                   - |
