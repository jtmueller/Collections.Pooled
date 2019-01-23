``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |    N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **6.775 us** | **0.0339 us** | **0.0317 us** |  **1.00** |    **0.00** |     **10.5209** |           **-** |           **-** |             **33056 B** |
|     PooledAdd_ValueType |  256 |   5.045 us | 0.0221 us | 0.0207 us |  0.74 |    0.00 |      0.0076 |           - |           - |                40 B |
|   ListAdd_ReferenceType |  256 |  13.081 us | 0.0668 us | 0.0592 us |  1.93 |    0.01 |     20.8282 |           - |           - |             65808 B |
| PooledAdd_ReferenceType |  256 |  12.418 us | 0.0599 us | 0.0531 us |  1.83 |    0.01 |           - |           - |           - |                40 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **13.446 us** | **0.0515 us** | **0.0481 us** |  **1.00** |    **0.00** |     **20.8282** |           **-** |           **-** |             **65848 B** |
|     PooledAdd_ValueType |  512 |   9.549 us | 0.0510 us | 0.0452 us |  0.71 |    0.00 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType |  512 |  26.133 us | 0.1407 us | 0.1316 us |  1.94 |    0.01 |     41.6565 |           - |           - |            131368 B |
| PooledAdd_ReferenceType |  512 |  24.123 us | 0.1086 us | 0.0963 us |  1.79 |    0.01 |           - |           - |           - |                40 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** | **110.632 us** | **0.3649 us** | **0.3234 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262504 B** |
|     PooledAdd_ValueType | 2048 |  36.812 us | 0.1344 us | 0.1258 us |  0.33 |    0.00 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType | 2048 | 275.013 us | 1.7875 us | 1.6720 us |  2.48 |    0.02 |    124.5117 |    124.5117 |    124.5117 |            524632 B |
| PooledAdd_ReferenceType | 2048 |  94.889 us | 0.4594 us | 0.4073 us |  0.86 |    0.00 |           - |           - |           - |                40 B |
