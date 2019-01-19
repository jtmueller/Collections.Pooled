``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |    N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----- |-----------:|----------:|----------:|-----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|       **ListAdd_ValueType** |  **256** |   **6.805 us** | **0.0266 us** | **0.0249 us** |   **6.808 us** |  **1.00** |    **0.00** |     **10.5209** |           **-** |           **-** |             **33056 B** |
|     PooledAdd_ValueType |  256 |   5.365 us | 0.1071 us | 0.1002 us |   5.388 us |  0.79 |    0.01 |      0.0076 |           - |           - |                40 B |
|   ListAdd_ReferenceType |  256 |  12.930 us | 0.2107 us | 0.1868 us |  12.992 us |  1.90 |    0.03 |     20.8282 |           - |           - |             65808 B |
| PooledAdd_ReferenceType |  256 |  13.633 us | 0.0385 us | 0.0360 us |  13.623 us |  2.00 |    0.01 |           - |           - |           - |                40 B |
|                         |      |            |           |           |            |       |         |             |             |             |                     |
|       **ListAdd_ValueType** |  **512** |  **13.204 us** | **0.0237 us** | **0.0222 us** |  **13.203 us** |  **1.00** |    **0.00** |     **20.8282** |           **-** |           **-** |             **65848 B** |
|     PooledAdd_ValueType |  512 |   9.902 us | 0.1940 us | 0.3692 us |   9.844 us |  0.74 |    0.03 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType |  512 |  25.606 us | 0.0840 us | 0.0785 us |  25.583 us |  1.94 |    0.01 |     41.6565 |           - |           - |            131368 B |
| PooledAdd_ReferenceType |  512 |  25.166 us | 0.4997 us | 0.9864 us |  24.673 us |  1.90 |    0.08 |           - |           - |           - |                40 B |
|                         |      |            |           |           |            |       |         |             |             |             |                     |
|       **ListAdd_ValueType** | **2048** |  **97.535 us** | **1.9428 us** | **1.9951 us** |  **96.867 us** |  **1.00** |    **0.00** |     **41.6260** |     **41.6260** |     **41.6260** |            **262504 B** |
|     PooledAdd_ValueType | 2048 |  39.734 us | 0.1255 us | 0.1174 us |  39.728 us |  0.41 |    0.01 |           - |           - |           - |                40 B |
|   ListAdd_ReferenceType | 2048 | 232.558 us | 1.6603 us | 1.3865 us | 232.358 us |  2.39 |    0.05 |    124.7559 |    124.7559 |    124.7559 |            524632 B |
| PooledAdd_ReferenceType | 2048 | 103.888 us | 0.3618 us | 0.3384 us | 103.893 us |  1.07 |    0.02 |           - |           - |           - |                40 B |
