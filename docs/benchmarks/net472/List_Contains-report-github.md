``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|         Method |      N |       Mean |     Error |    StdDev | Ratio |
|--------------- |------- |-----------:|----------:|----------:|------:|
|   **ListContains** |   **1000** |   **5.382 ms** | **0.0610 ms** | **0.0541 ms** |  **1.00** |
| PooledContains |   1000 |   1.870 ms | 0.0042 ms | 0.0037 ms |  0.35 |
|                |        |            |           |           |       |
|   **ListContains** |  **10000** |  **52.590 ms** | **0.1811 ms** | **0.1605 ms** |  **1.00** |
| PooledContains |  10000 |  17.614 ms | 0.0526 ms | 0.0466 ms |  0.33 |
|                |        |            |           |           |       |
|   **ListContains** | **100000** | **524.955 ms** | **1.5586 ms** | **1.4579 ms** |  **1.00** |
| PooledContains | 100000 | 174.929 ms | 0.3220 ms | 0.2854 ms |  0.33 |
