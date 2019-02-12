``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                         Method | InitialSetSize |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |--------------- |-----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsSuperset_Hashset** |          **32000** |   **538.6 us** |  **1.7017 us** |  **1.5085 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_IsSuperset_PooledSet |          32000 |   534.0 us |  1.6954 us |  1.5859 us |  0.99 |    0.00 |           - |           - |           - |                40 B |
|        HashSet_IsSuperset_Enum |          32000 |   909.7 us |  2.2009 us |  2.0587 us |  1.69 |    0.00 |           - |           - |           - |                40 B |
|      PooledSet_IsSuperset_Enum |          32000 |   911.3 us |  0.9200 us |  0.8606 us |  1.69 |    0.00 |           - |           - |           - |                40 B |
|       HashSet_IsSuperset_Array |          32000 |   947.7 us |  2.6767 us |  2.5038 us |  1.76 |    0.01 |           - |           - |           - |                32 B |
|     PooledSet_IsSuperset_Array |          32000 |   664.1 us |  3.0022 us |  2.8082 us |  1.23 |    0.01 |           - |           - |           - |                   - |
|                                |                |            |            |            |       |         |             |             |             |                     |
|     **HashSet_IsSuperset_Hashset** |         **320000** |   **811.0 us** |  **2.8516 us** |  **2.6673 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_IsSuperset_PooledSet |         320000 |   800.0 us |  2.3567 us |  2.2045 us |  0.99 |    0.01 |           - |           - |           - |                40 B |
|        HashSet_IsSuperset_Enum |         320000 | 9,312.2 us | 47.7123 us | 39.8419 us | 11.48 |    0.08 |           - |           - |           - |                40 B |
|      PooledSet_IsSuperset_Enum |         320000 | 8,835.2 us | 38.3575 us | 35.8796 us | 10.89 |    0.06 |           - |           - |           - |                40 B |
|       HashSet_IsSuperset_Array |         320000 | 8,916.6 us | 26.4932 us | 22.1230 us | 11.00 |    0.06 |           - |           - |           - |                32 B |
|     PooledSet_IsSuperset_Array |         320000 | 6,592.2 us | 19.8394 us | 18.5577 us |  8.13 |    0.03 |           - |           - |           - |                   - |
