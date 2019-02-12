``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                     Method | CountToIntersect | InitialSetSize |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |----------------- |--------------- |-----------:|----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Except_Hashset** |            **32000** |        **8000000** | **1,336.7 us** |  **11.67 us** |   **9.112 us** | **1,337.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Except_PooledSet |            32000 |        8000000 |   982.6 us |  19.26 us |  32.171 us |   969.1 us |  0.75 |    0.02 |           - |           - |           - |                40 B |
|        HashSet_Except_Enum |            32000 |        8000000 | 1,450.2 us |  21.15 us |  16.514 us | 1,455.2 us |  1.08 |    0.02 |           - |           - |           - |                40 B |
|      PooledSet_Except_Enum |            32000 |        8000000 | 1,197.2 us |  23.87 us |  55.324 us | 1,177.5 us |  0.90 |    0.03 |           - |           - |           - |                40 B |
|       HashSet_Except_Array |            32000 |        8000000 | 1,463.2 us |  28.77 us |  26.908 us | 1,462.2 us |  1.10 |    0.02 |           - |           - |           - |                32 B |
|     PooledSet_Except_Array |            32000 |        8000000 | 1,050.0 us |  14.39 us |  12.019 us | 1,048.8 us |  0.79 |    0.01 |           - |           - |           - |                   - |
|                            |                  |                |            |           |            |            |       |         |             |             |             |                     |
|     **HashSet_Except_Hashset** |           **320000** |        **8000000** | **3,485.1 us** |  **72.11 us** | **105.700 us** | **3,433.3 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Except_PooledSet |           320000 |        8000000 | 3,105.1 us |  61.62 us | 120.188 us | 3,108.4 us |  0.90 |    0.04 |           - |           - |           - |                40 B |
|        HashSet_Except_Enum |           320000 |        8000000 | 8,437.6 us |  82.19 us |  68.636 us | 8,399.7 us |  2.44 |    0.07 |           - |           - |           - |                40 B |
|      PooledSet_Except_Enum |           320000 |        8000000 | 8,528.7 us | 120.36 us | 106.693 us | 8,542.2 us |  2.46 |    0.09 |           - |           - |           - |                40 B |
|       HashSet_Except_Array |           320000 |        8000000 | 8,715.4 us | 172.52 us | 169.433 us | 8,659.4 us |  2.51 |    0.09 |           - |           - |           - |                32 B |
|     PooledSet_Except_Array |           320000 |        8000000 | 7,371.9 us | 127.47 us | 119.231 us | 7,312.0 us |  2.12 |    0.08 |           - |           - |           - |                   - |
