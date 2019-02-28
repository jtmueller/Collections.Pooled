``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_True** |   **1000** |   **6.057 us** | **0.1252 us** | **0.2558 us** |   **5.948 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True |   1000 |   5.724 us | 0.0238 us | 0.0222 us |   5.730 us |  0.91 |    0.05 |           - |           - |           - |                   - |
|                            |        |            |           |           |            |       |         |             |             |             |                     |
|   **DictContainsKey_Int_True** |  **10000** |  **58.777 us** | **0.1270 us** | **0.1126 us** |  **58.779 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True |  10000 |  57.173 us | 0.2322 us | 0.2172 us |  57.157 us |  0.97 |    0.00 |           - |           - |           - |                   - |
|                            |        |            |           |           |            |       |         |             |             |             |                     |
|   **DictContainsKey_Int_True** | **100000** | **588.909 us** | **1.4976 us** | **1.4008 us** | **588.931 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True | 100000 | 572.838 us | 3.0431 us | 2.8465 us | 571.810 us |  0.97 |    0.00 |           - |           - |           - |                   - |
