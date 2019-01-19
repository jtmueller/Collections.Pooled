``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                           Method | LargeSets |      Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------------- |---------- |----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_NoCapacityIncrease** |     **False** | **657.88 us** |  **4.3307 us** |  **3.6163 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |     False | 587.11 us | 13.1647 us | 12.3143 us |  0.89 |    0.02 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |     False | 665.53 us |  1.2958 us |  1.2121 us |  1.01 |    0.01 |           - |           - |           - |                   - |
|                                                  |           |           |            |            |       |         |             |             |             |                     |
|              **ListAddRange_Int_NoCapacityIncrease** |      **True** |  **20.06 us** |  **0.3754 us** |  **0.3328 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_Int_NoCapacityIncrease_Array |      True |  21.91 us |  0.0216 us |  0.0180 us |  1.09 |    0.02 |           - |           - |           - |                   - |
| PooledAddRange_Int_NoCapacityIncrease_Enumerable |      True |  20.07 us |  0.0757 us |  0.0632 us |  1.00 |    0.02 |           - |           - |           - |                   - |
