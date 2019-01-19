``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictTryGetValue** |   **1000** | **98.09 us** | **0.2240 us** | **0.2095 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |   1000 | 67.38 us | 0.2888 us | 0.2560 us |  0.69 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** |  **10000** | **84.10 us** | **0.2398 us** | **0.2243 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |  10000 | 72.51 us | 0.1946 us | 0.1725 us |  0.86 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** | **100000** | **83.75 us** | **1.2311 us** | **1.1516 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue | 100000 | 72.45 us | 0.1323 us | 0.1105 us |  0.87 |           - |           - |           - |                   - |
