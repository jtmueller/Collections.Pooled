``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |      N |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD |
|------------------------ |------- |------------:|-----------:|-----------:|------------:|------:|--------:|
|       **DefaultStringHash** |   **1000** |    **15.46 us** |  **0.0741 us** |  **0.0693 us** |    **15.43 us** |  **1.00** |    **0.00** |
| NonRandomizedStringHash |   1000 |    15.19 us |  0.3012 us |  0.4948 us |    15.45 us |  0.98 |    0.03 |
|                         |        |             |            |            |             |       |         |
|       **DefaultStringHash** |  **10000** |   **154.82 us** |  **0.3493 us** |  **0.3096 us** |   **154.73 us** |  **1.00** |    **0.00** |
| NonRandomizedStringHash |  10000 |   153.99 us |  2.9647 us |  3.7493 us |   155.12 us |  0.99 |    0.03 |
|                         |        |             |            |            |             |       |         |
|       **DefaultStringHash** | **100000** | **1,573.73 us** | **11.6039 us** | **10.2865 us** | **1,572.34 us** |  **1.00** |    **0.00** |
| NonRandomizedStringHash | 100000 | 1,574.57 us |  7.2007 us |  6.7356 us | 1,571.48 us |  1.00 |    0.01 |
