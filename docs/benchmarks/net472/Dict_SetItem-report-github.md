``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|        Method |      N |       Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |-----------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** |   **947.5 us** | **3.814 us** | **3.185 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 |   796.0 us | 6.925 us | 6.478 us |  0.84 |           - |           - |           - |                   - |
|               |        |            |          |          |       |             |             |             |                     |
|   **DictSetItem** |  **10000** |   **946.6 us** | **2.750 us** | **2.297 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 |   739.7 us | 3.871 us | 3.233 us |  0.78 |           - |           - |           - |                   - |
|               |        |            |          |          |       |             |             |             |                     |
|   **DictSetItem** | **100000** | **1,027.9 us** | **2.588 us** | **2.421 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 |   740.4 us | 2.783 us | 2.324 us |  0.72 |           - |           - |           - |                   - |
