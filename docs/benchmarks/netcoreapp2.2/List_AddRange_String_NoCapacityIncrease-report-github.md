``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                              Method | LargeSets |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------------------------------- |---------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|              **ListAddRange_String_NoCapacityIncrease** |     **False** |   **802.4 us** | **2.2347 us** | **1.9810 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |     False | 1,002.1 us | 2.8914 us | 2.7046 us |  1.25 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |     False |   817.5 us | 4.9216 us | 4.6036 us |  1.02 |           - |           - |           - |                   - |
|                                                     |           |            |           |           |       |             |             |             |                     |
|              **ListAddRange_String_NoCapacityIncrease** |      **True** |   **112.0 us** | **0.4394 us** | **0.3895 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|      PooledAddRange_String_NoCapacityIncrease_Array |      True |   114.1 us | 0.7944 us | 0.7431 us |  1.02 |           - |           - |           - |                   - |
| PooledAddRange_String_NoCapacityIncrease_Enumerable |      True |   115.8 us | 0.3495 us | 0.3098 us |  1.03 |           - |           - |           - |                   - |
