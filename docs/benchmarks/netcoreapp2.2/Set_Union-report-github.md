``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                    Method | CountToUnion | InitialSetSize |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------------- |--------------- |------------:|----------:|----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Union_Hashset** |        **32000** |         **320000** |  **1,378.9 us** |  **36.79 us** | **102.56 us** |  **1,358.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |        32000 |         320000 |  1,049.7 us |  48.33 us | 134.72 us |    985.0 us |  0.77 |    0.11 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |        32000 |         320000 |  1,542.7 us |  48.06 us | 131.56 us |  1,517.4 us |  1.12 |    0.13 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |        32000 |         320000 |  1,190.6 us |  34.67 us |  94.32 us |  1,165.6 us |  0.87 |    0.11 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |        32000 |         320000 |  1,497.0 us |  42.07 us | 114.46 us |  1,468.2 us |  1.09 |    0.10 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |        32000 |         320000 |    906.2 us |  38.47 us | 109.13 us |    874.7 us |  0.66 |    0.09 |           - |           - |           - |                   - |
|                           |              |                |             |           |           |             |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |        **32000** |        **8000000** |  **1,859.4 us** | **130.88 us** | **385.91 us** |  **1,963.5 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |        32000 |        8000000 |  1,382.7 us | 118.34 us | 347.08 us |  1,516.2 us |  0.75 |    0.13 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |        32000 |        8000000 |  2,009.5 us | 134.94 us | 397.87 us |  2,130.3 us |  1.09 |    0.16 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |        32000 |        8000000 |  1,488.0 us | 117.48 us | 346.40 us |  1,631.6 us |  0.81 |    0.15 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |        32000 |        8000000 |  1,962.3 us | 124.37 us | 366.71 us |  2,047.4 us |  1.07 |    0.18 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |        32000 |        8000000 |  1,088.2 us |  88.92 us | 262.18 us |  1,172.1 us |  0.59 |    0.12 |           - |           - |           - |                   - |
|                           |              |                |             |           |           |             |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |       **320000** |         **320000** |  **4,191.3 us** | **122.14 us** | **356.28 us** |  **4,064.2 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |       320000 |         320000 |  3,744.4 us | 127.11 us | 372.80 us |  3,573.8 us |  0.90 |    0.12 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |       320000 |         320000 | 12,655.7 us | 251.75 us | 453.95 us | 12,616.0 us |  3.02 |    0.25 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |       320000 |         320000 | 11,643.1 us | 239.49 us | 510.36 us | 11,523.0 us |  2.82 |    0.25 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |       320000 |         320000 | 12,331.2 us | 246.28 us | 486.13 us | 12,261.9 us |  2.97 |    0.25 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |       320000 |         320000 |  9,077.0 us | 206.94 us | 576.86 us |  8,978.8 us |  2.20 |    0.22 |           - |           - |           - |                   - |
|                           |              |                |             |           |           |             |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |       **320000** |        **8000000** |  **4,580.4 us** | **164.85 us** | **486.07 us** |  **4,522.7 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_PooledSet |       320000 |        8000000 |  3,934.3 us | 135.02 us | 393.87 us |  3,933.1 us |  0.86 |    0.09 |           - |           - |           - |                40 B |
|        HashSet_Union_Enum |       320000 |        8000000 | 12,224.3 us | 260.24 us | 420.24 us | 12,285.5 us |  2.86 |    0.26 |           - |           - |           - |                40 B |
|      PooledSet_Union_Enum |       320000 |        8000000 | 11,171.9 us | 222.26 us | 473.66 us | 10,988.3 us |  2.49 |    0.28 |           - |           - |           - |                40 B |
|       HashSet_Union_Array |       320000 |        8000000 | 12,324.7 us | 243.44 us | 341.28 us | 12,391.1 us |  2.91 |    0.23 |           - |           - |           - |                32 B |
|     PooledSet_Union_Array |       320000 |        8000000 |  8,378.8 us | 166.93 us | 406.34 us |  8,274.5 us |  1.83 |    0.20 |           - |           - |           - |                   - |
