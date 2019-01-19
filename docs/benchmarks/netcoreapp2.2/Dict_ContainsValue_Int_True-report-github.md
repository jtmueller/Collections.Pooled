``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                       Method |      N |           Mean |         Error |        StdDev | Ratio | RatioSD |
|----------------------------- |------- |---------------:|--------------:|--------------:|------:|--------:|
|   **DictContainsValue_Int_True** |   **1000** |       **568.5 us** |      **7.717 us** |      **7.219 us** |  **1.00** |    **0.00** |
| PooledContainsValue_Int_True |   1000 |       542.9 us |     10.630 us |     18.051 us |  0.96 |    0.04 |
|                              |        |                |               |               |       |         |
|   **DictContainsValue_Int_True** |  **10000** |    **56,331.0 us** |    **117.527 us** |     **98.140 us** |  **1.00** |    **0.00** |
| PooledContainsValue_Int_True |  10000 |    53,174.1 us |  1,843.768 us |  1,724.662 us |  0.94 |    0.03 |
|                              |        |                |               |               |       |         |
|   **DictContainsValue_Int_True** | **100000** | **5,728,276.7 us** | **55,400.542 us** | **49,111.137 us** |  **1.00** |    **0.00** |
| PooledContainsValue_Int_True | 100000 | 5,702,700.0 us | 29,686.579 us | 27,768.844 us |  1.00 |    0.01 |
