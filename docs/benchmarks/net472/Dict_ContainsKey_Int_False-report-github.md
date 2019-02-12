``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |      N |       Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------- |-----------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_False** |   **1000** |   **5.877 us** |  **0.0106 us** |  **0.0100 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |   1000 |   4.706 us |  0.0166 us |  0.0156 us |  0.80 |           - |           - |           - |                   - |
|                             |        |            |            |            |       |             |             |             |                     |
|   **DictContainsKey_Int_False** |  **10000** |  **58.690 us** |  **0.0886 us** |  **0.0785 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False |  10000 |  47.097 us |  0.1499 us |  0.1402 us |  0.80 |           - |           - |           - |                   - |
|                             |        |            |            |            |       |             |             |             |                     |
|   **DictContainsKey_Int_False** | **100000** | **591.143 us** | **11.4010 us** | **10.1067 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_False | 100000 | 470.216 us |  2.1133 us |  1.9768 us |  0.80 |           - |           - |           - |                   - |
