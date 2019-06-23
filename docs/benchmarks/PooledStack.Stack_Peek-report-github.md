``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|     Method |  Job | Runtime |      N |   Type |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------- |----- |-------- |------- |------- |-----------:|----------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|  **StackPeek** |  **Clr** |     **Clr** |  **10000** |    **Int** |  **11.785 us** | **0.2285 us** |  **0.1908 us** |  **11.700 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr |  10000 |    Int |   8.373 us | 0.4618 us |  0.6322 us |   8.200 us |  0.72 |    0.08 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  StackPeek | Core |    Core |  10000 |    Int |   8.282 us | 0.8378 us |  0.8604 us |   8.100 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core |  10000 |    Int |   9.021 us | 1.8653 us |  2.0733 us |   8.200 us |  1.11 |    0.29 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** |  **10000** | **String** |  **10.144 us** | **0.2082 us** |  **0.4988 us** |  **10.000 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr |  10000 | String |   7.635 us | 0.1458 us |  0.1498 us |   7.600 us |  0.76 |    0.04 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  StackPeek | Core |    Core |  10000 | String |   7.658 us | 0.0660 us |  0.0515 us |   7.700 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core |  10000 | String |  11.104 us | 1.6801 us |  4.7660 us |   7.800 us |  1.77 |    0.73 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** | **100000** |    **Int** | **149.075 us** | **8.7501 us** | **25.1058 us** | **145.650 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr | 100000 |    Int |  97.509 us | 7.0680 us | 20.0507 us |  93.800 us |  0.67 |    0.19 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  StackPeek | Core |    Core | 100000 |    Int | 103.891 us | 5.9886 us | 17.3741 us | 105.300 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core | 100000 |    Int |  98.315 us | 4.8858 us | 13.5384 us |  95.200 us |  0.97 |    0.20 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  **StackPeek** |  **Clr** |     **Clr** | **100000** | **String** | **105.953 us** | **4.5670 us** | **12.7308 us** | **101.150 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledPeek |  Clr |     Clr | 100000 | String |  75.038 us | 6.1287 us |  7.2958 us |  73.200 us |  0.71 |    0.06 |     - |     - |     - |         - |
|            |      |         |        |        |            |           |            |            |       |         |       |       |       |           |
|  StackPeek | Core |    Core | 100000 | String |  95.941 us | 7.6454 us | 22.5426 us |  90.350 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledPeek | Core |    Core | 100000 | String |  94.442 us | 6.8607 us | 20.2291 us |  90.100 us |  1.04 |    0.33 |     - |     - |     - |         - |
