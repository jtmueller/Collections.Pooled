``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictTryGetValue** |   **1000** | **76.76 us** | **0.3268 us** | **0.3056 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |   1000 | 66.98 us | 0.2376 us | 0.2223 us |  0.87 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** |  **10000** | **76.60 us** | **0.2296 us** | **0.2147 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |  10000 | 66.51 us | 0.1742 us | 0.1630 us |  0.87 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** | **100000** | **76.75 us** | **0.4035 us** | **0.3775 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue | 100000 | 66.45 us | 0.2022 us | 0.1892 us |  0.87 |           - |           - |           - |                   - |
