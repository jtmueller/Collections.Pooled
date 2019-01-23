``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     N |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictEnumeration_ValueType** |  **1024** |  **11.50 us** | **0.0413 us** | **0.0345 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  1024 |  11.47 us | 0.0378 us | 0.0353 us |  1.00 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** |  **8192** |  **91.81 us** | **0.1508 us** | **0.1337 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  8192 |  94.76 us | 0.3919 us | 0.3474 us |  1.03 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** | **16384** | **189.31 us** | **0.5419 us** | **0.5069 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType | 16384 | 189.51 us | 0.5843 us | 0.5179 us |  1.00 |           - |           - |           - |                   - |
