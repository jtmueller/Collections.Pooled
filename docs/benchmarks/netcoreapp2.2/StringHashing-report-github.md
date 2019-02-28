``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |      N |        Mean |     Error |    StdDev | Ratio |
|------------------------ |------- |------------:|----------:|----------:|------:|
|       **DefaultStringHash** |   **1000** |    **14.10 us** | **0.0149 us** | **0.0125 us** |  **1.00** |
| NonRandomizedStringHash |   1000 |    14.15 us | 0.0403 us | 0.0377 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** |  **10000** |   **141.70 us** | **1.6124 us** | **1.4293 us** |  **1.00** |
| NonRandomizedStringHash |  10000 |   141.96 us | 0.4286 us | 0.4009 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** | **100000** | **1,431.74 us** | **5.0419 us** | **4.7162 us** |  **1.00** |
| NonRandomizedStringHash | 100000 | 1,430.55 us | 2.0937 us | 1.9585 us |  1.00 |
