``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                           Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **1,608.8 us** | **4.1458 us** | **3.6751 us** |  **1.00** |    **345.7031** |           **-** |           **-** |           **1088014 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False |   700.3 us | 2.4041 us | 2.2488 us |  0.44 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 1,034.2 us | 1.9230 us | 1.7047 us |  0.64 |           - |           - |           - |                   - |
|                                                  |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |   **208.2 us** | **0.6265 us** | **0.5860 us** |  **1.00** |    **215.0879** |           **-** |           **-** |            **680816 B** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |   174.1 us | 0.5803 us | 0.5428 us |  0.84 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |   159.0 us | 0.5034 us | 0.4463 us |  0.76 |           - |           - |           - |                   - |
