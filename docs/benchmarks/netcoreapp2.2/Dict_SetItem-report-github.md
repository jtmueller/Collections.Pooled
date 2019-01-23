``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **677.4 us** | **3.403 us** | **2.841 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 | 687.7 us | 3.425 us | 2.860 us |  1.02 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictSetItem** |  **10000** | **678.1 us** | **3.834 us** | **3.586 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 | 686.7 us | 3.028 us | 2.833 us |  1.01 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictSetItem** | **100000** | **679.2 us** | **2.690 us** | **2.516 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 | 788.4 us | 5.693 us | 5.325 us |  1.16 |           - |           - |           - |                   - |
