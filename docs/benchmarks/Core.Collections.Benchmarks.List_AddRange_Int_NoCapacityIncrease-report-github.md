``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                           Method | LargeSets |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **645.08 us** | **3.0289 us** | **2.8332 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False | 620.00 us | 2.5643 us | 2.3986 us |  0.96 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 651.26 us | 2.6867 us | 2.2435 us |  1.01 |           - |           - |           - |                   - |
|                                                  |           |           |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |  **19.90 us** | **0.1595 us** | **0.1245 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |  21.87 us | 0.0692 us | 0.0613 us |  1.10 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |  21.43 us | 0.1688 us | 0.1579 us |  1.08 |           - |           - |           - |                   - |
