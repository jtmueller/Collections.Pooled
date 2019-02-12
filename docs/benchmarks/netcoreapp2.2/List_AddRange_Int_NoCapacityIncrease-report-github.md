``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                           Method | LargeSets |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **588.11 us** | **2.4683 us** | **2.3088 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False | 568.40 us | 1.4385 us | 1.3456 us |  0.97 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 620.70 us | 1.9538 us | 1.8276 us |  1.06 |           - |           - |           - |                   - |
|                                                  |           |           |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |  **19.75 us** | **0.0925 us** | **0.0865 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |  20.17 us | 0.0338 us | 0.0283 us |  1.02 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |  19.88 us | 0.0764 us | 0.0715 us |  1.01 |           - |           - |           - |                   - |
