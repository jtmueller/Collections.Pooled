``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                           Method | LargeSets |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **1,660.4 us** | **3.7750 us** | **3.3464 us** |  **1.00** |    **0.00** |    **345.7031** |           **-** |           **-** |           **1088014 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False |   712.0 us | 1.5510 us | 1.4508 us |  0.43 |    0.00 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 1,129.9 us | 3.0561 us | 2.5520 us |  0.68 |    0.00 |           - |           - |           - |                   - |
|                                                  |           |            |           |           |       |         |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |   **211.5 us** | **0.5588 us** | **0.5227 us** |  **1.00** |    **0.00** |    **215.0879** |           **-** |           **-** |            **680816 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |   189.3 us | 3.4971 us | 3.4346 us |  0.89 |    0.02 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |   164.9 us | 3.1858 us | 4.4660 us |  0.78 |    0.02 |           - |           - |           - |                   - |
