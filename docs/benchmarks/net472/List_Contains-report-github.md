``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio |
|--------------- |------- |-----------:|----------:|----------:|------:|
|   **ListContains** |   **1000** |   **5.875 ms** | **0.0109 ms** | **0.0102 ms** |  **1.00** |
| PooledContains |   1000 |   1.887 ms | 0.0072 ms | 0.0064 ms |  0.32 |
|                |        |            |           |           |       |
|   **ListContains** |  **10000** |  **57.760 ms** | **0.1256 ms** | **0.1175 ms** |  **1.00** |
| PooledContains |  10000 |  17.854 ms | 0.0662 ms | 0.0587 ms |  0.31 |
|                |        |            |           |           |       |
|   **ListContains** | **100000** | **577.398 ms** | **1.5392 ms** | **1.3644 ms** |  **1.00** |
| PooledContains | 100000 | 180.894 ms | 3.6036 ms | 3.0092 ms |  0.31 |
