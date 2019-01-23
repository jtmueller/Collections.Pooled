``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                        Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_False** |   **1000** |      **1.049 ms** |  **0.0068 ms** |  **0.0063 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False |   1000 |      1.042 ms |  0.0042 ms |  0.0035 ms |  0.99 |           - |           - |           - |                   - |
|                               |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_Int_False** |  **10000** |    **105.039 ms** |  **0.7668 ms** |  **0.7173 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False |  10000 |    104.371 ms |  0.6693 ms |  0.6261 ms |  0.99 |           - |           - |           - |                   - |
|                               |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_Int_False** | **100000** | **10,645.568 ms** | **15.9919 ms** | **14.1764 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False | 100000 | 10,649.485 ms | 46.9583 ms | 39.2123 ms |  1.00 |           - |           - |           - |                   - |
