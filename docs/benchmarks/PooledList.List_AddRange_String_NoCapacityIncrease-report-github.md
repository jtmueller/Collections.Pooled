``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime | LargeSets |       Mean |      Error |     StdDev | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |----- |-------- |---------- |-----------:|-----------:|-----------:|------:|--------:|---------:|------:|------:|----------:|
|        **List_Array** |  **Clr** |     **Clr** |     **False** | **2,324.7 us** | **16.0412 us** | **15.0049 us** |  **1.00** |    **0.00** | **562.5000** |     **-** |     **-** | **1773256 B** |
|      Pooled_Array |  Clr |     Clr |     False | 2,337.8 us | 20.7441 us | 19.4040 us |  1.01 |    0.01 | 562.5000 |     - |     - | 1773256 B |
|   List_Enumerable |  Clr |     Clr |     False | 3,597.2 us | 43.2913 us | 38.3766 us |  1.55 |    0.02 | 257.8125 |     - |     - |  818419 B |
| Pooled_Enumerable |  Clr |     Clr |     False | 3,596.4 us | 19.6431 us | 18.3742 us |  1.55 |    0.01 | 257.8125 |     - |     - |  818419 B |
|                   |      |         |           |            |            |            |       |         |          |       |       |           |
|        List_Array | Core |    Core |     False |   896.7 us |  8.4835 us |  7.9354 us |  1.00 |    0.00 |        - |     - |     - |         - |
|      Pooled_Array | Core |    Core |     False |   894.0 us |  3.4873 us |  3.2620 us |  1.00 |    0.01 |        - |     - |     - |         - |
|   List_Enumerable | Core |    Core |     False | 3,356.9 us | 25.1669 us | 23.5412 us |  3.74 |    0.04 | 257.8125 |     - |     - |  816000 B |
| Pooled_Enumerable | Core |    Core |     False | 3,319.0 us | 11.1460 us | 10.4260 us |  3.70 |    0.04 | 257.8125 |     - |     - |  816000 B |
|                   |      |         |           |            |            |            |       |         |          |       |       |           |
|        **List_Array** |  **Clr** |     **Clr** |      **True** |   **705.1 us** |  **9.5366 us** |  **8.9205 us** |  **1.00** |    **0.00** | **424.8047** |     **-** |     **-** | **1360816 B** |
|      Pooled_Array |  Clr |     Clr |      True |   698.8 us |  3.6718 us |  3.4346 us |  0.99 |    0.01 | 424.8047 |     - |     - | 1360816 B |
|   List_Enumerable |  Clr |     Clr |      True | 2,730.6 us | 22.4341 us | 20.9848 us |  3.87 |    0.06 |        - |     - |     - |     832 B |
| Pooled_Enumerable |  Clr |     Clr |      True | 2,722.8 us | 18.9658 us | 17.7406 us |  3.86 |    0.06 |        - |     - |     - |     832 B |
|                   |      |         |           |            |            |            |       |         |          |       |       |           |
|        List_Array | Core |    Core |      True |   132.3 us |  0.9596 us |  0.8976 us |  1.00 |    0.00 |        - |     - |     - |         - |
|      Pooled_Array | Core |    Core |      True |   133.9 us |  0.7440 us |  0.6959 us |  1.01 |    0.01 |        - |     - |     - |         - |
|   List_Enumerable | Core |    Core |      True | 2,276.7 us | 16.3313 us | 15.2763 us | 17.21 |    0.16 |        - |     - |     - |     816 B |
| Pooled_Enumerable | Core |    Core |      True | 2,520.5 us | 15.0848 us | 14.1103 us | 19.05 |    0.17 |        - |     - |     - |     816 B |
