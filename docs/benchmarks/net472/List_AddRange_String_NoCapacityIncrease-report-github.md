``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                              Method | LargeSets |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** | **1,964.6 us** |  **6.358 us** |  **5.310 us** |  **1.00** |    **0.00** |    **558.5938** |           **-** |           **-** |           **1768040 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 3,094.5 us | 57.825 us | 54.089 us |  1.57 |    0.03 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False | 1,338.3 us |  4.282 us |  3.576 us |  0.68 |    0.00 |           - |           - |           - |                   - |
|                                                     |           |            |           |           |       |         |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **577.2 us** |  **2.738 us** |  **2.137 us** |  **1.00** |    **0.00** |    **424.8047** |           **-** |           **-** |           **1360816 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   735.9 us |  2.873 us |  2.547 us |  1.27 |    0.01 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   465.6 us | 11.992 us | 16.010 us |  0.82 |    0.03 |           - |           - |           - |                   - |
