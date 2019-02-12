``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **ListContains** |   **1000** |   **5.366 ms** | **0.0118 ms** | **0.0111 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |   1000 |   1.856 ms | 0.0051 ms | 0.0045 ms |  0.35 |           - |           - |           - |                   - |
|                |        |            |           |           |       |             |             |             |                     |
|   **ListContains** |  **10000** |  **52.746 ms** | **0.2093 ms** | **0.1957 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains |  10000 |  17.633 ms | 0.0515 ms | 0.0482 ms |  0.33 |           - |           - |           - |                   - |
|                |        |            |           |           |       |             |             |             |                     |
|   **ListContains** | **100000** | **525.606 ms** | **1.8710 ms** | **1.7502 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContains | 100000 | 175.320 ms | 0.5458 ms | 0.4838 ms |  0.33 |           - |           - |           - |                   - |
