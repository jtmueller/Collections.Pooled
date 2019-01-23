``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_False** |   **1000** |   **5.160 us** | **0.1085 us** | **0.1753 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |   1000 |   4.487 us | 0.0140 us | 0.0131 us |  0.85 |    0.03 |           - |           - |           - |                   - |
|                             |        |            |           |           |       |         |             |             |             |                     |
|   **DictContainsKey_Int_False** |  **10000** |  **50.540 us** | **0.1793 us** | **0.1677 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |  10000 |  44.814 us | 0.2960 us | 0.2624 us |  0.89 |    0.00 |           - |           - |           - |                   - |
|                             |        |            |           |           |       |         |             |             |             |                     |
|   **DictContainsKey_Int_False** | **100000** | **506.217 us** | **2.3871 us** | **2.2329 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False | 100000 | 446.333 us | 2.0924 us | 1.7472 us |  0.88 |    0.01 |           - |           - |           - |                   - |
