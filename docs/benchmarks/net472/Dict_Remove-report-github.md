``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|       Method |     N |         Mean |       Error |      StdDev | Ratio |
|------------- |------ |-------------:|------------:|------------:|------:|
|   **DictRemove** |    **10** |     **80.98 ns** |   **0.1519 ns** |   **0.1421 ns** |  **1.00** |
| PooledRemove |    10 |     79.55 ns |   0.2901 ns |   0.2713 ns |  0.98 |
|              |       |              |             |             |       |
|   **DictRemove** |   **100** |    **817.82 ns** |   **2.0988 ns** |   **1.9632 ns** |  **1.00** |
| PooledRemove |   100 |    777.32 ns |   0.6469 ns |   0.5735 ns |  0.95 |
|              |       |              |             |             |       |
|   **DictRemove** | **10000** | **84,153.13 ns** | **207.9359 ns** | **194.5033 ns** |  **1.00** |
| PooledRemove | 10000 | 77,384.20 ns | 347.3462 ns | 324.9079 ns |  0.92 |
