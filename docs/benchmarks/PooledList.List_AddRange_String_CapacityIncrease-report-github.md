``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime | LargeSets |       Mean |     Error |     StdDev | Ratio | RatioSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|------------------ |----- |-------- |---------- |-----------:|----------:|-----------:|------:|--------:|----------:|----------:|---------:|----------:|
|        **List_Array** |  **Clr** |     **Clr** |     **False** | **3,773.6 us** | **35.320 us** |  **33.039 us** |  **1.00** |    **0.00** | **1582.0313** | **1039.0625** | **968.7500** | **7020808 B** |
|      Pooled_Array |  Clr |     Clr |     False | 4,357.1 us | 22.528 us |  18.812 us |  1.16 |    0.01 |         - |         - |        - |      64 B |
|   List_Enumerable |  Clr |     Clr |     False | 5,663.5 us | 40.604 us |  37.981 us |  1.50 |    0.02 |  992.1875 |  992.1875 | 992.1875 | 5013880 B |
| Pooled_Enumerable |  Clr |     Clr |     False | 4,698.4 us | 31.432 us |  29.401 us |  1.25 |    0.01 |  257.8125 |         - |        - |  818515 B |
|                   |      |         |           |            |           |            |       |         |           |           |          |           |
|        List_Array | Core |    Core |     False | 2,382.2 us | 46.748 us |  55.650 us |  1.00 |    0.00 |  847.6563 |  800.7813 | 796.8750 | 5243351 B |
|      Pooled_Array | Core |    Core |     False | 1,440.7 us | 11.961 us |  10.603 us |  0.61 |    0.02 |         - |         - |        - |      56 B |
|   List_Enumerable | Core |    Core |     False | 5,547.9 us | 53.031 us |  49.605 us |  2.34 |    0.07 |  992.1875 |  992.1875 | 992.1875 | 5010712 B |
| Pooled_Enumerable | Core |    Core |     False | 3,570.5 us | 24.410 us |  22.833 us |  1.50 |    0.04 |  257.8125 |         - |        - |  816056 B |
|                   |      |         |           |            |           |            |       |         |           |           |          |           |
|        **List_Array** |  **Clr** |     **Clr** |      **True** | **2,928.2 us** | **46.076 us** |  **43.099 us** |  **1.00** |    **0.00** |  **578.1250** |  **144.5313** | **128.9063** | **6405429 B** |
|      Pooled_Array |  Clr |     Clr |      True | 1,631.7 us |  8.109 us |   7.585 us |  0.56 |    0.01 |         - |         - |        - |      64 B |
|   List_Enumerable |  Clr |     Clr |      True | 4,704.0 us | 48.992 us |  45.828 us |  1.61 |    0.02 |  992.1875 |  992.1875 | 992.1875 | 4202808 B |
| Pooled_Enumerable |  Clr |     Clr |      True | 3,709.9 us | 37.644 us |  33.371 us |  1.27 |    0.02 |         - |         - |        - |     896 B |
|                   |      |         |           |            |           |            |       |         |           |           |          |           |
|        List_Array | Core |    Core |      True | 2,758.5 us | 55.059 us | 128.698 us |  1.00 |    0.00 |  105.4688 |   82.0313 |  82.0313 | 5040331 B |
|      Pooled_Array | Core |    Core |      True |   447.6 us |  3.078 us |   2.879 us |  0.16 |    0.01 |         - |         - |        - |      56 B |
|   List_Enumerable | Core |    Core |      True | 4,461.4 us | 46.753 us |  43.733 us |  1.60 |    0.08 |  992.1875 |  992.1875 | 992.1875 | 4195528 B |
| Pooled_Enumerable | Core |    Core |      True | 2,593.9 us | 31.571 us |  29.531 us |  0.93 |    0.04 |         - |         - |        - |     872 B |
