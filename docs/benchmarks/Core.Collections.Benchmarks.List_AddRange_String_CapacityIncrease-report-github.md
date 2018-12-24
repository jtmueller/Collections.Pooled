``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |       Mean |     Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |-----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **1,817.9 us** | **15.047 us** |  **13.338 us** |  **1.00** |    **0.00** |   **1035.1563** |    **984.3750** |    **982.4219** |           **5243290 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 1,306.1 us | 10.639 us |   9.431 us |  0.72 |    0.01 |           - |           - |           - |                32 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 1,139.6 us |  4.071 us |   3.399 us |  0.63 |    0.00 |           - |           - |           - |                32 B |
|                                                |           |            |           |            |       |         |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2,230.9 us** | **58.654 us** | **172.942 us** |  **1.00** |    **0.00** |    **156.2500** |    **132.8125** |    **132.8125** |           **5040469 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True |   404.5 us |  4.551 us |   4.257 us |  0.18 |    0.02 |           - |           - |           - |                32 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |   406.8 us |  3.249 us |   3.039 us |  0.18 |    0.02 |           - |           - |           - |                32 B |
