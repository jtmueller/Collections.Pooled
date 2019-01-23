``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |           Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |---------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_True** |   **1000** |       **534.2 us** |      **4.472 us** |      **3.735 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |   1000 |       533.7 us |      2.441 us |      2.164 us |  1.00 |           - |           - |           - |                   - |
|                              |        |                |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** |  **10000** |    **52,759.4 us** |    **325.137 us** |    **304.134 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |  10000 |    52,673.6 us |    348.078 us |    290.661 us |  1.00 |           - |           - |           - |                   - |
|                              |        |                |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** | **100000** | **5,339,005.1 us** | **12,157.899 us** | **10,777.661 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True | 100000 | 5,329,368.5 us | 19,271.149 us | 17,083.371 us |  1.00 |           - |           - |           - |                   - |
