``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                    Method |      N |        Mean |       Error |     StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------ |------- |------------:|------------:|-----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False_IgnoreCase** |   **1000** |    **60.22 us** |   **1.2004 us** |  **2.7094 us** |    **59.34 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase |   1000 |    58.84 us |   0.5815 us |  0.5439 us |    58.80 us |  0.95 |    0.05 |           - |           - |           - |                40 B |
|                                           |        |             |             |            |             |       |         |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** |  **10000** |   **676.88 us** |  **13.6626 us** | **37.8590 us** |   **659.01 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase |  10000 |   563.21 us |   5.9433 us |  5.2686 us |   562.68 us |  0.78 |    0.04 |           - |           - |           - |                40 B |
|                                           |        |             |             |            |             |       |         |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** | **100000** | **6,206.67 us** |  **55.8041 us** | **49.4689 us** | **6,193.35 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase | 100000 | 6,266.39 us | 106.7863 us | 99.8880 us | 6,239.62 us |  1.01 |    0.02 |           - |           - |           - |                40 B |
