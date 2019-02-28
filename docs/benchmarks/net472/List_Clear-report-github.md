``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |         Mean |        Error |       StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |-------------:|-------------:|-------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** |  **41,513.8 us** |   **335.243 us** |   **297.184 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 |     133.0 us |     1.991 us |     1.765 us | 0.003 |           - |           - |           - |                   - |
|             |        |              |              |              |       |             |             |             |                     |
|   **ClearList** | **100000** | **418,440.7 us** | **6,352.317 us** | **5,941.961 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 |     145.6 us |     2.850 us |     3.393 us | 0.000 |           - |           - |           - |                   - |
