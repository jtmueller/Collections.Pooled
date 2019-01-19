``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     N |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------------- |------ |----------:|----------:|----------:|------:|--------:|
|   **DictEnumeration_ValueType** |  **1024** |  **11.71 us** | **0.2298 us** | **0.3068 us** |  **1.00** |    **0.00** |
| PooledEnumeration_ValueType |  1024 |  12.29 us | 0.2453 us | 0.2920 us |  1.05 |    0.05 |
|                             |       |           |           |           |       |         |
|   **DictEnumeration_ValueType** |  **8192** |  **95.50 us** | **0.6720 us** | **0.6285 us** |  **1.00** |    **0.00** |
| PooledEnumeration_ValueType |  8192 | 102.05 us | 2.0390 us | 2.7911 us |  1.07 |    0.03 |
|                             |       |           |           |           |       |         |
|   **DictEnumeration_ValueType** | **16384** | **190.29 us** | **1.5453 us** | **1.2065 us** |  **1.00** |    **0.00** |
| PooledEnumeration_ValueType | 16384 | 200.46 us | 4.0141 us | 3.3520 us |  1.05 |    0.02 |
