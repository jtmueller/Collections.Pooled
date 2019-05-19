``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|          Method |  Job | Runtime |      N |            Mean |          Error |         StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |----- |-------- |------- |----------------:|---------------:|---------------:|------:|--------:|------:|------:|------:|----------:|
|   **Dict_Contains** |  **Clr** |     **Clr** |   **1000** |      **1,029.1 us** |      **13.328 us** |      **12.467 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr |   1000 |        953.7 us |      17.863 us |      16.709 us |  0.93 |    0.02 |     - |     - |     - |         - |
|                 |      |         |        |                 |                |                |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |   1000 |        558.5 us |       6.059 us |       5.668 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core |   1000 |        557.5 us |       8.504 us |       7.954 us |  1.00 |    0.02 |     - |     - |     - |         - |
|                 |      |         |        |                 |                |                |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** |  **10000** |    **101,569.5 us** |   **1,377.891 us** |   **1,288.880 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr |  10000 |     93,333.3 us |   1,122.261 us |     994.855 us |  0.92 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |                 |                |                |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core |  10000 |     56,269.9 us |   1,078.429 us |   1,283.794 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core |  10000 |     56,109.9 us |   1,420.265 us |   1,394.890 us |  1.00 |    0.03 |     - |     - |     - |         - |
|                 |      |         |        |                 |                |                |       |         |       |       |       |           |
|   **Dict_Contains** |  **Clr** |     **Clr** | **100000** | **10,183,966.4 us** |  **81,748.536 us** |  **76,467.631 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| Pooled_Contains |  Clr |     Clr | 100000 |  9,385,733.2 us |  42,564.650 us |  35,543.410 us |  0.92 |    0.01 |     - |     - |     - |         - |
|                 |      |         |        |                 |                |                |       |         |       |       |       |           |
|   Dict_Contains | Core |    Core | 100000 |  5,751,920.8 us | 111,016.868 us | 127,847.190 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_Contains | Core |    Core | 100000 |  5,554,944.7 us |  79,234.575 us |  74,116.070 us |  0.96 |    0.02 |     - |     - |     - |         - |
