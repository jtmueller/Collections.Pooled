``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetKeys** |   **1000** | **64.39 us** | **0.2769 us** | **0.2590 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |   1000 | 81.81 us | 0.3129 us | 0.2927 us |  1.27 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** |  **10000** | **64.25 us** | **0.1930 us** | **0.1805 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |  10000 | 81.85 us | 0.2627 us | 0.2457 us |  1.27 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** | **100000** | **64.27 us** | **0.0986 us** | **0.0874 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys | 100000 | 81.74 us | 0.3029 us | 0.2685 us |  1.27 |           - |           - |           - |                   - |
