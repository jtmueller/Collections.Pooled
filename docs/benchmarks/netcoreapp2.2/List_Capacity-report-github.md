``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |      N |         Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |     **30.73 us** |   **0.4379 us** |   **0.3656 us** |  **1.00** |    **127.9907** |           **-** |           **-** |            **403200 B** |
| PooledSetCapacity |   1000 |     12.36 us |   0.1690 us |   0.1581 us |  0.40 |           - |           - |           - |                   - |
|                   |        |              |             |             |       |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |    **354.48 us** |   **4.2444 us** |   **3.5443 us** |  **1.00** |   **1265.6250** |           **-** |           **-** |           **4003200 B** |
| PooledSetCapacity |  10000 |    111.28 us |   0.8479 us |   0.7517 us |  0.31 |           - |           - |           - |                   - |
|                   |        |              |             |             |       |             |             |             |                     |
|   **ListSetCapacity** | **100000** | **10,140.64 us** | **201.5095 us** | **197.9093 us** |  **1.00** |   **9203.1250** |   **9203.1250** |   **9203.1250** |          **40003200 B** |
| PooledSetCapacity | 100000 |  1,450.84 us |  17.1561 us |  16.0478 us |  0.14 |           - |           - |           - |                   - |
