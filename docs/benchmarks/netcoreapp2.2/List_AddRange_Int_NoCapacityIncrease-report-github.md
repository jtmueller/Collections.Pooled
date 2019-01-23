``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                           Method | LargeSets |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **618.88 us** | **4.3454 us** | **3.8521 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False | 568.59 us | 1.1245 us | 1.0519 us |  0.92 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 632.32 us | 7.6737 us | 6.8025 us |  1.02 |           - |           - |           - |                   - |
|                                                  |           |           |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |  **19.84 us** | **0.0850 us** | **0.0710 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |  20.43 us | 0.1053 us | 0.0879 us |  1.03 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |  19.93 us | 0.0552 us | 0.0489 us |  1.00 |           - |           - |           - |                   - |
