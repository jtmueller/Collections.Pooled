``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |      N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_False** |   **1000** |   **5.872 us** | **0.0167 us** | **0.0157 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |   1000 |   4.667 us | 0.0109 us | 0.0102 us |  0.79 |           - |           - |           - |                   - |
|                             |        |            |           |           |       |             |             |             |                     |
|   **DictContainsKey_Int_False** |  **10000** |  **58.769 us** | **0.1607 us** | **0.1425 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |  10000 |  44.180 us | 0.1610 us | 0.1506 us |  0.75 |           - |           - |           - |                   - |
|                             |        |            |           |           |       |             |             |             |                     |
|   **DictContainsKey_Int_False** | **100000** | **588.734 us** | **8.5577 us** | **7.1461 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False | 100000 | 441.095 us | 1.7512 us | 1.4624 us |  0.75 |           - |           - |           - |                   - |
