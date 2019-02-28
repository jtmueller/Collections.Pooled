``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **2,030.1 us** | **4.6662 us** | **4.1364 us** |  **1.00** |    **796.8750** |    **597.6563** |    **597.6563** |           **3713614 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False | 1,020.6 us | 2.9588 us | 2.6229 us |  0.50 |           - |           - |           - |                64 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,348.0 us | 5.4209 us | 5.0707 us |  0.66 |           - |           - |           - |                64 B |
|                                                |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **600.1 us** | **9.8569 us** | **8.2309 us** |  **1.00** |    **827.1484** |    **579.1016** |    **538.0859** |           **3204328 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |   484.5 us | 1.4093 us | 1.2493 us |  0.81 |           - |           - |           - |                60 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   471.5 us | 0.7851 us | 0.7344 us |  0.79 |           - |           - |           - |                60 B |
