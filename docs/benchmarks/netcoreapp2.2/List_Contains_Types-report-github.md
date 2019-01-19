``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|---------------------- |----------:|----------:|----------:|----------:|------:|--------:|
|      ListContains_Int |  19.38 us | 0.0566 us | 0.0502 us |  19.36 us |  1.00 |    0.00 |
|    PooledContains_Int |  20.93 us | 0.4111 us | 0.4037 us |  21.04 us |  1.08 |    0.02 |
|   ListContains_String | 110.55 us | 0.8311 us | 0.6489 us | 110.53 us |  5.70 |    0.04 |
| PooledContains_String | 113.70 us | 2.2725 us | 5.5315 us | 110.91 us |  6.03 |    0.39 |
