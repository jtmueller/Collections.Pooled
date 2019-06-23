``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|    Method |  Job | Runtime |       Kind |             Mean |           Error |          StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------- |-----------------:|----------------:|----------------:|------:|--------:|------:|------:|------:|----------:|
|   **HashSet** |  **Clr** |     **Clr** |        **Set** |         **11.17 ns** |       **0.0279 ns** |       **0.0247 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledSet |  Clr |     Clr |        Set |         13.52 ns |       0.0975 ns |       0.0865 ns |  1.21 |    0.01 |     - |     - |     - |         - |
|           |      |         |            |                  |                 |                 |       |         |       |       |       |           |
|   HashSet | Core |    Core |        Set |         12.83 ns |       0.2934 ns |       0.3139 ns |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |        Set |         12.23 ns |       0.0659 ns |       0.0616 ns |  0.95 |    0.02 |     - |     - |     - |         - |
|           |      |         |            |                  |                 |                 |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** | **Enumerable** | **12,358,144.98 ns** |  **77,262.1894 ns** |  **68,490.9185 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **12168 B** |
| PooledSet |  Clr |     Clr | Enumerable | 11,668,615.87 ns |  74,738.3764 ns |  62,409.9292 ns |  0.94 |    0.01 |     - |     - |     - |   12168 B |
|           |      |         |            |                  |                 |                 |       |         |       |       |       |           |
|   HashSet | Core |    Core | Enumerable | 11,826,328.54 ns | 131,018.9833 ns | 122,555.2383 ns |  1.00 |    0.00 |     - |     - |     - |   12056 B |
| PooledSet | Core |    Core | Enumerable | 11,830,616.67 ns | 103,639.3983 ns |  96,944.3574 ns |  1.00 |    0.02 |     - |     - |     - |   12056 B |
|           |      |         |            |                  |                 |                 |       |         |       |       |       |           |
|   **HashSet** |  **Clr** |     **Clr** |      **Array** | **12,182,845.09 ns** | **162,384.2800 ns** | **143,949.4346 ns** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **12168 B** |
| PooledSet |  Clr |     Clr |      Array | 11,772,766.18 ns |  94,800.7294 ns |  84,038.3774 ns |  0.97 |    0.02 |     - |     - |     - |   12168 B |
|           |      |         |            |                  |                 |                 |       |         |       |       |       |           |
|   HashSet | Core |    Core |      Array | 11,618,930.62 ns |  63,870.8746 ns |  59,744.8557 ns |  1.00 |    0.00 |     - |     - |     - |   12048 B |
| PooledSet | Core |    Core |      Array | 11,709,679.17 ns |  86,633.0414 ns |  81,036.6007 ns |  1.01 |    0.01 |     - |     - |     - |   12048 B |
