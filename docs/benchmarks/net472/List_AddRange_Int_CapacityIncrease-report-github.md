``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |       Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **2,207.1 us** |  **4.9937 us** |  **4.6711 us** |  **1.00** |    **796.8750** |    **597.6563** |    **597.6563** |           **3713604 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False | 1,128.9 us |  2.9905 us |  2.6510 us |  0.51 |           - |           - |           - |                48 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,501.3 us | 26.6920 us | 24.9677 us |  0.68 |           - |           - |           - |                48 B |
|                                                |           |            |            |            |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **599.1 us** | **10.7019 us** | **10.0105 us** |  **1.00** |    **827.1484** |    **570.3125** |    **530.2734** |           **3204224 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |   488.0 us |  1.8982 us |  1.5851 us |  0.81 |           - |           - |           - |                44 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   515.2 us |  0.8342 us |  0.7395 us |  0.86 |           - |           - |           - |                48 B |
