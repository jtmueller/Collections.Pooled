``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                              Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** |   **797.6 us** | **2.6919 us** | **2.5180 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 1,003.5 us | 2.3428 us | 2.1915 us |  1.26 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False |   796.8 us | 1.9487 us | 1.7275 us |  1.00 |           - |           - |           - |                   - |
|                                                     |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **111.3 us** | **0.1746 us** | **0.1363 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   113.4 us | 0.8422 us | 0.7877 us |  1.02 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   112.4 us | 0.2195 us | 0.2053 us |  1.01 |           - |           - |           - |                   - |
