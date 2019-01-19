``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                        Method |      N |          Mean |      Error |     StdDev | Ratio |
|------------------------------ |------- |--------------:|-----------:|-----------:|------:|
|   **DictContainsValue_Int_False** |   **1000** |      **1.119 ms** |  **0.0025 ms** |  **0.0023 ms** |  **1.00** |
| PooledContainsValue_Int_False |   1000 |      1.118 ms |  0.0132 ms |  0.0103 ms |  1.00 |
|                               |        |               |            |            |       |
|   **DictContainsValue_Int_False** |  **10000** |    **112.612 ms** |  **0.4375 ms** |  **0.3879 ms** |  **1.00** |
| PooledContainsValue_Int_False |  10000 |    111.758 ms |  2.0788 ms |  1.9445 ms |  1.00 |
|                               |        |               |            |            |       |
|   **DictContainsValue_Int_False** | **100000** | **11,460.679 ms** | **60.3230 ms** | **56.4262 ms** |  **1.00** |
| PooledContainsValue_Int_False | 100000 | 11,378.696 ms | 62.5115 ms | 58.4733 ms |  0.99 |
