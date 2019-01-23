``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                         Method |      N |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |------- |------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False** |   **1000** |    **13.76 us** |  **0.2858 us** |  **0.8337 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False |   1000 |    12.26 us |  0.2414 us |  0.2874 us |  0.85 |    0.04 |           - |           - |           - |                40 B |
|                                |        |             |            |            |       |         |             |             |             |                     |
|   **DictContainsKey_String_False** |  **10000** |   **167.28 us** |  **3.1838 us** |  **4.0265 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False |  10000 |   111.73 us |  1.9184 us |  1.6020 us |  0.67 |    0.02 |           - |           - |           - |                40 B |
|                                |        |             |            |            |       |         |             |             |             |                     |
|   **DictContainsKey_String_False** | **100000** | **1,552.16 us** | **18.2707 us** | **16.1965 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False | 100000 | 1,120.09 us | 21.7039 us | 21.3162 us |  0.72 |    0.02 |           - |           - |           - |                40 B |
