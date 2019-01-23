``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                              Method | LargeSets |       Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** | **1,928.8 us** | **4.194 us** | **3.923 us** |  **1.00** |    **560.5469** |           **-** |           **-** |           **1768040 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 2,840.4 us | 7.311 us | 6.839 us |  1.47 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False | 1,342.5 us | 5.865 us | 5.486 us |  0.70 |           - |           - |           - |                   - |
|                                                     |           |            |          |          |       |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **571.8 us** | **2.239 us** | **2.094 us** |  **1.00** |    **424.8047** |           **-** |           **-** |           **1360816 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   701.7 us | 2.476 us | 2.195 us |  1.23 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   447.9 us | 1.317 us | 1.232 us |  0.78 |           - |           - |           - |                   - |
