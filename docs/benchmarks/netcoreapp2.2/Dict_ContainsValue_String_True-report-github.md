``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                          Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------------- |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_True** |   **1000** |      **3.791 ms** |  **0.0074 ms** |  **0.0066 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |   1000 |      3.786 ms |  0.0085 ms |  0.0079 ms |  1.00 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_True** |  **10000** |    **338.496 ms** |  **4.0169 ms** |  **3.7574 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True |  10000 |    335.066 ms |  4.9963 ms |  4.6735 ms |  0.99 |           - |           - |           - |                   - |
|                                 |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_True** | **100000** | **36,708.432 ms** | **69.2058 ms** | **54.0313 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_String_True | 100000 | 36,853.838 ms | 34.5931 ms | 28.8868 ms |  1.00 |           - |           - |           - |                   - |
