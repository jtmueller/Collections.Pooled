``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                           Method |      N |          Mean |       Error |      StdDev | Ratio | RatioSD |
|--------------------------------- |------- |--------------:|------------:|------------:|------:|--------:|
|   **DictContainsValue_String_False** |   **1000** |      **4.764 ms** |   **0.0295 ms** |   **0.0247 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_False |   1000 |      5.174 ms |   0.0301 ms |   0.0281 ms |  1.09 |    0.01 |
|                                  |        |               |             |             |       |         |
|   **DictContainsValue_String_False** |  **10000** |    **472.406 ms** |   **1.7063 ms** |   **1.5126 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_False |  10000 |    507.007 ms |   9.7949 ms |  10.8870 ms |  1.07 |    0.03 |
|                                  |        |               |             |             |       |         |
|   **DictContainsValue_String_False** | **100000** | **49,583.533 ms** | **257.4693 ms** | **240.8370 ms** |  **1.00** |    **0.00** |
| PooledContainsValue_String_False | 100000 | 53,152.316 ms | 324.9168 ms | 271.3202 ms |  1.07 |    0.01 |
