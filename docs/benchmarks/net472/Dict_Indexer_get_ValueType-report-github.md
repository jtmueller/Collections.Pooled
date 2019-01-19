``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                      Method |     N |       Mean |     Error |     StdDev | Ratio | RatioSD |
|---------------------------- |------ |-----------:|----------:|-----------:|------:|--------:|
|   **DictIndexer_get_ValueType** |  **1024** |   **252.1 us** |  **1.056 us** |  **0.8816 us** |  **1.00** |    **0.00** |
| PooledIndexer_get_ValueType |  1024 |   266.9 us |  4.544 us |  4.2506 us |  1.06 |    0.02 |
|                             |       |            |           |            |       |         |
|   **DictIndexer_get_ValueType** |  **8192** | **2,032.2 us** |  **5.563 us** |  **4.6457 us** |  **1.00** |    **0.00** |
| PooledIndexer_get_ValueType |  8192 | 1,987.3 us | 10.463 us |  9.7867 us |  0.98 |    0.01 |
|                             |       |            |           |            |       |         |
|   **DictIndexer_get_ValueType** | **16384** | **4,367.6 us** | **71.398 us** | **63.2923 us** |  **1.00** |    **0.00** |
| PooledIndexer_get_ValueType | 16384 | 4,035.9 us | 98.770 us | 97.0057 us |  0.93 |    0.03 |
