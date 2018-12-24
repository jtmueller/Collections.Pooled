``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                              Method | LargeSets |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** | **1,947.6 us** | **15.1943 us** | **11.8627 us** |  **1.00** |    **0.00** |    **558.5938** |           **-** |           **-** |           **1768040 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 2,864.7 us |  5.7615 us |  4.8111 us |  1.47 |    0.01 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False | 1,338.4 us |  5.2781 us |  4.9371 us |  0.69 |    0.01 |           - |           - |           - |                   - |
|                                                     |           |            |            |            |       |         |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **577.3 us** |  **7.7831 us** |  **6.8995 us** |  **1.00** |    **0.00** |    **424.8047** |           **-** |           **-** |           **1360816 B** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   693.2 us |  1.3789 us |  1.2898 us |  1.20 |    0.02 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   446.4 us |  0.3481 us |  0.2718 us |  0.77 |    0.01 |           - |           - |           - |                   - |
