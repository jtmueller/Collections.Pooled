``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |       Mean |     Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **1,782.4 us** | **34.267 us** |  **40.793 us** |  **1.00** |    **0.00** |   **1050.7813** |    **998.0469** |    **998.0469** |           **5243396 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 1,296.3 us | 24.137 us |  42.274 us |  0.73 |    0.02 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,136.2 us | 17.469 us |  16.341 us |  0.64 |    0.02 |           - |           - |           - |                40 B |
|                                                |           |            |           |            |       |         |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2,055.7 us** | **55.300 us** | **160.436 us** |  **1.00** |    **0.00** |    **304.6875** |    **285.1563** |    **281.2500** |           **5040629 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True |   397.9 us |  4.583 us |   4.287 us |  0.19 |    0.01 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   401.5 us |  7.702 us |   9.169 us |  0.19 |    0.01 |           - |           - |           - |                40 B |
