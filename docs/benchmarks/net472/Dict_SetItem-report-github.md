``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **948.4 us** | **12.413 us** | **11.611 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 | 728.9 us |  2.059 us |  1.926 us |  0.77 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictSetItem** |  **10000** | **942.2 us** |  **6.817 us** |  **6.376 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 | 733.2 us |  2.031 us |  1.696 us |  0.78 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictSetItem** | **100000** | **928.2 us** |  **3.839 us** |  **3.403 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 | 728.5 us |  2.114 us |  1.977 us |  0.78 |           - |           - |           - |                   - |
