``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                               Method | InitialSetSize |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------- |--------------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsProperSuperset_Hashset** |          **32000** |   **537.4 us** |  **2.579 us** |  **2.413 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_IsProperSuperset_PooledSet |          32000 |   535.0 us |  1.690 us |  1.498 us |  1.00 |    0.01 |           - |           - |           - |                40 B |
|        HashSet_IsProperSuperset_Enum |          32000 |   908.4 us |  3.914 us |  3.661 us |  1.69 |    0.01 |           - |           - |           - |                40 B |
|      PooledSet_IsProperSuperset_Enum |          32000 |   881.4 us |  2.510 us |  2.347 us |  1.64 |    0.01 |           - |           - |           - |                40 B |
|       HashSet_IsProperSuperset_Array |          32000 |   873.5 us |  2.932 us |  2.743 us |  1.63 |    0.01 |           - |           - |           - |                32 B |
|     PooledSet_IsProperSuperset_Array |          32000 |   664.2 us |  1.377 us |  1.288 us |  1.24 |    0.01 |           - |           - |           - |                   - |
|                                      |                |            |           |           |       |         |             |             |             |                     |
|     **HashSet_IsProperSuperset_Hashset** |         **320000** |   **811.6 us** |  **2.968 us** |  **2.777 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_IsProperSuperset_PooledSet |         320000 |   799.8 us |  2.958 us |  2.767 us |  0.99 |    0.01 |           - |           - |           - |                40 B |
|        HashSet_IsProperSuperset_Enum |         320000 | 9,285.5 us | 27.275 us | 25.513 us | 11.44 |    0.05 |           - |           - |           - |                40 B |
|      PooledSet_IsProperSuperset_Enum |         320000 | 8,700.2 us | 26.417 us | 24.711 us | 10.72 |    0.05 |           - |           - |           - |                40 B |
|       HashSet_IsProperSuperset_Array |         320000 | 8,834.7 us | 42.893 us | 40.122 us | 10.89 |    0.06 |           - |           - |           - |                32 B |
|     PooledSet_IsProperSuperset_Array |         320000 | 6,583.4 us | 12.166 us | 10.159 us |  8.12 |    0.03 |           - |           - |           - |                   - |
