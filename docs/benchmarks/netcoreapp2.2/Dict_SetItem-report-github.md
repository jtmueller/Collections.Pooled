``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|---------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **678.4 us** |  **2.966 us** |  **2.476 us** | **678.2 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 | 723.6 us | 14.448 us | 19.776 us | 735.5 us |  1.06 |    0.03 |           - |           - |           - |                   - |
|               |        |          |           |           |          |       |         |             |             |             |                     |
|   **DictSetItem** |  **10000** | **683.5 us** |  **2.999 us** |  **2.658 us** | **683.1 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 | 691.5 us | 14.544 us | 21.319 us | 681.3 us |  1.02 |    0.04 |           - |           - |           - |                   - |
|               |        |          |           |           |          |       |         |             |             |             |                     |
|   **DictSetItem** | **100000** | **732.2 us** |  **1.757 us** |  **1.643 us** | **731.6 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 | 685.8 us |  2.467 us |  2.060 us | 686.0 us |  0.94 |    0.00 |           - |           - |           - |                   - |
