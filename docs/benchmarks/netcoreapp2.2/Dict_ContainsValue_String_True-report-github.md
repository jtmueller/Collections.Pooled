``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                          Method |      N |          Mean |       Error |      StdDev | Ratio | RatioSD |
|-------------------------------- |------- |--------------:|------------:|------------:|------:|--------:|
|   **DictContainsValue_String_True** |   **1000** |      **3.898 ms** |   **0.0485 ms** |   **0.0430 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_True |   1000 |      4.334 ms |   0.0786 ms |   0.0735 ms |  1.11 |    0.02 |
|                                 |        |               |             |             |       |         |
|   **DictContainsValue_String_True** |  **10000** |    **345.132 ms** |   **2.8862 ms** |   **2.5585 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_True |  10000 |    371.720 ms |   7.1666 ms |  10.0465 ms |  1.07 |    0.04 |
|                                 |        |               |             |             |       |         |
|   **DictContainsValue_String_True** | **100000** | **38,694.100 ms** | **654.9700 ms** | **612.6594 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_True | 100000 | 42,727.952 ms | 218.8039 ms | 204.6693 ms |  1.10 |    0.02 |
