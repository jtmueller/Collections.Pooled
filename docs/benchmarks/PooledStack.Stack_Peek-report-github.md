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
|     Method |  Job | Runtime |       N |   Type |         Mean |      Error |     StdDev |       Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------- |----- |-------- |-------- |------- |-------------:|-----------:|-----------:|-------------:|------:|--------:|------:|------:|------:|----------:|
|  **StackPeek** |  **Clr** |     **Clr** |   **10000** |    **Int** |    **11.673 us** |  **0.1828 us** |  **0.1710 us** |    **11.600 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr |   10000 |    Int |     8.129 us |  0.1206 us |  0.1069 us |     8.200 us |  0.70 |    0.01 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  StackPeek | Core |    Core |   10000 |    Int |     7.931 us |  0.0899 us |  0.0751 us |     7.900 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core |   10000 |    Int |     8.050 us |  0.1641 us |  0.1454 us |     8.000 us |  1.01 |    0.02 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** |   **10000** | **String** |     **9.676 us** |  **0.1871 us** |  **0.1921 us** |     **9.700 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr |   10000 | String |     7.200 us |  0.0000 us |  0.0000 us |     7.200 us |  0.75 |    0.01 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  StackPeek | Core |    Core |   10000 | String |     7.414 us |  0.0869 us |  0.0770 us |     7.400 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core |   10000 | String |     7.294 us |  0.1462 us |  0.1436 us |     7.300 us |  0.98 |    0.02 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** |  **100000** |    **Int** |   **112.754 us** |  **2.1592 us** |  **1.8031 us** |   **114.300 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr |  100000 |    Int |    80.756 us |  1.6066 us |  3.9411 us |    78.500 us |  0.73 |    0.05 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  StackPeek | Core |    Core |  100000 |    Int |    94.483 us |  5.9344 us | 17.4047 us |    81.100 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core |  100000 |    Int |    87.768 us |  4.1526 us | 11.8476 us |    81.000 us |  0.96 |    0.22 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** |  **100000** | **String** |    **96.142 us** |  **2.1026 us** |  **5.7912 us** |    **94.700 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr |  100000 | String |    70.800 us |  1.3811 us |  1.1533 us |    71.000 us |  0.72 |    0.05 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  StackPeek | Core |    Core |  100000 | String |    98.013 us |  8.8720 us | 26.0200 us |   105.900 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core |  100000 | String |    99.428 us |  8.6855 us | 25.3361 us |   107.000 us |  1.07 |    0.33 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** | **1000000** |    **Int** | **1,193.785 us** | **31.1941 us** | **88.9985 us** | **1,151.000 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr | 1000000 |    Int |   809.904 us | 20.0063 us | 57.0790 us |   798.150 us |  0.68 |    0.07 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  StackPeek | Core |    Core | 1000000 |    Int |   815.222 us | 16.2038 us | 37.5549 us |   808.100 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core | 1000000 |    Int |   836.969 us | 17.9731 us | 52.4283 us |   830.200 us |  1.03 |    0.08 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** | **1000000** | **String** |   **954.340 us** | **19.0639 us** | **43.8025 us** |   **935.600 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr | 1000000 | String |   704.098 us | 13.6400 us | 26.2796 us |   707.000 us |  0.74 |    0.04 |     - |     - |     - |         - |
|            |      |         |         |        |              |            |            |              |       |         |       |       |       |           |
|  StackPeek | Core |    Core | 1000000 | String |   734.510 us | 15.6761 us | 38.7474 us |   717.850 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core | 1000000 | String |   748.663 us | 14.8993 us | 42.9880 us |   726.200 us |  1.03 |    0.08 |     - |     - |     - |         - |
