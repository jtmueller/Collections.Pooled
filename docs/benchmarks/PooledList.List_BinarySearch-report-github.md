``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|        Method |  Job | Runtime |     N |         Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |------ |-------------:|------------:|------------:|------:|--------:|------:|------:|------:|----------:|
|      **List_Int** |  **Clr** |     **Clr** |  **1000** |     **60.78 us** |   **0.8496 us** |   **0.7948 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|    Pooled_Int |  Clr |     Clr |  1000 |     59.18 us |   0.8018 us |   0.7500 us |  0.97 |    0.02 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr |  1000 |    866.68 us |  15.5537 us |  14.5490 us | 14.26 |    0.25 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr |  1000 |    889.47 us |  14.6554 us |  13.7087 us | 14.64 |    0.33 |     - |     - |     - |         - |
|               |      |         |       |              |             |             |       |         |       |       |       |           |
|      List_Int | Core |    Core |  1000 |     47.02 us |   0.4313 us |   0.4034 us |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core |  1000 |     48.59 us |   0.3897 us |   0.3645 us |  1.03 |    0.01 |     - |     - |     - |         - |
|   List_String | Core |    Core |  1000 |    658.79 us |   6.8269 us |   6.3859 us | 14.01 |    0.21 |     - |     - |     - |         - |
| Pooled_String | Core |    Core |  1000 |    641.76 us |   8.8567 us |   8.2845 us | 13.65 |    0.21 |     - |     - |     - |         - |
|               |      |         |       |              |             |             |       |         |       |       |       |           |
|      **List_Int** |  **Clr** |     **Clr** | **10000** |    **724.24 us** |   **5.5547 us** |   **5.1959 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|    Pooled_Int |  Clr |     Clr | 10000 |    709.13 us |   4.6285 us |   4.3295 us |  0.98 |    0.01 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr | 10000 | 11,598.72 us | 150.6016 us | 140.8729 us | 16.02 |    0.16 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr | 10000 | 11,607.59 us |  96.0536 us |  85.1490 us | 16.03 |    0.17 |     - |     - |     - |         - |
|               |      |         |       |              |             |             |       |         |       |       |       |           |
|      List_Int | Core |    Core | 10000 |    560.42 us |   6.4760 us |   6.0577 us |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core | 10000 |    570.99 us |   6.5162 us |   5.7765 us |  1.02 |    0.01 |     - |     - |     - |         - |
|   List_String | Core |    Core | 10000 |  9,112.72 us |  87.4864 us |  81.8349 us | 16.26 |    0.14 |     - |     - |     - |         - |
| Pooled_String | Core |    Core | 10000 |  9,541.02 us |  94.2984 us |  88.2067 us | 17.03 |    0.27 |     - |     - |     - |         - |
