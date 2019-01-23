``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                          Method |      N |          Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------------- |------- |--------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_True** |   **1000** |      **3.869 ms** |  **0.0259 ms** |  **0.0229 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |   1000 |      3.870 ms |  0.0416 ms |  0.0347 ms |  1.00 |    0.01 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |         |             |             |             |                     |
|   **DictContainsValue_String_True** |  **10000** |    **350.008 ms** |  **3.4700 ms** |  **3.2458 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |  10000 |    337.799 ms |  6.3509 ms |  5.9406 ms |  0.97 |    0.02 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |         |             |             |             |                     |
|   **DictContainsValue_String_True** | **100000** | **37,402.154 ms** | **61.3269 ms** | **51.2108 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True | 100000 | 37,497.173 ms | 73.3371 ms | 65.0114 ms |  1.00 |    0.00 |           - |           - |           - |                   - |
