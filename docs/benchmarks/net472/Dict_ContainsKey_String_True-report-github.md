``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                        Method |      N |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_True** |   **1000** |    **21.79 us** |  **0.1222 us** |  **0.1083 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True |   1000 |    30.07 us |  0.1344 us |  0.1257 us |  1.38 |           - |           - |           - |                   - |
|                               |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_True** |  **10000** |   **254.97 us** |  **0.8798 us** |  **0.8230 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True |  10000 |   310.40 us |  1.1584 us |  1.0269 us |  1.22 |           - |           - |           - |                   - |
|                               |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_True** | **100000** | **2,548.79 us** | **11.1168 us** |  **9.8548 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True | 100000 | 3,908.79 us | 14.5231 us | 12.8744 us |  1.53 |           - |           - |           - |                   - |
