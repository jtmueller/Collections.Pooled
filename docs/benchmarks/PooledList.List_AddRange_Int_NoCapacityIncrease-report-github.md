``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|            Method |  Job | Runtime | LargeSets |        Mean |      Error |     StdDev | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |----- |-------- |---------- |------------:|-----------:|-----------:|------:|--------:|---------:|------:|------:|----------:|
|        **List_Array** |  **Clr** |     **Clr** |     **False** | **1,520.49 us** | **18.8132 us** | **17.5979 us** |  **1.00** |    **0.00** | **345.7031** |     **-** |     **-** | **1091214 B** |
|      Pooled_Array |  Clr |     Clr |     False |   736.32 us | 12.2841 us | 11.4906 us |  0.48 |    0.01 |        - |     - |     - |         - |
|   List_Enumerable |  Clr |     Clr |     False | 1,509.60 us | 29.0989 us | 27.2191 us |  0.99 |    0.02 | 345.7031 |     - |     - | 1091214 B |
| Pooled_Enumerable |  Clr |     Clr |     False | 1,048.56 us | 17.2760 us | 16.1600 us |  0.69 |    0.01 |        - |     - |     - |         - |
|                   |      |         |           |             |            |            |       |         |          |       |       |           |
|        List_Array | Core |    Core |     False |   642.14 us | 12.2459 us | 11.4549 us |  1.00 |    0.00 |        - |     - |     - |         - |
|      Pooled_Array | Core |    Core |     False |   475.37 us |  7.1458 us |  6.6842 us |  0.74 |    0.02 |        - |     - |     - |         - |
|   List_Enumerable | Core |    Core |     False |   625.62 us |  9.0605 us |  8.4752 us |  0.97 |    0.02 |        - |     - |     - |         - |
| Pooled_Enumerable | Core |    Core |     False |   617.43 us |  9.8655 us |  9.2282 us |  0.96 |    0.02 |        - |     - |     - |         - |
|                   |      |         |           |             |            |            |       |         |          |       |       |           |
|        **List_Array** |  **Clr** |     **Clr** |      **True** |   **219.86 us** |  **4.2632 us** |  **3.9878 us** |  **1.00** |    **0.00** | **215.0879** |     **-** |     **-** |  **680816 B** |
|      Pooled_Array |  Clr |     Clr |      True |   181.87 us |  3.6116 us |  3.3783 us |  0.83 |    0.02 |        - |     - |     - |         - |
|   List_Enumerable |  Clr |     Clr |      True |   220.23 us |  3.7073 us |  3.4678 us |  1.00 |    0.02 | 215.0879 |     - |     - |  680816 B |
| Pooled_Enumerable |  Clr |     Clr |      True |   164.01 us |  2.1808 us |  2.0400 us |  0.75 |    0.02 |        - |     - |     - |         - |
|                   |      |         |           |             |            |            |       |         |          |       |       |           |
|        List_Array | Core |    Core |      True |    20.87 us |  0.2513 us |  0.2351 us |  1.00 |    0.00 |        - |     - |     - |         - |
|      Pooled_Array | Core |    Core |      True |    21.19 us |  0.2288 us |  0.2140 us |  1.02 |    0.01 |        - |     - |     - |         - |
|   List_Enumerable | Core |    Core |      True |    20.94 us |  0.2050 us |  0.1917 us |  1.00 |    0.01 |        - |     - |     - |         - |
| Pooled_Enumerable | Core |    Core |      True |    20.90 us |  0.2347 us |  0.2196 us |  1.00 |    0.02 |        - |     - |     - |         - |
