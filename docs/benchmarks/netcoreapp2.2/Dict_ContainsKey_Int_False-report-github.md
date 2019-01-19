``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |      N |       Mean |      Error |     StdDev |     Median | Ratio | RatioSD |
|---------------------------- |------- |-----------:|-----------:|-----------:|-----------:|------:|--------:|
|   **DictContainsKey_Int_False** |   **1000** |   **5.488 us** |  **0.1079 us** |  **0.1108 us** |   **5.493 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_False |   1000 |   5.359 us |  0.1054 us |  0.2031 us |   5.459 us |  0.96 |    0.03 |
|                             |        |            |            |            |            |       |         |
|   **DictContainsKey_Int_False** |  **10000** |  **51.698 us** |  **1.0076 us** |  **0.9425 us** |  **51.370 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_False |  10000 |  55.888 us |  1.1135 us |  2.0639 us |  54.656 us |  1.08 |    0.05 |
|                             |        |            |            |            |            |       |         |
|   **DictContainsKey_Int_False** | **100000** | **537.854 us** | **10.0186 us** |  **8.8812 us** | **540.035 us** |  **1.00** |    **0.00** |
| PooledContainsKey_Int_False | 100000 | 536.913 us | 10.1990 us | 10.9129 us | 540.034 us |  1.00 |    0.03 |
