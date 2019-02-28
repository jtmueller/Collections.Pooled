``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |     N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictIndexer_get_ValueType** |  **1024** |   **250.5 us** | **0.9257 us** | **0.8659 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  1024 |   242.4 us | 0.5099 us | 0.4769 us |  0.97 |           - |           - |           - |                   - |
|                             |       |            |           |           |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** |  **8192** | **2,017.3 us** | **7.7786 us** | **7.2761 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType |  8192 | 1,951.4 us | 3.8996 us | 3.6477 us |  0.97 |           - |           - |           - |                   - |
|                             |       |            |           |           |       |             |             |             |                     |
|   **DictIndexer_get_ValueType** | **16384** | **4,021.2 us** | **9.2706 us** | **8.2182 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledIndexer_get_ValueType | 16384 | 3,903.8 us | 9.6881 us | 8.5882 us |  0.97 |           - |           - |           - |                   - |
