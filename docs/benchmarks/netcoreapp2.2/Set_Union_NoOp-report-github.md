``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|               Method | CountToUnion | InitialSetSize |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------------- |--------------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **HashSet_Union_NoOp** |        **32000** |          **32000** |   **822.9 us** |  **2.599 us** |  **2.170 us** |  **1.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |        32000 |          32000 |   592.8 us |  2.021 us |  1.890 us |  0.72 |           - |           - |           - |                   - |
|                      |              |                |            |           |           |       |             |             |             |                     |
|   **HashSet_Union_NoOp** |        **32000** |         **320000** |   **836.9 us** |  **3.578 us** |  **3.347 us** |  **1.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |        32000 |         320000 |   550.2 us |  2.525 us |  2.239 us |  0.66 |           - |           - |           - |                   - |
|                      |              |                |            |           |           |       |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |          **32000** | **9,888.7 us** | **57.357 us** | **53.651 us** |  **1.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |       320000 |          32000 | 7,373.8 us | 27.305 us | 25.541 us |  0.75 |           - |           - |           - |                   - |
|                      |              |                |            |           |           |       |             |             |             |                     |
|   **HashSet_Union_NoOp** |       **320000** |         **320000** | **7,846.2 us** | **48.672 us** | **45.527 us** |  **1.00** |           **-** |           **-** |           **-** |                **32 B** |
| PooledSet_Union_NoOp |       320000 |         320000 | 5,268.5 us | 31.525 us | 29.489 us |  0.67 |           - |           - |           - |                   - |
