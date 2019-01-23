``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **5.760 ms** | **0.1141 ms** | **0.2529 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204928 B** |
| PooledAdd |   1000 |  4.580 ms | 0.1003 ms | 0.2941 ms |  0.80 |    0.06 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** |  **9.425 ms** | **0.1863 ms** | **0.2612 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645904 B** |
| PooledAdd |  10000 |  7.284 ms | 0.1509 ms | 0.3528 ms |  0.78 |    0.05 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **10.779 ms** | **0.2119 ms** | **0.3422 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  8.081 ms | 0.1680 ms | 0.4794 ms |  0.75 |    0.04 |           - |           - |           - |                   - |
