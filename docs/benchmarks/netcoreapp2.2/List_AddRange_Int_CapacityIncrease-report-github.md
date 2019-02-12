``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **1,134.49 us** | **16.6571 us** | **15.5811 us** |  **1.00** |    **650.3906** |    **650.3906** |    **650.3906** |           **2621824 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False |   472.21 us |  1.6872 us |  1.5782 us |  0.42 |           - |           - |           - |                56 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False |   648.41 us |  2.1949 us |  2.0531 us |  0.57 |           - |           - |           - |                56 B |
|                                                |           |             |            |            |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **448.11 us** | **17.0095 us** | **20.2486 us** |  **1.00** |    **531.7383** |    **503.4180** |    **493.1641** |           **2521769 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |    56.01 us |  0.9807 us |  0.8694 us |  0.12 |           - |           - |           - |                56 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |    55.13 us |  0.4024 us |  0.3764 us |  0.12 |           - |           - |           - |                56 B |
