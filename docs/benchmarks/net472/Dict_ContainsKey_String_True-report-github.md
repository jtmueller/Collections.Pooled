``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                        Method |      N |        Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------------ |------- |------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_String_True** |   **1000** |    **21.63 us** |  **0.0665 us** |  **0.0622 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True |   1000 |    30.04 us |  0.0656 us |  0.0613 us |  1.39 |           - |           - |           - |                   - |
|                               |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_True** |  **10000** |   **253.21 us** |  **0.9919 us** |  **0.9278 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True |  10000 |   312.05 us |  0.7397 us |  0.6557 us |  1.23 |           - |           - |           - |                   - |
|                               |        |             |            |            |       |             |             |             |                     |
|   **DictContainsKey_String_True** | **100000** | **2,558.04 us** | **12.3952 us** | **11.5945 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_String_True | 100000 | 3,918.44 us | 20.1746 us | 18.8713 us |  1.53 |           - |           - |           - |                   - |
