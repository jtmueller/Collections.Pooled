``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|              Method |     N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------ |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListRemove_Int** |  **3000** |   **2.650 ms** | **0.0528 ms** | **0.1363 ms** |   **2.598 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.656 ms | 0.0529 ms | 0.1316 ms |   2.594 ms |  1.00 |    0.07 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  23.659 ms | 0.4600 ms | 0.4303 ms |  23.597 ms |  8.96 |    0.52 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  23.491 ms | 0.4535 ms | 0.4020 ms |  23.551 ms |  8.91 |    0.46 |           - |           - |           - |                   - |
|                     |       |            |           |           |            |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **25.432 ms** | **0.5013 ms** | **0.4186 ms** |  **25.501 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  25.753 ms | 0.5095 ms | 0.7626 ms |  25.467 ms |  1.01 |    0.03 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 262.251 ms | 0.7381 ms | 0.6163 ms | 262.296 ms | 10.31 |    0.18 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 263.788 ms | 1.5033 ms | 1.4062 ms | 263.445 ms | 10.38 |    0.15 |           - |           - |           - |                   - |
