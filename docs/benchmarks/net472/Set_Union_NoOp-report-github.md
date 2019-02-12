``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|               Method | CountToUnion | InitialSetSize |        Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------------- |--------------- |------------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Union_NoOp** |        **32000** |          **32000** |    **815.0 us** |  **6.759 us** |  **6.323 us** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_NoOp |        32000 |          32000 |    609.0 us |  4.982 us |  4.660 us |  0.75 |           - |           - |           - |                   - |
|                      |              |                |             |           |           |       |             |             |             |                     |
|   **HashSet_Union_NoOp** |        **32000** |         **320000** |    **798.4 us** |  **6.509 us** |  **6.088 us** |  **1.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_NoOp |        32000 |         320000 |    561.3 us |  7.690 us |  7.193 us |  0.70 |           - |           - |           - |                   - |
|                      |              |                |             |           |           |       |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |          **32000** | **10,022.4 us** | **80.772 us** | **75.554 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_NoOp |       320000 |          32000 |  8,036.2 us | 78.257 us | 73.201 us |  0.80 |           - |           - |           - |                   - |
|                      |              |                |             |           |           |       |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |         **320000** |  **7,994.0 us** | **87.959 us** | **82.277 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_NoOp |       320000 |         320000 |  5,590.3 us | 62.317 us | 58.292 us |  0.70 |           - |           - |           - |                   - |
