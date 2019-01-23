``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |     N |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictEnumeration_ValueType** |  **1024** |  **11.98 us** | **0.0760 us** | **0.0593 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  1024 |  11.78 us | 0.0481 us | 0.0426 us |  0.98 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** |  **8192** |  **98.74 us** | **0.3252 us** | **0.2883 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  8192 |  91.67 us | 0.2705 us | 0.2398 us |  0.93 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** | **16384** | **192.78 us** | **0.6335 us** | **0.5926 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType | 16384 | 187.99 us | 0.5582 us | 0.5222 us |  0.98 |           - |           - |           - |                   - |
