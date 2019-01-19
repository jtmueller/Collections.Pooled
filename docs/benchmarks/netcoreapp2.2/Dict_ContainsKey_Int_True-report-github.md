``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |       Mean |      Error |     StdDev |     Median | Ratio | RatioSD |
|--------------------------- |------- |-----------:|-----------:|-----------:|-----------:|------:|--------:|
|   **DictContainsKey_Int_True** |   **1000** |   **6.367 us** |  **0.1265 us** |  **0.1183 us** |   **6.402 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_True |   1000 |   6.202 us |  0.0125 us |  0.0117 us |   6.203 us |  0.97 |    0.02 |
|                            |        |            |            |            |            |       |         |
|   **DictContainsKey_Int_True** |  **10000** |  **63.221 us** |  **1.2458 us** |  **2.0469 us** |  **64.195 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_True |  10000 |  63.026 us |  1.2035 us |  1.0669 us |  62.508 us |  1.01 |    0.06 |
|                            |        |            |            |            |            |       |         |
|   **DictContainsKey_Int_True** | **100000** | **642.750 us** |  **3.8619 us** |  **3.4235 us** | **643.362 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_True | 100000 | 602.626 us | 12.0433 us | 24.3280 us | 591.693 us |  0.93 |    0.04 |
