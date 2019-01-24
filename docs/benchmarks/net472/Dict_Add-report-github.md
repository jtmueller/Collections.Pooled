``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **6.343 ms** | **0.1999 ms** | **0.5863 ms** |  **6.201 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204928 B** |
| PooledAdd |   1000 |  4.714 ms | 0.1454 ms | 0.4287 ms |  4.559 ms |  0.75 |    0.09 |           - |           - |           - |                   - |
|           |        |           |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** | **10.453 ms** | **0.2397 ms** | **0.7031 ms** | **10.267 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645904 B** |
| PooledAdd |  10000 |  8.025 ms | 0.2383 ms | 0.6988 ms |  7.869 ms |  0.77 |    0.08 |           - |           - |           - |                   - |
|           |        |           |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **11.857 ms** | **0.3133 ms** | **0.9089 ms** | **11.626 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  9.068 ms | 0.2694 ms | 0.7858 ms |  8.888 ms |  0.77 |    0.09 |           - |           - |           - |                   - |
