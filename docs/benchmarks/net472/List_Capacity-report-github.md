``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|            Method |      N |         Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-------------:|-----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **ListSetCapacity** |   **1000** |     **25.01 us** |  **0.1119 us** |  **0.1047 us** |  **1.00** |    **0.00** |    **128.0212** |           **-** |           **-** |            **403302 B** |
| PooledSetCapacity |   1000 |     97.91 us |  0.3313 us |  0.3099 us |  3.91 |    0.02 |           - |           - |           - |                   - |
|                   |        |              |            |            |       |         |             |             |             |                     |
|   **ListSetCapacity** |  **10000** |    **283.24 us** |  **1.7956 us** |  **1.6796 us** |  **1.00** |    **0.00** |   **1265.6250** |           **-** |           **-** |           **4005600 B** |
| PooledSetCapacity |  10000 |  1,437.02 us |  3.4578 us |  3.2344 us |  5.07 |    0.03 |           - |           - |           - |                   - |
|                   |        |              |            |            |       |         |             |             |             |                     |
|   **ListSetCapacity** | **100000** |  **7,277.49 us** | **46.1589 us** | **43.1771 us** |  **1.00** |    **0.00** |   **7820.3125** |   **7796.8750** |   **7796.8750** |          **40003200 B** |
| PooledSetCapacity | 100000 | 12,210.16 us | 29.1606 us | 25.8501 us |  1.68 |    0.01 |           - |           - |           - |                   - |
