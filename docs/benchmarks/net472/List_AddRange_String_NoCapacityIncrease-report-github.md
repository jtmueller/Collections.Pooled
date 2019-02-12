``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                              Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** | **1,918.8 us** | **8.4148 us** | **7.4595 us** |  **1.00** |    **558.5938** |           **-** |           **-** |           **1768040 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 2,880.2 us | 7.4341 us | 6.9538 us |  1.50 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False | 1,360.7 us | 3.7886 us | 3.5439 us |  0.71 |           - |           - |           - |                   - |
|                                                     |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **572.9 us** | **1.6473 us** | **1.5409 us** |  **1.00** |    **424.8047** |           **-** |           **-** |           **1360816 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   704.2 us | 1.8959 us | 1.7734 us |  1.23 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   448.8 us | 0.6006 us | 0.5324 us |  0.78 |           - |           - |           - |                   - |
