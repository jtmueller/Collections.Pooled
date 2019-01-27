``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                     Method | CountToIntersect | InitialSetSize |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |----------------- |--------------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_Except_Hashset** |            **32000** |        **8000000** |  **1.812 ms** | **0.1416 ms** | **0.4132 ms** |  **1.740 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Except_PooledSet |            32000 |        8000000 |  1.445 ms | 0.1394 ms | 0.4111 ms |  1.294 ms |  0.83 |    0.27 |           - |           - |           - |                40 B |
|        HashSet_Except_Enum |            32000 |        8000000 |  1.937 ms | 0.1183 ms | 0.3336 ms |  1.914 ms |  1.11 |    0.33 |           - |           - |           - |                40 B |
|      PooledSet_Except_Enum |            32000 |        8000000 |  1.608 ms | 0.1419 ms | 0.4118 ms |  1.494 ms |  0.94 |    0.36 |           - |           - |           - |                40 B |
|       HashSet_Except_Array |            32000 |        8000000 |  1.922 ms | 0.1134 ms | 0.3307 ms |  1.990 ms |  1.11 |    0.30 |           - |           - |           - |                32 B |
|     PooledSet_Except_Array |            32000 |        8000000 |  1.385 ms | 0.1145 ms | 0.3375 ms |  1.305 ms |  0.78 |    0.16 |           - |           - |           - |                   - |
|                            |                  |                |           |           |           |           |       |         |             |             |             |                     |
|     **HashSet_Except_Hashset** |           **320000** |        **8000000** |  **4.311 ms** | **0.1688 ms** | **0.4870 ms** |  **4.367 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Except_PooledSet |           320000 |        8000000 |  3.810 ms | 0.1476 ms | 0.4281 ms |  3.809 ms |  0.89 |    0.11 |           - |           - |           - |                40 B |
|        HashSet_Except_Enum |           320000 |        8000000 | 10.289 ms | 0.2198 ms | 0.6272 ms | 10.233 ms |  2.41 |    0.30 |           - |           - |           - |                40 B |
|      PooledSet_Except_Enum |           320000 |        8000000 | 10.189 ms | 0.1994 ms | 0.2795 ms | 10.231 ms |  2.55 |    0.28 |           - |           - |           - |                40 B |
|       HashSet_Except_Array |           320000 |        8000000 | 10.120 ms | 0.1999 ms | 0.4302 ms | 10.082 ms |  2.34 |    0.33 |           - |           - |           - |                32 B |
|     PooledSet_Except_Array |           320000 |        8000000 |  7.365 ms | 0.1910 ms | 0.5479 ms |  7.179 ms |  1.73 |    0.27 |           - |           - |           - |                   - |
