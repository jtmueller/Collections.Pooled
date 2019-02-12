``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                           Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **1,640.8 us** | **3.9986 us** | **3.5446 us** |  **1.00** |    **345.7031** |           **-** |           **-** |           **1088014 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False |   701.8 us | 1.6897 us | 1.4979 us |  0.43 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 1,070.0 us | 2.5096 us | 2.3475 us |  0.65 |           - |           - |           - |                   - |
|                                                  |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |   **208.7 us** | **0.5962 us** | **0.5285 us** |  **1.00** |    **215.0879** |           **-** |           **-** |            **680816 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |   174.0 us | 0.3909 us | 0.3465 us |  0.83 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |   158.3 us | 0.4358 us | 0.3863 us |  0.76 |           - |           - |           - |                   - |
