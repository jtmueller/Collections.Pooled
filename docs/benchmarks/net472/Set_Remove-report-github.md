``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|           Method | CountToRemove | InitialSetSize |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------------- |--------------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Remove** |             **1** |        **8000000** |     **997.0 ns** |     **54.37 ns** |    **158.59 ns** |     **995.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |             1 |        8000000 |     684.2 ns |     31.06 ns |     89.10 ns |     700.0 ns |  0.71 |    0.15 |           - |           - |           - |                   - |
|                  |               |                |              |              |              |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |           **100** |        **8000000** |   **8,226.9 ns** |    **455.92 ns** |  **1,337.12 ns** |   **8,935.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |           100 |        8000000 |   9,579.5 ns |    190.20 ns |    290.46 ns |   9,515.0 ns |  1.21 |    0.21 |           - |           - |           - |                   - |
|                  |               |                |              |              |              |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |         **10000** |        **8000000** | **414,989.8 ns** | **26,464.86 ns** | **77,199.23 ns** | **454,000.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |         10000 |        8000000 | 467,860.7 ns |  5,608.06 ns |  4,971.40 ns | 467,775.0 ns |  1.14 |    0.22 |           - |           - |           - |                   - |
