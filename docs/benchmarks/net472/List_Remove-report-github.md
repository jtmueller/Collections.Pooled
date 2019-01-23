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
|      **ListRemove_Int** |  **3000** |   **2.715 ms** | **0.0614 ms** | **0.1783 ms** |   **2.662 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int |  3000 |   2.601 ms | 0.0480 ms | 0.0375 ms |   2.587 ms |  1.00 |    0.06 |           - |           - |           - |                   - |
|   ListRemove_String |  3000 |  24.246 ms | 0.5918 ms | 0.6333 ms |  24.173 ms |  9.21 |    0.45 |           - |           - |           - |                   - |
| PooledRemove_String |  3000 |  24.626 ms | 0.4852 ms | 0.5192 ms |  24.454 ms |  9.36 |    0.53 |           - |           - |           - |                   - |
|                     |       |            |           |           |            |       |         |             |             |             |                     |
|      **ListRemove_Int** | **10000** |  **25.880 ms** | **0.4143 ms** | **0.3460 ms** |  **25.966 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledRemove_Int | 10000 |  26.371 ms | 0.5064 ms | 0.6585 ms |  26.345 ms |  1.01 |    0.03 |           - |           - |           - |                   - |
|   ListRemove_String | 10000 | 264.597 ms | 1.0173 ms | 0.9018 ms | 264.474 ms | 10.23 |    0.13 |           - |           - |           - |                   - |
| PooledRemove_String | 10000 | 264.692 ms | 1.3596 ms | 1.2718 ms | 264.644 ms | 10.23 |    0.13 |           - |           - |           - |                   - |
