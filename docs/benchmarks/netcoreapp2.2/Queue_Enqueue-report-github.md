``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|        Method |      N |   Type |         Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |------- |------- |-------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|  **QueueEnqueue** |   **1000** |    **Int** |     **4.516 us** |  **0.0206 us** |  **0.0183 us** |  **1.00** |      **2.6779** |           **-** |           **-** |              **8440 B** |
| PooledEnqueue |   1000 |    Int |     4.316 us |  0.0162 us |  0.0152 us |  0.96 |      0.0153 |           - |           - |                64 B |
|               |        |        |              |            |            |       |             |             |             |                     |
|  **QueueEnqueue** |   **1000** | **String** |     **6.601 us** |  **0.0268 us** |  **0.0251 us** |  **1.00** |      **5.2719** |           **-** |           **-** |             **16616 B** |
| PooledEnqueue |   1000 | String |     6.865 us |  0.0210 us |  0.0197 us |  1.04 |      0.0153 |           - |           - |                64 B |
|               |        |        |              |            |            |       |             |             |             |                     |
|  **QueueEnqueue** |  **10000** |    **Int** |    **39.782 us** |  **0.4571 us** |  **0.4275 us** |  **1.00** |     **41.6260** |           **-** |           **-** |            **131416 B** |
| PooledEnqueue |  10000 |    Int |    38.930 us |  0.1092 us |  0.0912 us |  0.98 |           - |           - |           - |                64 B |
|               |        |        |              |            |            |       |             |             |             |                     |
|  **QueueEnqueue** |  **10000** | **String** |   **118.502 us** |  **0.3126 us** |  **0.2924 us** |  **1.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262472 B** |
| PooledEnqueue |  10000 | String |    66.053 us |  0.2733 us |  0.2556 us |  0.56 |           - |           - |           - |                64 B |
|               |        |        |              |            |            |       |             |             |             |                     |
|  **QueueEnqueue** | **100000** |    **Int** |   **628.225 us** |  **2.9903 us** |  **2.6509 us** |  **1.00** |    **207.0313** |    **166.9922** |    **165.0391** |           **1049905 B** |
| PooledEnqueue | 100000 |    Int |   379.524 us |  1.1491 us |  1.0748 us |  0.60 |           - |           - |           - |                64 B |
|               |        |        |              |            |            |       |             |             |             |                     |
|  **QueueEnqueue** | **100000** | **String** | **1,230.951 us** | **13.9278 us** | **13.0281 us** |  **1.00** |    **326.1719** |    **285.1563** |    **285.1563** |           **2098583 B** |
| PooledEnqueue | 100000 | String |   664.420 us |  4.4443 us |  4.1572 us |  0.54 |           - |           - |           - |                64 B |
