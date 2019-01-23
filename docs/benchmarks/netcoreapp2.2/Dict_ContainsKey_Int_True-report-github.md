``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |       Mean |      Error |      StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-----------:|-----------:|------------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_True** |   **1000** |   **6.020 us** |  **0.0551 us** |   **0.0460 us** |   **6.012 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True |   1000 |   5.790 us |  0.0262 us |   0.0245 us |   5.797 us |  0.96 |    0.01 |           - |           - |           - |                   - |
|                            |        |            |            |             |            |       |         |             |             |             |                     |
|   **DictContainsKey_Int_True** |  **10000** |  **59.681 us** |  **0.3156 us** |   **0.2798 us** |  **59.697 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True |  10000 |  57.700 us |  0.2134 us |   0.1782 us |  57.696 us |  0.97 |    0.01 |           - |           - |           - |                   - |
|                            |        |            |            |             |            |       |         |             |             |             |                     |
|   **DictContainsKey_Int_True** | **100000** | **596.784 us** |  **3.3150 us** |   **3.1009 us** | **596.970 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True | 100000 | 851.594 us | 71.7982 us | 211.6987 us | 787.938 us |  1.18 |    0.08 |           - |           - |           - |                   - |
