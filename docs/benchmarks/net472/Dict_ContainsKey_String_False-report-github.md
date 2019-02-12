``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                         Method |      N |        Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------- |------- |------------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_False** |   **1000** |    **12.30 us** | **0.0411 us** | **0.0343 us** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledContainsKey_String_False |   1000 |    20.36 us | 0.0666 us | 0.0590 us |  1.66 |           - |           - |           - |                40 B |
|                                |        |             |           |           |       |             |             |             |                     |
|   **DictContainsKey_String_False** |  **10000** |   **138.44 us** | **0.4346 us** | **0.3853 us** |  **1.00** |           **-** |           **-** |           **-** |                **42 B** |
| PooledContainsKey_String_False |  10000 |   205.33 us | 0.4378 us | 0.4095 us |  1.48 |           - |           - |           - |                42 B |
|                                |        |             |           |           |       |             |             |             |                     |
|   **DictContainsKey_String_False** | **100000** | **1,457.51 us** | **3.7702 us** | **3.1483 us** |  **1.00** |           **-** |           **-** |           **-** |                **48 B** |
| PooledContainsKey_String_False | 100000 | 2,049.63 us | 8.6432 us | 7.2175 us |  1.41 |           - |           - |           - |                64 B |
