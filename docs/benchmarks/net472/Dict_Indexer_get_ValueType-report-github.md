``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |     N |       Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |-----------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictIndexer_get_ValueType** |  **1024** |   **250.9 us** |  **0.8727 us** |  **0.7737 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  1024 |   240.5 us |  0.5331 us |  0.4726 us |  0.96 |           - |           - |           - |                   - |
|                             |       |            |            |            |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** |  **8192** | **2,020.0 us** | **10.6821 us** |  **9.9920 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  8192 | 1,934.9 us |  5.3365 us |  4.9918 us |  0.96 |           - |           - |           - |                   - |
|                             |       |            |            |            |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** | **16384** | **4,035.6 us** | **22.2539 us** | **19.7275 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType | 16384 | 3,889.5 us |  5.2026 us |  4.8665 us |  0.96 |           - |           - |           - |                   - |
