``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **975.4 us** | **2.995 us** | **2.802 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 | 745.9 us | 1.913 us | 1.790 us |  0.76 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictSetItem** |  **10000** | **974.5 us** | **3.871 us** | **3.232 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 | 745.6 us | 2.402 us | 2.247 us |  0.77 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictSetItem** | **100000** | **973.8 us** | **2.580 us** | **2.413 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 | 746.2 us | 1.385 us | 1.296 us |  0.77 |           - |           - |           - |                   - |
