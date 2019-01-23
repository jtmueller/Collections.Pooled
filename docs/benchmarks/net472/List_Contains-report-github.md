``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListContains** |   **1000** |   **5.353 ms** | **0.0156 ms** | **0.0146 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |   1000 |   1.856 ms | 0.0119 ms | 0.0112 ms |  0.35 |           - |           - |           - |                   - |
|                |        |            |           |           |       |             |             |             |                     |
|   **ListContains** |  **10000** |  **52.892 ms** | **0.3112 ms** | **0.2911 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |  10000 |  17.639 ms | 0.0571 ms | 0.0534 ms |  0.33 |           - |           - |           - |                   - |
|                |        |            |           |           |       |             |             |             |                     |
|   **ListContains** | **100000** | **526.751 ms** | **2.6519 ms** | **2.4806 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains | 100000 | 175.292 ms | 0.3003 ms | 0.2345 ms |  0.33 |           - |           - |           - |                   - |
