``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |        Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |    **24.17 us** |   **0.2996 us** |   **0.2803 us** |  **1.00** |    **128.0212** |           **-** |           **-** |            **403200 B** |
| PooledSetCapacity |   1000 |    12.28 us |   0.1322 us |   0.1237 us |  0.51 |           - |           - |           - |                   - |
|                   |        |             |             |             |       |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |   **284.23 us** |   **1.7574 us** |   **1.4675 us** |  **1.00** |   **1265.6250** |           **-** |           **-** |           **4003200 B** |
| PooledSetCapacity |  10000 |   110.22 us |   0.3365 us |   0.3148 us |  0.39 |           - |           - |           - |                   - |
|                   |        |             |             |             |       |             |             |             |                     |
|   **ListSetCapacity** | **100000** | **9,121.51 us** | **121.6882 us** | **113.8273 us** |  **1.00** |   **9359.3750** |   **9359.3750** |   **9359.3750** |          **40003200 B** |
| PooledSetCapacity | 100000 | 1,427.30 us |   3.2928 us |   2.7496 us |  0.16 |           - |           - |           - |                   - |
