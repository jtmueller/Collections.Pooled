``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     N |       Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |-----------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictIndexer_get_ValueType** |  **1024** |   **248.5 us** |  **0.9627 us** |  **0.9005 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  1024 |   242.3 us |  0.9346 us |  0.8742 us |  0.98 |           - |           - |           - |                   - |
|                             |       |            |            |            |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** |  **8192** | **1,998.5 us** |  **6.8014 us** |  **6.0292 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  8192 | 1,949.9 us |  5.8133 us |  5.1534 us |  0.98 |           - |           - |           - |                   - |
|                             |       |            |            |            |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** | **16384** | **3,991.3 us** | **13.0004 us** | **11.5245 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType | 16384 | 3,889.6 us |  7.3702 us |  6.1544 us |  0.97 |           - |           - |           - |                   - |
