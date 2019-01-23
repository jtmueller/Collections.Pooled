``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetKeys** |   **1000** | **58.50 us** | **0.2983 us** | **0.2791 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |   1000 | 75.86 us | 0.2002 us | 0.1775 us |  1.30 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** |  **10000** | **58.50 us** | **0.1909 us** | **0.1692 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |  10000 | 75.88 us | 0.1815 us | 0.1698 us |  1.30 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** | **100000** | **58.50 us** | **0.1737 us** | **0.1625 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys | 100000 | 75.82 us | 0.1522 us | 0.1424 us |  1.30 |           - |           - |           - |                   - |
