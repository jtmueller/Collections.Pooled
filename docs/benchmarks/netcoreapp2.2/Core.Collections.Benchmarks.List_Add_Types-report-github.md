``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |    N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **6.166 us** | **0.0144 us** | **0.0134 us** |  **1.00** |    **0.00** |     **10.5209** |           **-** |           **-** |             **33056 B** |
|     PooledAdd_ValueType |  256 |   4.985 us | 0.0181 us | 0.0169 us |  0.81 |    0.00 |      0.0076 |           - |           - |                32 B |
|   ListAdd_ReferenceType |  256 |  12.002 us | 0.0382 us | 0.0357 us |  1.95 |    0.01 |     20.8282 |           - |           - |             65808 B |
| PooledAdd_ReferenceType |  256 |  12.443 us | 0.0147 us | 0.0131 us |  2.02 |    0.01 |           - |           - |           - |                32 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **12.287 us** | **0.0326 us** | **0.0305 us** |  **1.00** |    **0.00** |     **20.8282** |           **-** |           **-** |             **65848 B** |
|     PooledAdd_ValueType |  512 |   9.506 us | 0.0202 us | 0.0179 us |  0.77 |    0.00 |           - |           - |           - |                32 B |
|   ListAdd_ReferenceType |  512 |  23.541 us | 0.0453 us | 0.0402 us |  1.92 |    0.00 |     41.6565 |           - |           - |            131368 B |
| PooledAdd_ReferenceType |  512 |  24.629 us | 0.5184 us | 0.7268 us |  2.03 |    0.07 |           - |           - |           - |                32 B |
|                         |      |            |           |           |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** |  **94.743 us** | **0.2577 us** | **0.2411 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262504 B** |
|     PooledAdd_ValueType | 2048 |  36.441 us | 0.5529 us | 0.4902 us |  0.38 |    0.00 |           - |           - |           - |                32 B |
|   ListAdd_ReferenceType | 2048 | 235.027 us | 0.5032 us | 0.4461 us |  2.48 |    0.01 |    124.7559 |    124.7559 |    124.7559 |            524632 B |
| PooledAdd_ReferenceType | 2048 |  94.932 us | 0.3028 us | 0.2529 us |  1.00 |    0.00 |           - |           - |           - |                32 B |
