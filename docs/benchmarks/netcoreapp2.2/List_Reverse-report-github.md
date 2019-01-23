``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|               Method |      N |      Mean |      Error |     StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------- |----------:|-----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListReverse_Int** | **100000** |  **49.64 us** |  **0.8958 us** |  **0.7941 us** |  **49.58 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 100000 |  51.08 us |  0.8511 us |  0.6645 us |  51.17 us |  1.03 |    0.02 |           - |           - |           - |                   - |
|   ListReverse_String | 100000 | 219.73 us |  0.8780 us |  0.7332 us | 219.39 us |  4.43 |    0.07 |           - |           - |           - |                   - |
| PooledReverse_String | 100000 | 219.83 us |  0.5711 us |  0.4459 us | 219.80 us |  4.43 |    0.07 |           - |           - |           - |                   - |
|                      |        |           |            |            |           |       |         |             |             |             |                     |
|      **ListReverse_Int** | **200000** |  **96.84 us** |  **1.9098 us** |  **3.2943 us** |  **97.20 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledReverse_Int | 200000 | 123.06 us |  9.2906 us | 27.1011 us | 103.93 us |  1.19 |    0.28 |           - |           - |           - |                   - |
|   ListReverse_String | 200000 | 431.00 us |  8.4865 us | 13.2124 us | 436.32 us |  4.45 |    0.19 |           - |           - |           - |                   - |
| PooledReverse_String | 200000 | 466.28 us | 11.0684 us | 31.9349 us | 457.61 us |  4.76 |    0.23 |           - |           - |           - |                   - |
