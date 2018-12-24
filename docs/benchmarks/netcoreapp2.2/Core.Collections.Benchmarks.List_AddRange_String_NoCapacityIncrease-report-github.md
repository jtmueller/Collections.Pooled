``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                              Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** |   **779.8 us** | **3.3691 us** | **3.1515 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 1,000.5 us | 2.4353 us | 2.0336 us |  1.28 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False |   798.5 us | 2.2397 us | 2.0950 us |  1.02 |           - |           - |           - |                   - |
|                                                     |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **112.0 us** | **0.2350 us** | **0.2198 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   111.6 us | 0.4345 us | 0.3852 us |  1.00 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   111.3 us | 0.2189 us | 0.1940 us |  0.99 |           - |           - |           - |                   - |
