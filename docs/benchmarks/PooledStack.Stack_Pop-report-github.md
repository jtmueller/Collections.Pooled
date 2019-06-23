``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|    Method |  Job | Runtime |      N |   Type |      Mean |       Error |     StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |------- |------- |----------:|------------:|-----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|  **StackPop** |  **Clr** |     **Clr** |  **10000** |    **Int** |  **31.37 us** |   **2.9797 us** |   **8.645 us** |  **26.70 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPop |  Clr |     Clr |  10000 |    Int |  27.52 us |   0.9311 us |   2.485 us |  26.80 us |  0.92 |    0.21 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  StackPop | Core |    Core |  10000 |    Int |  62.58 us |   3.9267 us |  11.203 us |  63.70 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPop | Core |    Core |  10000 |    Int |  82.77 us |   5.9922 us |  17.384 us |  78.35 us |  1.36 |    0.40 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  **StackPop** |  **Clr** |     **Clr** |  **10000** | **String** |  **34.53 us** |   **3.0266 us** |   **8.829 us** |  **29.40 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPop |  Clr |     Clr |  10000 | String |  34.18 us |   3.3460 us |   9.546 us |  28.25 us |  1.03 |    0.33 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  StackPop | Core |    Core |  10000 | String | 140.53 us |  11.8858 us |  34.293 us | 135.80 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPop | Core |    Core |  10000 | String | 197.18 us |  10.7948 us |  31.145 us | 191.35 us |  1.48 |    0.40 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  **StackPop** |  **Clr** |     **Clr** | **100000** |    **Int** | **284.73 us** |  **11.4605 us** |  **33.249 us** | **276.00 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPop |  Clr |     Clr | 100000 |    Int | 283.12 us |   9.9417 us |  27.713 us | 277.20 us |  1.00 |    0.14 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  StackPop | Core |    Core | 100000 |    Int | 579.51 us |  28.0331 us |  81.774 us | 546.50 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPop | Core |    Core | 100000 |    Int | 688.40 us |  21.4676 us |  61.595 us | 667.20 us |  1.21 |    0.19 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  **StackPop** |  **Clr** |     **Clr** | **100000** | **String** | **307.50 us** |  **15.5999 us** |  **44.000 us** | **290.95 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPop |  Clr |     Clr | 100000 | String | 307.17 us |  14.5055 us |  40.436 us | 295.20 us |  1.02 |    0.19 |     - |     - |     - |         - |
|           |      |         |        |        |           |             |            |           |       |         |       |       |       |           |
|  StackPop | Core |    Core | 100000 | String | 734.27 us | 160.1542 us | 472.218 us | 440.40 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPop | Core |    Core | 100000 | String | 321.27 us |  18.3271 us |  48.601 us | 313.10 us |  0.57 |    0.34 |     - |     - |     - |         - |
