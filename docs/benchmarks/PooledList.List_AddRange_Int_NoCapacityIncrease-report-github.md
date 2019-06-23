``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime | LargeSets |        Mean |      Error |     StdDev | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |----- |-------- |---------- |------------:|-----------:|-----------:|------:|--------:|---------:|------:|------:|----------:|
|        **List_Array** |  **Clr** |     **Clr** |     **False** | **1,767.47 us** |  **8.5659 us** |  **8.0125 us** |  **1.00** |    **0.00** | **345.7031** |     **-** |     **-** | **1091214 B** |
|      Pooled_Array |  Clr |     Clr |     False |   853.26 us |  6.4615 us |  5.7279 us |  0.48 |    0.00 |        - |     - |     - |         - |
|   List_Enumerable |  Clr |     Clr |     False | 2,765.11 us |  9.7520 us |  8.1433 us |  1.56 |    0.01 | 214.8438 |     - |     - |  682030 B |
| Pooled_Enumerable |  Clr |     Clr |     False | 2,488.44 us | 15.0385 us | 14.0670 us |  1.41 |    0.01 | 214.8438 |     - |     - |  682030 B |
|                   |      |         |           |             |            |            |       |         |          |       |       |           |
|        List_Array | Core |    Core |     False |   665.08 us |  2.9436 us |  2.6094 us |  1.00 |    0.00 |        - |     - |     - |         - |
|      Pooled_Array | Core |    Core |     False |   553.49 us |  3.6271 us |  3.2153 us |  0.83 |    0.01 |        - |     - |     - |         - |
|   List_Enumerable | Core |    Core |     False | 2,361.20 us | 31.9416 us | 29.8782 us |  3.55 |    0.05 | 214.8438 |     - |     - |  680000 B |
| Pooled_Enumerable | Core |    Core |     False | 2,445.54 us | 29.0744 us | 25.7737 us |  3.68 |    0.04 | 214.8438 |     - |     - |  680000 B |
|                   |      |         |           |             |            |            |       |         |          |       |       |           |
|        **List_Array** |  **Clr** |     **Clr** |      **True** |   **275.31 us** |  **1.6279 us** |  **1.4431 us** |  **1.00** |    **0.00** | **214.8438** |     **-** |     **-** |  **680816 B** |
|      Pooled_Array |  Clr |     Clr |      True |   209.43 us |  1.0726 us |  0.9508 us |  0.76 |    0.01 |        - |     - |     - |         - |
|   List_Enumerable |  Clr |     Clr |      True | 1,906.00 us | 15.7998 us | 13.1935 us |  6.93 |    0.07 |        - |     - |     - |     704 B |
| Pooled_Enumerable |  Clr |     Clr |      True | 1,698.75 us | 13.1849 us | 12.3331 us |  6.17 |    0.05 |        - |     - |     - |     688 B |
|                   |      |         |           |             |            |            |       |         |          |       |       |           |
|        List_Array | Core |    Core |      True |    22.23 us |  0.4150 us |  0.6702 us |  1.00 |    0.00 |        - |     - |     - |         - |
|      Pooled_Array | Core |    Core |      True |    23.93 us |  0.1549 us |  0.1373 us |  1.07 |    0.03 |        - |     - |     - |         - |
|   List_Enumerable | Core |    Core |      True | 1,550.74 us | 21.5873 us | 19.1366 us | 69.10 |    2.41 |        - |     - |     - |     680 B |
| Pooled_Enumerable | Core |    Core |      True | 1,554.09 us |  8.8947 us |  7.8849 us | 69.25 |    2.29 |        - |     - |     - |     680 B |
