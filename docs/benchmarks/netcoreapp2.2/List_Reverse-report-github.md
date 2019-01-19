``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **64.68 us** |  **7.6151 us** | **22.214 us** |  **50.89 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  50.25 us |  0.9637 us |  1.110 us |  50.05 us |  0.81 |    0.25 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 242.49 us |  9.5096 us | 27.437 us | 225.32 us |  4.10 |    1.21 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 227.63 us | 10.0266 us | 13.037 us | 222.06 us |  3.63 |    1.09 |           - |           - |           - |                   - |
|                      |        |           |            |           |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** | **125.94 us** | **10.3395 us** | **30.486 us** | **129.68 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 | 118.05 us |  7.6424 us | 22.293 us | 104.84 us |  0.99 |    0.30 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 469.65 us | 11.1421 us | 32.148 us | 450.49 us |  3.93 |    0.94 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 468.72 us | 10.2826 us | 27.974 us | 459.82 us |  3.91 |    0.96 |           - |           - |           - |                   - |
