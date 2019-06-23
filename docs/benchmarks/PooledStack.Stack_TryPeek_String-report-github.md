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
|        Method |  Job | Runtime |      N | EmptyStack |       Mean |      Error |     StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |------- |----------- |-----------:|-----------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|  **StackTryPeek** |  **Clr** |     **Clr** |  **10000** |      **False** |  **10.213 us** |  **0.1998 us** |  **0.1962 us** |  **10.150 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  10000 |      False |   7.513 us |  0.1551 us |  0.1961 us |   7.500 us |  0.73 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  10000 |      False |   7.436 us |  0.1240 us |  0.1099 us |   7.400 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  10000 |      False |   7.514 us |  0.1317 us |  0.1167 us |   7.500 us |  1.01 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |  **10000** |       **True** |   **6.333 us** |  **0.0997 us** |  **0.0778 us** |   **6.350 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  10000 |       True |  11.445 us |  0.2558 us |  0.7001 us |  11.600 us |  1.79 |    0.14 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  10000 |       True |  10.619 us |  0.6049 us |  1.6862 us |   9.500 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  10000 |       True |   6.331 us |  0.1235 us |  0.1032 us |   6.300 us |  0.63 |    0.07 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **100000** |      **False** | **105.513 us** |  **3.5059 us** |  **9.6563 us** | **101.300 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 100000 |      False |  78.939 us |  3.6821 us | 10.5646 us |  73.400 us |  0.75 |    0.12 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 100000 |      False |  93.519 us |  7.1227 us | 20.7773 us |  89.350 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 100000 |      False |  92.270 us |  6.9554 us | 20.1789 us |  89.200 us |  1.03 |    0.29 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **100000** |       **True** |  **69.003 us** |  **3.9544 us** | **10.8915 us** |  **62.900 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 100000 |       True | 119.271 us |  5.1754 us | 14.1675 us | 115.100 us |  1.76 |    0.32 |     - |     - |     - |         - |
|               |      |         |        |            |            |            |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 100000 |       True | 132.215 us |  9.3714 us | 26.7372 us | 128.450 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 100000 |       True | 132.753 us | 10.6687 us | 30.4384 us | 126.100 us |  1.05 |    0.36 |     - |     - |     - |         - |
