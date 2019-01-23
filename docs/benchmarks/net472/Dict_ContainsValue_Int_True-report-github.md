``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                       Method |      N |            Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |----------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_True** |   **1000** |        **965.0 us** |      **2.543 us** |      **2.254 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |   1000 |        888.1 us |      1.694 us |      1.502 us |  0.92 |           - |           - |           - |                   - |
|                              |        |                 |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** |  **10000** |     **95,246.6 us** |    **396.800 us** |    **371.167 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |  10000 |     87,713.2 us |    339.492 us |    317.561 us |  0.92 |           - |           - |           - |                   - |
|                              |        |                 |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** | **100000** | **10,567,170.2 us** | **18,591.887 us** | **16,481.224 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True | 100000 |  9,946,770.2 us | 17,188.513 us | 15,237.169 us |  0.94 |           - |           - |           - |                   - |
