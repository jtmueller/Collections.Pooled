``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **6.609 ms** | **0.2384 ms** | **0.6991 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204928 B** |
| PooledAdd |   1000 |  3.657 ms | 0.1275 ms | 0.3718 ms |  0.56 |    0.07 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** | **10.387 ms** | **0.2862 ms** | **0.8258 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645904 B** |
| PooledAdd |  10000 |  5.693 ms | 0.1839 ms | 0.5307 ms |  0.55 |    0.06 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **11.107 ms** | **0.2168 ms** | **0.3798 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  6.971 ms | 0.2392 ms | 0.6977 ms |  0.62 |    0.06 |           - |           - |           - |                   - |
