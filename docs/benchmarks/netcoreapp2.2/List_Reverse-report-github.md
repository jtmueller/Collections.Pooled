``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |     StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **49.00 us** |  **2.2281 us** |  **2.5659 us** |  **48.10 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  50.31 us |  0.6251 us |  0.4880 us |  50.21 us |  1.04 |    0.03 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 218.08 us |  4.2499 us |  3.9754 us | 219.47 us |  4.50 |    0.15 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 246.76 us | 11.0075 us | 31.7591 us | 226.51 us |  5.02 |    0.75 |           - |           - |           - |                   - |
|                      |        |           |            |            |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** |  **98.10 us** |  **2.9792 us** |  **7.9521 us** |  **97.18 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 | 132.25 us |  9.7631 us | 28.6336 us | 137.07 us |  1.38 |    0.32 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 421.49 us | 12.2314 us | 12.5607 us | 416.11 us |  4.40 |    0.30 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 470.15 us | 12.0563 us | 34.0050 us | 458.14 us |  4.84 |    0.50 |           - |           - |           - |                   - |
