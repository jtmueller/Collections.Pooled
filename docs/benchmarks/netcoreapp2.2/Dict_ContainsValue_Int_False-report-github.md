``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                        Method |      N |          Mean |       Error |      StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |------- |--------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_False** |   **1000** |      **1.124 ms** |   **0.0259 ms** |   **0.0242 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False |   1000 |      1.113 ms |   0.0200 ms |   0.0187 ms |  0.99 |    0.02 |           - |           - |           - |                   - |
|                               |        |               |             |             |       |         |             |             |             |                     |
|   **DictContainsValue_Int_False** |  **10000** |    **114.439 ms** |   **2.2753 ms** |   **4.5961 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False |  10000 |    114.590 ms |   2.2762 ms |   4.7007 ms |  1.00 |    0.05 |           - |           - |           - |                   - |
|                               |        |               |             |             |       |         |             |             |             |                     |
|   **DictContainsValue_Int_False** | **100000** | **11,669.245 ms** | **161.2834 ms** | **150.8646 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False | 100000 | 10,534.913 ms |  70.1819 ms |  58.6051 ms |  0.90 |    0.01 |           - |           - |           - |                   - |
