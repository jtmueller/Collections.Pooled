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
|        Method |  Job | Runtime |      N | EmptyStack |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |------- |----------- |-----------:|----------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|  **StackTryPeek** |  **Clr** |     **Clr** |  **10000** |      **False** |  **11.747 us** | **0.2281 us** |  **0.2134 us** |  **11.600 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  10000 |      False |   9.492 us | 0.0332 us |  0.0277 us |   9.500 us |  0.81 |    0.01 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  10000 |      False |   7.921 us | 0.1412 us |  0.1251 us |   7.900 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  10000 |      False |   8.100 us | 0.1445 us |  0.1128 us |   8.150 us |  1.02 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** |  **10000** |       **True** |   **6.741 us** | **0.3961 us** |  **0.8442 us** |   **6.550 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr |  10000 |       True |   9.567 us | 0.1190 us |  0.1113 us |   9.500 us |  1.39 |    0.19 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core |  10000 |       True |   9.686 us | 0.2260 us |  0.5326 us |   9.700 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core |  10000 |       True |   9.650 us | 0.1811 us |  0.1605 us |   9.600 us |  0.99 |    0.02 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **100000** |      **False** | **124.366 us** | **5.2365 us** | **14.6836 us** | **115.100 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 100000 |      False | 105.475 us | 5.4407 us | 15.4344 us |  94.500 us |  0.86 |    0.16 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 100000 |      False |  98.406 us | 6.9541 us | 20.2855 us |  95.400 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 100000 |      False |  91.219 us | 5.2158 us | 14.9650 us |  81.500 us |  0.96 |    0.23 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  **StackTryPeek** |  **Clr** |     **Clr** | **100000** |       **True** |  **72.576 us** | **5.1962 us** | **14.9090 us** |  **64.700 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |         **-** |
| PooledTryPeek |  Clr |     Clr | 100000 |       True | 105.862 us | 6.2750 us | 17.8010 us |  94.300 us |  1.51 |    0.36 |     - |     - |     - |         - |
|               |      |         |        |            |            |           |            |            |       |         |       |       |       |           |
|  StackTryPeek | Core |    Core | 100000 |       True | 117.555 us | 8.2851 us | 23.5033 us | 111.400 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledTryPeek | Core |    Core | 100000 |       True | 106.438 us | 6.5926 us | 18.3775 us |  94.100 us |  0.94 |    0.23 |     - |     - |     - |         - |
