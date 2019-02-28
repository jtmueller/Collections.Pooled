``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |     N |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictEnumeration_ValueType** |  **1024** |  **11.98 us** | **0.0370 us** | **0.0346 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  1024 |  11.40 us | 0.0229 us | 0.0214 us |  0.95 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** |  **8192** |  **98.71 us** | **0.2639 us** | **0.2468 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  8192 |  94.56 us | 0.3264 us | 0.3053 us |  0.96 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** | **16384** | **197.12 us** | **0.3842 us** | **0.3593 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType | 16384 | 188.91 us | 0.4362 us | 0.3642 us |  0.96 |           - |           - |           - |                   - |
