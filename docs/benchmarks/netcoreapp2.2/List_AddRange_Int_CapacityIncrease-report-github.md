``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **1,165.07 us** | **21.6631 us** | **19.2038 us** |  **1.00** |    **0.00** |    **628.9063** |    **628.9063** |    **628.9063** |           **2621824 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False |   470.69 us |  3.8071 us |  3.3749 us |  0.40 |    0.01 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False |   710.56 us | 13.9057 us | 19.0343 us |  0.61 |    0.02 |           - |           - |           - |                40 B |
|                                                |           |             |            |            |       |         |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **440.66 us** |  **8.4026 us** | **17.9066 us** |  **1.00** |    **0.00** |    **553.2227** |    **535.1563** |    **523.9258** |           **2520594 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |    61.38 us |  0.8513 us |  0.7109 us |  0.14 |    0.00 |           - |           - |           - |                40 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |    59.68 us |  0.4915 us |  0.4597 us |  0.14 |    0.00 |           - |           - |           - |                40 B |
