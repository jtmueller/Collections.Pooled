``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     N |       Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |-----------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictIndexer_get_ValueType** |  **1024** |   **246.8 us** |  **1.0752 us** |  **1.0057 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  1024 |   240.6 us |  0.5830 us |  0.5453 us |  0.98 |           - |           - |           - |                   - |
|                             |       |            |            |            |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** |  **8192** | **1,979.7 us** |  **7.7546 us** |  **7.2537 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  8192 | 1,932.7 us |  2.8611 us |  2.6763 us |  0.98 |           - |           - |           - |                   - |
|                             |       |            |            |            |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** | **16384** | **3,961.0 us** | **13.9187 us** | **12.3386 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType | 16384 | 3,866.0 us |  7.9799 us |  7.4644 us |  0.98 |           - |           - |           - |                   - |
