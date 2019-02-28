``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictSetItem** |   **1000** | **668.3 us** | **1.848 us** | **1.728 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |   1000 | 691.0 us | 2.478 us | 2.317 us |  1.03 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictSetItem** |  **10000** | **667.5 us** | **1.386 us** | **1.296 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem |  10000 | 690.8 us | 2.520 us | 2.234 us |  1.03 |           - |           - |           - |                   - |
|               |        |          |          |          |       |             |             |             |                     |
|   **DictSetItem** | **100000** | **668.1 us** | **2.246 us** | **2.101 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSetItem | 100000 | 691.0 us | 2.369 us | 2.216 us |  1.03 |           - |           - |           - |                   - |
