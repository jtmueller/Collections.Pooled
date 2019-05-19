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
|             Method |  Job | Runtime |      N |      Mean |      Error |     StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |------- |----------:|-----------:|-----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|           **List_Int** |  **Clr** |     **Clr** | **100000** |  **42.43 us** |  **0.8452 us** |  **1.1283 us** |  **42.30 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|         Pooled_Int |  Clr |     Clr | 100000 |  44.60 us |  0.8575 us |  1.0531 us |  44.80 us |  1.05 |    0.04 |     - |     - |     - |         - |
|    Pooled_Span_Int |  Clr |     Clr | 100000 |  51.60 us |  1.0330 us |  2.5533 us |  50.55 us |  1.22 |    0.07 |     - |     - |     - |         - |
|        List_String |  Clr |     Clr | 100000 | 280.25 us |  6.6540 us | 18.5486 us | 276.70 us |  6.62 |    0.55 |     - |     - |     - |         - |
|      Pooled_String |  Clr |     Clr | 100000 | 298.65 us | 10.5567 us | 29.0761 us | 287.25 us |  7.30 |    0.73 |     - |     - |     - |         - |
| Pooled_Span_String |  Clr |     Clr | 100000 | 276.03 us |  7.1537 us | 19.9416 us | 268.20 us |  6.50 |    0.58 |     - |     - |     - |         - |
|                    |      |         |        |           |            |            |           |       |         |       |       |       |           |
|           List_Int | Core |    Core | 100000 |  45.99 us |  2.2555 us |  5.9023 us |  44.05 us |  1.00 |    0.00 |     - |     - |     - |         - |
|         Pooled_Int | Core |    Core | 100000 |  43.20 us |  0.8190 us |  0.6839 us |  43.25 us |  1.00 |    0.08 |     - |     - |     - |         - |
|    Pooled_Span_Int | Core |    Core | 100000 |  68.28 us |  8.0693 us | 23.6657 us |  70.60 us |  1.58 |    0.55 |     - |     - |     - |         - |
|        List_String | Core |    Core | 100000 | 308.72 us | 10.4961 us | 29.6044 us | 303.45 us |  6.77 |    0.95 |     - |     - |     - |         - |
|      Pooled_String | Core |    Core | 100000 | 334.44 us | 13.3327 us | 38.6807 us | 332.50 us |  7.33 |    1.07 |     - |     - |     - |         - |
| Pooled_Span_String | Core |    Core | 100000 | 295.86 us | 10.4905 us | 30.2674 us | 296.05 us |  6.57 |    0.98 |     - |     - |     - |         - |
|                    |      |         |        |           |            |            |           |       |         |       |       |       |           |
|           **List_Int** |  **Clr** |     **Clr** | **200000** |  **85.92 us** |  **1.8911 us** |  **4.6389 us** |  **85.20 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
|         Pooled_Int |  Clr |     Clr | 200000 |  83.83 us |  1.6745 us |  1.6446 us |  83.55 us |  0.96 |    0.07 |     - |     - |     - |         - |
|    Pooled_Span_Int |  Clr |     Clr | 200000 | 100.20 us |  2.5300 us |  6.8831 us |  99.65 us |  1.18 |    0.11 |     - |     - |     - |         - |
|        List_String |  Clr |     Clr | 200000 | 541.31 us | 10.7824 us | 17.4115 us | 542.30 us |  6.20 |    0.40 |     - |     - |     - |         - |
|      Pooled_String |  Clr |     Clr | 200000 | 558.86 us | 12.8589 us | 35.4172 us | 551.80 us |  6.48 |    0.54 |     - |     - |     - |         - |
| Pooled_Span_String |  Clr |     Clr | 200000 | 557.46 us | 18.2999 us | 52.7993 us | 538.75 us |  6.52 |    0.72 |     - |     - |     - |         - |
|                    |      |         |        |           |            |            |           |       |         |       |       |       |           |
|           List_Int | Core |    Core | 200000 |  87.94 us |  3.3086 us |  9.0012 us |  84.80 us |  1.00 |    0.00 |     - |     - |     - |         - |
|         Pooled_Int | Core |    Core | 200000 | 119.64 us |  9.5685 us | 28.2131 us | 118.80 us |  1.40 |    0.33 |     - |     - |     - |         - |
|    Pooled_Span_Int | Core |    Core | 200000 | 122.95 us | 10.1909 us | 30.0481 us | 126.25 us |  1.45 |    0.35 |     - |     - |     - |         - |
|        List_String | Core |    Core | 200000 | 532.16 us | 21.6287 us | 62.4037 us | 507.85 us |  6.13 |    0.95 |     - |     - |     - |         - |
|      Pooled_String | Core |    Core | 200000 | 568.57 us | 22.6036 us | 64.8541 us | 545.00 us |  6.53 |    0.92 |     - |     - |     - |         - |
| Pooled_Span_String | Core |    Core | 200000 | 595.55 us | 17.3794 us | 49.3023 us | 607.40 us |  6.83 |    0.90 |     - |     - |     - |         - |
