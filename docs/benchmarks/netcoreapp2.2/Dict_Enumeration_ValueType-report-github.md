``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     N |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |------ |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictEnumeration_ValueType** |  **1024** |  **11.42 us** | **0.0440 us** | **0.0390 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  1024 |  11.75 us | 0.0287 us | 0.0268 us |  1.03 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** |  **8192** |  **91.20 us** | **0.2671 us** | **0.2499 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType |  8192 |  91.59 us | 0.3663 us | 0.3427 us |  1.00 |           - |           - |           - |                   - |
|                             |       |           |           |           |       |             |             |             |                     |
|   **DictEnumeration_ValueType** | **16384** | **182.44 us** | **0.4112 us** | **0.3847 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledEnumeration_ValueType | 16384 | 187.40 us | 0.4635 us | 0.4336 us |  1.03 |           - |           - |           - |                   - |
