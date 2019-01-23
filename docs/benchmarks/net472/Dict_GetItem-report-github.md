``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetItem** |   **1000** | **851.4 us** | **3.498 us** | **3.101 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |   1000 | 615.2 us | 1.669 us | 1.561 us |  0.72 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** |  **10000** | **851.3 us** | **3.043 us** | **2.846 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem |  10000 | 615.2 us | 2.099 us | 1.964 us |  0.72 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictGetItem** | **100000** | **877.4 us** | **2.727 us** | **2.550 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetItem | 100000 | 646.8 us | 2.375 us | 2.221 us |  0.74 |           - |           - |           - |                   - |
