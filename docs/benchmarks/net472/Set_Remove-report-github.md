``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|           Method | CountToRemove | InitialSetSize |         Mean |        Error |      StdDev |       Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |-------------- |--------------- |-------------:|-------------:|------------:|-------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Remove** |             **1** |        **8000000** |     **947.5 ns** |     **56.67 ns** |    **166.2 ns** |     **900.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |             1 |        8000000 |     719.0 ns |     44.06 ns |    127.1 ns |     695.0 ns |  0.78 |    0.21 |           - |           - |           - |                   - |
|                  |               |                |              |              |             |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |           **100** |        **8000000** |   **8,057.1 ns** |    **443.35 ns** |  **1,286.3 ns** |   **8,320.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |           100 |        8000000 |   9,658.1 ns |    210.50 ns |    430.0 ns |   9,615.0 ns |  1.22 |    0.21 |           - |           - |           - |                   - |
|                  |               |                |              |              |             |              |       |         |             |             |             |                     |
|   **HashSet_Remove** |         **10000** |        **8000000** | **423,471.5 ns** | **24,240.73 ns** | **69,940.1 ns** | **441,985.0 ns** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Remove |         10000 |        8000000 | 479,342.9 ns |  9,587.48 ns | 21,044.8 ns | 480,155.0 ns |  1.16 |    0.19 |           - |           - |           - |                   - |
