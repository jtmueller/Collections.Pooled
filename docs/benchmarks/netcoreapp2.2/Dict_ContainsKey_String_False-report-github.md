``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                         Method |      N |        Mean |      Error |     StdDev | Ratio | RatioSD |
|------------------------------- |------- |------------:|-----------:|-----------:|------:|--------:|
|   **DictContainsKey_String_False** |   **1000** |    **11.27 us** |  **0.0469 us** |  **0.0366 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False |   1000 |    10.49 us |  0.0571 us |  0.0477 us |  0.93 |    0.01 |
|                                |        |             |            |            |       |         |
|   **DictContainsKey_String_False** |  **10000** |   **153.41 us** |  **3.0497 us** |  **2.9952 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False |  10000 |   107.06 us |  0.5608 us |  0.5245 us |  0.70 |    0.02 |
|                                |        |             |            |            |       |         |
|   **DictContainsKey_String_False** | **100000** | **1,574.42 us** | **30.6343 us** | **39.8332 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_False | 100000 | 1,147.36 us | 22.7885 us | 25.3294 us |  0.73 |    0.02 |
