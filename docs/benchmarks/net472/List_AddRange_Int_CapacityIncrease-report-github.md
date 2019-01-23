``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |       Mean |      Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|-----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **2,089.9 us** |  **6.5199 us** | **6.0987 us** |  **1.00** |    **796.8750** |    **597.6563** |    **597.6563** |           **3713497 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False | 1,015.9 us |  3.5520 us | 2.9661 us |  0.49 |           - |           - |           - |                48 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,374.6 us |  2.9711 us | 2.6338 us |  0.66 |           - |           - |           - |                48 B |
|                                                |           |            |            |           |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **596.1 us** | **10.2063 us** | **9.5470 us** |  **1.00** |    **832.0313** |    **583.0078** |    **529.2969** |           **3204232 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |   484.0 us |  0.8373 us | 0.7422 us |  0.81 |           - |           - |           - |                44 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   471.7 us |  1.4435 us | 1.2797 us |  0.79 |           - |           - |           - |                44 B |
