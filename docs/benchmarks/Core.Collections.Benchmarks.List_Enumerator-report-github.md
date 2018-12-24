``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|              Method |      N |         Mean |      Error |     StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------- |------- |-------------:|-----------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|       **ListEnumerate** |   **1000** |    **20.482 ms** |  **0.2118 ms** |  **0.1981 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|     PooledEnumerate |   1000 |    21.298 ms |  0.2081 ms |  0.1946 ms |  1.04 |           - |           - |           - |                   - |
| PooledSpanEnumerate |   1000 |     3.208 ms |  0.0145 ms |  0.0129 ms |  0.16 |           - |           - |           - |                   - |
|                     |        |              |            |            |       |             |             |             |                     |
|       **ListEnumerate** |  **10000** |   **197.936 ms** |  **0.7426 ms** |  **0.6201 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|     PooledEnumerate |  10000 |   212.245 ms |  3.5110 ms |  3.2842 ms |  1.07 |           - |           - |           - |                   - |
| PooledSpanEnumerate |  10000 |    31.043 ms |  0.0948 ms |  0.0840 ms |  0.16 |           - |           - |           - |                   - |
|                     |        |              |            |            |       |             |             |             |                     |
|       **ListEnumerate** | **100000** | **1,976.363 ms** |  **2.6867 ms** |  **2.0976 ms** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
|     PooledEnumerate | 100000 | 2,173.094 ms | 14.1611 ms | 12.5534 ms |  1.10 |           - |           - |           - |                   - |
| PooledSpanEnumerate | 100000 |   311.170 ms |  3.0996 ms |  2.5883 ms |  0.16 |           - |           - |           - |                   - |
