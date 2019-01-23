``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |         Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |-------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** |  **41,659.0 us** |   **392.7764 us** |   **327.9860 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 |     126.6 us |     0.9507 us |     0.7939 us | 0.003 |           - |           - |           - |                   - |
|             |        |              |               |               |       |             |             |             |                     |
|   **ClearList** | **100000** | **414,810.6 us** | **3,273.6554 us** | **2,902.0102 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 |     142.3 us |     2.7668 us |     3.3978 us | 0.000 |           - |           - |           - |                   - |
