``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                               Method | InitialSetSize |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------- |--------------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsProperSuperset_Hashset** |          **32000** |   **527.4 us** |  **7.527 us** |  **6.285 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsProperSuperset_PooledSet |          32000 |   532.2 us |  6.035 us |  5.350 us |  1.01 |    0.01 |           - |           - |           - |                48 B |
|        HashSet_IsProperSuperset_Enum |          32000 |   877.9 us |  8.771 us |  8.205 us |  1.66 |    0.03 |           - |           - |           - |                48 B |
|      PooledSet_IsProperSuperset_Enum |          32000 |   881.6 us | 11.157 us |  9.891 us |  1.67 |    0.03 |           - |           - |           - |                48 B |
|       HashSet_IsProperSuperset_Array |          32000 |   854.3 us |  7.063 us |  6.607 us |  1.62 |    0.02 |           - |           - |           - |                40 B |
|     PooledSet_IsProperSuperset_Array |          32000 |   661.2 us |  6.705 us |  6.272 us |  1.25 |    0.02 |           - |           - |           - |                   - |
|                                      |                |            |           |           |       |         |             |             |             |                     |
|     **HashSet_IsProperSuperset_Hashset** |         **320000** |   **786.6 us** |  **6.578 us** |  **6.153 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledSet_IsProperSuperset_PooledSet |         320000 |   787.3 us |  6.250 us |  5.846 us |  1.00 |    0.01 |           - |           - |           - |                48 B |
|        HashSet_IsProperSuperset_Enum |         320000 | 8,701.8 us | 90.271 us | 84.439 us | 11.06 |    0.13 |           - |           - |           - |                   - |
|      PooledSet_IsProperSuperset_Enum |         320000 | 8,946.6 us | 93.322 us | 87.293 us | 11.37 |    0.15 |           - |           - |           - |                   - |
|       HashSet_IsProperSuperset_Array |         320000 | 8,544.4 us | 74.377 us | 69.572 us | 10.86 |    0.12 |           - |           - |           - |                   - |
|     PooledSet_IsProperSuperset_Array |         320000 | 6,784.2 us | 60.631 us | 56.714 us |  8.63 |    0.12 |           - |           - |           - |                   - |
