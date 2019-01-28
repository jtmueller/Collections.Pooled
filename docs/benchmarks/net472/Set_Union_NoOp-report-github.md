``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|               Method | CountToUnion | InitialSetSize |        Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------------- |--------------- |------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Union_NoOp** |        **32000** |          **32000** |    **849.9 us** |   **4.019 us** |   **3.759 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_NoOp |        32000 |          32000 |    627.1 us |   3.177 us |   2.972 us |  0.74 |    0.00 |           - |           - |           - |                   - |
|                      |              |                |             |            |            |       |         |             |             |             |                     |
|   **HashSet_Union_NoOp** |        **32000** |         **320000** |    **834.1 us** |   **4.587 us** |   **4.066 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                **40 B** |
| PooledSet_Union_NoOp |        32000 |         320000 |    575.4 us |   2.766 us |   2.588 us |  0.69 |    0.00 |           - |           - |           - |                   - |
|                      |              |                |             |            |            |       |         |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |          **32000** | **10,448.0 us** |  **94.538 us** |  **88.431 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_NoOp |       320000 |          32000 |  8,380.3 us | 211.740 us | 260.036 us |  0.80 |    0.03 |           - |           - |           - |                   - |
|                      |              |                |             |            |            |       |         |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |         **320000** |  **8,331.5 us** |  **74.275 us** |  **65.842 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_Union_NoOp |       320000 |         320000 |  5,824.4 us |  33.554 us |  31.387 us |  0.70 |    0.01 |           - |           - |           - |                   - |
