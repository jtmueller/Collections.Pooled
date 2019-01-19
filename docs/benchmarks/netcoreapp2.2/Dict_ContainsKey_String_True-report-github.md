``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                        Method |      N |        Mean |      Error |     StdDev | Ratio | RatioSD |
|------------------------------ |------- |------------:|-----------:|-----------:|------:|--------:|
|   **DictContainsKey_String_True** |   **1000** |    **19.22 us** |  **0.0599 us** |  **0.0560 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_True |   1000 |    21.31 us |  0.4229 us |  0.6329 us |  1.11 |    0.03 |
|                               |        |             |            |            |       |         |
|   **DictContainsKey_String_True** |  **10000** |   **230.70 us** |  **0.4712 us** |  **0.4407 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_True |  10000 |   228.46 us |  0.3493 us |  0.2917 us |  0.99 |    0.00 |
|                               |        |             |            |            |       |         |
|   **DictContainsKey_String_True** | **100000** | **2,489.78 us** | **52.3127 us** | **92.9857 us** |  **1.00** |    **0.00** |
| PooledContainsKey_String_True | 100000 | 2,877.46 us | 52.1858 us | 46.2614 us |  1.15 |    0.04 |
