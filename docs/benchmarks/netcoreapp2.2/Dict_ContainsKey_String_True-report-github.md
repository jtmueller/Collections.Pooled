``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                        Method |      N |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_True** |   **1000** |    **17.62 us** |  **0.1022 us** |  **0.0956 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True |   1000 |    19.59 us |  0.1305 us |  0.1156 us |  1.11 |           - |           - |           - |                   - |
|                               |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_True** |  **10000** |   **216.62 us** |  **0.7236 us** |  **0.6042 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True |  10000 |   209.10 us |  0.9899 us |  0.9259 us |  0.97 |           - |           - |           - |                   - |
|                               |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_True** | **100000** | **2,170.94 us** | **16.0586 us** | **15.0212 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True | 100000 | 2,660.50 us | 21.2414 us | 19.8693 us |  1.23 |           - |           - |           - |                   - |
