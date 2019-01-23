``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |      N |        Mean |     Error |    StdDev | Ratio |
|------------------------ |------- |------------:|----------:|----------:|------:|
|       **DefaultStringHash** |   **1000** |    **14.33 us** | **0.0631 us** | **0.0590 us** |  **1.00** |
| NonRandomizedStringHash |   1000 |    14.32 us | 0.0744 us | 0.0660 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** |  **10000** |   **143.18 us** | **0.8066 us** | **0.7545 us** |  **1.00** |
| NonRandomizedStringHash |  10000 |   143.35 us | 0.5679 us | 0.5034 us |  1.00 |
|                         |        |             |           |           |       |
|       **DefaultStringHash** | **100000** | **1,450.02 us** | **4.6781 us** | **3.9065 us** |  **1.00** |
| NonRandomizedStringHash | 100000 | 1,455.81 us | 8.9557 us | 8.3772 us |  1.00 |
