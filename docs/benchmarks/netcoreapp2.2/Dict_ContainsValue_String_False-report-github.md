``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                           Method |      N |          Mean |       Error |      StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------------- |------- |--------------:|------------:|------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_False** |   **1000** |      **4.788 ms** |   **0.0193 ms** |   **0.0181 ms** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsValue_String_False |   1000 |      5.327 ms |   0.0333 ms |   0.0311 ms |  1.11 |           - |           - |           - |                40 B |
|                                  |        |               |             |             |       |             |             |             |                     |
|   **DictContainsValue_String_False** |  **10000** |    **475.768 ms** |   **4.3750 ms** |   **3.8784 ms** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsValue_String_False |  10000 |    505.144 ms |   2.7513 ms |   2.4390 ms |  1.06 |           - |           - |           - |                40 B |
|                                  |        |               |             |             |       |             |             |             |                     |
|   **DictContainsValue_String_False** | **100000** | **49,026.183 ms** | **181.7180 ms** | **161.0883 ms** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsValue_String_False | 100000 | 52,582.447 ms | 270.8829 ms | 253.3840 ms |  1.07 |           - |           - |           - |                40 B |
