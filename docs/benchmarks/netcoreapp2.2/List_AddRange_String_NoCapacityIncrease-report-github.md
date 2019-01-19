``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                              Method | LargeSets |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** |   **878.3 us** |  **5.221 us** |  **4.359 us** |   **876.4 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 1,098.8 us |  8.448 us |  7.489 us | 1,100.1 us |  1.25 |    0.01 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False |   856.1 us | 11.987 us | 10.627 us |   857.7 us |  0.97 |    0.01 |           - |           - |           - |                   - |
|                                                     |           |            |           |           |            |       |         |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **117.2 us** |  **2.290 us** |  **4.245 us** |   **116.6 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   122.2 us |  2.997 us |  2.943 us |   120.9 us |  1.05 |    0.04 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   114.4 us |  2.266 us |  3.970 us |   111.9 us |  0.98 |    0.05 |           - |           - |           - |                   - |
