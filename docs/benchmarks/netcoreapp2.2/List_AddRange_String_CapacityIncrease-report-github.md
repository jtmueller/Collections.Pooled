``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |       Mean |     Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **1,945.2 us** | **20.907 us** |  **18.533 us** |  **1.00** |   **1046.8750** |    **996.0938** |    **996.0938** |           **5243382 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 1,206.1 us |  6.552 us |   6.129 us |  0.62 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,032.3 us |  4.828 us |   4.280 us |  0.53 |           - |           - |           - |                40 B |
|                                                |           |            |           |            |       |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2,229.3 us** | **54.794 us** | **161.560 us** |  **1.00** |    **187.5000** |    **164.0625** |    **164.0625** |           **5040501 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True |   364.6 us |  1.956 us |   1.734 us |  0.16 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   367.6 us |  1.240 us |   1.160 us |  0.16 |           - |           - |           - |                40 B |
