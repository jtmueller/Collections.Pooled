``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                       Method |      N |           Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |---------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_True** |   **1000** |       **966.5 us** |      **4.782 us** |      **4.473 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |   1000 |       888.3 us |      3.200 us |      2.993 us |  0.92 |           - |           - |           - |                   - |
|                              |        |                |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** |  **10000** |    **95,088.8 us** |    **271.124 us** |    **253.610 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |  10000 |    87,867.6 us |    402.612 us |    376.603 us |  0.92 |           - |           - |           - |                   - |
|                              |        |                |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** | **100000** | **9,496,377.2 us** | **19,147.114 us** | **14,948.807 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True | 100000 | 8,767,619.6 us | 14,397.541 us | 12,022.599 us |  0.92 |           - |           - |           - |                   - |
