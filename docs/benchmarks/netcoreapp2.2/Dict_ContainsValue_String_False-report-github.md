``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                           Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------------- |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_String_False** |   **1000** |      **4.754 ms** |  **0.0349 ms** |  **0.0292 ms** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsValue_String_False |   1000 |      4.742 ms |  0.0235 ms |  0.0208 ms |  1.00 |           - |           - |           - |                40 B |
|                                  |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_False** |  **10000** |    **470.153 ms** |  **2.7719 ms** |  **2.3147 ms** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsValue_String_False |  10000 |    469.287 ms |  1.6795 ms |  1.4888 ms |  1.00 |           - |           - |           - |                40 B |
|                                  |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_String_False** | **100000** | **48,307.662 ms** | **42.5700 ms** | **39.8200 ms** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsValue_String_False | 100000 | 48,193.808 ms | 45.6191 ms | 42.6721 ms |  1.00 |           - |           - |           - |                40 B |
