``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |           Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------- |------- |---------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_True** |   **1000** |       **523.9 us** |      **3.739 us** |      **3.315 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |   1000 |       525.2 us |      3.072 us |      2.874 us |  1.00 |           - |           - |           - |                   - |
|                              |        |                |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** |  **10000** |    **51,785.2 us** |    **192.784 us** |    **170.898 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True |  10000 |    51,764.9 us |    242.472 us |    214.945 us |  1.00 |           - |           - |           - |                   - |
|                              |        |                |               |               |       |             |             |             |                     |
|   **DictContainsValue_Int_True** | **100000** | **5,245,491.4 us** | **12,166.472 us** | **11,380.526 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_True | 100000 | 5,251,339.6 us | 11,936.348 us | 11,165.267 us |  1.00 |           - |           - |           - |                   - |
