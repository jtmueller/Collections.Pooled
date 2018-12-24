``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|      Method |      N |         Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |------- |-------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   **ClearList** |  **10000** |  **41,348.4 us** |   **356.4383 us** |   **278.2835 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled |  10000 |     130.3 us |     1.6035 us |     1.4999 us | 0.003 |           - |           - |           - |                   - |
|             |        |              |               |               |       |             |             |             |                     |
|   **ClearList** | **100000** | **413,960.2 us** | **4,583.0742 us** | **4,287.0104 us** | **1.000** |           **-** |           **-** |           **-** |                   **-** |
| ClearPooled | 100000 |     140.1 us |     0.9747 us |     0.8139 us | 0.000 |           - |           - |           - |                   - |
