``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |         Mean |        Error |      StdDev |       Median | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |-------------:|-------------:|------------:|-------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** |  **45,356.5 us** |   **169.892 us** |   **141.87 us** |  **45,355.8 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 |     150.7 us |     7.259 us |    21.29 us |     139.2 us | 0.003 |           - |           - |           - |                   - |
|             |        |              |              |             |              |       |             |             |             |                     |
|   **ClearList** | **100000** | **452,875.4 us** | **1,266.708 us** | **1,057.76 us** | **452,852.5 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 |     168.2 us |     9.779 us |    28.21 us |     153.2 us | 0.000 |           - |           - |           - |                   - |
