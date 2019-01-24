``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **1,122.0 us** |  **9.173 us** |  **8.581 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 |   880.9 us |  5.281 us |  4.681 us |  0.78 |    0.01 |           - |           - |           - |                   - |
|               |        |            |           |           |       |         |             |             |             |                     |
|   **DictSetItem** |  **10000** | **1,223.6 us** | **19.323 us** | **18.075 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 |   854.1 us |  5.210 us |  4.619 us |  0.70 |    0.01 |           - |           - |           - |                   - |
|               |        |            |           |           |       |         |             |             |             |                     |
|   **DictSetItem** | **100000** | **1,119.4 us** | **21.491 us** | **24.749 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 |   880.6 us | 17.545 us | 32.082 us |  0.78 |    0.03 |           - |           - |           - |                   - |
