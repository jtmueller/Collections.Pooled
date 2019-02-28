``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetItem** |   **1000** | **850.6 us** | **1.972 us** | **1.844 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |   1000 | 647.5 us | 1.825 us | 1.707 us |  0.76 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** |  **10000** | **850.7 us** | **2.699 us** | **2.525 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |  10000 | 617.5 us | 9.179 us | 7.664 us |  0.73 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** | **100000** | **849.4 us** | **1.534 us** | **1.435 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem | 100000 | 615.7 us | 2.293 us | 2.033 us |  0.72 |           - |           - |           - |                   - |
