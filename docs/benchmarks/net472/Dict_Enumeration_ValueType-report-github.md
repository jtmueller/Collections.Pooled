``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                      Method |     N |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------------- |------ |----------:|----------:|----------:|------:|--------:|
|   **DictEnumeration_ValueType** |  **1024** |  **12.61 us** | **0.2469 us** | **0.2939 us** |  **1.00** |    **0.00** |
| PooledEnumeration_ValueType |  1024 |  12.57 us | 0.2489 us | 0.3407 us |  1.00 |    0.04 |
|                             |       |           |           |           |       |         |
|   **DictEnumeration_ValueType** |  **8192** | **103.14 us** | **2.0497 us** | **2.3604 us** |  **1.00** |    **0.00** |
| PooledEnumeration_ValueType |  8192 | 104.32 us | 0.2082 us | 0.1846 us |  1.01 |    0.03 |
|                             |       |           |           |           |       |         |
|   **DictEnumeration_ValueType** | **16384** | **206.11 us** | **4.0435 us** | **4.9658 us** |  **1.00** |    **0.00** |
| PooledEnumeration_ValueType | 16384 | 186.93 us | 5.0239 us | 5.5841 us |  0.91 |    0.04 |
