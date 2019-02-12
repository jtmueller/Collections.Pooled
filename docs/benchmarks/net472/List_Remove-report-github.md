``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|              Method |     N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------ |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListRemove_Int** |  **3000** |   **2.552 ms** | **0.0506 ms** | **0.0741 ms** |   **2.562 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.659 ms | 0.0545 ms | 0.1538 ms |   2.592 ms |  1.04 |    0.07 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  24.060 ms | 0.4610 ms | 0.5994 ms |  23.930 ms |  9.44 |    0.35 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  23.834 ms | 0.3836 ms | 0.3400 ms |  23.862 ms |  9.35 |    0.24 |           - |           - |           - |                   - |
|                     |       |            |           |           |            |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **25.915 ms** | **0.5176 ms** | **0.8059 ms** |  **25.647 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  25.495 ms | 0.3000 ms | 0.2505 ms |  25.492 ms |  0.99 |    0.03 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 265.168 ms | 2.0613 ms | 1.9282 ms | 264.691 ms | 10.25 |    0.33 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 265.871 ms | 2.0636 ms | 1.9303 ms | 265.766 ms | 10.28 |    0.33 |           - |           - |           - |                   - |
