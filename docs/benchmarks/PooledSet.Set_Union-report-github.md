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
|              Method |  Job | Runtime | CountToUnion | InitialSetSize |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |----- |-------- |------------- |--------------- |------------:|-----------:|-----------:|------------:|------:|--------:|------:|------:|------:|----------:|
|     **HashSet_Hashset** |  **Clr** |     **Clr** |        **32000** |         **320000** |  **1,237.5 us** |  **27.085 us** |  **75.502 us** |  **1,224.2 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet_PooledSet |  Clr |     Clr |        32000 |         320000 |    957.3 us |  19.075 us |  38.094 us |    945.2 us |  0.78 |    0.05 |     - |     - |     - |    8192 B |
|        HashSet_Enum |  Clr |     Clr |        32000 |         320000 |  1,322.2 us |  29.379 us |  82.381 us |  1,310.8 us |  1.07 |    0.09 |     - |     - |     - |    8192 B |
|      PooledSet_Enum |  Clr |     Clr |        32000 |         320000 |  1,067.3 us |  21.333 us |  29.201 us |  1,054.7 us |  0.86 |    0.05 |     - |     - |     - |    8192 B |
|       HashSet_Array |  Clr |     Clr |        32000 |         320000 |  1,297.6 us |  28.599 us |  81.596 us |  1,276.8 us |  1.05 |    0.08 |     - |     - |     - |    8192 B |
|     PooledSet_Array |  Clr |     Clr |        32000 |         320000 |    837.5 us |  15.988 us |  17.771 us |    834.0 us |  0.68 |    0.03 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |        32000 |         320000 |  1,228.0 us |  30.859 us |  87.039 us |  1,219.5 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |        32000 |         320000 |    973.9 us |  20.391 us |  49.246 us |    956.4 us |  0.79 |    0.07 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |        32000 |         320000 |  1,342.7 us |  42.979 us | 124.691 us |  1,314.0 us |  1.10 |    0.13 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |        32000 |         320000 |  1,049.8 us |  20.778 us |  43.371 us |  1,036.7 us |  0.84 |    0.07 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |        32000 |         320000 |  1,238.0 us |  29.602 us |  82.026 us |  1,228.7 us |  1.01 |    0.10 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |        32000 |         320000 |    847.7 us |  21.194 us |  61.150 us |    814.5 us |  0.69 |    0.07 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     **HashSet_Hashset** |  **Clr** |     **Clr** |        **32000** |        **8000000** |  **1,375.1 us** |  **28.629 us** |  **70.764 us** |  **1,355.5 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet_PooledSet |  Clr |     Clr |        32000 |        8000000 |    898.6 us |   9.404 us |   7.342 us |    899.2 us |  0.67 |    0.02 |     - |     - |     - |    8192 B |
|        HashSet_Enum |  Clr |     Clr |        32000 |        8000000 |  1,483.0 us |  29.443 us |  65.243 us |  1,464.4 us |  1.08 |    0.08 |     - |     - |     - |    8192 B |
|      PooledSet_Enum |  Clr |     Clr |        32000 |        8000000 |  1,022.1 us |  20.851 us |  46.205 us |    997.7 us |  0.75 |    0.05 |     - |     - |     - |    8192 B |
|       HashSet_Array |  Clr |     Clr |        32000 |        8000000 |  1,423.3 us |  28.285 us |  59.662 us |  1,403.3 us |  1.04 |    0.06 |     - |     - |     - |    8192 B |
|     PooledSet_Array |  Clr |     Clr |        32000 |        8000000 |    809.6 us |  16.811 us |  48.502 us |    780.6 us |  0.59 |    0.05 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |        32000 |        8000000 |  1,391.9 us |  28.350 us |  76.160 us |  1,367.5 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |        32000 |        8000000 |    937.7 us |  21.024 us |  30.153 us |    932.5 us |  0.65 |    0.04 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |        32000 |        8000000 |  1,479.6 us |  29.258 us |  52.006 us |  1,467.2 us |  1.05 |    0.07 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |        32000 |        8000000 |  1,046.1 us |  20.690 us |  49.172 us |  1,033.9 us |  0.75 |    0.05 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |        32000 |        8000000 |  1,454.2 us |  28.977 us |  70.534 us |  1,435.9 us |  1.05 |    0.07 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |        32000 |        8000000 |    822.1 us |  16.294 us |  38.723 us |    818.5 us |  0.59 |    0.04 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     **HashSet_Hashset** |  **Clr** |     **Clr** |       **320000** |         **320000** |  **3,826.6 us** |  **75.269 us** | **168.350 us** |  **3,791.3 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet_PooledSet |  Clr |     Clr |       320000 |         320000 |  3,202.2 us |  63.074 us |  79.768 us |  3,202.9 us |  0.84 |    0.04 |     - |     - |     - |    8192 B |
|        HashSet_Enum |  Clr |     Clr |       320000 |         320000 | 11,438.4 us | 223.931 us | 275.007 us | 11,405.1 us |  3.00 |    0.14 |     - |     - |     - |    8192 B |
|      PooledSet_Enum |  Clr |     Clr |       320000 |         320000 | 10,577.3 us | 208.229 us | 222.803 us | 10,608.6 us |  2.79 |    0.11 |     - |     - |     - |    8192 B |
|       HashSet_Array |  Clr |     Clr |       320000 |         320000 | 11,371.7 us | 164.041 us | 153.444 us | 11,381.7 us |  3.02 |    0.11 |     - |     - |     - |    8192 B |
|     PooledSet_Array |  Clr |     Clr |       320000 |         320000 |  8,110.6 us | 125.598 us | 104.880 us |  8,080.1 us |  2.14 |    0.07 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |       320000 |         320000 |  3,593.3 us |  71.069 us | 160.414 us |  3,553.9 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |       320000 |         320000 |  3,148.2 us |  62.937 us | 113.489 us |  3,123.5 us |  0.87 |    0.05 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |       320000 |         320000 | 11,389.9 us | 224.012 us | 220.009 us | 11,347.9 us |  3.08 |    0.16 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |       320000 |         320000 | 10,433.9 us | 123.795 us | 115.798 us | 10,444.9 us |  2.81 |    0.10 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |       320000 |         320000 | 10,755.1 us | 238.080 us | 447.172 us | 10,637.4 us |  2.98 |    0.15 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |       320000 |         320000 |  7,864.5 us | 185.210 us | 259.638 us |  7,842.8 us |  2.17 |    0.12 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     **HashSet_Hashset** |  **Clr** |     **Clr** |       **320000** |        **8000000** |  **3,755.6 us** |  **72.747 us** |  **94.592 us** |  **3,744.3 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |    **8192 B** |
| PooledSet_PooledSet |  Clr |     Clr |       320000 |        8000000 |  3,198.7 us |  72.274 us | 105.938 us |  3,185.6 us |  0.85 |    0.03 |     - |     - |     - |    8192 B |
|        HashSet_Enum |  Clr |     Clr |       320000 |        8000000 | 11,295.7 us | 219.119 us | 194.243 us | 11,273.5 us |  3.00 |    0.11 |     - |     - |     - |    8192 B |
|      PooledSet_Enum |  Clr |     Clr |       320000 |        8000000 | 10,191.4 us | 144.571 us | 135.232 us | 10,194.6 us |  2.71 |    0.08 |     - |     - |     - |    8192 B |
|       HashSet_Array |  Clr |     Clr |       320000 |        8000000 | 10,923.2 us | 196.776 us | 164.317 us | 10,937.3 us |  2.90 |    0.09 |     - |     - |     - |    8192 B |
|     PooledSet_Array |  Clr |     Clr |       320000 |        8000000 |  8,012.6 us | 155.086 us | 236.833 us |  7,929.1 us |  2.14 |    0.09 |     - |     - |     - |         - |
|                     |      |         |              |                |             |            |            |             |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |       320000 |        8000000 |  3,675.1 us |  72.141 us | 122.502 us |  3,662.3 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |       320000 |        8000000 |  3,084.0 us |  57.201 us |  50.707 us |  3,084.3 us |  0.84 |    0.03 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |       320000 |        8000000 | 10,992.0 us | 234.546 us | 219.395 us | 10,968.1 us |  2.98 |    0.12 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |       320000 |        8000000 |  9,871.5 us | 172.189 us | 161.066 us |  9,895.2 us |  2.67 |    0.10 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |       320000 |        8000000 | 10,401.7 us | 215.530 us | 256.574 us | 10,344.5 us |  2.82 |    0.09 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |       320000 |        8000000 |  7,623.9 us | 151.909 us | 222.666 us |  7,590.6 us |  2.08 |    0.11 |     - |     - |     - |         - |
