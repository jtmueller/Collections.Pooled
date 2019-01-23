``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictTryGetValue** |   **1000** | **88.61 us** | **0.2695 us** | **0.2389 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |   1000 | 67.70 us | 0.2412 us | 0.2256 us |  0.76 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** |  **10000** | **77.25 us** | **0.4262 us** | **0.3986 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue |  10000 | 68.11 us | 0.7948 us | 0.7435 us |  0.88 |           - |           - |           - |                   - |
|                   |        |          |           |           |       |             |             |             |                     |
|   **DictTryGetValue** | **100000** | **76.99 us** | **0.2470 us** | **0.2063 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledTryGetValue | 100000 | 73.07 us | 0.2118 us | 0.1981 us |  0.95 |           - |           - |           - |                   - |
