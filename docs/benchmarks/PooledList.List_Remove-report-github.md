``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|        Method |  Job | Runtime |     N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |------ |-----------:|----------:|----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|      **List_Int** |  **Clr** |     **Clr** |  **3000** |   **2.554 ms** | **0.0505 ms** | **0.1009 ms** |   **2.529 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|    Pooled_Int |  Clr |     Clr |  3000 |   2.591 ms | 0.0514 ms | 0.1346 ms |   2.561 ms |  1.02 |    0.07 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr |  3000 |  25.020 ms | 0.4965 ms | 1.1206 ms |  24.912 ms |  9.80 |    0.64 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr |  3000 |  25.277 ms | 0.4942 ms | 0.7839 ms |  25.246 ms |  9.84 |    0.48 |     - |     - |     - |         - |
|               |      |         |       |            |           |           |            |       |         |       |       |       |           |
|      List_Int | Core |    Core |  3000 |   2.570 ms | 0.0531 ms | 0.1472 ms |   2.522 ms |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core |  3000 |   2.606 ms | 0.0516 ms | 0.1472 ms |   2.566 ms |  1.02 |    0.08 |     - |     - |     - |         - |
|   List_String | Core |    Core |  3000 |  26.610 ms | 0.5303 ms | 0.8860 ms |  26.617 ms | 10.32 |    0.75 |     - |     - |     - |         - |
| Pooled_String | Core |    Core |  3000 |  25.605 ms | 0.5091 ms | 1.1281 ms |  25.673 ms |  9.97 |    0.67 |     - |     - |     - |         - |
|               |      |         |       |            |           |           |            |       |         |       |       |       |           |
|      **List_Int** |  **Clr** |     **Clr** | **10000** |  **26.719 ms** | **0.5398 ms** | **0.8405 ms** |  **26.534 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|    Pooled_Int |  Clr |     Clr | 10000 |  26.890 ms | 0.4762 ms | 0.4454 ms |  26.709 ms |  0.99 |    0.03 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr | 10000 | 267.149 ms | 5.2280 ms | 9.6904 ms | 263.271 ms | 10.03 |    0.54 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr | 10000 | 268.040 ms | 5.2351 ms | 6.0287 ms | 266.435 ms |  9.92 |    0.38 |     - |     - |     - |         - |
|               |      |         |       |            |           |           |            |       |         |       |       |       |           |
|      List_Int | Core |    Core | 10000 |  27.100 ms | 0.5363 ms | 0.5267 ms |  27.089 ms |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core | 10000 |  27.861 ms | 0.5485 ms | 0.5869 ms |  27.904 ms |  1.03 |    0.03 |     - |     - |     - |         - |
|   List_String | Core |    Core | 10000 | 261.022 ms | 4.0537 ms | 3.7918 ms | 260.298 ms |  9.62 |    0.23 |     - |     - |     - |         - |
| Pooled_String | Core |    Core | 10000 | 258.825 ms | 5.5939 ms | 6.6591 ms | 256.322 ms |  9.53 |    0.32 |     - |     - |     - |         - |
