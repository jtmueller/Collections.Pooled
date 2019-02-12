``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |      N |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------- |-----------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_False** |   **1000** |   **5.027 us** |  **0.0286 us** |  **0.0268 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |   1000 |   4.423 us |  0.0134 us |  0.0119 us |  0.88 |    0.01 |           - |           - |           - |                   - |
|                             |        |            |            |            |       |         |             |             |             |                     |
|   **DictContainsKey_Int_False** |  **10000** |  **49.965 us** |  **0.1690 us** |  **0.1580 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |  10000 |  44.102 us |  0.1222 us |  0.1143 us |  0.88 |    0.00 |           - |           - |           - |                   - |
|                             |        |            |            |            |       |         |             |             |             |                     |
|   **DictContainsKey_Int_False** | **100000** | **499.847 us** |  **2.5882 us** |  **2.0207 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False | 100000 | 524.893 us | 11.8543 us | 22.5540 us |  1.07 |    0.07 |           - |           - |           - |                   - |
