``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **1,121.65 us** | **13.1624 us** | **12.3121 us** |  **1.00** |    **656.2500** |    **656.2500** |    **656.2500** |           **2621824 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False |   461.68 us |  0.9341 us |  0.8738 us |  0.41 |           - |           - |           - |                32 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False |   643.55 us |  1.5719 us |  1.4703 us |  0.57 |           - |           - |           - |                32 B |
|                                                |           |             |            |            |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **430.14 us** |  **7.0177 us** |  **6.2210 us** |  **1.00** |    **581.5430** |    **572.2656** |    **568.3594** |           **2520288 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |    54.83 us |  0.0731 us |  0.0648 us |  0.13 |           - |           - |           - |                32 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |    55.82 us |  0.3025 us |  0.2829 us |  0.13 |           - |           - |           - |                32 B |
