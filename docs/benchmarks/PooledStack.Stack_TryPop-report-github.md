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
|       Method |  Job | Runtime |       N |   Type |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------- |----- |-------- |-------- |------- |------------:|------------:|------------:|------------:|------:|--------:|------:|------:|------:|----------:|
|  **StackTryPop** |  **Clr** |     **Clr** |   **10000** |    **Int** |    **26.22 us** |   **0.5151 us** |   **0.5512 us** |    **26.30 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPop |  Clr |     Clr |   10000 |    Int |    25.89 us |   0.4342 us |   0.4061 us |    25.60 us |  0.99 |    0.02 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  StackTryPop | Core |    Core |   10000 |    Int |    57.14 us |   5.5402 us |  16.3354 us |    46.30 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPop | Core |    Core |   10000 |    Int |    30.83 us |   0.7951 us |   1.3064 us |    30.50 us |  0.56 |    0.12 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  **StackTryPop** |  **Clr** |     **Clr** |   **10000** | **String** |    **25.30 us** |   **0.1545 us** |   **0.1206 us** |    **25.30 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPop |  Clr |     Clr |   10000 | String |    40.23 us |   0.4971 us |   0.4151 us |    40.10 us |  1.59 |    0.01 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  StackTryPop | Core |    Core |   10000 | String |    79.78 us |  13.3863 us |  39.0484 us |    90.95 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPop | Core |    Core |   10000 | String |    51.42 us |   4.6574 us |  13.7323 us |    43.10 us |  0.87 |    0.57 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  **StackTryPop** |  **Clr** |     **Clr** |  **100000** |    **Int** |   **267.78 us** |   **6.9074 us** |  **19.2550 us** |   **262.65 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPop |  Clr |     Clr |  100000 |    Int |   260.92 us |   5.0961 us |   4.5175 us |   260.35 us |  0.98 |    0.06 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  StackTryPop | Core |    Core |  100000 |    Int |   446.56 us |   6.9071 us |   6.1229 us |   445.80 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPop | Core |    Core |  100000 |    Int |   309.86 us |   7.5522 us |  20.9272 us |   302.70 us |  0.68 |    0.03 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  **StackTryPop** |  **Clr** |     **Clr** |  **100000** | **String** |   **274.02 us** |   **6.6720 us** |  **16.6155 us** |   **266.90 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPop |  Clr |     Clr |  100000 | String |   423.21 us |   9.4087 us |   9.6620 us |   422.50 us |  1.51 |    0.13 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  StackTryPop | Core |    Core |  100000 | String |   494.96 us |  17.7229 us |  50.8502 us |   484.90 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPop | Core |    Core |  100000 | String |   477.38 us |  17.6462 us |  51.4749 us |   469.95 us |  0.97 |    0.14 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  **StackTryPop** |  **Clr** |     **Clr** | **1000000** |    **Int** | **2,680.51 us** |  **53.3346 us** | **148.6756 us** | **2,650.20 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPop |  Clr |     Clr | 1000000 |    Int | 2,596.01 us |  50.4462 us |  53.9768 us | 2,581.70 us |  0.98 |    0.07 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  StackTryPop | Core |    Core | 1000000 |    Int | 2,305.76 us |  97.9808 us | 258.1205 us | 2,241.60 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPop | Core |    Core | 1000000 |    Int | 2,829.19 us |  56.0406 us | 141.6217 us | 2,811.70 us |  1.24 |    0.14 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  **StackTryPop** |  **Clr** |     **Clr** | **1000000** | **String** | **2,648.29 us** |  **52.6662 us** |  **73.8307 us** | **2,642.70 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPop |  Clr |     Clr | 1000000 | String | 4,022.77 us |  96.9296 us | 111.6243 us | 3,987.05 us |  1.51 |    0.07 |     - |     - |     - |         - |
|              |      |         |         |        |             |             |             |             |       |         |       |       |       |           |
|  StackTryPop | Core |    Core | 1000000 | String | 3,996.43 us |  77.2032 us |  85.8112 us | 3,989.50 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPop | Core |    Core | 1000000 | String | 4,177.63 us | 112.5015 us |  93.9438 us | 4,143.05 us |  1.05 |    0.04 |     - |     - |     - |         - |
