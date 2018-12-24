``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                           Method | LargeSets |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **603.19 us** | **1.1193 us** | **1.0470 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False | 568.53 us | 2.0933 us | 1.9581 us |  0.94 |    0.00 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 606.01 us | 2.5705 us | 2.4044 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|                                                  |           |           |           |           |       |         |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |  **18.21 us** | **0.0604 us** | **0.0535 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |  20.66 us | 0.7860 us | 0.7352 us |  1.14 |    0.04 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |  19.98 us | 0.0632 us | 0.0528 us |  1.10 |    0.00 |           - |           - |           - |                   - |
