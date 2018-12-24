``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **2,022.6 us** | **27.796 us** | **26.000 us** | **2,027.8 us** |  **1.00** |    **0.00** |    **796.8750** |    **597.6563** |    **597.6563** |           **3713544 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False | 1,028.8 us |  6.340 us |  5.620 us | 1,028.4 us |  0.51 |    0.01 |           - |           - |           - |                48 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,370.8 us |  5.103 us |  4.524 us | 1,370.4 us |  0.68 |    0.01 |           - |           - |           - |                48 B |
|                                                |           |            |           |           |            |       |         |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **597.3 us** | **13.664 us** | **40.288 us** |   **578.3 us** |  **1.00** |    **0.00** |    **821.2891** |    **569.3359** |    **531.2500** |           **3204288 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |   483.2 us |  1.176 us |  1.100 us |   483.4 us |  0.74 |    0.02 |           - |           - |           - |                36 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   471.8 us |  2.495 us |  2.083 us |   471.4 us |  0.73 |    0.02 |           - |           - |           - |                36 B |
