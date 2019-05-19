``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime | LargeSets |        Mean |      Error |     StdDev | Ratio | RatioSD |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|------------------ |----- |-------- |---------- |------------:|-----------:|-----------:|------:|--------:|---------:|---------:|---------:|----------:|
|        **List_Array** |  **Clr** |     **Clr** |     **False** | **2,002.78 us** | **30.2631 us** | **28.3082 us** |  **1.00** |    **0.00** | **796.8750** | **597.6563** | **597.6563** | **3715572 B** |
|   List_Enumerable |  Clr |     Clr |     False | 1,939.97 us |  9.8226 us |  9.1881 us |  0.97 |    0.02 | 798.8281 | 599.6094 | 599.6094 | 3715700 B |
|      Pooled_Array |  Clr |     Clr |     False | 1,070.71 us | 18.0492 us | 16.8832 us |  0.53 |    0.01 |        - |        - |        - |      64 B |
| Pooled_Enumerable |  Clr |     Clr |     False | 1,395.05 us | 26.1874 us | 26.8925 us |  0.70 |    0.01 |        - |        - |        - |      64 B |
|                   |      |         |           |             |            |            |       |         |          |          |          |           |
|        List_Array | Core |    Core |     False | 1,097.65 us | 21.2782 us | 20.8981 us |  1.00 |    0.00 | 599.6094 | 599.6094 | 599.6094 | 2621816 B |
|   List_Enumerable | Core |    Core |     False | 1,118.02 us | 21.6827 us | 26.6284 us |  1.02 |    0.03 | 599.6094 | 599.6094 | 599.6094 | 2621816 B |
|      Pooled_Array | Core |    Core |     False |   505.58 us |  9.5817 us |  8.9627 us |  0.46 |    0.01 |        - |        - |        - |      56 B |
| Pooled_Enumerable | Core |    Core |     False |   692.47 us | 13.4598 us | 14.9606 us |  0.63 |    0.02 |        - |        - |        - |      56 B |
|                   |      |         |           |             |            |            |       |         |          |          |          |           |
|        **List_Array** |  **Clr** |     **Clr** |      **True** |   **537.74 us** |  **8.0839 us** |  **6.7504 us** |  **1.00** |    **0.00** | **750.0000** | **507.8125** | **501.9531** | **3205672 B** |
|   List_Enumerable |  Clr |     Clr |      True |   524.42 us |  9.0053 us |  8.4235 us |  0.98 |    0.01 | 748.0469 | 499.0234 | 499.0234 | 3205216 B |
|      Pooled_Array |  Clr |     Clr |      True |   506.60 us |  9.1132 us |  8.5245 us |  0.94 |    0.02 |        - |        - |        - |      64 B |
| Pooled_Enumerable |  Clr |     Clr |      True |   504.82 us |  9.9989 us |  8.8637 us |  0.94 |    0.01 |        - |        - |        - |      64 B |
|                   |      |         |           |             |            |            |       |         |          |          |          |           |
|        List_Array | Core |    Core |      True |   383.20 us |  7.1109 us |  6.9838 us |  1.00 |    0.00 | 446.2891 | 408.2031 | 406.7383 | 2521725 B |
|   List_Enumerable | Core |    Core |      True |   383.43 us |  7.3477 us |  7.8619 us |  1.00 |    0.03 | 442.8711 | 404.2969 | 403.3203 | 2521678 B |
|      Pooled_Array | Core |    Core |      True |    57.82 us |  0.3558 us |  0.2971 us |  0.15 |    0.00 |        - |        - |        - |      56 B |
| Pooled_Enumerable | Core |    Core |      True |    57.19 us |  1.1231 us |  1.4204 us |  0.15 |    0.01 |        - |        - |        - |      56 B |
