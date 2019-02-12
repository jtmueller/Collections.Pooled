``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                        Method |      N |          Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |------- |--------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsValue_Int_False** |   **1000** |      **1.915 ms** |  **0.0071 ms** |  **0.0066 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False |   1000 |      1.760 ms |  0.0044 ms |  0.0041 ms |  0.92 |           - |           - |           - |                   - |
|                               |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_Int_False** |  **10000** |    **190.483 ms** |  **0.7102 ms** |  **0.6643 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False |  10000 |    175.045 ms |  0.5016 ms |  0.4188 ms |  0.92 |           - |           - |           - |                   - |
|                               |        |               |            |            |       |             |             |             |                     |
|   **DictContainsValue_Int_False** | **100000** | **18,990.388 ms** | **24.7964 ms** | **21.9813 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsValue_Int_False | 100000 | 17,526.146 ms | 18.5625 ms | 17.3633 ms |  0.92 |           - |           - |           - |                   - |
