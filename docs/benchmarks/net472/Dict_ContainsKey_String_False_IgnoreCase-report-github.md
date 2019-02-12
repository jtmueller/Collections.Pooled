``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                    Method |      N |        Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------------------ |------- |------------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False_IgnoreCase** |   **1000** |    **26.87 us** | **0.0611 us** | **0.0572 us** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False_IgnoreCase |   1000 |    26.35 us | 0.0721 us | 0.0675 us |  0.98 |           - |           - |           - |                40 B |
|                                           |        |             |           |           |       |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** |  **10000** |   **268.80 us** | **2.0596 us** | **1.8258 us** |  **1.00** |           **-** |           **-** |           **-** |                **44 B** |
| PooledContainsKey_String_False_IgnoreCase |  10000 |   272.08 us | 1.0639 us | 0.9951 us |  1.01 |           - |           - |           - |                44 B |
|                                           |        |             |           |           |       |             |             |             |                     |
|   **DictContainsKey_String_False_IgnoreCase** | **100000** | **2,689.53 us** | **6.8311 us** | **6.0556 us** |  **1.00** |           **-** |           **-** |           **-** |                **64 B** |
| PooledContainsKey_String_False_IgnoreCase | 100000 | 2,634.72 us | 6.5607 us | 5.8159 us |  0.98 |           - |           - |           - |                64 B |
