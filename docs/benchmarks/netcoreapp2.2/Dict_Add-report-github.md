``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|    Method |      N |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   **DictAdd** |   **1000** |  **5.513 ms** | **0.1091 ms** | **0.1761 ms** |  **1.00** |    **0.00** |   **1000.0000** |   **1000.0000** |   **1000.0000** |           **7204784 B** |
| PooledAdd |   1000 |  2.881 ms | 0.0629 ms | 0.1816 ms |  0.53 |    0.04 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** |  **10000** |  **9.228 ms** | **0.1812 ms** | **0.2874 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **14645880 B** |
| PooledAdd |  10000 |  4.434 ms | 0.1047 ms | 0.3088 ms |  0.47 |    0.03 |           - |           - |           - |                   - |
|           |        |           |           |           |       |         |             |             |             |                     |
|   **DictAdd** | **100000** | **10.708 ms** | **0.2083 ms** | **0.2709 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |          **13850984 B** |
| PooledAdd | 100000 |  5.887 ms | 0.1436 ms | 0.4165 ms |  0.56 |    0.04 |           - |           - |           - |                   - |
