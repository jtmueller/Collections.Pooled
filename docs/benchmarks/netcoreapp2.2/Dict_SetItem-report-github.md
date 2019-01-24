``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|---------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **782.7 us** |  **8.791 us** |  **7.341 us** | **782.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 | 815.0 us | 16.145 us | 39.605 us | 801.7 us |  1.03 |    0.05 |           - |           - |           - |                   - |
|               |        |          |           |           |          |       |         |             |             |             |                     |
|   **DictSetItem** |  **10000** | **811.0 us** | **17.815 us** | **30.730 us** | **802.9 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 | 809.3 us | 19.631 us | 26.207 us | 804.9 us |  1.00 |    0.05 |           - |           - |           - |                   - |
|               |        |          |           |           |          |       |         |             |             |             |                     |
|   **DictSetItem** | **100000** | **812.2 us** | **16.142 us** | **23.660 us** | **806.0 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 | 811.2 us | 16.004 us | 28.447 us | 811.6 us |  1.00 |    0.05 |           - |           - |           - |                   - |
