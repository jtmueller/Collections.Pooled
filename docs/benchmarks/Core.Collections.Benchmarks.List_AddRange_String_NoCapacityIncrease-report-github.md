``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                              Method | LargeSets |       Mean |      Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|-----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** |   **873.4 us** |  **3.3746 us** | **3.1566 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 1,107.7 us | 10.5443 us | 9.8631 us |  1.27 |    0.01 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False |   845.7 us |  5.6008 us | 5.2390 us |  0.97 |    0.01 |           - |           - |           - |                   - |
|                                                     |           |            |            |           |       |         |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **123.1 us** |  **0.9205 us** | **0.8160 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   121.0 us |  2.1970 us | 2.0551 us |  0.98 |    0.02 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   122.8 us |  0.4724 us | 0.4419 us |  1.00 |    0.01 |           - |           - |           - |                   - |
