``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|            Method |      N |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------ |------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      **ListSort_Int** | **100000** |   **6.605 ms** | **0.1312 ms** | **0.3637 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 100000 |   6.637 ms | 0.1311 ms | 0.2737 ms |  1.01 |    0.06 |           - |           - |           - |                   - |
|   ListSort_String | 100000 | 194.328 ms | 1.9949 ms | 1.8660 ms | 30.23 |    1.06 |           - |           - |           - |                   - |
| PooledSort_String | 100000 | 190.280 ms | 1.1201 ms | 0.9929 ms | 29.57 |    1.21 |           - |           - |           - |                   - |
|                   |        |            |           |           |       |         |             |             |             |                     |
|      **ListSort_Int** | **200000** |  **13.486 ms** | **0.2626 ms** | **0.2457 ms** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
|    PooledSort_Int | 200000 |  13.474 ms | 0.1827 ms | 0.1525 ms |  1.00 |    0.02 |           - |           - |           - |                   - |
|   ListSort_String | 200000 | 410.902 ms | 4.6797 ms | 4.1484 ms | 30.52 |    0.75 |           - |           - |           - |                   - |
| PooledSort_String | 200000 | 416.719 ms | 2.6086 ms | 2.3125 ms | 30.95 |    0.52 |           - |           - |           - |                   - |
