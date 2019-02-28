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
|   **DictAdd** |   **1000** |  **5.773 ms** | **0.1137 ms** | **0.2496 ms** |  **5.783 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204928 B** |
| PooledAdd |   1000 |  4.510 ms | 0.1036 ms | 0.2974 ms |  4.416 ms |  0.78 |    0.06 |           - |           - |           - |                   - |
|           |        |           |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** |  **9.497 ms** | **0.1897 ms** | **0.2839 ms** |  **9.577 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645904 B** |
| PooledAdd |  10000 |  7.212 ms | 0.1431 ms | 0.2922 ms |  7.257 ms |  0.76 |    0.04 |           - |           - |           - |                   - |
|           |        |           |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **10.679 ms** | **0.2113 ms** | **0.3701 ms** | **10.643 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  8.077 ms | 0.1610 ms | 0.4461 ms |  8.090 ms |  0.75 |    0.04 |           - |           - |           - |                   - |
