``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      N |   Type |           Mean |         Error |        StdDev | Ratio | RatioSD |       Gen 0 |       Gen 1 |       Gen 2 |     Allocated |
|------------------- |----- |-------- |------- |------- |---------------:|--------------:|--------------:|------:|--------:|------------:|------------:|------------:|--------------:|
|  **Queue_ICollection** |  **Clr** |     **Clr** |   **1000** |    **Int** |    **12,300.6 us** |     **98.493 us** |     **87.312 us** |  **1.00** |    **0.00** |   **2687.5000** |           **-** |           **-** |    **8306.93 KB** |
| Pooled_ICollection |  Clr |     Clr |   1000 |    Int |     1,074.0 us |      9.587 us |      8.968 us |  0.09 |    0.00 |     19.5313 |           - |           - |      62.69 KB |
|  Queue_IEnumerable |  Clr |     Clr |   1000 |    Int |    11,293.1 us |    114.304 us |    106.920 us |  0.92 |    0.01 |   2687.5000 |           - |           - |    8306.93 KB |
| Pooled_IEnumerable |  Clr |     Clr |   1000 |    Int |    10,064.8 us |    193.018 us |    189.569 us |  0.82 |    0.02 |     46.8750 |           - |           - |     156.75 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  Queue_ICollection | Core |    Core |   1000 |    Int |       284.1 us |      5.457 us |      5.104 us |  1.00 |    0.00 |   1294.9219 |           - |           - |    3968.75 KB |
| Pooled_ICollection | Core |    Core |   1000 |    Int |       143.8 us |      2.811 us |      3.655 us |  0.51 |    0.02 |     20.2637 |           - |           - |       62.5 KB |
|  Queue_IEnumerable | Core |    Core |   1000 |    Int |     7,409.9 us |    113.518 us |    106.185 us | 26.09 |    0.61 |   2695.3125 |           - |           - |    8273.44 KB |
| Pooled_IEnumerable | Core |    Core |   1000 |    Int |     7,056.7 us |    136.621 us |    127.795 us | 24.84 |    0.59 |     46.8750 |           - |           - |     156.25 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  **Queue_ICollection** |  **Clr** |     **Clr** |   **1000** | **String** |    **16,558.7 us** |    **325.096 us** |    **319.287 us** |  **1.00** |    **0.00** |   **5281.2500** |           **-** |           **-** |   **16320.72 KB** |
| Pooled_ICollection |  Clr |     Clr |   1000 | String |     2,187.8 us |     43.258 us |     42.485 us |  0.13 |    0.00 |     19.5313 |           - |           - |      62.69 KB |
|  Queue_IEnumerable |  Clr |     Clr |   1000 | String |    14,218.6 us |    238.048 us |    222.670 us |  0.86 |    0.02 |   5296.8750 |           - |           - |    16334.4 KB |
| Pooled_IEnumerable |  Clr |     Clr |   1000 | String |    20,197.1 us |    342.805 us |    320.660 us |  1.22 |    0.04 |     31.2500 |           - |           - |     164.75 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  Queue_ICollection | Core |    Core |   1000 | String |       715.1 us |     14.189 us |     17.425 us |  1.00 |    0.00 |   2570.3125 |           - |           - |       7875 KB |
| Pooled_ICollection | Core |    Core |   1000 | String |       604.5 us |     10.801 us |     10.103 us |  0.85 |    0.03 |     19.5313 |           - |           - |       62.5 KB |
|  Queue_IEnumerable | Core |    Core |   1000 | String |    12,231.0 us |    134.757 us |    126.052 us | 17.18 |    0.42 |   5296.8750 |           - |           - |   16265.63 KB |
| Pooled_IEnumerable | Core |    Core |   1000 | String |    13,708.3 us |    230.223 us |    215.351 us | 19.26 |    0.60 |     46.8750 |           - |           - |     164.06 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  **Queue_ICollection** |  **Clr** |     **Clr** |  **10000** |    **Int** |   **122,576.1 us** |  **1,546.081 us** |  **1,446.205 us** |  **1.00** |    **0.00** |  **41600.0000** |           **-** |           **-** |   **128521.9 KB** |
| Pooled_ICollection |  Clr |     Clr |  10000 |    Int |    15,449.4 us |    265.111 us |    247.985 us |  0.13 |    0.00 |     15.6250 |           - |           - |      62.75 KB |
|  Queue_IEnumerable |  Clr |     Clr |  10000 |    Int |   108,778.9 us |  1,949.750 us |  1,823.797 us |  0.89 |    0.02 |  41600.0000 |           - |           - |   128521.9 KB |
| Pooled_IEnumerable |  Clr |     Clr |  10000 |    Int |   108,852.3 us |  1,667.115 us |  1,559.420 us |  0.89 |    0.01 |           - |           - |           - |      156.8 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  Queue_ICollection | Core |    Core |  10000 |    Int |     2,892.1 us |     48.581 us |     43.066 us |  1.00 |    0.00 |  12656.2500 |           - |           - |      39125 KB |
| Pooled_ICollection | Core |    Core |  10000 |    Int |     1,175.0 us |     19.415 us |     18.160 us |  0.41 |    0.01 |     19.5313 |           - |           - |       62.5 KB |
|  Queue_IEnumerable | Core |    Core |  10000 |    Int |    70,135.4 us |    858.401 us |    802.949 us | 24.25 |    0.57 |  41625.0000 |           - |           - |  128367.19 KB |
| Pooled_IEnumerable | Core |    Core |  10000 |    Int |    62,260.3 us |    619.748 us |    579.713 us | 21.51 |    0.36 |           - |           - |           - |     156.25 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  **Queue_ICollection** |  **Clr** |     **Clr** |  **10000** | **String** |   **229,113.9 us** |  **2,450.224 us** |  **2,291.942 us** |  **1.00** |    **0.00** |  **41333.3333** |  **41333.3333** |  **41333.3333** |  **256544.27 KB** |
| Pooled_ICollection |  Clr |     Clr |  10000 | String |    32,689.2 us |    436.659 us |    408.451 us |  0.14 |    0.00 |           - |           - |           - |         63 KB |
|  Queue_IEnumerable |  Clr |     Clr |  10000 | String |   207,814.8 us |  3,548.990 us |  3,319.727 us |  0.91 |    0.01 |  41333.3333 |  41333.3333 |  41333.3333 |  256544.27 KB |
| Pooled_IEnumerable |  Clr |     Clr |  10000 | String |   226,178.1 us |  1,910.402 us |  1,786.991 us |  0.99 |    0.01 |           - |           - |           - |     165.33 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  Queue_ICollection | Core |    Core |  10000 | String |     7,110.8 us |     86.468 us |     80.883 us |  1.00 |    0.00 |  24992.1875 |           - |           - |    78187.5 KB |
| Pooled_ICollection | Core |    Core |  10000 | String |     8,286.6 us |    165.648 us |    162.689 us |  1.16 |    0.02 |     15.6250 |           - |           - |       62.5 KB |
|  Queue_IEnumerable | Core |    Core |  10000 | String |   169,872.0 us |  3,207.603 us |  3,293.972 us | 23.87 |    0.53 |  41333.3333 |  41333.3333 |  41333.3333 |  256359.38 KB |
| Pooled_IEnumerable | Core |    Core |  10000 | String |   139,045.0 us |  2,761.330 us |  2,582.950 us | 19.56 |    0.44 |           - |           - |           - |     164.06 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  **Queue_ICollection** |  **Clr** |     **Clr** | **100000** |    **Int** | **1,280,781.0 us** |  **6,526.848 us** |  **6,105.218 us** |  **1.00** |    **0.00** | **207000.0000** | **167000.0000** | **165000.0000** | **1027758.86 KB** |
| Pooled_ICollection |  Clr |     Clr | 100000 |    Int |   124,897.5 us |  1,353.424 us |  1,265.994 us |  0.10 |    0.00 |           - |           - |           - |         64 KB |
|  Queue_IEnumerable |  Clr |     Clr | 100000 |    Int | 1,144,557.9 us |  3,255.949 us |  2,718.865 us |  0.89 |    0.00 | 199000.0000 | 159000.0000 | 157000.0000 | 1027039.57 KB |
| Pooled_IEnumerable |  Clr |     Clr | 100000 |    Int |   955,008.2 us |  4,469.525 us |  3,732.256 us |  0.75 |    0.01 |           - |           - |           - |        160 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  Queue_ICollection | Core |    Core | 100000 |    Int |    65,496.1 us |  1,049.399 us |    981.609 us |  1.00 |    0.00 |  23750.0000 |  23750.0000 |  23750.0000 |  390855.54 KB |
| Pooled_ICollection | Core |    Core | 100000 |    Int |    14,694.3 us |    290.058 us |    377.157 us |  0.23 |    0.01 |     15.6250 |           - |           - |       62.5 KB |
|  Queue_IEnumerable | Core |    Core | 100000 |    Int |   744,531.5 us |  8,530.815 us |  7,979.730 us | 11.37 |    0.17 | 192000.0000 | 153000.0000 | 150000.0000 | 1025323.98 KB |
| Pooled_IEnumerable | Core |    Core | 100000 |    Int |   636,483.9 us |  5,816.555 us |  5,440.810 us |  9.72 |    0.17 |           - |           - |           - |     156.25 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  **Queue_ICollection** |  **Clr** |     **Clr** | **100000** | **String** | **1,967,578.3 us** | **16,723.900 us** | **14,825.301 us** |  **1.00** |    **0.00** | **300000.0000** | **262000.0000** | **258000.0000** | **2051633.25 KB** |
| Pooled_ICollection |  Clr |     Clr | 100000 | String |   272,364.3 us |  3,174.249 us |  2,969.195 us |  0.14 |    0.00 |           - |           - |           - |         64 KB |
|  Queue_IEnumerable |  Clr |     Clr | 100000 | String | 1,748,578.7 us | 11,539.916 us | 10,794.445 us |  0.89 |    0.01 | 293000.0000 | 254000.0000 | 251000.0000 | 2051585.25 KB |
| Pooled_IEnumerable |  Clr |     Clr | 100000 | String | 2,086,378.5 us | 13,454.845 us | 11,927.369 us |  1.06 |    0.01 |           - |           - |           - |        168 KB |
|                    |      |         |        |        |                |               |               |       |         |             |             |             |               |
|  Queue_ICollection | Core |    Core | 100000 | String |   426,523.9 us |  8,462.458 us | 22,144.777 us |  1.00 |    0.00 |  31000.0000 |  31000.0000 |  31000.0000 |  781444.31 KB |
| Pooled_ICollection | Core |    Core | 100000 | String |    85,906.0 us |    975.721 us |    912.690 us |  0.21 |    0.02 |           - |           - |           - |       62.5 KB |
|  Queue_IEnumerable | Core |    Core | 100000 | String | 1,427,234.4 us | 10,566.744 us |  9,884.139 us |  3.50 |    0.35 | 271000.0000 | 232000.0000 | 228000.0000 | 2049905.63 KB |
| Pooled_IEnumerable | Core |    Core | 100000 | String | 1,413,649.1 us |  7,195.798 us |  6,730.954 us |  3.47 |    0.35 |           - |           - |           - |     164.06 KB |
