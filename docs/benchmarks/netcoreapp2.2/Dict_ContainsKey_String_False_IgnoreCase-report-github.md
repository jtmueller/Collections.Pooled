``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                                    Method |      N |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------ |------- |------------:|-----------:|-----------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False_IgnoreCase** |   **1000** |    **60.68 us** |   **1.176 us** |   **1.761 us** |    **60.13 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase |   1000 |    64.34 us |   1.400 us |   1.310 us |    63.90 us |  1.06 |    0.05 |           - |           - |           - |                40 B |
|                                           |        |             |            |            |             |       |         |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** |  **10000** |   **620.34 us** |  **12.352 us** |  **21.307 us** |   **623.84 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase |  10000 |   632.99 us |  13.754 us |  33.740 us |   614.79 us |  1.03 |    0.09 |           - |           - |           - |                40 B |
|                                           |        |             |            |            |             |       |         |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** | **100000** | **5,867.10 us** |  **28.026 us** |  **23.403 us** | **5,868.25 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase | 100000 | 5,561.67 us | 191.838 us | 188.410 us | 5,486.78 us |  0.95 |    0.04 |           - |           - |           - |                40 B |
