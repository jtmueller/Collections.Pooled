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
|        Method |  Job | Runtime |       N |   Type | EmptyQueue |         Mean |      Error |      StdDev |       Median |          P95 | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |-------- |------- |----------- |-------------:|-----------:|------------:|-------------:|-------------:|------:|--------:|------:|------:|------:|----------:|
|  **QueueTryPeek** |  **Clr** |     **Clr** |   **10000** |    **Int** |      **False** |    **14.564 us** |  **0.4907 us** |   **1.3841 us** |    **15.300 us** |    **15.800 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 |    Int |      False |     7.967 us |  0.1256 us |   0.1175 us |     7.900 us |     8.130 us |  0.57 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |   10000 |    Int |      False |     8.056 us |  0.1576 us |   0.1548 us |     8.050 us |     8.225 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 |    Int |      False |     8.071 us |  0.1621 us |   0.1437 us |     8.150 us |     8.200 us |  1.00 |    0.02 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |   **10000** |    **Int** |       **True** |     **6.471 us** |  **0.1277 us** |   **0.1312 us** |     **6.500 us** |     **6.600 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 |    Int |       True |     9.567 us |  0.1839 us |   0.1435 us |     9.500 us |     9.800 us |  1.48 |    0.05 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |   10000 |    Int |       True |    11.664 us |  0.2282 us |   0.2023 us |    11.550 us |    11.935 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 |    Int |       True |    11.184 us |  0.3341 us |   0.9423 us |    11.550 us |    12.000 us |  0.96 |    0.08 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |   **10000** | **String** |      **False** |    **11.431 us** |  **0.0755 us** |   **0.0630 us** |    **11.400 us** |    **11.500 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 | String |      False |     6.467 us |  0.1322 us |   0.1414 us |     6.450 us |     6.700 us |  0.57 |    0.01 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |   10000 | String |      False |     6.238 us |  0.1258 us |   0.1722 us |     6.200 us |     6.475 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 | String |      False |     6.379 us |  0.0480 us |   0.0426 us |     6.400 us |     6.400 us |  1.03 |    0.02 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |   **10000** | **String** |       **True** |     **6.329 us** |  **0.1285 us** |   **0.1139 us** |     **6.350 us** |     **6.500 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |   10000 | String |       True |     6.164 us |  0.0561 us |   0.0497 us |     6.200 us |     6.200 us |  0.97 |    0.02 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |   10000 | String |       True |     6.342 us |  0.1277 us |   0.1748 us |     6.400 us |     6.675 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |   10000 | String |       True |     6.286 us |  0.1159 us |   0.1027 us |     6.300 us |     6.400 us |  0.99 |    0.04 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |  **100000** |    **Int** |      **False** |   **144.017 us** |  **5.5361 us** |  **15.6147 us** |   **151.900 us** |   **161.280 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 |    Int |      False |    76.023 us |  0.0717 us |   0.0599 us |    76.000 us |    76.100 us |  0.51 |    0.05 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |  100000 |    Int |      False |    78.408 us |  0.0370 us |   0.0289 us |    78.400 us |    78.445 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 |    Int |      False |    80.306 us |  1.4971 us |   1.4704 us |    80.900 us |    82.000 us |  1.02 |    0.02 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |  **100000** |    **Int** |       **True** |    **62.736 us** |  **1.2211 us** |   **1.0825 us** |    **62.700 us** |    **64.700 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 |    Int |       True |    97.041 us |  1.8945 us |   2.3266 us |    97.100 us |   100.965 us |  1.54 |    0.05 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |  100000 |    Int |       True |    63.852 us |  1.4326 us |   1.8118 us |    62.800 us |    64.800 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 |    Int |       True |   118.553 us |  2.4348 us |   5.4458 us |   118.600 us |   125.715 us |  1.83 |    0.14 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |  **100000** | **String** |      **False** |   **112.767 us** |  **2.1769 us** |   **4.1942 us** |   **113.400 us** |   **119.100 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 | String |      False |    60.800 us |  1.2351 us |   1.2684 us |    61.100 us |    61.880 us |  0.54 |    0.02 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |  100000 | String |      False |    85.437 us |  8.5912 us |  25.0608 us |    93.250 us |   124.365 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 | String |      False |    85.654 us |  8.5952 us |  25.2082 us |    81.600 us |   127.760 us |  1.05 |    0.33 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** |  **100000** | **String** |       **True** |    **60.879 us** |  **1.0359 us** |   **0.9183 us** |    **61.100 us** |    **61.235 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  100000 | String |       True |    63.886 us |  2.6403 us |   2.3406 us |    62.900 us |    67.240 us |  1.05 |    0.04 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core |  100000 | String |       True |    62.425 us |  2.3359 us |   5.0283 us |    60.800 us |    78.075 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  100000 | String |       True |    62.272 us |  1.2635 us |   3.1232 us |    61.000 us |    66.500 us |  1.00 |    0.08 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** | **1000000** |    **Int** |      **False** | **1,444.331 us** | **41.1771 us** | **120.1156 us** | **1,428.600 us** | **1,659.515 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 |    Int |      False |   796.845 us | 18.4939 us |  53.3592 us |   774.300 us |   890.225 us |  0.55 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core | 1000000 |    Int |      False |   839.024 us | 19.2434 us |  56.1339 us |   833.200 us |   952.425 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 |    Int |      False |   816.471 us | 17.0284 us |  44.5603 us |   801.500 us |   893.350 us |  0.98 |    0.09 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** | **1000000** |    **Int** |       **True** |   **651.411 us** | **14.6228 us** |  **41.4826 us** |   **631.000 us** |   **743.760 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 |    Int |       True |   976.795 us | 22.8503 us |  65.1933 us |   947.900 us | 1,116.150 us |  1.50 |    0.12 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core | 1000000 |    Int |       True | 1,199.581 us | 25.7312 us |  73.4125 us | 1,185.300 us | 1,348.010 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 |    Int |       True |   648.480 us | 14.2683 us |  29.1463 us |   633.700 us |   707.600 us |  0.55 |    0.05 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** | **1000000** | **String** |      **False** | **1,145.670 us** | **22.6933 us** |  **50.7568 us** | **1,114.450 us** | **1,255.050 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 | String |      False |   613.911 us | 13.3663 us |  38.5648 us |   611.650 us |   684.200 us |  0.53 |    0.04 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core | 1000000 | String |      False |   626.203 us | 16.2280 us |  25.2650 us |   613.200 us |   682.235 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 | String |      False |   634.259 us | 12.6893 us |  31.1270 us |   624.000 us |   704.700 us |  1.03 |    0.06 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  **QueueTryPeek** |  **Clr** |     **Clr** | **1000000** | **String** |       **True** |   **615.955 us** | **19.5417 us** |  **57.0041 us** |   **577.050 us** |   **738.375 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 1000000 | String |       True |   579.900 us | 14.3295 us |  17.0582 us |   573.400 us |   628.300 us |  0.93 |    0.09 |     - |     - |     - |         - |
|               |      |         |         |        |            |              |            |             |              |              |       |         |       |       |       |           |
|  QueueTryPeek | Core |    Core | 1000000 | String |       True | 1,106.753 us | 27.7904 us |  80.6249 us | 1,092.100 us | 1,238.940 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 1000000 | String |       True | 1,086.021 us | 26.6546 us |  76.9045 us | 1,061.450 us | 1,224.650 us |  0.99 |    0.10 |     - |     - |     - |         - |
