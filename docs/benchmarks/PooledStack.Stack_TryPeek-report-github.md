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
|        Method |  Job | Runtime |       N |   Type | EmptyStack |         Mean |      Error |      StdDev |       Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |-------- |------- |----------- |-------------:|-----------:|------------:|-------------:|------:|--------:|------:|------:|------:|----------:|
|  **StackTryPeek** |  **Clr** |     **Clr** |   **10000** |    **Int** |      **False** |    **16.356 us** |  **5.7860 us** |   **5.6826 us** |    **13.800 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 |    Int |      False |     7.279 us |  0.1479 us |   0.1311 us |     7.300 us |  0.47 |    0.12 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |   10000 |    Int |      False |     7.100 us |  0.0000 us |   0.0000 us |     7.100 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 |    Int |      False |     7.243 us |  0.1229 us |   0.1089 us |     7.200 us |  1.02 |    0.01 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |   **10000** |    **Int** |       **True** |     **6.493 us** |  **0.1028 us** |   **0.0961 us** |     **6.500 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 |    Int |       True |    11.457 us |  0.6148 us |   1.7641 us |    12.600 us |  1.71 |    0.25 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |   10000 |    Int |       True |    12.268 us |  0.4147 us |   1.1627 us |    12.600 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 |    Int |       True |     6.450 us |  0.1232 us |   0.1092 us |     6.450 us |  0.53 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |   **10000** | **String** |      **False** |    **14.129 us** |  **0.6455 us** |   **1.8416 us** |    **15.300 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 | String |      False |     6.271 us |  0.1279 us |   0.1953 us |     6.200 us |  0.47 |    0.07 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |   10000 | String |      False |     6.313 us |  0.1269 us |   0.1187 us |     6.300 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 | String |      False |     6.353 us |  0.1247 us |   0.1281 us |     6.400 us |  1.01 |    0.03 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |   **10000** | **String** |       **True** |     **6.340 us** |  **0.1128 us** |   **0.1056 us** |     **6.400 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 | String |       True |     6.186 us |  0.0410 us |   0.0363 us |     6.200 us |  0.98 |    0.02 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |   10000 | String |       True |    12.039 us |  0.4167 us |   1.1407 us |    12.600 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 | String |       True |    11.329 us |  0.2048 us |   0.1816 us |    11.200 us |  0.95 |    0.10 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |  **100000** |    **Int** |      **False** |   **138.682 us** |  **4.7152 us** |  **13.1440 us** |   **133.100 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 |    Int |      False |    70.675 us |  0.0796 us |   0.0622 us |    70.700 us |  0.51 |    0.05 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  100000 |    Int |      False |    72.000 us |  1.3236 us |   1.1053 us |    72.800 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 |    Int |      False |    72.355 us |  2.4701 us |   4.2609 us |    70.600 us |  1.03 |    0.09 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |  **100000** |    **Int** |       **True** |    **64.412 us** |  **1.4509 us** |   **1.4899 us** |    **64.700 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 |    Int |       True |   114.145 us |  6.3102 us |  18.2064 us |   121.800 us |  1.87 |    0.41 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  100000 |    Int |       True |    63.859 us |  2.7930 us |   2.8682 us |    62.700 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 |    Int |       True |   115.943 us |  5.8117 us |  17.0446 us |   125.300 us |  1.83 |    0.23 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |  **100000** | **String** |      **False** |   **142.985 us** |  **5.6334 us** |  **15.3260 us** |   **152.000 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 | String |      False |    60.600 us |  1.2000 us |   1.6823 us |    60.900 us |  0.43 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  100000 | String |      False |    88.180 us |  7.5597 us |  22.1713 us |    92.200 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 | String |      False |    88.580 us |  8.8683 us |  25.8693 us |    94.650 us |  1.06 |    0.39 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |  **100000** | **String** |       **True** |    **61.429 us** |  **1.5783 us** |   **2.0522 us** |    **61.000 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 | String |       True |    61.809 us |  1.2514 us |   1.5368 us |    61.000 us |  1.00 |    0.04 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  100000 | String |       True |   118.500 us |  6.6373 us |  19.0437 us |   121.700 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 | String |       True |   115.610 us |  5.8030 us |  16.0801 us |   121.600 us |  1.00 |    0.21 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **1000000** |    **Int** |      **False** | **1,415.582 us** | **42.0970 us** | **121.4595 us** | **1,381.350 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 |    Int |      False |   739.414 us | 18.6583 us |  52.3199 us |   722.800 us |  0.53 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 1000000 |    Int |      False |   739.085 us | 18.2876 us |  51.2804 us |   726.400 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 |    Int |      False |   733.626 us | 14.5024 us |  36.9132 us |   730.750 us |  1.00 |    0.08 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **1000000** |    **Int** |       **True** |   **658.336 us** | **16.5110 us** |  **46.0260 us** |   **639.150 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 |    Int |       True | 1,098.620 us | 49.6957 us | 145.7490 us | 1,069.700 us |  1.68 |    0.25 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 1000000 |    Int |       True | 1,134.831 us | 48.6202 us | 142.5947 us | 1,119.900 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 |    Int |       True |   672.528 us | 19.2752 us |  53.7314 us |   662.050 us |  0.60 |    0.09 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **1000000** | **String** |      **False** | **1,276.415 us** | **46.9055 us** | **137.5658 us** | **1,246.600 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 | String |      False |   619.554 us | 15.3550 us |  44.5477 us |   611.400 us |  0.49 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 1000000 | String |      False |   651.181 us | 13.3170 us |  37.1225 us |   644.750 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 | String |      False |   647.781 us | 13.6712 us |  39.8796 us |   642.400 us |  1.00 |    0.08 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **1000000** | **String** |       **True** |   **621.215 us** | **12.5582 us** |  **36.0318 us** |   **611.800 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 | String |       True |   604.913 us | 11.5224 us |  21.9225 us |   611.300 us |  0.98 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 1000000 | String |       True | 1,134.489 us | 22.6720 us |  53.4405 us | 1,125.600 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 | String |       True | 1,096.354 us | 24.0355 us |  69.7312 us | 1,081.800 us |  0.97 |    0.07 |     - |     - |     - |         - |
