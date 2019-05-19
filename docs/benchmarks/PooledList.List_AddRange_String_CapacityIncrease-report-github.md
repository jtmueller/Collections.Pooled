``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime | LargeSets |       Mean |     Error |     StdDev | Ratio | RatioSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|------------------ |----- |-------- |---------- |-----------:|----------:|-----------:|------:|--------:|----------:|----------:|---------:|----------:|
|        **List_Array** |  **Clr** |     **Clr** |     **False** | **3,175.4 us** | **62.524 us** |  **74.430 us** |  **1.00** |    **0.00** | **1546.8750** | **1007.8125** | **933.5938** | **7020369 B** |
|      Pooled_Array |  Clr |     Clr |     False | 3,672.2 us | 16.519 us |  15.452 us |  1.15 |    0.03 |         - |         - |        - |      64 B |
|   List_Enumerable |  Clr |     Clr |     False | 3,255.9 us | 64.067 us |  83.305 us |  1.03 |    0.03 | 1468.7500 |  929.6875 | 855.4688 | 7020048 B |
| Pooled_Enumerable |  Clr |     Clr |     False | 2,043.3 us | 26.681 us |  24.958 us |  0.64 |    0.02 |         - |         - |        - |      64 B |
|                   |      |         |           |            |           |            |       |         |           |           |          |           |
|        List_Array | Core |    Core |     False | 1,790.4 us | 23.164 us |  21.667 us |  1.00 |    0.00 | 1050.7813 |  998.0469 | 998.0469 | 5243385 B |
|      Pooled_Array | Core |    Core |     False | 1,237.0 us | 13.632 us |  12.751 us |  0.69 |    0.01 |         - |         - |        - |      56 B |
|   List_Enumerable | Core |    Core |     False | 1,812.5 us | 33.524 us |  31.358 us |  1.01 |    0.02 | 1050.7813 |  998.0469 | 998.0469 | 5243385 B |
| Pooled_Enumerable | Core |    Core |     False | 1,068.8 us | 19.704 us |  18.431 us |  0.60 |    0.01 |         - |         - |        - |      56 B |
|                   |      |         |           |            |           |            |       |         |           |           |          |           |
|        **List_Array** |  **Clr** |     **Clr** |      **True** | **2,509.0 us** | **49.513 us** |  **50.846 us** |  **1.00** |    **0.00** |  **625.0000** |  **207.0313** | **175.7813** | **6405805 B** |
|      Pooled_Array |  Clr |     Clr |      True | 1,406.1 us | 12.480 us |  11.674 us |  0.56 |    0.01 |         - |         - |        - |      64 B |
|   List_Enumerable |  Clr |     Clr |      True | 2,543.6 us | 50.735 us |  47.458 us |  1.01 |    0.03 |  628.9063 |  195.3125 | 179.6875 | 6405656 B |
| Pooled_Enumerable |  Clr |     Clr |      True | 1,104.0 us | 11.019 us |  10.307 us |  0.44 |    0.01 |         - |         - |        - |      64 B |
|                   |      |         |           |            |           |            |       |         |           |           |          |           |
|        List_Array | Core |    Core |      True | 2,144.4 us | 42.735 us | 122.615 us |  1.00 |    0.00 |   66.4063 |   42.9688 |  42.9688 | 5040269 B |
|      Pooled_Array | Core |    Core |      True |   390.0 us |  4.408 us |   4.124 us |  0.18 |    0.01 |         - |         - |        - |      56 B |
|   List_Enumerable | Core |    Core |      True | 2,139.8 us | 45.202 us | 131.856 us |  1.00 |    0.08 |  152.3438 |  128.9063 | 128.9063 | 5040363 B |
| Pooled_Enumerable | Core |    Core |      True |   389.1 us |  3.667 us |   3.430 us |  0.18 |    0.01 |         - |         - |        - |      56 B |
