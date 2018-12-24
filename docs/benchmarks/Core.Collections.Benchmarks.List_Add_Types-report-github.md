``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |    N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **7.831 us** | **0.0425 us** | **0.0376 us** |  **1.00** |    **0.00** |     **10.5133** |           **-** |           **-** |             **33056 B** |
|     PooledAdd_ValueType |  256 |   5.448 us | 0.0596 us | 0.0557 us |  0.70 |    0.01 |      0.0076 |           - |           - |                32 B |
|   ListAdd_ReferenceType |  256 |  14.951 us | 0.1003 us | 0.0889 us |  1.91 |    0.02 |     20.8282 |           - |           - |             65808 B |
| PooledAdd_ReferenceType |  256 |  13.736 us | 0.1752 us | 0.1639 us |  1.76 |    0.02 |           - |           - |           - |                32 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **15.257 us** | **0.0634 us** | **0.0562 us** |  **1.00** |    **0.00** |     **20.8130** |           **-** |           **-** |             **65848 B** |
|     PooledAdd_ValueType |  512 |  10.469 us | 0.1274 us | 0.1129 us |  0.69 |    0.01 |           - |           - |           - |                32 B |
|   ListAdd_ReferenceType |  512 |  28.054 us | 0.2395 us | 0.2240 us |  1.84 |    0.01 |     41.6565 |           - |           - |            131368 B |
| PooledAdd_ReferenceType |  512 |  26.947 us | 0.2452 us | 0.2174 us |  1.77 |    0.02 |           - |           - |           - |                32 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** | **107.812 us** | **2.1097 us** | **1.8702 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262504 B** |
|     PooledAdd_ValueType | 2048 |  40.244 us | 0.3764 us | 0.3337 us |  0.37 |    0.01 |           - |           - |           - |                32 B |
|   ListAdd_ReferenceType | 2048 | 268.781 us | 0.6172 us | 0.5472 us |  2.49 |    0.05 |    124.5117 |    124.5117 |    124.5117 |            524632 B |
| PooledAdd_ReferenceType | 2048 | 107.064 us | 1.3930 us | 1.1632 us |  0.99 |    0.02 |           - |           - |           - |                32 B |
