``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |       Mean |     Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **1,783.8 us** | **31.142 us** | **29.1301 us** |  **1.00** |   **1050.7813** |    **998.0469** |    **998.0469** |           **5243396 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 1,188.6 us |  6.881 us |  5.7462 us |  0.67 |           - |           - |           - |                56 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,023.0 us |  3.591 us |  3.1830 us |  0.57 |           - |           - |           - |                56 B |
|                                                |           |            |           |            |       |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2,007.8 us** | **39.931 us** | **95.6718 us** |  **1.00** |    **222.6563** |    **199.2188** |    **197.2656** |           **5040580 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True |   361.5 us |  2.120 us |  1.9834 us |  0.18 |           - |           - |           - |                56 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   364.1 us |  1.041 us |  0.9735 us |  0.18 |           - |           - |           - |                56 B |
