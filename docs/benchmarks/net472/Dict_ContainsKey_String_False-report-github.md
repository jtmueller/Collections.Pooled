``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                         Method |      N |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False** |   **1000** |    **12.02 us** |  **0.0388 us** |  **0.0324 us** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False |   1000 |    21.69 us |  0.1254 us |  0.1173 us |  1.81 |           - |           - |           - |                40 B |
|                                |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_False** |  **10000** |   **140.22 us** |  **0.7180 us** |  **0.6716 us** |  **1.00** |           **-** |           **-** |           **-** |                **42 B** |
| PooledContainsKey_String_False |  10000 |   226.20 us |  2.0535 us |  1.9209 us |  1.61 |           - |           - |           - |                42 B |
|                                |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_False** | **100000** | **1,463.59 us** |  **3.3153 us** |  **3.1011 us** |  **1.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledContainsKey_String_False | 100000 | 2,284.00 us | 12.2633 us | 11.4711 us |  1.56 |           - |           - |           - |                64 B |
