``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                    Method |      N |        Mean |      Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------ |------- |------------:|-----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False_IgnoreCase** |   **1000** |    **26.90 us** |  **0.0603 us** | **0.0564 us** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase |   1000 |    26.10 us |  0.0431 us | 0.0403 us |  0.97 |           - |           - |           - |                40 B |
|                                           |        |             |            |           |       |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** |  **10000** |   **269.69 us** |  **0.8295 us** | **0.7759 us** |  **1.00** |           **-** |           **-** |           **-** |                **44 B** |
| PooledContainsKey_String_False_IgnoreCase |  10000 |   261.83 us |  1.1252 us | 0.8785 us |  0.97 |           - |           - |           - |                44 B |
|                                           |        |             |            |           |       |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** | **100000** | **2,701.84 us** | **10.2913 us** | **9.6265 us** |  **1.00** |           **-** |           **-** |           **-** |                **64 B** |
| PooledContainsKey_String_False_IgnoreCase | 100000 | 2,649.86 us |  7.8299 us | 6.9410 us |  0.98 |           - |           - |           - |                64 B |
