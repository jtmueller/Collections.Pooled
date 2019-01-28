``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|                    Method | CountToUnion | InitialSetSize |        Mean |     Error |      StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |------------- |--------------- |------------:|----------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Union_Hashset** |        **32000** |         **320000** |  **1,237.5 us** |  **44.87 us** |   **124.32 us** |  **1,234.8 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_PooledSet |        32000 |         320000 |    929.4 us |  20.78 us |    55.45 us |    898.5 us |  0.76 |    0.10 |           - |           - |           - |                   - |
|        HashSet_Union_Enum |        32000 |         320000 |  1,356.1 us |  40.63 us |   111.23 us |  1,347.5 us |  1.11 |    0.14 |           - |           - |           - |                   - |
|      PooledSet_Union_Enum |        32000 |         320000 |  1,073.7 us |  27.84 us |    76.68 us |  1,059.8 us |  0.88 |    0.12 |           - |           - |           - |                   - |
|       HashSet_Union_Array |        32000 |         320000 |  1,286.4 us |  35.66 us |    95.19 us |  1,297.0 us |  1.05 |    0.13 |           - |           - |           - |                   - |
|     PooledSet_Union_Array |        32000 |         320000 |    822.0 us |  19.20 us |    45.99 us |    795.3 us |  0.67 |    0.09 |           - |           - |           - |                   - |
|                           |              |                |             |           |             |             |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |        **32000** |        **8000000** |  **1,390.4 us** |  **27.74 us** |    **59.12 us** |  **1,353.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_PooledSet |        32000 |        8000000 |    926.0 us |  18.45 us |    40.88 us |    929.3 us |  0.67 |    0.04 |           - |           - |           - |                   - |
|        HashSet_Union_Enum |        32000 |        8000000 |  1,417.0 us |  28.23 us |    73.88 us |  1,377.6 us |  1.02 |    0.07 |           - |           - |           - |                   - |
|      PooledSet_Union_Enum |        32000 |        8000000 |  1,045.1 us |  28.33 us |    52.52 us |  1,025.7 us |  0.76 |    0.04 |           - |           - |           - |                   - |
|       HashSet_Union_Array |        32000 |        8000000 |  1,372.6 us |  27.42 us |    67.77 us |  1,328.3 us |  0.99 |    0.07 |           - |           - |           - |                   - |
|     PooledSet_Union_Array |        32000 |        8000000 |    816.0 us |  53.41 us |    63.58 us |    797.0 us |  0.59 |    0.05 |           - |           - |           - |                   - |
|                           |              |                |             |           |             |             |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |       **320000** |         **320000** |  **3,891.5 us** | **108.15 us** |   **315.49 us** |  **3,834.2 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_PooledSet |       320000 |         320000 |  3,465.7 us | 128.37 us |   376.50 us |  3,357.0 us |  0.90 |    0.12 |           - |           - |           - |                   - |
|        HashSet_Union_Enum |       320000 |         320000 | 11,744.7 us | 233.86 us |   427.63 us | 11,729.8 us |  3.07 |    0.20 |           - |           - |           - |                   - |
|      PooledSet_Union_Enum |       320000 |         320000 | 13,927.8 us | 796.32 us | 2,271.96 us | 13,802.7 us |  3.59 |    0.68 |           - |           - |           - |                   - |
|       HashSet_Union_Array |       320000 |         320000 | 11,489.6 us | 229.43 us |   419.53 us | 11,403.3 us |  3.00 |    0.22 |           - |           - |           - |                   - |
|     PooledSet_Union_Array |       320000 |         320000 |  8,835.7 us | 176.48 us |   405.49 us |  8,875.9 us |  2.31 |    0.22 |           - |           - |           - |                   - |
|                           |              |                |             |           |             |             |       |         |             |             |             |                     |
|     **HashSet_Union_Hashset** |       **320000** |        **8000000** |  **3,955.8 us** |  **97.01 us** |   **284.52 us** |  **3,863.2 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_PooledSet |       320000 |        8000000 |  3,330.1 us |  89.97 us |   265.27 us |  3,246.2 us |  0.85 |    0.10 |           - |           - |           - |                   - |
|        HashSet_Union_Enum |       320000 |        8000000 | 11,366.8 us | 226.44 us |   251.68 us | 11,435.8 us |  2.97 |    0.21 |           - |           - |           - |                   - |
|      PooledSet_Union_Enum |       320000 |        8000000 | 10,414.5 us | 204.99 us |   331.03 us | 10,354.9 us |  2.69 |    0.21 |           - |           - |           - |                   - |
|       HashSet_Union_Array |       320000 |        8000000 | 10,855.3 us | 210.26 us |   250.30 us | 10,835.3 us |  2.84 |    0.20 |           - |           - |           - |                   - |
|     PooledSet_Union_Array |       320000 |        8000000 |  8,176.4 us | 162.38 us |   300.99 us |  8,197.6 us |  2.11 |    0.18 |           - |           - |           - |                   - |
