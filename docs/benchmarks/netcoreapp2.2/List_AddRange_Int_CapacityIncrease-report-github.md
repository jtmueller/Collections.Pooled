``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **1,260.93 us** | **23.4119 us** | **21.8995 us** |  **1.00** |    **613.2813** |    **613.2813** |    **613.2813** |           **2621824 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False |   489.61 us |  1.8778 us |  1.7565 us |  0.39 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False |   668.14 us |  2.6451 us |  2.3448 us |  0.53 |           - |           - |           - |                40 B |
|                                                |           |             |            |            |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **554.38 us** |  **6.4387 us** |  **5.7077 us** |  **1.00** |    **541.9922** |    **520.5078** |    **515.6250** |           **2520775 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |    55.27 us |  0.4710 us |  0.4175 us |  0.10 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |    55.61 us |  0.4431 us |  0.4145 us |  0.10 |           - |           - |           - |                40 B |
