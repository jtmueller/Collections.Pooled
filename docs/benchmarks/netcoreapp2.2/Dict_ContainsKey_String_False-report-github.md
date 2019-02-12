``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                         Method |      N |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |------- |------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False** |   **1000** |    **11.18 us** |  **0.0350 us** |  **0.0327 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False |   1000 |    10.65 us |  0.0383 us |  0.0359 us |  0.95 |    0.00 |           - |           - |           - |                40 B |
|                                |        |             |            |            |       |         |             |             |             |                     |
|   **DictContainsKey_String_False** |  **10000** |   **141.64 us** |  **0.5129 us** |  **0.4797 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False |  10000 |   111.63 us |  0.2791 us |  0.2474 us |  0.79 |    0.00 |           - |           - |           - |                40 B |
|                                |        |             |            |            |       |         |             |             |             |                     |
|   **DictContainsKey_String_False** | **100000** | **1,478.97 us** |  **4.4984 us** |  **3.9877 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False | 100000 | 1,286.13 us | 25.5186 us | 65.4138 us |  0.85 |    0.08 |           - |           - |           - |                40 B |
