``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictGetKeys** |   **1000** | **59.50 us** | **0.4403 us** | **0.3903 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |   1000 | 76.88 us | 0.3915 us | 0.3269 us |  1.29 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** |  **10000** | **59.38 us** | **0.2386 us** | **0.2115 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys |  10000 | 77.01 us | 0.2582 us | 0.2415 us |  1.30 |           - |           - |           - |                   - |
|               |        |          |           |           |       |             |             |             |                     |
|   **DictGetKeys** | **100000** | **88.86 us** | **0.6094 us** | **0.5089 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledGetKeys | 100000 | 71.16 us | 0.4592 us | 0.4296 us |  0.80 |           - |           - |           - |                   - |
