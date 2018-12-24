``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                         Method | LargeSets |        Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |------------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_Int_CapacityIncrease** |     **False** | **1,205.99 us** | **3.6620 us** | **3.4255 us** |  **1.00** |    **660.1563** |    **660.1563** |    **660.1563** |           **2621824 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |     False |   504.58 us | 1.7442 us | 1.5462 us |  0.42 |           - |           - |           - |                32 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False |   727.22 us | 3.2462 us | 2.7107 us |  0.60 |           - |           - |           - |                32 B |
|                                                |           |             |           |           |       |             |             |             |                     |
|              **ListAddRange_Int_CapacityIncrease** |      **True** |   **478.07 us** | **7.0145 us** | **5.8574 us** |  **1.00** |    **551.2695** |    **526.8555** |    **521.9727** |           **2520976 B** |
|      PooledAddRange_Int_CapacityIncrease_Array |      True |    59.97 us | 0.3419 us | 0.2855 us |  0.13 |           - |           - |           - |                32 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True |    60.58 us | 0.5025 us | 0.4701 us |  0.13 |           - |           - |           - |                32 B |
