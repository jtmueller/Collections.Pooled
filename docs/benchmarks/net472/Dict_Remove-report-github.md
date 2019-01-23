``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|       Method |     N |         Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------- |------ |-------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictRemove** |    **10** |     **83.87 ns** |   **0.3218 ns** |   **0.3010 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove |    10 |     73.65 ns |   0.0882 ns |   0.0782 ns |  0.88 |           - |           - |           - |                   - |
|              |       |              |             |             |       |             |             |             |                     |
|   **DictRemove** |   **100** |    **758.30 ns** |   **2.9654 ns** |   **2.7738 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove |   100 |    709.93 ns |   1.4241 ns |   1.3321 ns |  0.94 |           - |           - |           - |                   - |
|              |       |              |             |             |       |             |             |             |                     |
|   **DictRemove** | **10000** | **86,856.52 ns** | **214.1231 ns** | **200.2909 ns** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledRemove | 10000 | 70,850.57 ns | 415.6068 ns | 368.4246 ns |  0.82 |           - |           - |           - |                   - |
