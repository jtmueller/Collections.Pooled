``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|              Method |     N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------ |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListRemove_Int** |  **3000** |   **2.562 ms** | **0.0495 ms** | **0.0486 ms** |   **2.567 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.623 ms | 0.0522 ms | 0.1472 ms |   2.559 ms |  1.01 |    0.05 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  26.435 ms | 0.4927 ms | 0.9840 ms |  26.333 ms | 10.36 |    0.40 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  23.681 ms | 0.4436 ms | 0.3704 ms |  23.683 ms |  9.26 |    0.20 |           - |           - |           - |                   - |
|                     |       |            |           |           |            |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **27.577 ms** | **0.3312 ms** | **0.3098 ms** |  **27.579 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  26.042 ms | 0.5766 ms | 0.6170 ms |  25.798 ms |  0.95 |    0.03 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 287.358 ms | 0.8187 ms | 0.7658 ms | 287.252 ms | 10.42 |    0.13 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 264.896 ms | 1.1914 ms | 1.0561 ms | 264.693 ms |  9.61 |    0.11 |           - |           - |           - |                   - |
