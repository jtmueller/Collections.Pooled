``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                           Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **1,611.0 us** | **1.9045 us** | **1.5903 us** |  **1.00** |    **345.7031** |           **-** |           **-** |           **1088014 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False |   714.6 us | 2.4250 us | 2.2683 us |  0.44 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 1,055.8 us | 4.1416 us | 3.8740 us |  0.66 |           - |           - |           - |                   - |
|                                                  |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |   **209.5 us** | **0.4665 us** | **0.4136 us** |  **1.00** |    **215.0879** |           **-** |           **-** |            **680816 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |   174.4 us | 0.2163 us | 0.1918 us |  0.83 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |   158.8 us | 0.4219 us | 0.3947 us |  0.76 |           - |           - |           - |                   - |
